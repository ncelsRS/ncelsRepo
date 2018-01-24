namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LimsEquipmentActView")]
    public partial class LimsEquipmentActView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid EquipmentId { get; set; }

        public string EquipmentName { get; set; }

        [StringLength(500)]
        public string SerialNumber { get; set; }

        [StringLength(500)]
        public string InventoryNumber { get; set; }

        public Guid? LaboratoryId { get; set; }

        [StringLength(4000)]
        public string LaboratoryName { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid ActTypeId { get; set; }

        [StringLength(4000)]
        public string ActTypeCode { get; set; }

        public string Reason { get; set; }

        public string State { get; set; }

        public Guid? HeadOfLaboratoryId { get; set; }

        [StringLength(1000)]
        public string HeadOfLaboratoryName { get; set; }

        public Guid? ResponsiblePersonId { get; set; }

        [StringLength(4000)]
        public string ResponsiblePersonName { get; set; }

        public Guid? DirectorRCId { get; set; }

        [StringLength(1000)]
        public string DirectorRCName { get; set; }

        public Guid? EngineerId { get; set; }

        [StringLength(1000)]
        public string EngineerName { get; set; }

        public int? Quantity { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        [StringLength(4000)]
        public string ProducerName { get; set; }
    }
}
