namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReestrDirectionToPayView")]
    public partial class ReestrDirectionToPayView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }

        [StringLength(2000)]
        public string TypeNameRu { get; set; }

        [StringLength(2000)]
        public string TypeNameKz { get; set; }

        [StringLength(500)]
        public string DeclarationNumber { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StatusId { get; set; }

        [Key]
        [Column(Order = 4)]
        public Guid OwnerId { get; set; }

        public Guid? ContractId { get; set; }

        [StringLength(500)]
        public string ContractNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ContractDate { get; set; }

        [StringLength(500)]
        public string DeclarationNameRu { get; set; }

        [StringLength(500)]
        public string DeclarationNameKz { get; set; }

        [StringLength(500)]
        public string DeclarationNameEn { get; set; }

        public int? DrugFormId { get; set; }

        [StringLength(500)]
        public string DrugFormRu { get; set; }

        [StringLength(1000)]
        public string DrugFormKz { get; set; }

        public string DosageRu { get; set; }

        public string DosageKz { get; set; }

        public Guid? PayerOrganizationId { get; set; }

        [StringLength(500)]
        public string PayerNameRu { get; set; }

        [StringLength(500)]
        public string PayerNameKz { get; set; }

        [StringLength(500)]
        public string PayerNameEn { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(512)]
        public string DirectionToPayNumber { get; set; }

        public decimal? TotalPrice { get; set; }

        [StringLength(512)]
        public string InvoiceNumber1C { get; set; }

        public decimal? PaymentValue1C { get; set; }

        public DateTime? ModifyDate { get; set; }

        [StringLength(500)]
        public string PrimaryNumber { get; set; }

        public DateTime? PrimaryDate { get; set; }

        public decimal? PrimaryTotalPrice { get; set; }

        [StringLength(500)]
        public string ZobNumber { get; set; }

        public DateTime? ZobDate { get; set; }

        public decimal? ZobTotalPrice { get; set; }

        public Guid? PrimaryId { get; set; }

        public Guid? ZobId { get; set; }
    }
}
