namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_Applicant
    {
        public Guid Id { get; set; }

        [StringLength(1000)]
        public string NameRu { get; set; }

        [StringLength(1000)]
        public string NameKz { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ExpireDate { get; set; }
    }
}
