namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_CertificateOfCompletion
    {
        public Guid Id { get; set; }

        [StringLength(500)]
        public string Number { get; set; }

        public int? DicStageId { get; set; }

        public Guid? DrugDeclarationId { get; set; }

        public decimal? TotalPrice { get; set; }

        public Guid? StatusId { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? SendDate { get; set; }

        public Guid? CreateEmployeeId { get; set; }

        public virtual Dictionary Status { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual EXP_DIC_Stage EXP_DIC_Stage { get; set; }

        public virtual EXP_DrugDeclaration EXP_DrugDeclaration { get; set; }
    }
}
