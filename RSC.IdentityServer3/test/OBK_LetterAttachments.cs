namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_LetterAttachments
    {
        public long ID { get; set; }

        [StringLength(100)]
        public string AttachmentName { get; set; }

        public byte[] ContentFile { get; set; }

        public long? LetterId { get; set; }

        public string SignXmlBigData { get; set; }
    }
}
