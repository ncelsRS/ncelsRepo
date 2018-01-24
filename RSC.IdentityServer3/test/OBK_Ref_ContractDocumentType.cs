namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_Ref_ContractDocumentType
    {
        public Guid Id { get; set; }

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
