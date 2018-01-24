namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LimsEquipmentPlanView")]
    public partial class LimsEquipmentPlanView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid PlanTypeId { get; set; }

        [StringLength(4000)]
        public string PlanTypeCode { get; set; }

        public int? Year { get; set; }

        public Guid? DirectorRcId { get; set; }

        [StringLength(4000)]
        public string DirectorRcShortName { get; set; }

        [StringLength(4000)]
        public string DirectorRcFullName { get; set; }

        public Guid? HeadOfOpoloId { get; set; }

        [StringLength(4000)]
        public string HeadOfOpoloShortName { get; set; }

        [StringLength(4000)]
        public string HeadOfOpoloFullName { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}
