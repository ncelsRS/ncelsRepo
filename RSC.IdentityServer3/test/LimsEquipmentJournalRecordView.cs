namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LimsEquipmentJournalRecordView")]
    public partial class LimsEquipmentJournalRecordView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid EquipmentId { get; set; }

        public Guid? JournalId { get; set; }

        public DateTime? ActionDate { get; set; }

        public DateTime? NextActionDate { get; set; }

        public string ActionInfo { get; set; }

        public string ActionResult { get; set; }

        public string Note { get; set; }

        public Guid? ExecutorId { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public string EquipmentName { get; set; }

        [StringLength(4000)]
        public string EquipmentModelName { get; set; }

        [StringLength(4000)]
        public string EquipmentProducerName { get; set; }

        public int? EquipmentYearInstallation { get; set; }

        [StringLength(4000)]
        public string ExecutorName { get; set; }
    }
}
