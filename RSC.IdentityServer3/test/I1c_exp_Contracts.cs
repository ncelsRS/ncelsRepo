namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class I1c_exp_Contracts
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(500)]
        public string ContractNumber { get; set; }

        public DateTime ContractDate { get; set; }

        [Required]
        [StringLength(500)]
        public string ContractUrl { get; set; }
    }
}
