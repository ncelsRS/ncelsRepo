namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_aspnet_WebPartState_Paths
    {
        [Key]
        [Column(Order = 0)]
        public Guid ApplicationId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid PathId { get; set; }

        [StringLength(512)]
        public string Path { get; set; }

        [StringLength(512)]
        public string LoweredPath { get; set; }
    }
}
