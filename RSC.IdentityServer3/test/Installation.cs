namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Installation")]
    public partial class Installation
    {
        public Guid Id { get; set; }

        [StringLength(450)]
        public string Name { get; set; }

        [StringLength(450)]
        public string Model { get; set; }

        [StringLength(450)]
        public string ManufacturerFactory { get; set; }

        [StringLength(450)]
        public string Country { get; set; }

        [StringLength(450)]
        public string FactoryNumber { get; set; }

        [StringLength(450)]
        public string InvertoryNumber { get; set; }

        [StringLength(450)]
        public string LaboratoryRoom { get; set; }

        [StringLength(450)]
        public string InstallationYear { get; set; }

        [StringLength(450)]
        public string Note { get; set; }

        [StringLength(450)]
        public string CheckDate { get; set; }

        [StringLength(450)]
        public string Type { get; set; }

        [StringLength(450)]
        public string Laboratory { get; set; }

        [StringLength(450)]
        public string TypeIn { get; set; }

        [StringLength(450)]
        public string BuhList { get; set; }

        [StringLength(450)]
        public string State { get; set; }

        [StringLength(450)]
        public string StateIn { get; set; }
    }
}
