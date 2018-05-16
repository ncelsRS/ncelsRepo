using PW.Ncels.Database.Notifications;

namespace Ncels.Core.ActivityManager
{
    public class DocumentActionTypeKey
    {
        public DocumentActionTypeKey(string docTypeCode, string activityTypeCode)
        {
            DocTypeCode = docTypeCode;
            ActivityTypeCode = activityTypeCode;
        }

        public string DocTypeCode { get; private set; }
        public string ActivityTypeCode { get; private set; }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            var key2 = obj as DocumentActionTypeKey;
            if (key2 == null) return false;
            return this.DocTypeCode == key2.DocTypeCode && this.ActivityTypeCode == key2.ActivityTypeCode;
        }

        public override int GetHashCode()
        {
            return this.DocTypeCode.GetHashCode() ^ this.ActivityTypeCode.GetHashCode();
        }
    }
}