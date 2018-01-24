namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_OP_Commission
    {
        public Guid Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfCreation { get; set; }

        public Guid DeclarationId { get; set; }

        public Guid EmployeeId { get; set; }

        public Guid RoleId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual OBK_AssessmentDeclaration OBK_AssessmentDeclaration { get; set; }

        public virtual OBK_OP_CommissionRoles OBK_OP_CommissionRoles { get; set; }
    }
}
