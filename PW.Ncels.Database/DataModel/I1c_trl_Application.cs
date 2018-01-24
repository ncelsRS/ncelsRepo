namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class I1c_trl_Application
    {
        public Guid Id { get; set; }

        public Guid ApplicationId { get; set; }

        [StringLength(4000)]
        public string ApplicationNumber { get; set; }

        public DateTime? ApplicationDatetime { get; set; }

        [StringLength(4000)]
        public string TranslatorFIO { get; set; }

        public Guid? TranslatorId { get; set; }

        public int? PageQuantity { get; set; }

        public decimal? PagePrice { get; set; }

        public decimal? TotalPrice { get; set; }

        [StringLength(512)]
        public string InvoiceCode_1C { get; set; }

        [StringLength(512)]
        public string InvoiceNumber_1C { get; set; }

        public DateTime? InvoiceDatetime_1C { get; set; }

        public DateTime ExportDatetime { get; set; }

        public DateTime? ImportDatetime { get; set; }

        public Guid refPrimaryApplication { get; set; }
    }
}
