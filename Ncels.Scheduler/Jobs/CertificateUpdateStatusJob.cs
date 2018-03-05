using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Repository.OBK;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ncels.Scheduler.Jobs
{
    class CertificateUpdateStatusJob : IJob
    {
        private OBKCertificateRepository repository = new OBKCertificateRepository();

        public void Execute(IJobExecutionContext context)
        {
            System.Threading.Thread.Sleep(100000);
            repository.UpdateCertificates();
        }
    }
}
