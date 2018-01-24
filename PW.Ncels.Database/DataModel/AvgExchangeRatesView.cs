namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AvgExchangeRatesView")]
    public partial class AvgExchangeRatesView
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int currency_id { get; set; }

        [StringLength(10)]
        public string currency_code { get; set; }

        public int? year { get; set; }

        public int? month { get; set; }

        public decimal? rate { get; set; }
    }
}
