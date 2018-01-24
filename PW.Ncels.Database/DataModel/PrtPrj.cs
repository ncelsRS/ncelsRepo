namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PrtPrj
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }

        public Guid ProtocolId { get; set; }

        [StringLength(450)]
        public string ResultDictionaryId { get; set; }
    }
}
