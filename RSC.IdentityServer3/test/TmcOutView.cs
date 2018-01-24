namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TmcOutView")]
    public partial class TmcOutView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid CreatedEmployeeId { get; set; }

        [StringLength(4000)]
        public string CreatedEmployeeValue { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StateType { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(12)]
        public string StateTypeValue { get; set; }

        [StringLength(450)]
        public string Note { get; set; }

        public Guid? OutTypeDicId { get; set; }

        [StringLength(4000)]
        public string OutTypeDicValue { get; set; }

        public Guid? StorageDicId { get; set; }

        [StringLength(4000)]
        public string StorageDicValue { get; set; }

        [StringLength(450)]
        public string Safe { get; set; }

        [StringLength(450)]
        public string Rack { get; set; }

        public Guid? OwnerEmployeeId { get; set; }

        [StringLength(4000)]
        public string OwnerEmployeeValue { get; set; }
    }
}
