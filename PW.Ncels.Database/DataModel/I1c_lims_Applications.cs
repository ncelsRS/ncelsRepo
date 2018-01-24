namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class I1c_lims_Applications
    {
        public Guid Id { get; set; }

        public Guid Number { get; set; }

        public Guid? ContractId { get; set; }

        [StringLength(450)]
        public string ContractNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ContractDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastDeliveryDate { get; set; }

        public Guid? ProviderId { get; set; }

        [StringLength(450)]
        public string Provider { get; set; }

        [StringLength(450)]
        public string ProviderBin { get; set; }

        public Guid? FrpersonId { get; set; }

        [StringLength(450)]
        public string FrpersonFio { get; set; }

        public Guid? OrganizationId { get; set; }

        public bool? FullDelivery { get; set; }

        public Guid? PowerOfAttorneyId_1C { get; set; }

        [StringLength(450)]
        public string PowerOfAttorneyNumber_1C { get; set; }

        public DateTime? PowerOfAttorneyDatetime_1C { get; set; }

        public string FilePath { get; set; }

        public string Note { get; set; }

        public DateTime? CreateDatetime { get; set; }

        public DateTime? ExportDatetime { get; set; }

        public DateTime? ImportDatetime { get; set; }

        [StringLength(50)]
        public string OrganizationCode { get; set; }
    }
}
