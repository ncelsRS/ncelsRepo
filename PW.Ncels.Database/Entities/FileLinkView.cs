using System.IO;

namespace PW.Ncels.Database.DataModel
{
    public partial class FileLinkView
    {
        public string FilePath
        {
            get
            {
                return string.Format("{0}{1}", Id, Path.GetExtension(FileName));
            }
        }
    }
}