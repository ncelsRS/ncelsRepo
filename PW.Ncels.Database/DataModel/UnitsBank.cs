namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UnitsBank")]
    public partial class UnitsBank
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid UnitsId { get; set; }

        public Guid? CurrencyId { get; set; }

        [StringLength(500)]
        public string BankNameRu { get; set; }

        [StringLength(500)]
        public string BankNameKz { get; set; }

        [StringLength(50)]
        public string KBE { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(50)]
        public string SWIFT { get; set; }

        [StringLength(50)]
        public string IIK { get; set; }

        [StringLength(500)]
        public string CorrespondentBank { get; set; }

        [StringLength(500)]
        public string CorrespondentAccount { get; set; }

        [StringLength(50)]
        public string SWIFT1 { get; set; }

        [StringLength(500)]
        public string CorrespondentAccount1 { get; set; }

        [StringLength(50)]
        public string SWIFT2 { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
