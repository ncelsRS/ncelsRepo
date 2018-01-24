namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Report
    {
        public Guid Id { get; set; }

        public int? Type { get; set; }

        [StringLength(4000)]
        public string Text { get; set; }

        [StringLength(4000)]
        public string AnswersId { get; set; }

        [StringLength(4000)]
        public string AnswersValue { get; set; }

        public DateTime? ExecutionDate { get; set; }

        public Guid? DocumentId { get; set; }

        public Guid? TaskId { get; set; }

        [StringLength(4000)]
        public string ModifiedUser { get; set; }

        [StringLength(100)]
        public string Ip { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? PageCount { get; set; }

        public int? SymbolCount { get; set; }

        [StringLength(4000)]
        public string TitleDicId { get; set; }

        [StringLength(4000)]
        public string TitleDicValue { get; set; }

        public virtual Document Document { get; set; }

        public virtual Task Task { get; set; }
    }
}
