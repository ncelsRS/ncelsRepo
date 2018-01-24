namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_StatementMedicalDevicePackage
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        public string Kind { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(50)]
        public string VolumeValue { get; set; }

        [StringLength(50)]
        public string VolumeUnit { get; set; }

        public int? Count { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public Guid? StatementId { get; set; }
    }
}
