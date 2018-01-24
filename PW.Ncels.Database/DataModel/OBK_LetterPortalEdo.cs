namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_LetterPortalEdo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_LetterPortalEdo()
        {
            OBK_LetterFromEdo = new HashSet<OBK_LetterFromEdo>();
        }

        public long ID { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? LetterSentEdoDate { get; set; }

        public Guid? AuthorID { get; set; }

        public DateTime? LetterRegDate { get; set; }

        [StringLength(1000)]
        public string LetterContent { get; set; }

        public int? LetterStatusId { get; set; }

        public Guid? ContractId { get; set; }

        [StringLength(50)]
        public string EdoRegNomer { get; set; }

        public DateTime? EdoRegDate { get; set; }

        public long? OBKLetterRegID { get; set; }

        public virtual OBK_Contract OBK_Contract { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_LetterFromEdo> OBK_LetterFromEdo { get; set; }

        public virtual OBK_LetterRegistration OBK_LetterRegistration { get; set; }
    }
}
