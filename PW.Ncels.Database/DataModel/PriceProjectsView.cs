namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PriceProjectsView")]
    public partial class PriceProjectsView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Type { get; set; }

        [StringLength(500)]
        public string Number { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Status { get; set; }

        [Key]
        [Column(Order = 4)]
        public Guid OwnerId { get; set; }

        [Key]
        [Column(Order = 5)]
        public Guid ManufacturerOrganizationId { get; set; }

        [Key]
        [Column(Order = 6)]
        public Guid HolderOrganizationId { get; set; }

        [Key]
        [Column(Order = 7)]
        public Guid ProxyOrganizationId { get; set; }

        [StringLength(500)]
        public string DoverennostNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DoverennostCreatedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DoverennostExpiryDate { get; set; }

        [StringLength(500)]
        public string Filial { get; set; }

        [StringLength(1000)]
        public string NameKz { get; set; }

        [StringLength(1000)]
        public string NameRu { get; set; }

        [StringLength(1000)]
        public string NameChangedRu { get; set; }

        [StringLength(500)]
        public string RegNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RegDate { get; set; }

        public Guid? LsTypeDicId { get; set; }

        [StringLength(500)]
        public string NameOriginal { get; set; }

        [StringLength(500)]
        public string MnnRu { get; set; }

        [StringLength(500)]
        public string MnnEn { get; set; }

        [StringLength(500)]
        public string FormNameKz { get; set; }

        [StringLength(500)]
        public string FormNameRu { get; set; }

        [StringLength(500)]
        public string Dosage { get; set; }

        [StringLength(500)]
        public string CountPackage { get; set; }

        [StringLength(500)]
        public string Concentration { get; set; }

        [StringLength(500)]
        public string CodeAtx { get; set; }

        public Guid? IntroducingMethodDicId { get; set; }

        [Key]
        [Column(Order = 8)]
        public bool IsConvention { get; set; }

        public Guid? ImnSecuryTypeDicId { get; set; }

        public Guid? RePriceDicId { get; set; }

        [StringLength(4000)]
        public string OwnerLastName { get; set; }

        [StringLength(4000)]
        public string OwnerFirstName { get; set; }

        [StringLength(4000)]
        public string OwnerMiddleName { get; set; }

        [StringLength(500)]
        public string ManufacturerOrganizationName { get; set; }

        [StringLength(500)]
        public string HolderOrganizationName { get; set; }

        [StringLength(500)]
        public string ProxyOrganizationName { get; set; }

        [StringLength(4000)]
        public string LsTypeName { get; set; }

        [StringLength(4000)]
        public string IntroducingMethodName { get; set; }

        [StringLength(4000)]
        public string ImnSecuryTypeName { get; set; }

        [StringLength(4000)]
        public string RePriceName { get; set; }

        public Guid? PriceProjectId { get; set; }

        public int? RegisterId { get; set; }

        public int? RegisterDfId { get; set; }

        [StringLength(500)]
        public string Volume { get; set; }
    }
}
