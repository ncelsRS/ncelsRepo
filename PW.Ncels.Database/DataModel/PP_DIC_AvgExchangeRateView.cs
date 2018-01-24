namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PP_DIC_AvgExchangeRateView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid CurrencyId { get; set; }

        [StringLength(4000)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal Rate { get; set; }

        public Guid? ChangeEmployeeId { get; set; }
    }
}
