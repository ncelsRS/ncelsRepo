namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ZBKCopyStageSignData
    {
        public Guid Id { get; set; }

        public Guid SignerId { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string SignXmlData { get; set; }

        public DateTime SignDateTime { get; set; }

        public Guid? StageId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual OBK_ZBKCopyStage OBK_ZBKCopyStage { get; set; }
    }
}
