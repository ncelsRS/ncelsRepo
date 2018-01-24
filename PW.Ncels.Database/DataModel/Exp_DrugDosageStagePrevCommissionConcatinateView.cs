namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Exp_DrugDosageStagePrevCommissionConcatinateView
    {
        [Key]
        public Guid DosageStageId { get; set; }

        public string PrevCommissions { get; set; }
    }
}
