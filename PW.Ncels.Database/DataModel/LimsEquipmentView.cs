namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LimsEquipmentView")]
    public partial class LimsEquipmentView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        [StringLength(500)]
        public string InventoryNumber { get; set; }

        [StringLength(500)]
        public string SerialNumber { get; set; }

        public int? YearInstallation { get; set; }

        public Guid? ModelId { get; set; }

        [StringLength(4000)]
        public string ModelName { get; set; }

        public Guid? ProducerId { get; set; }

        [StringLength(4000)]
        public string ProducerName { get; set; }

        public Guid? CountryProductionId { get; set; }

        [StringLength(4000)]
        public string CountryProductionName { get; set; }

        public Guid? EquipmentTypeId { get; set; }

        [StringLength(4000)]
        public string EquipmentTypeName { get; set; }

        public Guid? LocationId { get; set; }

        [StringLength(4000)]
        public string LocationName { get; set; }

        public Guid? StatusId { get; set; }

        [StringLength(4000)]
        public string StatusName { get; set; }

        public Guid? LaboratoryId { get; set; }

        [StringLength(4000)]
        public string LaboratoryName { get; set; }

        public Guid? ResponsiblePersonId { get; set; }

        [StringLength(4000)]
        public string ResponsiblePersonShortName { get; set; }

        [StringLength(4000)]
        public string ResponsiblePersonFullName { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid EquipmentPlanId { get; set; }

        public DateTime? TermDate { get; set; }

        [StringLength(4000)]
        public string EquipmentPlanCode { get; set; }

        [StringLength(500)]
        public string Number { get; set; }
    }
}
