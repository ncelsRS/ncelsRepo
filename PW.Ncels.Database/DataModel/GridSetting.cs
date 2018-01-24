namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GridSetting
    {
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Model { get; set; }

        [Required]
        [StringLength(250)]
        public string GridName { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
