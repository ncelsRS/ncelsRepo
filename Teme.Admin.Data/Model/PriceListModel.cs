using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Admin.Data.Model
{
    public class PriceListModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal ValueAddedTax { get; set; }
        public bool IsImport { get; set; }
        public string NameRu { get; set; }
        public string NameKz { get; set; }
        public PriceListModificationModel priceListModificationModels { get; set; }

    }

    public class PriceListModificationModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal ValueAddedTax { get; set; }
        public bool IsImport { get; set; }
        public string NameRu { get; set; }
        public string NameKz { get; set; }
    }
}
