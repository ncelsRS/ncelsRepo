namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LimsEquipmentJournalRecord")]
    public partial class LimsEquipmentJournalRecord
    {
        public Guid Id { get; set; }

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

        public virtual Employee ExecutorEmp { get; set; }

        public virtual LimsEquipment LimsEquipment { get; set; }

        public virtual LimsEquipmentJournal LimsEquipmentJournal { get; set; }
    }
}
