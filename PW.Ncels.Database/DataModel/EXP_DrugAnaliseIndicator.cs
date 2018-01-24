namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugAnaliseIndicator
    {
        public Guid Id { get; set; }

        public Guid DosageStageId { get; set; }

        public int? AnalyseIndicator { get; set; }

        public double? Temperature { get; set; }

        public double? Humidity { get; set; }

        [StringLength(500)]
        public string Designation { get; set; }

        [StringLength(2000)]
        public string Demand { get; set; }

        [StringLength(2000)]
        public string ActualResult { get; set; }

        public bool? IsMatches { get; set; }

        public bool InProtocol { get; set; }

        public int PositionNumber { get; set; }

        public virtual EXP_DIC_AnalyseIndicator EXP_DIC_AnalyseIndicator { get; set; }

        public virtual EXP_ExpertiseStageDosage EXP_ExpertiseStageDosage { get; set; }
    }
}
