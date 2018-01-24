namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LimsEquipmentJournal")]
    public partial class LimsEquipmentJournal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LimsEquipmentJournal()
        {
            LimsEquipmentJournalRecords = new HashSet<LimsEquipmentJournalRecord>();
        }

        public Guid Id { get; set; }

        public Guid JournalTypeId { get; set; }

        public int? Year { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public virtual Dictionary JournalTypeDic { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsEquipmentJournalRecord> LimsEquipmentJournalRecords { get; set; }
    }
}
