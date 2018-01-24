namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ContractPrice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_ContractPrice()
        {
            OBK_ContractPriceCom = new HashSet<OBK_ContractPriceCom>();
        }

        public Guid Id { get; set; }

        public Guid PriceRefId { get; set; }

        public int Count { get; set; }

        public double PriceWithoutTax { get; set; }

        public double PriceWithTax { get; set; }

        public int? ProductId { get; set; }

        public Guid ContractId { get; set; }

        public virtual OBK_Contract OBK_Contract { get; set; }

        public virtual OBK_Ref_PriceList OBK_Ref_PriceList { get; set; }

        public virtual OBK_RS_Products OBK_RS_Products { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ContractPriceCom> OBK_ContractPriceCom { get; set; }
    }
}
