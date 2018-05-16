using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;

namespace SynchronizationServer
{
    public class DbSynchronizer
    {
        protected DbSynchronizerConfiguration LocalConfiguration { get; private set; }
        protected DbSynchronizerConfiguration RemoteConfiguration { get; private set; }

        protected Action<string> Logger { get; set; }
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
            if (configuration == null || string.IsNullOrEmpty(configuration.ObjectSchema)) return;
            using (var connection = new SqlConnection(configuration.ConnectionString))
            {
                var checkCommand = connection.CreateCommand();
                checkCommand.CommandText = string.Format("select schema_id from sys.schemas where name='{0}'", configuration.ObjectSchema);
                connection.Open();
                var reader = checkCommand.ExecuteReader();
                var hasRows = reader.HasRows;
                reader.Close();
                if (!hasRows)
                {
                    var createSchemaCommand = connection.CreateCommand();
                    createSchemaCommand.CommandText = string.Format("CREATE SCHEMA {0}", configuration.ObjectSchema);
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
            SqlConnection remoteConnection = new SqlConnection(RemoteConfiguration.ConnectionString);
            SqlConnection localConnection = new SqlConnection(LocalConfiguration.ConnectionString);

            _orchestrator = new SyncOrchestrator();

            _orchestrator.LocalProvider = new SqlSyncProvider(LocalConfiguration.ScopeName, localConnection, LocalConfiguration.ObjectPrefix, LocalConfiguration.ObjectSchema);
            _orchestrator.RemoteProvider = new SqlSyncProvider(RemoteConfiguration.ScopeName, remoteConnection, RemoteConfiguration.ObjectPrefix, RemoteConfiguration.ObjectSchema);

            _orchestrator.Direction = directionOrder;

            if (RemoteChangesSelected != null)
                ((SqlSyncProvider)_orchestrator.RemoteProvider).ChangesSelected += RemoteChangesSelected;
            ((SqlSyncProvider) _orchestrator.RemoteProvider).CommandTimeout = 0;
            if (ApplyLocalChangesFailed != null)
                ((SqlSyncProvider)_orchestrator.LocalProvider).ApplyChangeFailed += ApplyLocalChangesFailed;
            ((SqlSyncProvider) _orchestrator.LocalProvider).CommandTimeout = 0;

            ((SqlSyncProvider)_orchestrator.RemoteProvider).ChangesSelected += DbSynchronizer_ChangesSelected;

            SyncOperationStatistics syncStats = _orchestrator.Synchronize();

            Log("Начало синхронизации: " + syncStats.SyncStartTime);
            Log("Changes Uploaded: " + syncStats.UploadChangesTotal);
            Log("Changes Downloaded: " + syncStats.DownloadChangesTotal);
            Log("Окончание синхронизации: " + syncStats.SyncEndTime);
            Log(string.Empty);
            _orchestrator = null;
        }

        private void DbSynchronizer_ChangesSelected(object sender, DbChangesSelectedEventArgs e)
        {
            Log("Извлечены изменения: " +DateTime.Now.ToLongTimeString());
        }

        public void CancelSync()
        {
            if (_orchestrator != null && (_orchestrator.State == SyncOrchestratorState.Uploading || _orchestrator.State == SyncOrchestratorState.Downloading))
                _orchestrator.Cancel();
        }

        protected void Log(string message)
        {
            if (Logger != null) Logger(message);
        }
    }
}