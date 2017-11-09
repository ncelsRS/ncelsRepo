namespace PW.Ncels.Database.Notifications
{
    /// <summary>
    /// Ключ события
    /// </summary>
    public class NotificationTypeKey
    {
        private readonly EventType _eventType;
        private readonly ObjectType _objectType;
        public NotificationTypeKey(EventType eventType, ObjectType objectType)
        {
            _eventType = eventType;
            _objectType = objectType;
        }
        /// <summary>
        /// Тип события
        /// </summary>
        public EventType EventType { get { return _eventType; } }

        /// <summary>
        /// Тип объекта
        /// </summary>
        public ObjectType ObjectType
        {
            get { return _objectType; }
        }

        public override bool Equals(object obj)
        {
            var key2 = obj as NotificationTypeKey;
            if (key2 == null) return false;
            return this.EventType.Equals(key2.EventType) && this.ObjectType.Equals(key2.ObjectType);
        }

        public override int GetHashCode()
        {
            return this.EventType.GetHashCode() ^ this.ObjectType.GetHashCode();
        }
    }
}