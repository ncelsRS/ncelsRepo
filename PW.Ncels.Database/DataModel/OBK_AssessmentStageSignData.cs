namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_AssessmentStageSignData
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid AssessmentStageId { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid SignerId { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string SignXmlData { get; set; }

        public DateTime SignDateTime { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual OBK_AssessmentStage OBK_AssessmentStage { get; set; }
    }
}
