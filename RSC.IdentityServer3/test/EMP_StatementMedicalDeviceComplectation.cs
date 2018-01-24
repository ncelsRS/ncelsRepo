namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_StatementMedicalDeviceComplectation
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Identifier { get; set; }

        [StringLength(255)]
        public string Model { get; set; }

        [StringLength(255)]
        public string Manufacturer { get; set; }

        [StringLength(255)]
        public string Country { get; set; }

        public Guid? StatementId { get; set; }
    }
}
