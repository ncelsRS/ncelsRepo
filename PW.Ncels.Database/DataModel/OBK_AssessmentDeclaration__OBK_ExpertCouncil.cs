namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_AssessmentDeclaration__OBK_ExpertCouncil
    {
        public int Id { get; set; }

        public Guid? DeclarationId { get; set; }

        public int? ExpertCouncilId { get; set; }

        public virtual OBK_AssessmentDeclaration OBK_AssessmentDeclaration { get; set; }

        public virtual OBK_ExpertCouncil OBK_ExpertCouncil { get; set; }
    }
}
