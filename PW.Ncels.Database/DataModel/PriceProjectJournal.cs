namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PriceProjectJournal")]
    public partial class PriceProjectJournal
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Type { get; set; }

        [StringLength(512)]
        public string Number { get; set; }

        [StringLength(512)]
        public string OutgoingNumber { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime CreatedDate { get; set; }

        public DateTime? OutgoingDate { get; set; }

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

        public Guid? ResultTypeDicId { get; set; }

        public bool? IsPayed { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PayDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ContrDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ConclusionDate { get; set; }

        public bool? IsStageExpired { get; set; }

        public int? ExpiredDayCount { get; set; }

        [StringLength(200)]
        public string ExpertAz { get; set; }

        [StringLength(200)]
        public string OutgoingDoc { get; set; }

        public int? DayCount { get; set; }

        public bool? IsNewManufacrurer { get; set; }

        [StringLength(500)]
        public string ManufacturerOrgName { get; set; }

        [StringLength(500)]
        public string ApplicantOrgName { get; set; }

        [StringLength(4000)]
        public string CountryName { get; set; }

        public Guid? ListTypeDicId { get; set; }

        public int? MnnOrderNumber { get; set; }

        [StringLength(4000)]
        public string RePriceName { get; set; }

        [StringLength(6)]
        public string ListTypeName { get; set; }

        [StringLength(4000)]
        public string TypeValue { get; set; }

        [StringLength(4000)]
        public string StatusValue { get; set; }

        public Guid? PriceProjectId { get; set; }

        public int? LeftDays { get; set; }

        public DateTime? ReturnDate { get; set; }

        public int? PassedDays { get; set; }

        [Key]
        [Column(Order = 9)]
        public bool IsSigned { get; set; }

        public int? RequestOrderYear { get; set; }
    }
}
