using PW.Ncels.Database.Repository.Price;
using Quartz;

namespace Ncels.Scheduler.Jobs
{
    public class CheckRefPassedDaysJob : IJob{
        public void Execute(IJobExecutionContext context){
            new PriceProjectRepository().SendAvgRateNotification();
            new PriceProjectRepository().CheckRefPassedDays();
        }
    }
}
