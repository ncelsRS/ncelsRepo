using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using log4net;
using Microsoft.Synchronization;
using Ncels.BL;

namespace Ncels._1CSync.Controller
{
    public class Sync1CController
    {
        protected ILog _logger = LogManager.GetLogger(typeof(Sync1CController).Name);
        public Action<string> LogAction { get; set; }

       //public bool IsExecuting { get; set; }

        public bool IsTerminate { get; set; }

        Timer _timer = null;
        
        public Sync1CController()
        {
            //IsExecuting = 
                IsTerminate = true;
        }
        
        public void StartSync()
        {
            _logger.Debug("StartSync invoke");
            try
            {
                long syncInterval = long.Parse(ConfigurationManager.AppSettings.Get("SyncInterval") ?? "5") * 60 * 1000;
                var localConfig = new DbSynchronizerConfiguration()
                {
                    ScopeName = "I1cRegisterScope",
                    ConnectionString = ConfigurationManager.ConnectionStrings["ExpDb"].ConnectionString,
                    ObjectPrefix = "sync_1c",
                    ObjectSchema = "DbSync1C"
                };
                List<TableMapping> tableMaps = GetTableMappingList(localConfig.ConnectionString);

                var i1CDbSynchronization = new I1CDbSynchronization(localConfig,
                    new DbSynchronizerConfiguration()
                    {
                        ScopeName = "I1cRegisterScope",
                        ConnectionString = ConfigurationManager.ConnectionStrings["I1cDb"].ConnectionString,
                        ObjectPrefix = "sync_1c",
                        ObjectSchema = "DbSync1C"
                    },
                    tableMaps);

                i1CDbSynchronization.Logger = this.LogAction;
                i1CDbSynchronization.ApplyProvision();

                _timer = new Timer((s) =>
                {
                    i1CDbSynchronization.Synchronize(SyncDirectionOrder.DownloadAndUpload);

                    if (IsTerminate)
                        _timer.Change(Timeout.Infinite, Timeout.Infinite);
                    else
                        _timer.Change(syncInterval, int.MaxValue);
                }, null, 0, int.MaxValue);

                //IsExecuting = true;
                IsTerminate = false;
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }

        public void StopSync()
        {
            IsTerminate = true;
            //IsExecuting = false;
        }


        private List<TableMapping> GetTableMappingList(string conectionStr)
        {
            var tableNames = ListTables(conectionStr);
            List<TableMapping> tableMaps = new List<TableMapping>();
            foreach (var tableName in tableNames)
            {
                tableMaps.Add(new TableMapping()
                {
                    GlobalName = tableName,
                    TableNameDest = tableName,
                    TableNameSrc = tableName
                });
            }
            return tableMaps;
        }

        private IList<string> ListTables(string conectionStr)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(conectionStr))
            {
                connection.Open();
                List<string> tables = new List<string>();
                DataTable dt = connection.GetSchema("Tables");
                foreach (DataRow row in dt.Rows)
                {
                    string tablename = (string)row[2];
                    if (tablename.StartsWith("I1c_"))
                        tables.Add(tablename);
                }
                return tables.ToList();
            }
        }
    }
}
