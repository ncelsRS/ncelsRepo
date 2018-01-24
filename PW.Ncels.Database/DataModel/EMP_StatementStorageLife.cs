namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_StatementStorageLife
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        public string Kind { get; set; }

        public DateTime? ExpirationDate { get; set; }

        [StringLength(50)]
        public string Measure { get; set; }

        public bool? IsIndefinitely { get; set; }

        public Guid? StatementId { get; set; }
    }
}
