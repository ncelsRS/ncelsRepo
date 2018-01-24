namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ContractSignedData
    {
        [Key]
        public Guid ContractId { get; set; }

        [Column(TypeName = "ntext")]
        public string ApplicantSig { get; set; }

        [Column(TypeName = "ntext")]
        public string CeoSign { get; set; }

        public virtual Contract Contract { get; set; }
    }
}
