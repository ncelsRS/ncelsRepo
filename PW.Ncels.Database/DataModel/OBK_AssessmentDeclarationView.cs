namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_AssessmentDeclarationView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid OwnerId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StatusId { get; set; }

        [StringLength(500)]
        public string Number { get; set; }

        public DateTime? SendDate { get; set; }

        [StringLength(2000)]
        public string StausName { get; set; }

        [Key]
        [Column(Order = 4)]
        public string TypeName { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime CreatedDate { get; set; }

        public DateTime? SortDate { get; set; }
    }
}
