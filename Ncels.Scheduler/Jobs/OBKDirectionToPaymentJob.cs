﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Notifications;
using PW.Ncels.Database.Repository.OBK;
using Quartz;

namespace Ncels.Scheduler.Jobs
{
    public class OBKDirectionToPaymentJob : IJob
    {
        /// <summary>
        /// Счет на оплыту
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            OBKPaymentRepository repo = new OBKPaymentRepository();

            // если есть оплата
            var declarantPiad = repo.GetDeclarantPaid();
            foreach (var dPaid in declarantPiad)
            {
                //полная оплата
                if (dPaid.IsPaid && !dPaid.IsNotFullPaid)
                {
                    //отправка уведоления
                    if (dPaid.ZBKCopy_id != null)
                    {
                        new NotificationManager().SendNotificationFromCompany(
                             string.Format("Оплата по счету {0} получена. Теперь необходимо предоставить оригинал ЗБК в ЦОЗ для оформления копий ЗБК.", dPaid.InvoiceNumber1C),
                            ObjectType.OBK_ZBKCopy, dPaid.ZBKCopy_id.ToString(),
                            (Guid)repo.GetZBKCopy((Guid)dPaid.ZBKCopy_id).EmployeeId);
                        repo.UpdateNotificationToPayment(dPaid, true);
                    }
                    else
                    {
                        new NotificationManager().SendNotificationFromCompany(
                            string.Format("Оплата по счету {0} получена. Теперь Вы можете отправлять на рассмотрение заявку для проведения оценки безопасности качества", dPaid.InvoiceNumber1C),
                            ObjectType.Contract, dPaid.OBK_Contract.Id.ToString(), (Guid)dPaid.OBK_Contract.EmployeeId);
                        repo.UpdateNotificationToPayment(dPaid, true);
                    }
                }
            }

            // если неполная оплата
            var declarantNotFullPiad = repo.GetDeclarantNotFullPaid();
            foreach (var dNotFullPaid in declarantNotFullPiad)
            {
                if (dNotFullPaid.IsPaid && dNotFullPaid.IsNotFullPaid)
                {
                    if (dNotFullPaid.ZBKCopy_id != null)
                    {
                        var pay = Math.Round((decimal)(dNotFullPaid.PaymentBill - dNotFullPaid.PaymentValue), 2);
                        //отправка уведоления
                        new NotificationManager().SendNotificationFromCompany(
                            string.Format("Оплата по счету {0} получена не в полном размере. Просим оплатить остаток суммы {1} для дальнейшей подачи заявки", dNotFullPaid.InvoiceNumber1C, pay),
                           ObjectType.OBK_ZBKCopy, dNotFullPaid.ZBKCopy_id.ToString(),
                            (Guid)repo.GetZBKCopy((Guid)dNotFullPaid.ZBKCopy_id).EmployeeId);
                        repo.UpdateNotificationToPayment(dNotFullPaid, false);
                    }
                    else
                    {

                        var pay = Math.Round((decimal)(dNotFullPaid.PaymentBill - dNotFullPaid.PaymentValue), 2);
                        //отправка уведоления
                        new NotificationManager().SendNotificationFromCompany(
                            string.Format("Оплата по счету {0} получена не в полном размере. Просим оплатить остаток суммы {1} для дальнейшей подачи заявки", dNotFullPaid.InvoiceNumber1C, pay),
                            ObjectType.Unknown, dNotFullPaid.OBK_Contract.Id.ToString(), (Guid)dNotFullPaid.OBK_Contract.EmployeeId);
                        repo.UpdateNotificationToPayment(dNotFullPaid, false);
                    }
                }
            }

            // если прошел срок оплаты
            var paidExpired = repo.GetPaidExpired();
            foreach (var pExpired in paidExpired)
            {
                repo.UpdateStatus(pExpired);
            }
        }
    }
}
