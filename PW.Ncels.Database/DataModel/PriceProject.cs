namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PriceProject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PriceProject()
        {
            PP_AttachRemarks = new HashSet<PP_AttachRemarks>();
            PriceProjectComs = new HashSet<PriceProjectCom>();
            PriceProjectFieldHistories = new HashSet<PriceProjectFieldHistory>();
            PriceProjectsHistories = new HashSet<PriceProjectsHistory>();
        }

        public Guid Id { get; set; }

        public int Type { get; set; }

        [StringLength(500)]
        public string Number { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Status { get; set; }

        public Guid OwnerId { get; set; }

        public Guid ManufacturerOrganizationId { get; set; }

        public Guid HolderOrganizationId { get; set; }

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

        public bool IsConvention { get; set; }

        public Guid? ImnSecuryTypeDicId { get; set; }

        public Guid? RePriceDicId { get; set; }

        public Guid? ListTypeDicId { get; set; }

        public int? MnnOrderNumber { get; set; }

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

        public bool IsOrfan { get; set; }

        public Guid? ReasonDicId { get; set; }

        public Guid? PriceProjectId { get; set; }

        [StringLength(500)]
        public string Volume { get; set; }

        public int? RegisterId { get; set; }

        public int? RegisterDfId { get; set; }

        public bool IsArchive { get; set; }

        public bool IsSended { get; set; }

        public bool IsSigned { get; set; }

        public int? RequestOrderYear { get; set; }

        public int? RequestOrderType { get; set; }

        [StringLength(1000)]
        public string NameChangedRu { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Guid? RejectReasonId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PP_AttachRemarks> PP_AttachRemarks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PriceProjectCom> PriceProjectComs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PriceProjectFieldHistory> PriceProjectFieldHistories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PriceProjectsHistory> PriceProjectsHistories { get; set; }
    }
}
