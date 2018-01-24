namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TmcApplicationComment
    {
        public Guid Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateDate { get; set; }

        public string Comment { get; set; }

        public Guid ApplicationId { get; set; }

        public Guid CreateEmployeeId { get; set; }
    }
}
