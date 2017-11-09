using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Repository.DirectionToPay;
using Quartz;

namespace Ncels.Scheduler.Jobs
{
    public class CommissionNotificationJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            CommissionHelper.SendNotifications();
        }
    }
}
