namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_RS_Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_RS_Products()
        {
            OBK_ContractPrice = new HashSet<OBK_ContractPrice>();
            OBK_MtPart = new HashSet<OBK_MtPart>();
            OBK_Procunts_Series = new HashSet<OBK_Procunts_Series>();
            OBK_RS_ProductsCom = new HashSet<OBK_RS_ProductsCom>();
            OBK_StageExpDocument = new HashSet<OBK_StageExpDocument>();
        }

        public int Id { get; set; }

        public string NameRu { get; set; }

        public string NameKz { get; set; }

        public string ProducerNameRu { get; set; }

        public string ProducerNameKz { get; set; }

        public string CountryNameRu { get; set; }

        public string CountryNameKZ { get; set; }

        [StringLength(20)]
        public string TnvedCode { get; set; }

        [StringLength(20)]
        public string KpvedCode { get; set; }

        [StringLength(20)]
        public string Price { get; set; }

        public Guid? ContractId { get; set; }

        public int RegTypeId { get; set; }

        public int? DegreeRiskId { get; set; }

        [StringLength(255)]
        public string DrugFormBoxCount { get; set; }

        [StringLength(4000)]
        public string DrugFormFullName { get; set; }

        [StringLength(4000)]
        public string DrugFormFullNameKz { get; set; }

        public Guid? CurrencyId { get; set; }

        public int RegisterId { get; set; }

        [StringLength(50)]
        public string RegNumber { get; set; }

        [StringLength(50)]
        public string RegNumberKz { get; set; }

        public DateTime RegDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        [StringLength(50)]
        public string NdName { get; set; }

        [StringLength(50)]
        public string NdNumber { get; set; }

        public int ExpertisePlace { get; set; }

        [StringLength(15)]
        public string Dimension { get; set; }

        public virtual OBK_Contract OBK_Contract { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ContractPrice> OBK_ContractPrice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_MtPart> OBK_MtPart { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_Procunts_Series> OBK_Procunts_Series { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_RS_ProductsCom> OBK_RS_ProductsCom { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_StageExpDocument> OBK_StageExpDocument { get; set; }
    }
}
