using System;
using System.Linq;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace PW.Ncels.Database.Notifications.NotificationGenerators
{
    public class PriceAppRegisteredNotificationGenerator : INotificationGenerator
    {
        public Notification TryGenerateNotification<T>(ncelsEntities dbContext, NotificationTypeKey notificationTypeKey, T sourceObject) where T : class
        {
            var priceApplicationDoc = sourceObject as Document;
            if (priceApplicationDoc == null) return null;
            if (priceApplicationDoc.DocumentType == 0 && priceApplicationDoc.StateType == 1)
            {
                var query = from d in dbContext.Documents
                            join c in dbContext.PriceProjects on d.Id equals c.Id
                            where c.Id == priceApplicationDoc.Id
                            select d;
                if (!query.Any()) return null;
                var priceDoc =
                    dbContext.Documents.FirstOrDefault(e => e.Id == priceApplicationDoc.Id);
                if (string.IsNullOrEmpty(priceDoc.CorrespondentsId)) return null;

                var ids = priceDoc.CorrespondentsId.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                Guid correspondentId;
                if (ids.Length > 0 && Guid.TryParse(ids[0], out correspondentId))
                {
                    var correspondentEmail =
                        dbContext.Employees.Where(e => e.Id == correspondentId).Select(e => e.Email).FirstOrDefault();
                    return new Notification()
                    {
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        EmployeesId = correspondentId.ToString(),
                        TableName = notificationTypeKey.ObjectType.GetDescription(),
                        ObjectId = priceApplicationDoc.Id.ToString(),
                        Email = correspondentEmail,
                        Note = string.Format("Заявка №{0} принята в работу", string.IsNullOrEmpty(priceDoc.OutgoingNumber) ? priceDoc.Number : priceDoc.OutgoingNumber),
                        StateType = priceDoc.StateType,
                        ModifiedUser = UserHelper.GetCurrentEmployee().DisplayName
                    };
                }
            }
            return null;
        }
    }
}