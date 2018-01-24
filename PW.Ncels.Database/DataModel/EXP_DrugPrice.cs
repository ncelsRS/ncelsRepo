namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugPrice
    {
        public long Id { get; set; }

        [StringLength(500)]
        public string PrimaryValue { get; set; }

        [StringLength(500)]
        public string SecondaryValue { get; set; }

        [StringLength(500)]
        public string IntermediateValue { get; set; }

        public double? CountUnit { get; set; }

        [StringLength(50)]
        public string Barcode { get; set; }

        public double? ManufacturePrice { get; set; }

        public double? RefPrice { get; set; }

        public double? RegPrice { get; set; }

        public long DrugDosageId { get; set; }

        public virtual EXP_DrugDosage EXP_DrugDosage { get; set; }
    }
}
