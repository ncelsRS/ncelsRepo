namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PP_DIC_AvgExchangeRate
    {
        public Guid Id { get; set; }

        public Guid CurrencyId { get; set; }

        public decimal Rate { get; set; }

        public DateTime? ChangeDate { get; set; }

        public Guid? ChangeEmployeeId { get; set; }

        public virtual Dictionary Dictionary { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
