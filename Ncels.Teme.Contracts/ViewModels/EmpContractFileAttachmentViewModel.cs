using System;
using System.Collections.Generic;

namespace Ncels.Teme.Contracts.ViewModels
{
    public class EmpContractFileAttachmentViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string MarkClassName { get; set; }
        public bool IsNotApplicable { get; set; }

        public IList<EmpContractFileAttachmentItemViewModel> Items { get; set; }
    }

    public class EmpContractFileAttachmentItemViewModel
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
        //public DIC_FileLinkStatus DicFileLinkStatus { get; set; }

        public string StatusName { get; set; }
        public string StatusCode { get; set; }
    }
}

