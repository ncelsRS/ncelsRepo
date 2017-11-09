using System;
using System.Collections.Generic;
using System.Linq;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Notifications.NotificationGenerators;

namespace PW.Ncels.Database.Notifications
{
    public class NotificationManager
    {
        private readonly ncelsEntities _dbContext;

        private static Dictionary<NotificationTypeKey, INotificationGenerator> _notificationGenerators;

        static NotificationManager()
        {
            RegisterEventTypes();
        }

        public NotificationManager()
        {
            _dbContext = new ncelsEntities();
        }
        /// <summary>
        /// Создаем уведомление
        /// </summary>
        /// <param name="notificationText">Текст уведомления</param>
        /// <param name="forObjectType">Тип объекта</param>
        /// <param name="objectId">Идентификатор объекта</param>
        /// <param name="forEmployee">Идентификатор сотрудника для которого создается уведомление</param>
        /// <param name="employeeEmail">Email сотрудника на который нужно отправить уведомление</param>
        /// <param name="modifiedUser">Информация о пользователе сформировавшем уведомление</param>
        /// <param name="objectState">Код состояния объекта</param>
        /// <returns>Сохраненное уведомление</returns>
        public Notification SendNotification(string notificationText, ObjectType forObjectType, Guid objectId,
            Guid forEmployee, string employeeEmail = null, int? objectState = null)
        {
            employeeEmail = string.IsNullOrEmpty(employeeEmail)
                ? _dbContext.Employees.Where(e => e.Id == forEmployee).Select(e => e.Email).FirstOrDefault()
                : employeeEmail;
            return CreateNotification(notificationText, forObjectType.GetDescription(), objectId.ToString(), forEmployee.ToString(), employeeEmail, UserHelper.GetCurrentName(), objectState);
        }

        public Notification SendNotificationAnonymous(string notificationText, ObjectType forObjectType, string objectId,
            Guid forEmployee, string employeeEmail = null, int? objectState = null)
        {
            employeeEmail = string.IsNullOrEmpty(employeeEmail)
                ? _dbContext.Employees.Where(e => e.Id == forEmployee).Select(e => e.Email).FirstOrDefault()
                : employeeEmail;
            return CreateNotification(notificationText, forObjectType.GetDescription(), objectId, forEmployee.ToString(), employeeEmail, null, objectState);
        }

        public Notification SendNotificationFromCompany(string notificationText, ObjectType forObjectType, string objectId,
            Guid forEmployee, string employeeEmail = null, int? objectState = null)
        {
            employeeEmail = string.IsNullOrEmpty(employeeEmail)
                ? _dbContext.Employees.Where(e => e.Id == forEmployee).Select(e => e.Email).FirstOrDefault()
                : employeeEmail;
            var organization = string.IsNullOrEmpty(forEmployee.ToString())
                ? _dbContext.Units.Where(e => e.Employee.Id == forEmployee).Select(e => e.DisplayName).FirstOrDefault()
                : "РГП на ПХВ «Национальный Центр экспертизы лекарственных средств, изделий медицинского назначения и медицинской техники»";
            return CreateNotification(notificationText, forObjectType.GetDescription(), objectId, forEmployee.ToString(), employeeEmail, organization, objectState);
        }

        /// <summary>
        /// Пробуем сгенерировать и отправить уведомление
        /// </summary>
        /// <typeparam name="T">Тип объекта источника уведомления</typeparam>
        /// <param name="eventType"></param>
        /// <param name="objectType"></param>
        /// <param name="sourceObject">Объект на основание которого генерируется уведомление</param>
        /// <returns>Уведомление либо null если не получилось сгенерировать</returns>
        public Notification TrySendNotification<T>(EventType eventType, ObjectType objectType, T sourceObject) where T : class
        {
            var notificationTypeKey = new NotificationTypeKey(eventType, objectType);

            var notificationGenerator = _notificationGenerators.ContainsKey(notificationTypeKey)
                ? _notificationGenerators[notificationTypeKey]
                : null;
            if (notificationGenerator != null)
            {
                var notification = notificationGenerator.TryGenerateNotification(_dbContext, notificationTypeKey, sourceObject);
                if (notification != null)
                {
                    _dbContext.Notifications.Add(notification);
                    _dbContext.SaveChanges();
                    return notification;
                }
            }
            return null;
        }

        private Notification CreateNotification(string notificationText, string forObjectType, string objectId, string forEmployee, string employeeEmail, string modifiedUser = null, int? objectState = null)
        {
            var notification = new Notification()
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                EmployeesId = forEmployee,
                TableName = forObjectType,
                ObjectId = objectId,
                Email = employeeEmail,
                Note = notificationText,
                StateType = objectState,
                ModifiedUser = modifiedUser
            };
            _dbContext.Notifications.Add(notification);
            _dbContext.SaveChanges();
            return notification;
        }

        public int GetCountNotification(Guid employeeId)
        {
            var count =  _dbContext.Notifications.Count(m => !m.IsRead && m.EmployeesId == employeeId.ToString());
            return count;
        }
        private static void RegisterEventTypes()
        {
            _notificationGenerators = new Dictionary<NotificationTypeKey, INotificationGenerator>();
            _notificationGenerators.Add(new NotificationTypeKey(EventType.TestEvent, ObjectType.TestObject), new TestNotificationGenerator());
            _notificationGenerators.Add(new NotificationTypeKey(EventType.ContractRejectedWithCommentByCounsel, ObjectType.Contract), new ContractRejectedNotificationGenerator());
            _notificationGenerators.Add(new NotificationTypeKey(EventType.PriceAppRegistered, ObjectType.PriceProject), new PriceAppRegisteredNotificationGenerator());
            _notificationGenerators.Add(new NotificationTypeKey(EventType.CommentForPriceApp, ObjectType.PriceProject), new CommentForPriceAppNotificationGenerator());
        }
    }
}