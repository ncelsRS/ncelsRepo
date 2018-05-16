using System.IO;

namespace PW.Ncels.Database.DataModel
{
    public partial class FileLink
    {
        public string FilePath
        {
            get
            {
                return Path.Combine(DocumentId != null ? DocumentId.ToString() : "",
                    CategoryId != null ? CategoryId.ToString() : "",
                    string.Format("{0}{1}", Id, Path.GetExtension(FileName)));
            }
        }
        public string AcceptFormats { get; set; }

        public string Extension
        {
            get { return string.IsNullOrEmpty(FileName) ? null : Path.GetExtension(FileName); }
        }
        public string OwnerName => Employee?.DisplayName;
    }
}