namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_DeclarantContact
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_DeclarantContact()
        {
            EMP_Contract = new HashSet<EMP_Contract>();
            EMP_Contract1 = new HashSet<EMP_Contract>();
            EMP_Contract2 = new HashSet<EMP_Contract>();
            OBK_Contract = new HashSet<OBK_Contract>();
        }

        public Guid Id { get; set; }

        public Guid? DeclarantId { get; set; }

        [StringLength(255)]
        public string AddressLegalRu { get; set; }

        [StringLength(255)]
        public string AddressLegalKz { get; set; }

        [StringLength(255)]
        public string AddressFact { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string BankNameRu { get; set; }

        [StringLength(255)]
        public string BankNameKz { get; set; }

        [StringLength(255)]
        public string BankIik { get; set; }

        [StringLength(255)]
        public string BankBik { get; set; }

        public Guid? CurrencyId { get; set; }

        [StringLength(255)]
        public string BossFio { get; set; }

        [StringLength(255)]
        public string BossPosition { get; set; }

        [StringLength(255)]
        public string BossLastName { get; set; }

        [StringLength(255)]
        public string BossFirstName { get; set; }

        [StringLength(255)]
        public string BossMiddleName { get; set; }

        [StringLength(255)]
        public string BossDocNumber { get; set; }

        public Guid? BossDocType { get; set; }

        public bool IsHasBossDocNumber { get; set; }

        public DateTime? BossDocCreatedDate { get; set; }

        public bool SignType { get; set; }

        [StringLength(255)]
        public string SignLastName { get; set; }

        [StringLength(255)]
        public string SignFirstName { get; set; }

        [StringLength(255)]
        public string SignMiddleName { get; set; }

        [StringLength(255)]
        public string SignPosition { get; set; }

        public Guid? SignDocType { get; set; }

        public bool IsHasSignDocNumber { get; set; }

        [StringLength(255)]
        public string SignDocNumber { get; set; }

        public DateTime? SignDocCreatedDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? SignDocEndDate { get; set; }

        public bool SignDocUnlimited { get; set; }

        public DateTime? BossDocEndDate { get; set; }

        public bool BossDocUnlimited { get; set; }

        public bool SignerIsBoss { get; set; }

        [StringLength(255)]
        public string SignPositionKz { get; set; }

        [StringLength(255)]
        public string BossPositionKz { get; set; }

        [StringLength(50)]
        public string BankAccount { get; set; }

        public int? BankId { get; set; }

        [StringLength(20)]
        public string Phone2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_Contract> EMP_Contract { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_Contract> EMP_Contract1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_Contract> EMP_Contract2 { get; set; }

        public virtual EMP_Ref_Bank EMP_Ref_Bank { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_Contract> OBK_Contract { get; set; }

        public virtual OBK_Declarant OBK_Declarant { get; set; }
    }
}
