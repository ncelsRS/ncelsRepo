namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_register_instructions
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int register_id { get; set; }

        public int instruction_type_id { get; set; }

        [StringLength(250)]
        public string comment { get; set; }

        public virtual sr_instruction_types sr_instruction_types { get; set; }

        public virtual sr_register sr_register { get; set; }

        public virtual sr_register_instructions_files sr_register_instructions_files { get; set; }
    }
}
