using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Repository.DirectionToPay;
using PW.Ncels.Database.Repository.Expertise;
using Quartz;

namespace Ncels.Scheduler.Jobs
{
    public class CheckSuspensionPeriods : IJob
    {
        public void Execute(IJobExecutionContext context){
            new DrugPrimaryRepository().CheckSuspensionPeriods();
        }
    }
}
