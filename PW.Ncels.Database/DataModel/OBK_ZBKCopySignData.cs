namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ZBKCopySignData
    {
        public Guid Id { get; set; }

        public Guid OBK_ZBKCopyId { get; set; }

        public Guid SignerId { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string SignXmlData { get; set; }

        public DateTime SignDateTime { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual OBK_ZBKCopy OBK_ZBKCopy { get; set; }
    }
}
