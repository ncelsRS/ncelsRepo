namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LimsTmcOutView")]
    public partial class LimsTmcOutView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid TmcOutId { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid TmcId { get; set; }

        [StringLength(450)]
        public string Code { get; set; }

        [StringLength(450)]
        public string Number { get; set; }

        public string Name { get; set; }

        public Guid? MeasureTypeConvertDicId { get; set; }

        [StringLength(4000)]
        public string MeasureTypeConvertName { get; set; }

        [StringLength(4000)]
        public string MeasureTypeConvertNameKz { get; set; }

        [Key]
        [Column(Order = 3)]
        public decimal Count { get; set; }

        [Key]
        [Column(Order = 4)]
        public decimal CountFact { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StateType { get; set; }

        [StringLength(450)]
        public string Note { get; set; }

        public string Comment { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public Guid? OutTypeDicId { get; set; }

        [StringLength(4000)]
        public string OutTypeName { get; set; }

        [StringLength(4000)]
        public string OutTypeNameKz { get; set; }

        [Key]
        [Column(Order = 7)]
        public Guid CreatedEmployeeId { get; set; }

        [StringLength(4000)]
        public string CreatedEmployeeFullName { get; set; }

        public Guid? OwnerEmployeeId { get; set; }

        [StringLength(4000)]
        public string OwnerEmployeeFullName { get; set; }

        public Guid? StorageDicId { get; set; }

        [StringLength(450)]
        public string Rack { get; set; }

        [StringLength(450)]
        public string Safe { get; set; }
    }
}
