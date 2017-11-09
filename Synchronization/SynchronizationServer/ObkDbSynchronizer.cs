using System;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;

namespace SynchronizationServer
{
    public class ObkDbSynchronizer : DbSynchronizer
    {
        public ObkDbSynchronizer(DbSynchronizerConfiguration localConfiguration, DbSynchronizerConfiguration remoteConfiguration) : base(localConfiguration, remoteConfiguration)
        {
            ApplyLocalChangesFailed += ApplyChangeFailed;
            Logger = s => Console.WriteLine(s);
        }

        protected override void DefineLocalScope(DbSyncScopeDescription scopeDescription)
        {
            using (var connection = new SqlConnection(LocalConfiguration.ConnectionString))
            {
                DbSyncTableDescription tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("obk_currencies", connection);
                tableDesc.GlobalName = "currencies";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("obk_products", connection);
                tableDesc.GlobalName = "products";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("obk_product_cost", connection);
                tableDesc.GlobalName = "product_cost";
                //tableDesc.Columns.Remove(tableDesc.Columns["cost_tenge"]);
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("obk_certifications", connection);
                tableDesc.GlobalName = "certifications";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("obk_certification_copies", connection);
                tableDesc.GlobalName = "certification_copies";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("obk_appendix", connection);
                tableDesc.GlobalName = "appendix";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("obk_appendix_series", connection);
                tableDesc.GlobalName = "appendix_series";
                scopeDescription.Tables.Add(tableDesc);
            }
        }

        protected override void DefineRemoteScope(DbSyncScopeDescription scopeDescription)
        {
            using (var connection = new SqlConnection(RemoteConfiguration.ConnectionString))
            {
                DbSyncTableDescription tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("currencies", connection);
                tableDesc.GlobalName = "currencies";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("products", connection);
                tableDesc.GlobalName = "products";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("product_cost", connection);
                tableDesc.GlobalName = "product_cost";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("certifications", connection);
                tableDesc.GlobalName = "certifications";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("certification_copies", connection);
                tableDesc.GlobalName = "certification_copies";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("appendix", connection);
                tableDesc.GlobalName = "appendix";
                scopeDescription.Tables.Add(tableDesc);
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("appendix_series", connection);
                tableDesc.GlobalName = "appendix_series";
                scopeDescription.Tables.Add(tableDesc);
            }
        }

        private void ApplyChangeFailed(object sender, DbApplyChangeFailedEventArgs e)
        {
            if (e.Conflict != null)
                Log(e.Conflict.Type.ToString());
            if (e.Error != null)
                Log(e.Error.ToString());
        }
    }
}