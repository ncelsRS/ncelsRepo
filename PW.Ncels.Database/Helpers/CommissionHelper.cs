using System;
using System.Linq;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Notifications;

namespace PW.Ncels.Database.Helpers
{
    public class CommissionHelper
    {
        public static void SendNotifications()
        {
            using (var db = new ncelsEntities())
            {
                var needDate = DateTime.Now.Date.AddDays(3);
                var dbUnits = db.Commissions.Where(x => x.IsComplete == false && x.Date <= needDate && x.IsNeedSendTimeOverNotifications != false)
                    .Join(db.CommissionUnits, x => x.Id, x => x.CommissionId, (commission, unit) => new { c = commission, u = unit }).ToList();
                
                var notificationManager = new NotificationManager();
                var sendCount = 0;
                foreach (var dbUnit in dbUnits)
                {
                    dbUnit.c.IsNeedSendTimeOverNotifications = false;
                    var msg = "Закрывается регистрация заявлений на повестку заседания " + dbUnit.c.FullNumber + " " + dbUnit.c.Date.ToShortDateString();
                    notificationManager.SendNotificationAnonymous(msg, ObjectType.Commission, dbUnit.c.Id.ToString(), dbUnit.u.EmployeeId);
                    sendCount++;
                }
                ActionLogger.WriteInt(db, (Guid?)null, "Сформировано " + sendCount + " уведомлений участникам ", withoutIp: true);
                db.SaveChanges();
            }
        }

        public static void SendChangeDateNotifications(int commissionId, DateTime prevDate)
        {
            using (var db = new ncelsEntities())
            {
                var dbCommission = db.Commissions.Single(x => x.Id == commissionId);
                var dbUnits = db.CommissionUnits.Where(x=>x.CommissionId == commissionId).ToList();

            var notificationManager = new NotificationManager();
            var sendCount = 0;
            foreach (var dbUnit in dbUnits)
            {
                    var msg = "Изменилась дата заседания " + dbCommission.FullNumber + " с " + prevDate.ToShortDateString() + " на " + dbCommission.Date.ToShortDateString();
                notificationManager.SendNotificationAnonymous(msg, ObjectType.Commission, dbCommission.Id.ToString(), dbUnit.EmployeeId);
                sendCount++;
            }
            ActionLogger.WriteInt(db, (Guid?)null, "Сформировано " + sendCount + " уведомлений участникам коммиссий о изменении даты заседания "+ dbCommission.FullNumber, withoutIp: true);
            db.SaveChanges();
            }
        }
    }
}