namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ActReception
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_ActReception()
        {
            OBK_Tasks = new HashSet<OBK_Tasks>();
        }

        public Guid Id { get; set; }

        [StringLength(50)]
        public string Number { get; set; }

        public DateTime? ActDate { get; set; }

        public string Address { get; set; }

        public string Worker { get; set; }

        public string Producer { get; set; }

        public string Provider { get; set; }

        public long? sr_measuresId { get; set; }

        public Guid? ProductSamplesId { get; set; }

        public Guid? InspectionInstalledId { get; set; }

        public Guid? PackageConditionId { get; set; }

        public Guid? MarkingId { get; set; }

        public Guid? StorageConditionsId { get; set; }

        public bool? Accept { get; set; }

        public Guid? ApplicantId { get; set; }

        [StringLength(300)]
        public string AttachPath { get; set; }

        [StringLength(1000)]
        public string Declarer { get; set; }

        public Guid? OBK_AssessmentDeclarationId { get; set; }

        public virtual Dictionary Dictionary { get; set; }

        public virtual OBK_Dictionaries OBK_Dictionaries { get; set; }

        public virtual OBK_Dictionaries OBK_Dictionaries1 { get; set; }

        public virtual OBK_AssessmentDeclaration OBK_AssessmentDeclaration { get; set; }

        public virtual OBK_Dictionaries OBK_Dictionaries2 { get; set; }

        public virtual sr_measures sr_measures { get; set; }

        public virtual OBK_Dictionaries OBK_Dictionaries3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_Tasks> OBK_Tasks { get; set; }
    }
}
