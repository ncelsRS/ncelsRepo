namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ExtensionExecution
    {
        public Guid Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime ExecutionDate { get; set; }

        [StringLength(2048)]
        public string Text { get; set; }

        public Guid DocumentId { get; set; }

        [StringLength(900)]
        public string AutorId { get; set; }

        [StringLength(900)]
        public string AutorValue { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
