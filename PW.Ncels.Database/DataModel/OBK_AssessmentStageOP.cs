namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_AssessmentStageOP
    {
        public Guid Id { get; set; }

        public Guid? DeclarationId { get; set; }

        public int? StageStatus { get; set; }

        public virtual OBK_AssessmentDeclaration OBK_AssessmentDeclaration { get; set; }

        public virtual OBK_Ref_StageStatus OBK_Ref_StageStatus { get; set; }
    }
}
