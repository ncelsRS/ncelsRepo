namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CommissionQuestion
    {
        public int Id { get; set; }

        public int CommissionId { get; set; }

        public int Number { get; set; }

        public int TypeId { get; set; }

        public string Comment { get; set; }

        public virtual Commission Commission { get; set; }

        public virtual CommissionQuestionType CommissionQuestionType { get; set; }
    }
}
