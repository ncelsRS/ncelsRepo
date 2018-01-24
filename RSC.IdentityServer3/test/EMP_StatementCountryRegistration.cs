namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_StatementCountryRegistration
    {
        public Guid Id { get; set; }

        [StringLength(255)]
        public string Country { get; set; }

        [StringLength(50)]
        public string RegistrationNumber { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public bool? IsIndefinitely { get; set; }

        public Guid? StatementId { get; set; }
    }
}
