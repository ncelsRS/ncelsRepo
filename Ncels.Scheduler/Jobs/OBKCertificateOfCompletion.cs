using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.Notifications;
using PW.Ncels.Database.Repository.OBK;
using Quartz;

namespace Ncels.Scheduler.Jobs
{
    /// <summary>
    /// Акт выполненных работ
    /// </summary>
    public class OBKCertificateOfCompletion : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            OBKPaymentRepository repo = new OBKPaymentRepository();

            var acts = repo.GetCertificateOfCompletions();
            foreach (var act in acts)
            {
                act.OBK_AssessmentDeclaration.StatusId = CodeConstManager.STATUS_OBK_SIGN_ACT;
                act.SendNotification = true;
                //отправка уведоления
                new NotificationManager().SendNotificationFromCompany(
                    string.Format("По заявке №{0} вынесено решение. Просим распечатать акт выполненных работ и предоставить Исполнителю с подписью и печатью.", act.OBK_AssessmentDeclaration.Number),
                    ObjectType.ObkDeclaration, act.OBK_AssessmentDeclaration.Id.ToString(), act.OBK_AssessmentDeclaration.EmployeeId);
                repo.UpdateNotificationToAct(act);
            }
        }
    }
}
