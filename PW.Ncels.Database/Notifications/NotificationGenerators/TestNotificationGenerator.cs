using System;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace PW.Ncels.Database.Notifications.NotificationGenerators
{
    public class TestNotificationGenerator : INotificationGenerator
    {
        public Notification TryGenerateNotification<T>(ncelsEntities dbContext, NotificationTypeKey notificationTypeKey, T sourceObject) where T : class
        {
            var contract = sourceObject as Contract;
            return new Notification()
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                EmployeesId = Guid.NewGuid().ToString(),
                ObjectId = contract.Id.ToString(),
                TableName = notificationTypeKey.ObjectType.GetDescription(),
                Note = "testNotification"
            };
        }
    }
}