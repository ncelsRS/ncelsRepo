namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TmcOff
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid CreatedEmployeeId { get; set; }

        public int StateType { get; set; }

        public Guid? TmcOutId { get; set; }

        [StringLength(450)]
        public string Note { get; set; }

        public decimal Count { get; set; }

        public Guid? ExpertiseStatementId { get; set; }

        [StringLength(512)]
        public string ExpertiseStatementNumber { get; set; }

        [StringLength(512)]
        public string ExpertiseStatementTypeStr { get; set; }

        public Guid? TmcId { get; set; }

        public DateTime? DeleteDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}
