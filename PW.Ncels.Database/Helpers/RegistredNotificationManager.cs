using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Helpers
{
    public class RegistredNotificationManager
    {
        public void RegistredNotification(Guid employeeId, string author, string note)
        {
            var db = UserHelper.GetCn();
            var model = new Notification
            {
                EmployeesId = employeeId,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Note = note,
                ModifiedUser = author
            };
            db.Notifications.Add(model);
            db.SaveChanges();
            var employee = db.Employees.FirstOrDefault(e => e.Id == employeeId);
            if (string.IsNullOrEmpty(employee?.Email))
            {
                return;
            }
            var mail = new MailMessage();
            var smptServer = new SmtpClient("smpt.mail.ru");
            mail.From = new MailAddress("ncels@inbox.ru");
            mail.To.Add("diweb@mail.ru");
            mail.Subject = "Уведомления с НЦЭЛС";
            mail.IsBodyHtml = true;
            mail.Body = note;
            smptServer.Port = 587;
            smptServer.Credentials = new System.Net.NetworkCredential("ncels@inbox.ru","!Q2w#E4r");
            smptServer.EnableSsl = true;
            smptServer.Send(mail);
        }
    }
}
