namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LimsEquipment")]
    public partial class LimsEquipment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LimsEquipment()
        {
            LimsApplicationJournals = new HashSet<LimsApplicationJournal>();
            LimsEquipmentActs = new HashSet<LimsEquipmentAct>();
            LimsEquipmentJournalRecords = new HashSet<LimsEquipmentJournalRecord>();
            LimsPlanEquipmentLinks = new HashSet<LimsPlanEquipmentLink>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        [StringLength(500)]
        public string InventoryNumber { get; set; }

        [StringLength(500)]
        public string SerialNumber { get; set; }

        public int? YearInstallation { get; set; }

        public Guid? ModelId { get; set; }

        public Guid? ProducerId { get; set; }

        public Guid? CountryProductionId { get; set; }

        public Guid? EquipmentTypeId { get; set; }

        public Guid? LocationId { get; set; }

        public Guid? StatusId { get; set; }

        public Guid? LaboratoryId { get; set; }

        public Guid? ResponsiblePersonId { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public virtual Dictionary CountryProductionDic { get; set; }

        public virtual Dictionary EquipmentTypeDic { get; set; }

        public virtual Dictionary LaboratoryDic { get; set; }

        public virtual Dictionary LocationDic { get; set; }

        public virtual Dictionary ModelDic { get; set; }

        public virtual Dictionary ProducerDic { get; set; }

        public virtual Dictionary StatusDic { get; set; }

        public virtual Employee ResponsiblePersonEmp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsApplicationJournal> LimsApplicationJournals { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsEquipmentAct> LimsEquipmentActs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsEquipmentJournalRecord> LimsEquipmentJournalRecords { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsPlanEquipmentLink> LimsPlanEquipmentLinks { get; set; }
    }
}
