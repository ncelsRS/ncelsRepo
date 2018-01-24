namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ContractFactoryCom
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_ContractFactoryCom()
        {
            OBK_ContractFactoryComRecord = new HashSet<OBK_ContractFactoryComRecord>();
        }

        public Guid Id { get; set; }

        public Guid ContractFactoryId { get; set; }

        public bool IsError { get; set; }

        public DateTime DateCreate { get; set; }

        public bool IsDelete { get; set; }

        public virtual OBK_ContractFactory OBK_ContractFactory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ContractFactoryComRecord> OBK_ContractFactoryComRecord { get; set; }
    }
}
