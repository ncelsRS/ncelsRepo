using System;
using System.Linq;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace PW.Ncels.Database.Notifications.NotificationGenerators
{
    public class ContractRejectedNotificationGenerator : INotificationGenerator
    {
        public Notification TryGenerateNotification<T>(ncelsEntities dbContext, NotificationTypeKey notificationTypeKey, T sourceObject) where T : class
        {
            var contractRemarkDoc = sourceObject as Document;
            if (contractRemarkDoc == null) return null;
            Guid contractDocId;
            if (contractRemarkDoc.DocumentType == 4 && contractRemarkDoc.StateType == 9
                && Guid.TryParse(contractRemarkDoc.AnswersId, out contractDocId))
            {
                var query = from d in dbContext.Documents
                            join c in dbContext.Contracts on d.Id equals c.Id
                            where c.Id == contractDocId
                            select d;
                if (!query.Any()) return null;
                var contractDoc =
                    dbContext.Documents.FirstOrDefault(e => e.Id == contractDocId);
                if (string.IsNullOrEmpty(contractDoc.CorrespondentsId)) return null;

                var ids = contractDoc.CorrespondentsId.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
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
                        ObjectId = contractDocId.ToString(),
                        Email = correspondentEmail,
                        Note = "Ваш договор отклонен с замечанием от юрисконсульта ЦОЗ",
                        StateType = contractDoc.StateType,
                        ModifiedUser = UserHelper.GetCurrentEmployee().DisplayName
                    };
                }
            }
            return null;
        }
    }
}