namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LimsEquipmentAct")]
    public partial class LimsEquipmentAct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LimsEquipmentAct()
        {
            LimsEquipmentActSpareParts = new HashSet<LimsEquipmentActSparePart>();
        }

        public Guid Id { get; set; }

        public Guid EquipmentId { get; set; }

        public Guid ActTypeId { get; set; }

        public string Reason { get; set; }

        public string State { get; set; }

        public Guid? HeadOfLaboratoryId { get; set; }

        [StringLength(1000)]
        public string HeadOfLaboratoryName { get; set; }

        public Guid? DirectorRCId { get; set; }

        [StringLength(1000)]
        public string DirectorRCName { get; set; }

        public Guid? EngineerId { get; set; }

        [StringLength(1000)]
        public string EngineerName { get; set; }

        public int? Quantity { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public virtual Dictionary ActTypeDic { get; set; }

        public virtual Employee DirectorRcEmp { get; set; }

        public virtual Employee EngineerEmp { get; set; }

        public virtual Employee HeadOfLaboratoryEmp { get; set; }

        public virtual LimsEquipment LimsEquipment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsEquipmentActSparePart> LimsEquipmentActSpareParts { get; set; }
    }
}
