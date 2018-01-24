namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_PriceListDrugTypeMapping
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        public string DrugTypeCode { get; set; }

        [StringLength(50)]
        public string PriceListCode { get; set; }

        [StringLength(50)]
        public string PriceListMulticomponentCode { get; set; }
    }
}
