namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProjectsView")]
    public partial class ProjectsView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Type { get; set; }

        [StringLength(4000)]
        public string TypeValue { get; set; }

        [StringLength(512)]
        public string Number { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime CreatedDate { get; set; }

        public DateTime? OutgoingDate { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Status { get; set; }

        [StringLength(4000)]
        public string StausValue { get; set; }

        [StringLength(1000)]
        public string NameRu { get; set; }

        public bool? IsRegisterProject { get; set; }

        [Key]
        [Column(Order = 4)]
        public Guid OwnerId { get; set; }

        [StringLength(1506)]
        public string info { get; set; }
    }
}
