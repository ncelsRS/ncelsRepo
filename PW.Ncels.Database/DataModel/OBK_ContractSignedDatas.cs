namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ContractSignedDatas
    {
        [Key]
        public Guid ContractId { get; set; }

        [Column(TypeName = "ntext")]
        public string ApplicantSign { get; set; }

        public DateTime? ApplicantSignDate { get; set; }

        [Column(TypeName = "ntext")]
        public string CeoSign { get; set; }

        public DateTime? CeoSignDate { get; set; }

        public virtual OBK_Contract OBK_Contract { get; set; }
    }
}
