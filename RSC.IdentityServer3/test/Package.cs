namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Package
    {
        public Guid Id { get; set; }

        public Guid? PackageTypeDicId { get; set; }

        public Guid? PackageNameDicId { get; set; }

        [StringLength(500)]
        public string PackageName { get; set; }

        public decimal Size { get; set; }

        public Guid? SizeMeasureTypeDicId { get; set; }

        public decimal Volume { get; set; }

        public Guid? VolumeMeasureTypeDicId { get; set; }

        public int Count { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        public Guid? ObjectId { get; set; }
    }
}
