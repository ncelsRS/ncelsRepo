namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ContractFactory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_ContractFactory()
        {
            OBK_ContractFactoryCom = new HashSet<OBK_ContractFactoryCom>();
        }

        public Guid Id { get; set; }

        public Guid ContractId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string LegalLocation { get; set; }

        [Required]
        [StringLength(255)]
        public string ActualLocation { get; set; }

        public int Count { get; set; }

        public virtual OBK_Contract OBK_Contract { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ContractFactoryCom> OBK_ContractFactoryCom { get; set; }
    }
}
