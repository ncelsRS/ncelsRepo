namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_Ref_ContractDocumentType
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [StringLength(255)]
        public string NameRu { get; set; }

        [Required]
        [StringLength(255)]
        public string NameKz { get; set; }

        [Required]
        [StringLength(255)]
        public string NameGenitiveRu { get; set; }

        [Required]
        [StringLength(255)]
        public string NameGenitiveKz { get; set; }
    }
}
