namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_register_instructions_files
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Column(TypeName = "image")]
        public byte[] file_kaz { get; set; }

        [Column(TypeName = "image")]
        public byte[] file_rus { get; set; }

        [StringLength(10)]
        public string file_ext_kz { get; set; }

        [StringLength(10)]
        public string file_ext { get; set; }

        public virtual sr_register_instructions sr_register_instructions { get; set; }
    }
}
