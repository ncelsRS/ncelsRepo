namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LimsPlanEquipmentLink")]
    public partial class LimsPlanEquipmentLink
    {
        public Guid Id { get; set; }

        [StringLength(500)]
        public string Number { get; set; }

        public Guid EquipmentId { get; set; }

        public Guid EquipmentPlanId { get; set; }

        public bool? IsSign { get; set; }

        public DateTime? SignDate { get; set; }

        public DateTime? TermDate { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public virtual LimsEquipment LimsEquipment { get; set; }

        public virtual LimsEquipmentPlan LimsEquipmentPlan { get; set; }
    }
}
