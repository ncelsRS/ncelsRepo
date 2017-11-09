using System;
using System.Linq;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace PW.Ncels.Database.Notifications.NotificationGenerators
{
    public class CommentForPriceAppNotificationGenerator : INotificationGenerator
    {
        public Notification TryGenerateNotification<T>(ncelsEntities dbContext, NotificationTypeKey notificationTypeKey, T sourceObject) where T : class
        {
            var priceAppComment = sourceObject as Document;
            if (priceAppComment == null) return null;
            Guid priceAppDocId;
            if (priceAppComment.DocumentType == 1
                && Guid.TryParse(priceAppComment.AnswersId, out priceAppDocId))
            {
                var query = from d in dbContext.Documents
                            join c in dbContext.PriceProjects on d.Id equals c.Id
                            where c.Id == priceAppDocId
                            select d;
                if (!query.Any()) return null;
                var priceDoc = dbContext.Documents.FirstOrDefault(e => e.Id == priceAppDocId);
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
                        ObjectId = priceAppDocId.ToString(),
                        Email = correspondentEmail,
                        Note = string.Format("Имеется замечание на заявку №{0}", priceDoc.Number),
                        StateType = priceDoc.StateType,
                        ModifiedUser = UserHelper.GetCurrentEmployee().DisplayName
                    };
                }
            }
            return null;
        }
    }
}