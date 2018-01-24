namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_StageExpDocumentResult
    {
        public Guid Id { get; set; }

        public bool ExpResult { get; set; }

        public Guid AssessmetDeclarationId { get; set; }

        public DateTime? SelectionDate { get; set; }

        public DateTime? SelectionTime { get; set; }

        [StringLength(4000)]
        public string SelectionPlace { get; set; }

        public virtual OBK_AssessmentDeclaration OBK_AssessmentDeclaration { get; set; }
    }
}
