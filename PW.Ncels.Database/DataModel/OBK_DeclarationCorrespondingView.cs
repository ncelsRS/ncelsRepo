namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_DeclarationCorrespondingView
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        public string BlankNumber { get; set; }

        public DateTime? ExtraditeDate { get; set; }

        [StringLength(50)]
        public string ExpConclusionNumber { get; set; }

        [StringLength(20)]
        public string ZBKType { get; set; }

        [StringLength(4000)]
        public string ProductName { get; set; }

        public string SeriesNumber { get; set; }

        public string SeriesEndDate { get; set; }

        public string SeriesParty { get; set; }

        public DateTime? ExpStartDate { get; set; }

        public DateTime? ExpEndDate { get; set; }

        public string RequestType { get; set; }

        public int? ZBKReviewDate { get; set; }

        [StringLength(4000)]
        public string OrganName { get; set; }

        public Guid? OrganId { get; set; }

        public bool? ExpResult { get; set; }

        public string ExpReason { get; set; }

        public int? RequestTypeId { get; set; }

        public string ProducerName { get; set; }

        public DateTime? RefuseDate { get; set; }
    }
}
