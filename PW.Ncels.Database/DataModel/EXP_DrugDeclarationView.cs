namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugDeclarationView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }

        [StringLength(2000)]
        public string TypeName { get; set; }

        [StringLength(500)]
        public string Number { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StatusId { get; set; }

        [StringLength(2000)]
        public string StausName { get; set; }

        [StringLength(2000)]
        public string NameRu { get; set; }

        [Key]
        [Column(Order = 4)]
        public Guid OwnerId { get; set; }

        public DateTime? SendDate { get; set; }

        public Guid? ExecuterId { get; set; }

        [StringLength(4000)]
        public string ExecuterName { get; set; }

        public int? CountDosageIsControl { get; set; }

        public DateTime? SortDate { get; set; }

        [StringLength(500)]
        public string DrugNameRu { get; set; }

        [StringLength(500)]
        public string DrugNameKz { get; set; }
    }
}
