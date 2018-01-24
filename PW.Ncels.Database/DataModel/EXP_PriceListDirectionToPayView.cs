namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_PriceListDirectionToPayView
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        public string Number { get; set; }

        [StringLength(450)]
        public string NameRu { get; set; }

        [StringLength(450)]
        public string NameKz { get; set; }

        [StringLength(450)]
        public string NameEn { get; set; }

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

        public Guid? DirectionToPayId { get; set; }

        public int? Count { get; set; }

        public decimal? Price { get; set; }

        public decimal? Total { get; set; }
    }
}
