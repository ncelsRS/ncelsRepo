namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_aspnet_Users
    {
        [Key]
        [Column(Order = 0)]
        public Guid ApplicationId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid UserId { get; set; }

        [StringLength(512)]
        public string UserName { get; set; }

        [StringLength(512)]
        public string LoweredUserName { get; set; }

        [StringLength(32)]
        public string MobileAlias { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool IsAnonymous { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime LastActivityDate { get; set; }
    }
}
