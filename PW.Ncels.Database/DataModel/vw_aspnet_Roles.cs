namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_aspnet_Roles
    {
        [Key]
        [Column(Order = 0)]
        public Guid ApplicationId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid RoleId { get; set; }

        [StringLength(512)]
        public string RoleName { get; set; }

        [StringLength(512)]
        public string LoweredRoleName { get; set; }

        [StringLength(512)]
        public string Description { get; set; }
    }
}
