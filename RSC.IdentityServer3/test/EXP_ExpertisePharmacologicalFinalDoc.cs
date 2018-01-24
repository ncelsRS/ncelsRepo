namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_ExpertisePharmacologicalFinalDoc
    {
        public Guid Id { get; set; }

        [Column(TypeName = "ntext")]
        public string Indicator1 { get; set; }

        [Column(TypeName = "ntext")]
        public string Indicator2 { get; set; }

        [Column(TypeName = "ntext")]
        public string Indicator3 { get; set; }

        [Column(TypeName = "ntext")]
        public string Indicator4 { get; set; }

        [Column(TypeName = "ntext")]
        public string Indicator5 { get; set; }

        [Column(TypeName = "ntext")]
        public string Indicator6 { get; set; }

        [Column(TypeName = "ntext")]
        public string Indicator7 { get; set; }

        [Column(TypeName = "ntext")]
        public string Indicator8 { get; set; }

        [Column(TypeName = "ntext")]
        public string Indicator9 { get; set; }

        [Column(TypeName = "ntext")]
        public string Indicator10 { get; set; }

        [Column(TypeName = "ntext")]
        public string Indicator11 { get; set; }

        [Column(TypeName = "ntext")]
        public string Indicator12 { get; set; }

        [Column(TypeName = "ntext")]
        public string Indicator13 { get; set; }

        [Column(TypeName = "ntext")]
        public string Indicator14 { get; set; }

        [Column(TypeName = "ntext")]
        public string Indicator15 { get; set; }

        [Column(TypeName = "ntext")]
        public string Indicator16 { get; set; }

        [Column(TypeName = "ntext")]
        public string Indicator17 { get; set; }

        [Column(TypeName = "ntext")]
        public string Indicator18 { get; set; }

        [Column(TypeName = "ntext")]
        public string Indicator19 { get; set; }

        [Column(TypeName = "ntext")]
        public string Indicator20 { get; set; }

        [Column(TypeName = "ntext")]
        public string Indicator21 { get; set; }

        [Column(TypeName = "ntext")]
        public string Indicator22 { get; set; }

        [Column(TypeName = "ntext")]
        public string Indicator23 { get; set; }

        public Guid DosageStageId { get; set; }

        public string ExpertConslusion { get; set; }

        public virtual EXP_ExpertiseStageDosage EXP_ExpertiseStageDosage { get; set; }
    }
}
