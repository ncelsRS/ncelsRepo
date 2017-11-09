using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using log4net;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;

namespace Ncels.BL
{
    public class I1CDbSynchronization : DbSynchronizer
    {
        private readonly List<TableMapping> _tableMappings;
        protected readonly ILog _logger = LogManager.GetLogger(typeof(I1CDbSynchronization));

        public I1CDbSynchronization(DbSynchronizerConfiguration localConfiguration, DbSynchronizerConfiguration remoteConfiguration, List<TableMapping> tableMapings) : base(localConfiguration, remoteConfiguration)
        {
            ApplyLocalChangesFailed += ApplyLocalChangesFailedFn;
            _tableMappings = tableMapings;
        }

        protected override void DefineLocalScope(DbSyncScopeDescription scopeDescription)
        {
            try
            {
                using (var connection = new SqlConnection(LocalConfiguration.ConnectionString))
                {
                    foreach (var tableMapping in _tableMappings)
                    {
                        DbSyncTableDescription tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable(tableMapping.TableNameDest, connection);
                        tableDesc.GlobalName = tableMapping.GlobalName;
                        scopeDescription.Tables.Add(tableDesc);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }

        protected override void DefineRemoteScope(DbSyncScopeDescription scopeDescription)
        {
            try
            {
                using (var connection = new SqlConnection(RemoteConfiguration.ConnectionString))
                {
                    foreach (var tableMapping in _tableMappings)
                    {
                        DbSyncTableDescription tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable(tableMapping.TableNameSrc, connection);
                        tableDesc.GlobalName = tableMapping.GlobalName;
                        scopeDescription.Tables.Add(tableDesc);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }

        private void ApplyLocalChangesFailedFn(object sender, DbApplyChangeFailedEventArgs e)
        {
            Console.WriteLine(e.Conflict.Type);

            Console.WriteLine(e.Error);
        }
    }
}
