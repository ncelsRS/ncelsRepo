namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ZBKCopy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_ZBKCopy()
        {
            OBK_ZBKCopyBlank = new HashSet<OBK_ZBKCopyBlank>();
            OBK_ZBKCopySignData = new HashSet<OBK_ZBKCopySignData>();
        }

        public Guid Id { get; set; }

        public Guid? OBK_StageExpDocumentId { get; set; }

        public int? CopyQuantity { get; set; }

        [StringLength(400)]
        public string AttachPath { get; set; }

        public bool? ExpApplication { get; set; }

        public DateTime? SendDate { get; set; }

        public int? StatusId { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        public Guid? EmployeeId { get; set; }

        [StringLength(500)]
        public string ReceiverFIO { get; set; }

        public DateTime? ExtraditeDate { get; set; }

        public bool? OriginalsGiven { get; set; }

        public bool? zbkCopiesReady { get; set; }

        [StringLength(100)]
        public string LetterNumber { get; set; }

        public DateTime? LetterDate { get; set; }

        public int? StartNumber { get; set; }

        public int? EndPrimeNumber { get; set; }

        public int? StartApplicationNumber { get; set; }

        public int? EndApplicationNumber { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual OBK_StageExpDocument OBK_StageExpDocument { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ZBKCopyBlank> OBK_ZBKCopyBlank { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ZBKCopySignData> OBK_ZBKCopySignData { get; set; }
    }
}
