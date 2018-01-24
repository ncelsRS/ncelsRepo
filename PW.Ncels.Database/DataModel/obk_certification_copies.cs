namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class obk_certification_copies
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        public long copies_application_id { get; set; }

        public long certification_id { get; set; }

        [Required]
        [StringLength(50)]
        public string blank_begin_number { get; set; }

        [Required]
        [StringLength(50)]
        public string blank_end_number { get; set; }

        [StringLength(50)]
        public string certificate_reg_number { get; set; }

        [StringLength(50)]
        public string certificate_blank_number { get; set; }

        [StringLength(250)]
        public string comment { get; set; }

        public virtual obk_certifications obk_certifications { get; set; }
    }
}
