namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ZBKCopyStageView
    {
        [Key]
        [Column(Order = 0)]
        public Guid OBK_ZBKCopyStageId { get; set; }

        [StringLength(50)]
        public string ExpConclusionNumber { get; set; }

        public DateTime? ExpStartDate { get; set; }

        public DateTime? ExpEndDate { get; set; }

        public bool? ExpApplication { get; set; }

        [StringLength(500)]
        public string DeclarationNumber { get; set; }

        public DateTime? ZBKCopySendDate { get; set; }

        [StringLength(765)]
        public string Declarer { get; set; }

        public string DeclarationType { get; set; }

        [StringLength(4000)]
        public string OrganizationName { get; set; }

        [StringLength(50)]
        public string StageStatusCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StageId { get; set; }
    }
}
