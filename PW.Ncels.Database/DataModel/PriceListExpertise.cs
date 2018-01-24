namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PriceListExpertise")]
    public partial class PriceListExpertise
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        public string Number { get; set; }

        [StringLength(450)]
        public string Name { get; set; }

        public decimal? PriceRegisterForeign { get; set; }

        public decimal? PriceRegisterForeignNds { get; set; }

        public decimal? PriceReRegisterForeign { get; set; }

        public decimal? PriceReRegisterForeignNds { get; set; }

        public decimal? PriceRegisterKz { get; set; }

        public decimal? PriceRegisterKzNds { get; set; }

        public decimal? PriceReRegisterKz { get; set; }

        public decimal? PriceReRegisterKzNds { get; set; }

        [StringLength(100)]
        public string Category { get; set; }
    }
}
