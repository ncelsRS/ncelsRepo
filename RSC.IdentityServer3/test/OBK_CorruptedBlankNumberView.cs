namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_CorruptedBlankNumberView
    {
        public Guid Id { get; set; }

        public int? Number { get; set; }

        public DateTime? CorruptDate { get; set; }

        public Guid? CorruptEmployee { get; set; }
    }
}
