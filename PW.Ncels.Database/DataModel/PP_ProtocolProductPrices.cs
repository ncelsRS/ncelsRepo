namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PP_ProtocolProductPrices
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid ProtocolId { get; set; }

        [StringLength(500)]
        public string ProductNameRu { get; set; }

        [StringLength(500)]
        public string ProductNameKz { get; set; }

        public decimal PriceFirst { get; set; }

        public decimal PriceNew { get; set; }

        public Guid? PriceProjectId { get; set; }

        public virtual PP_Protocols PP_Protocols { get; set; }
    }
}
