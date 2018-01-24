namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_Ref_ValueAddedTax
    {
        public Guid Id { get; set; }

        public double Value { get; set; }

        public int Year { get; set; }
    }
}
