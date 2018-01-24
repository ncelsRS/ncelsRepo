namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugDeclarationDirectionToPayView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [StringLength(500)]
        public string Number { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }

        [StringLength(2000)]
        public string TypeNameRu { get; set; }

        [StringLength(2000)]
        public string TypeNameKz { get; set; }

        [StringLength(500)]
        public string NameRu { get; set; }

        [StringLength(500)]
        public string NameKz { get; set; }

        [StringLength(500)]
        public string NameEn { get; set; }

        [StringLength(4000)]
        public string DrugFormRu { get; set; }

        [StringLength(4000)]
        public string DrugFormKz { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StatusId { get; set; }

        [StringLength(2000)]
        public string StausName { get; set; }

        [Key]
        [Column(Order = 4)]
        public Guid OwnerId { get; set; }

        public Guid? ContractId { get; set; }

        public Guid? ExecuterId { get; set; }

        public Guid? DirectionToPayId { get; set; }
    }
}
