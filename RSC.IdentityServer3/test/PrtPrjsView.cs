namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PrtPrjsView")]
    public partial class PrtPrjsView
    {
        public Guid Id { get; set; }

        public Guid? ProjectId { get; set; }

        [StringLength(512)]
        public string ProjectNumber { get; set; }

        public DateTime? ProjectDate { get; set; }

        [StringLength(500)]
        public string ManufaturerName { get; set; }

        [StringLength(4000)]
        public string CountryName { get; set; }

        [StringLength(500)]
        public string ApplicantName { get; set; }

        [StringLength(512)]
        public string ProtocolNumber { get; set; }

        public DateTime? ProtocolDate { get; set; }

        [StringLength(4000)]
        public string ProtocolSummary { get; set; }

        [StringLength(450)]
        public string ResultDictionaryId { get; set; }

        public Guid? ProtocolId { get; set; }

        [StringLength(4000)]
        public string Name { get; set; }
    }
}
