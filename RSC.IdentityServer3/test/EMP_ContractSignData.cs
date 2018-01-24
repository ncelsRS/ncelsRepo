namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_ContractSignData
    {
        [Key]
        public Guid ContractId { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string ApplicationSign { get; set; }

        public DateTime ApplicationSignDate { get; set; }

        [Column(TypeName = "ntext")]
        public string CeoSign { get; set; }

        public DateTime? CeoSignDate { get; set; }

        public virtual EMP_Contract EMP_Contract { get; set; }
    }
}
