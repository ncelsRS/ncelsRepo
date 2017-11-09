using System;
using System.Data.SqlClient;
using log4net;
using log4net.Core;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;

namespace Ncels.BL
{
    public class DbSynchronizer
    {
        protected readonly ILog _logger = LogManager.GetLogger(typeof(DbSynchronizer));

        protected DbSynchronizerConfiguration LocalConfiguration { get; private set; }
        protected DbSynchronizerConfiguration RemoteConfiguration { get; private set; }

        public Action<string> Logger { get; set; }
        protected EventHandler<DbApplyChangeFailedEventArgs> ApplyLocalChangesFailed { get; set; }
        protected EventHandler<DbChangesSelectedEventArgs> RemoteChangesSelected { get; set; }

        private SyncOrchestrator _orchestrator;

        public DbSynchronizer(DbSynchronizerConfiguration localConfiguration, DbSynchronizerConfiguration remoteConfiguration)
        {
            LocalConfiguration = localConfiguration;
            RemoteConfiguration = remoteConfiguration;
        }

        protected virtual void DefineLocalScope(DbSyncScopeDescription scopeDescription)
        {

        }

        protected virtual void DefineRemoteScope(DbSyncScopeDescription scopeDescription)
        {

        }

        public void ApplyProvision()
        {
            CheckSchema(LocalConfiguration);
            CheckSchema(RemoteConfiguration);
            ApplyProvision(LocalConfiguration, DefineLocalScope);
            ApplyProvision(RemoteConfiguration, DefineRemoteScope);
        }

        private void CheckSchema(DbSynchronizerConfiguration configuration)
        {
            if (string.IsNullOrEmpty(configuration?.ObjectSchema)) return;
            using (var connection = new SqlConnection(configuration.ConnectionString))
            {
                var checkCommand = connection.CreateCommand();
                checkCommand.CommandText = $"select schema_id from sys.schemas where name='{configuration.ObjectSchema}'";
                connection.Open();
                var reader = checkCommand.ExecuteReader();
                var hasRows = reader.HasRows;
                reader.Close();
                if (!hasRows)
                {
                    var createSchemaCommand = connection.CreateCommand();
                    createSchemaCommand.CommandText = $"CREATE SCHEMA {configuration.ObjectSchema}";
                    createSchemaCommand.ExecuteNonQuery();
                }
            }
        }

        private void ApplyProvision(DbSynchronizerConfiguration configuration, Action<DbSyncScopeDescription> defineScope)
        {
            using (var connection = new SqlConnection(configuration.ConnectionString))
            {
                var provision = new SqlSyncScopeProvisioning(connection)
                {
                    ObjectPrefix = configuration.ObjectPrefix ?? string.Empty,
                    ObjectSchema = configuration.ObjectSchema ?? string.Empty,
                };
                if (!provision.ScopeExists(configuration.ScopeName))
                {
                    var scopeDescription = new DbSyncScopeDescription(configuration.ScopeName);
                    defineScope(scopeDescription);
                    provision.SetCreateTableDefault(DbSyncCreationOption.Skip);
                    provision.PopulateFromScopeDescription(scopeDescription);
                    provision.Apply();
                }
            }
        }

        public void Synchronize(SyncDirectionOrder directionOrder)
        {
            try
            {
                SqlConnection remoteConnection = new SqlConnection(RemoteConfiguration.ConnectionString);
                SqlConnection localConnection = new SqlConnection(LocalConfiguration.ConnectionString);

                _orchestrator = new SyncOrchestrator();

                _orchestrator.LocalProvider = new SqlSyncProvider(LocalConfiguration.ScopeName, localConnection,
                    LocalConfiguration.ObjectPrefix, LocalConfiguration.ObjectSchema);
                _orchestrator.RemoteProvider = new SqlSyncProvider(RemoteConfiguration.ScopeName, remoteConnection,
                    RemoteConfiguration.ObjectPrefix, RemoteConfiguration.ObjectSchema);

                _orchestrator.Direction = directionOrder;

                if (RemoteChangesSelected != null)
                    ((SqlSyncProvider) _orchestrator.RemoteProvider).ChangesSelected += RemoteChangesSelected;
                if (ApplyLocalChangesFailed != null)
                    ((SqlSyncProvider) _orchestrator.LocalProvider).ApplyChangeFailed += ApplyLocalChangesFailed;

                ((SqlSyncProvider) _orchestrator.RemoteProvider).ChangesSelected += DbSynchronizer_ChangesSelected;
                
                SyncOperationStatistics syncStats = _orchestrator.Synchronize();

                Log("Start Time: " + syncStats.SyncStartTime);
                Log("Total Changes Uploaded: " + syncStats.UploadChangesTotal);
                Log("Total Changes Downloaded: " + syncStats.DownloadChangesTotal);
                Log("Complete Time: " + syncStats.SyncEndTime);
                Log(string.Empty);
                _orchestrator = null;
            }
            catch (Exception e)
            {
                Log(e.ToString());
                _logger.Error(e);
            }
        }

        private void DbSynchronizer_ChangesSelected(object sender, DbChangesSelectedEventArgs e)
        {
            Log("Start Time: " );
            Log(string.Empty);
        }

        public void CancelSync()
        {
            if (_orchestrator != null && (_orchestrator.State == SyncOrchestratorState.Uploading || _orchestrator.State == SyncOrchestratorState.Downloading))
                _orchestrator.Cancel();
        }

        private void Log(string message)
        {
            _logger.Debug(message);
            Logger?.Invoke(message);
        }
    }
}