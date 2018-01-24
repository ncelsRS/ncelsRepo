namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_AssessmentDeclarationFieldHistory
    {
        public int Id { get; set; }

        public string ControlId { get; set; }

        public Guid? UserId { get; set; }

        public string ValueField { get; set; }

        public string DisplayField { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid? AssessmentDeclarationId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual OBK_AssessmentDeclaration OBK_AssessmentDeclaration { get; set; }
    }
}
