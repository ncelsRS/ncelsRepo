namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LimsApplicationJournal")]
    public partial class LimsApplicationJournal
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

        public virtual Employee AccepterEmp { get; set; }

        public virtual Employee ApplicationEmp { get; set; }

        public virtual Employee EngineerEmp { get; set; }

        public virtual LimsEquipment LimsEquipment { get; set; }
    }
}
