namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PP_AttachRemarksView
    {
        [Key]
        [Column(Order = 0)]
        public Guid AttachRemarkId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid PriceProjectId { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid AttachDicId { get; set; }

        [StringLength(4000)]
        public string dicName { get; set; }

        public string RemarkNote { get; set; }
    }
}
