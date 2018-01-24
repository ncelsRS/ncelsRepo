namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LimsEquipmentPlan")]
    public partial class LimsEquipmentPlan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LimsEquipmentPlan()
        {
            LimsPlanEquipmentLinks = new HashSet<LimsPlanEquipmentLink>();
        }

        public Guid Id { get; set; }

        public Guid PlanTypeId { get; set; }

        public int? Year { get; set; }

        public Guid? DirectorRcId { get; set; }

        public Guid? HeadOfOpoloId { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public virtual Dictionary PlanTypeDic { get; set; }

        public virtual Employee DirectorRcEmp { get; set; }

        public virtual Employee HeadOfOpoloEmp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsPlanEquipmentLink> LimsPlanEquipmentLinks { get; set; }
    }
}
