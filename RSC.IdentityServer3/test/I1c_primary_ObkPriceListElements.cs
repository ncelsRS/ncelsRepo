namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class I1c_primary_ObkPriceListElements
    {
        public Guid Id { get; set; }

        public Guid? PriceListId { get; set; }

        [StringLength(512)]
        public string PriceListName { get; set; }

        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        public decimal? TotalPrice { get; set; }

        [StringLength(512)]
        public string DrugTradeName { get; set; }

        [StringLength(512)]
        public string DrugTradeNameKz { get; set; }

        [StringLength(512)]
        public string DrugFormName { get; set; }

        [StringLength(512)]
        public string DrugFormNameKz { get; set; }

        public Guid? refContractId { get; set; }

        public Guid? ZBKCopyId { get; set; }
    }
}
