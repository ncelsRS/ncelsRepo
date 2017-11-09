using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Microsoft.Synchronization;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace SynchronizationServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var nowDate = DateTime.Now.Date;
            long syncInterval = long.Parse(ConfigurationManager.AppSettings.Get("SyncInterval")) * 60 * 1000;

            var syncExchangeRateFrom = DateTime.ParseExact(ConfigurationManager.AppSettings.Get("SyncExchangeRateFrom"), "dd.MM.yyyy", CultureInfo.InvariantCulture);
            ExchangeRatesSynchronizer exchangeRatesSynchronizer = new ExchangeRatesSynchronizer(ConfigurationManager.ConnectionStrings["ExpDb"].ConnectionString,
                ConfigurationManager.AppSettings.Get("ExchangeRatesSourceUrl"));
            var syncExchangeRateStartTime = DateTime.ParseExact(ConfigurationManager.AppSettings.Get("SyncExchangeRateStartTime"), "HH:mm", CultureInfo.InvariantCulture);
            var scheduledRun = (new DateTime(nowDate.Year, nowDate.Month, nowDate.Day)).AddHours(syncExchangeRateStartTime.Hour).AddMinutes(syncExchangeRateStartTime.Minute);

            var registerDbSynchronizer = new DrugRegisterDbSynchronizer(new DbSynchronizerConfiguration()
            {
                ScopeName = "DrugRegisterScope",
                ConnectionString = ConfigurationManager.ConnectionStrings["ExpDb"].ConnectionString,
                ObjectPrefix = "sync_fr",
                ObjectSchema = "DbSync"
            }, new DbSynchronizerConfiguration()
            {
                ScopeName = "DrugRegisterScope",
                ConnectionString = ConfigurationManager.ConnectionStrings["SrDb"].ConnectionString,
                ObjectPrefix = "sync_fr",
                ObjectSchema = "DbSync"
            });
            var obkSynchronizer = new ObkDbSynchronizer(new DbSynchronizerConfiguration()
            {
                ScopeName = "ObkScope",
                ConnectionString = ConfigurationManager.ConnectionStrings["ExpDb"].ConnectionString,
                ObjectPrefix = "sync_fr",
                ObjectSchema = "DbSync"
            }, new DbSynchronizerConfiguration()
            {
                ScopeName = "ObkScope",
                ConnectionString = ConfigurationManager.ConnectionStrings["ObkDb"].ConnectionString,
                ObjectPrefix = "sync_fr",
                ObjectSchema = "DbSync"
            });

            Timer timer = null;
            timer=new Timer((s) =>
              {
                  Console.WriteLine("Старт синхронизации гос. реестра");
                  registerDbSynchronizer.ApplyProvision();
                  registerDbSynchronizer.Synchronize(SyncDirectionOrder.Download);
                  Console.WriteLine("Окончание синхронизации гос. реестра");

                  Console.WriteLine("Старт синхронизации ОБК");
                  obkSynchronizer.ApplyProvision();
                  obkSynchronizer.Synchronize(SyncDirectionOrder.Download);
                  Console.WriteLine("Окончание синхронизации ОБК");

                  try
                  {
                      var currentDate = DateTime.Now;
                      if (currentDate > scheduledRun)
                      {
                          Console.WriteLine("Загрузка курсов валют с {0} по{1}", syncExchangeRateFrom, currentDate);
                          exchangeRatesSynchronizer.Synchronize(syncExchangeRateFrom, currentDate);
                          Console.WriteLine("Загрузка курсов валют с {0} по{1} завершена", syncExchangeRateFrom, currentDate);
                          syncExchangeRateFrom = currentDate.AddDays(1);
                          scheduledRun=new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);
                          scheduledRun = scheduledRun.AddDays(1).AddHours(syncExchangeRateStartTime.Hour).AddMinutes(syncExchangeRateStartTime.Minute);
                          Console.WriteLine("Следующая синхронизация курсов валют: {0}", scheduledRun.ToLongDateString());
                      }
                  }
                  catch (Exception ex)
                  {
                      Console.WriteLine("Ошибка при загрузке курсов валют");
                      Console.WriteLine(ex);
                  }

                  timer.Change(syncInterval, int.MaxValue);
              }, null, 0, int.MaxValue);
            while (true)
            {
                Console.ReadKey();
                Console.WriteLine("Завершить синхронизацию? (y/n)");
                var input = Console.ReadLine();
                if (input.ToLower() == "y") break;
            }
            Console.WriteLine("Завершение...");
            timer.Dispose();
        }
    }
}
