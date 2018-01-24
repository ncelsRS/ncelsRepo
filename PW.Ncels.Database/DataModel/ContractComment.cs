namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ContractComment
    {
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(4000)]
        public string Comment { get; set; }

        public Guid AuthorId { get; set; }

        public Guid ContractId { get; set; }

        public bool Sended { get; set; }

        public virtual Employee Author { get; set; }

        public virtual Contract Contract { get; set; }
    }
}
