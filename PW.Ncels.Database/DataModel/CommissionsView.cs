namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommissionsView")]
    public partial class CommissionsView
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Number { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string FullNumber { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime Date { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool IsComplete { get; set; }

        [StringLength(4000)]
        public string Comment { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(300)]
        public string TypeName { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int KindId { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(200)]
        public string KindName { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(10)]
        public string KindShortName { get; set; }
    }
}
