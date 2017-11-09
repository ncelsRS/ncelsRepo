using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Repository.DirectionToPay;
using Quartz;

namespace Ncels.Scheduler.Jobs
{
    public class DirectionToPayJob: IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            DirectionToPayRepository repository = new DirectionToPayRepository(false);
            var cancelStatus = repository
                .GetDirectionToPayStatuses()
                .FirstOrDefault(s => s.Code == Dictionary.ExpDirectionToPayStatus.Canceled && s.ExpireDate == null);
            if (cancelStatus != null)
            {
                // TODO 40 дней перенести в настройки
                var sqlScript = string.Format(@"UPDATE [dbo].[EXP_DirectionToPays] SET [StatusId] = '{0}', [StatusValue] = '{1}' WHERE [DirectionDate] < DATEADD(DAY, {2}, GETDATE()) AND StatusId <> '{0}'", cancelStatus.Id, cancelStatus.Name, "-40");
                repository.RunSqlScript(sqlScript);
            }
        }
    }
}
