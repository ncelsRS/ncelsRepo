using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Models.Common
{
    public class FileGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string MarkClassName { get; set; }
        public IEnumerable<FileGroupItem>  FileGroupItems { get; set; }
    }

    public class FileGroupItem
    {
        public string AttachId { get; set; }
        public string AttachName { get; set; }
        public DateTime? sysCreatedDate { get; set; }
        public long? AttachSize { get; set; }
        public int? Version { get; set; }
        public Guid? OriginFileId { get; set; }
        public string OwnerName { get; set; }
        public string CreateDate { get; set; }
        public Guid? MetadataId { get; set; }
        public Guid OwnerId { get; set; }
        public string Comment { get; set; }
        public string Language { get; set; }
        public int? NumOfPages { get; set; }
        public string Stage { get; set; }
        public DIC_FileLinkStatus DicFileLinkStatus { get; set; }

        public string StatusName
        {
            get
            {
                {
                    if (DicFileLinkStatus != null)
                    {
                        return DicFileLinkStatus.NameRu;
                    }
                }
                return null;
            }
        }
        public string StatusCode
        {
            get
            {
                {
                    if (DicFileLinkStatus != null)
                    {
                        return DicFileLinkStatus.Code;
                    }
                }
                return null;
            }
        }
    }
}