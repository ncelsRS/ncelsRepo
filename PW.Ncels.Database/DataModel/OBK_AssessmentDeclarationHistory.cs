namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_AssessmentDeclarationHistory
    {
        public long Id { get; set; }

        public Guid? UserId { get; set; }

        public DateTime DateCreate { get; set; }

        public bool IsDelete { get; set; }

        public int StatusId { get; set; }

        public string Note { get; set; }

        public Guid AssessmentDeclarationId { get; set; }

        public string XmlSign { get; set; }

        public virtual OBK_AssessmentDeclaration OBK_AssessmentDeclaration { get; set; }

        public virtual OBK_Ref_Status OBK_Ref_Status { get; set; }
    }
}
