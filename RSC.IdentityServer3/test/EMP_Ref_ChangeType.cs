namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_Ref_ChangeType
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(500)]
        public string NameRu { get; set; }

        [Required]
        [StringLength(500)]
        public string NameKz { get; set; }

        [StringLength(10)]
        public string Code { get; set; }
    }
}
