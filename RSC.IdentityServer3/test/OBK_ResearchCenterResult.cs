namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ResearchCenterResult
    {
        public Guid Id { get; set; }

        public Guid TaskMaterialId { get; set; }

        public Guid LaboratoryMarkId { get; set; }

        [Required]
        [StringLength(255)]
        public string Claim { get; set; }

        [Required]
        [StringLength(255)]
        public string Humidity { get; set; }

        [Required]
        [StringLength(255)]
        public string FactResult { get; set; }

        public Guid? LaboratoryRegulationId { get; set; }

        public Guid? ExecutorId { get; set; }

        public bool? ExpertiseResult { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual OBK_Ref_LaboratoryMark OBK_Ref_LaboratoryMark { get; set; }

        public virtual OBK_Ref_LaboratoryRegulation OBK_Ref_LaboratoryRegulation { get; set; }

        public virtual OBK_TaskMaterial OBK_TaskMaterial { get; set; }
    }
}
