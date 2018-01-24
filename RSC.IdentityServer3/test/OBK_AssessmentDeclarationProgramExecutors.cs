namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_AssessmentDeclarationProgramExecutors
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid DeclarationId { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid ProgramId { get; set; }

        [Key]
        [Column(Order = 3)]
        public Guid EmployeeId { get; set; }
    }
}
