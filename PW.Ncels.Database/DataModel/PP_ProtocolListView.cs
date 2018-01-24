namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PP_ProtocolListView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        public bool? HasEdit { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime ProtocolDate { get; set; }

        [StringLength(500)]
        public string Number { get; set; }

        public bool? IsFinal { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Type { get; set; }

        [StringLength(4000)]
        public string TypeName { get; set; }

        public Guid? UserId { get; set; }

        [StringLength(4000)]
        public string OwnerName { get; set; }

        [StringLength(500)]
        public string RequesterName { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Status { get; set; }

        [StringLength(4000)]
        public string StatusName { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool IsDeleted { get; set; }

        public Guid? ChiefId { get; set; }
    }
}
