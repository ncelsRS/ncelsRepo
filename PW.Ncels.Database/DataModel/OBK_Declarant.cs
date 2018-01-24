namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_Declarant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_Declarant()
        {
            EMP_Contract = new HashSet<EMP_Contract>();
            EMP_Contract1 = new HashSet<EMP_Contract>();
            EMP_Contract2 = new HashSet<EMP_Contract>();
            EMP_DirectionToPayments = new HashSet<EMP_DirectionToPayments>();
            OBK_Contract = new HashSet<OBK_Contract>();
            OBK_DeclarantContact = new HashSet<OBK_DeclarantContact>();
            OBK_DirectionToPayments = new HashSet<OBK_DirectionToPayments>();
        }

        public Guid Id { get; set; }

        [StringLength(255)]
        public string NameKz { get; set; }

        [StringLength(255)]
        public string NameRu { get; set; }

        [StringLength(255)]
        public string NameEn { get; set; }

        public Guid? CountryId { get; set; }

        [StringLength(255)]
        public string Iin { get; set; }

        [StringLength(255)]
        public string Bin { get; set; }

        public Guid? OrganizationFormId { get; set; }

        public bool IsConfirmed { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsResident { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_Contract> EMP_Contract { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_Contract> EMP_Contract1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_Contract> EMP_Contract2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_DirectionToPayments> EMP_DirectionToPayments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_Contract> OBK_Contract { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_DeclarantContact> OBK_DeclarantContact { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_DirectionToPayments> OBK_DirectionToPayments { get; set; }
    }
}
