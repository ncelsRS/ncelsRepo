namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_StatementChange
    {
        public Guid Id { get; set; }

        [StringLength(2000)]
        public string Name { get; set; }

        [StringLength(2000)]
        public string Type { get; set; }

        [StringLength(2000)]
        public string BeforeChange { get; set; }

        [StringLength(2000)]
        public string AfterChange { get; set; }

        public Guid? StatementId { get; set; }
    }
}
