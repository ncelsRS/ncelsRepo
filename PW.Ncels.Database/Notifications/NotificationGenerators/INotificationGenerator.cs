using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Notifications.NotificationGenerators
{
    /// <summary>
    /// Интерфейс генератора уведомлений
    /// </summary>
    public interface INotificationGenerator
    {
        /// <summary>
        /// Пробуем сгенерировать уведомление на основание типа уведомления и объекта, и объекта источника уведомления
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="notificationTypeKey">Тип уведомления</param>
        /// <param name="sourceObject">Объект со всей необходимой информацией для генерации уведомления</param>
        /// <returns>Уведомление либо null если не получилось сгенерировать</returns>
        Notification TryGenerateNotification<T>(ncelsEntities dbContext, NotificationTypeKey notificationTypeKey, T sourceObject) where T : class;
    }
}