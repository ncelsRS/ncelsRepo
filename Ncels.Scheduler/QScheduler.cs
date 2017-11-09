using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ncels.Scheduler.Jobs;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Repository.Expertise;
using Quartz;
using Quartz.Impl;

namespace Ncels.Scheduler
{
    public class QScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail diretionToPayJob = JobBuilder.Create<DirectionToPayJob>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(3, 0))
                  )
                .Build();

            // for test
            /*ITrigger trigger1 = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(30)
                    .RepeatForever())
                .Build();
                */
            scheduler.ScheduleJob(diretionToPayJob, trigger);

            IJobDetail coommissionNotificationJob = JobBuilder.Create<CommissionNotificationJob>().Build();
            ITrigger coommissionNotificationTrigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(3, 0))
                  )
                .Build();
            scheduler.ScheduleJob(coommissionNotificationJob, coommissionNotificationTrigger);
            CommissionHelper.SendNotifications();//todo убрать после отладки

            IJobDetail checkSuspensionPeriodsJob = JobBuilder.Create<CommissionNotificationJob>().Build();
            ITrigger checkSuspensionPeriodsTrigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(3, 0))
                  )
                .Build();
            scheduler.ScheduleJob(checkSuspensionPeriodsJob, checkSuspensionPeriodsTrigger);




            IJobDetail diretionToPaymentJob = JobBuilder.Create<OBKDirectionToPaymentJob>().Build();
            //ITrigger paymenTrigger = TriggerBuilder.Create()
            //    .WithDailyTimeIntervalSchedule
            //    (s =>
            //        s.WithIntervalInHours(24)
            //            .OnEveryDay()
            //            .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(3, 0))
            //    )
            //    .Build();


            ITrigger trigger1 = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(30)
                    .RepeatForever())
                .Build();
            scheduler.ScheduleJob(diretionToPaymentJob, trigger1);


            IJobDetail certificateOfComplection = JobBuilder.Create<OBKCertificateOfCompletion>().Build();
            //ITrigger paymenTrigger = TriggerBuilder.Create()
            //    .WithDailyTimeIntervalSchedule
            //    (s =>
            //        s.WithIntervalInHours(24)
            //            .OnEveryDay()
            //            .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(3, 0))
            //    )
            //    .Build();


            ITrigger trigger2 = TriggerBuilder.Create()
                .WithIdentity("trigger2", "group2")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(30)
                    .RepeatForever())
                .Build();
            scheduler.ScheduleJob(certificateOfComplection, trigger2);
        }
    }
}
