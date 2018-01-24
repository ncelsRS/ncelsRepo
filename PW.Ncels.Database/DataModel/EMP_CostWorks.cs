namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_CostWorks
    {
        public Guid Id { get; set; }

        public Guid PriceListId { get; set; }

        public int? Count { get; set; }

        public decimal? Price { get; set; }

        public decimal? TotalPrice { get; set; }

        public Guid ContractId { get; set; }

        public virtual EMP_Contract EMP_Contract { get; set; }

        public virtual EMP_Ref_PriceList EMP_Ref_PriceList { get; set; }
    }
}
