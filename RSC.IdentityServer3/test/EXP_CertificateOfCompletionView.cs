namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_CertificateOfCompletionView
    {
        public Guid Id { get; set; }

        [StringLength(500)]
        public string Number { get; set; }

        public int? DicStageId { get; set; }

        public Guid? DrugDeclarationId { get; set; }

        public decimal? TotalPrice { get; set; }

        public Guid? StatusId { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? SendDate { get; set; }

        public Guid? CreateEmployeeId { get; set; }

        [StringLength(500)]
        public string TradeNameRu { get; set; }

        [StringLength(500)]
        public string TradeNameKz { get; set; }

        [StringLength(500)]
        public string TradeNameEn { get; set; }

        [StringLength(500)]
        public string ManufacturerNameRu { get; set; }

        [StringLength(500)]
        public string ManufacturerNameKz { get; set; }

        [StringLength(500)]
        public string ManufacturerNameEn { get; set; }

        [StringLength(4000)]
        public string CountryName { get; set; }

        [StringLength(4000)]
        public string CountryNameKz { get; set; }

        [StringLength(4000)]
        public string StatusStr { get; set; }

        [StringLength(4000)]
        public string StatusStrKz { get; set; }

        [StringLength(4000)]
        public string StatusCode { get; set; }

        [StringLength(4000)]
        public string CreateEmployeeStr { get; set; }

        [StringLength(2000)]
        public string Stage { get; set; }

        [StringLength(2000)]
        public string StageKz { get; set; }

        [StringLength(4000)]
        public string TaskExecutorValue { get; set; }

        public Guid? TaskExecutorId { get; set; }

        public DateTime? TaskExecutedDate { get; set; }

        [StringLength(4000)]
        public string TaskComment { get; set; }

        [StringLength(500)]
        public string ActNumber1C { get; set; }

        public DateTime? ActDate1C { get; set; }
    }
}
