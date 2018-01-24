namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PP_ProcessingJournal
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Type { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Status { get; set; }

        public int? SelectStatus { get; set; }

        [StringLength(512)]
        public string Number { get; set; }

        [StringLength(500)]
        public string IncomeNumber { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 4)]
        public Guid ProxyOrganizationId { get; set; }

        [StringLength(500)]
        public string ProxyOrgName { get; set; }

        [Key]
        [Column(Order = 5)]
        public Guid ManufacturerOrganizationId { get; set; }

        [StringLength(500)]
        public string ManufacturerOrgName { get; set; }

        [StringLength(500)]
        public string RegNumber { get; set; }

        [StringLength(500)]
        public string MnnRu { get; set; }

        [StringLength(500)]
        public string MnnEn { get; set; }

        [StringLength(1000)]
        public string TradeName { get; set; }

        public bool? HasSign { get; set; }

        public DateTime? EndExecDate { get; set; }
    }
}
