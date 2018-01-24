namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PackagesView")]
    public partial class PackagesView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        public Guid? PackageTypeDicId { get; set; }

        public Guid? PackageNameDicId { get; set; }

        [StringLength(500)]
        public string PackageName { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal Size { get; set; }

        public Guid? SizeMeasureTypeDicId { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal Volume { get; set; }

        public Guid? VolumeMeasureTypeDicId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Count { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        public Guid? ObjectId { get; set; }

        [StringLength(4000)]
        public string PackageNameDicName { get; set; }

        [StringLength(4000)]
        public string PackageTypeName { get; set; }

        [StringLength(4000)]
        public string SizeMeasureTypeName { get; set; }

        [StringLength(4000)]
        public string VolumeMeasureName { get; set; }

        [StringLength(4000)]
        public string PackageNameAuto { get; set; }
    }
}
