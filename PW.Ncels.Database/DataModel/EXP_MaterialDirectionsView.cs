namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_MaterialDirectionsView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        [StringLength(128)]
        public string Number { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid DrugDeclarationId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StatusId { get; set; }

        [StringLength(4000)]
        public string StatusCode { get; set; }

        [StringLength(4000)]
        public string StatusStr { get; set; }

        [StringLength(4000)]
        public string StatusKzStr { get; set; }

        public Guid? SendEmployeeId { get; set; }

        public DateTime? SendDate { get; set; }

        public Guid? ExecutorEmployeeId { get; set; }

        public DateTime? ReceiveDate { get; set; }

        public DateTime? RejectDate { get; set; }

        public string Comment { get; set; }

        [StringLength(500)]
        public string DdNumber { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime RegisteredDate { get; set; }

        [StringLength(2000)]
        public string RegistrationTypeRu { get; set; }

        [StringLength(2000)]
        public string RegistrationTypeKz { get; set; }

        [StringLength(500)]
        public string TradeNameRu { get; set; }

        [StringLength(500)]
        public string TradeNameKz { get; set; }

        [StringLength(500)]
        public string TradeNameEn { get; set; }

        [Key]
        [Column(Order = 4)]
        public decimal Dosage { get; set; }

        public long? DosageMeasureTypeId { get; set; }

        [StringLength(255)]
        public string DosageMeasureTypeName { get; set; }

        [StringLength(500)]
        public string ConcentrationRu { get; set; }

        [StringLength(500)]
        public string ConcentrationKz { get; set; }

        [StringLength(500)]
        public string DrugFormName { get; set; }

        [StringLength(1000)]
        public string DrugFormNameKz { get; set; }

        [StringLength(2000)]
        public string DrugTypeNameRu { get; set; }

        [StringLength(2000)]
        public string DrugTypeNameKz { get; set; }

        [StringLength(500)]
        public string ProducerNameRu { get; set; }

        [StringLength(500)]
        public string ProducerNameKz { get; set; }

        [StringLength(500)]
        public string ProducerNameEn { get; set; }

        [StringLength(4000)]
        public string CountryName { get; set; }

        [StringLength(4000)]
        public string CountryNameKz { get; set; }
    }
}
