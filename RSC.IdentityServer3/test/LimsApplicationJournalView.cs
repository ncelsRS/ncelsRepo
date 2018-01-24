namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LimsApplicationJournalView")]
    public partial class LimsApplicationJournalView
    {
        public Guid Id { get; set; }

        public Guid? EquipmentId { get; set; }

        public DateTime? ApplicationDate { get; set; }

        public string TypeOfMalfunction { get; set; }

        public Guid? ApplicantId { get; set; }

        public DateTime? ApplicationSignDate { get; set; }

        public Guid? EngineerId { get; set; }

        public DateTime? EngineerSignDate { get; set; }

        public Guid? AccepterId { get; set; }

        public DateTime? AccepterSignDate { get; set; }

        public string Result { get; set; }

        public string Note { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public string LimsEquipmentName { get; set; }

        [StringLength(500)]
        public string LimsEquipmentInventoryNumber { get; set; }

        [StringLength(500)]
        public string LimsEquipmentSerialNumber { get; set; }

        [StringLength(4000)]
        public string LimsEquipmentLocationName { get; set; }

        [StringLength(4000)]
        public string LimsEquipmentLaboratoryName { get; set; }

        [StringLength(4000)]
        public string ApplicationEmpName { get; set; }

        [StringLength(4000)]
        public string EngineerEmpName { get; set; }

        [StringLength(4000)]
        public string AccepterEmpName { get; set; }
    }
}
