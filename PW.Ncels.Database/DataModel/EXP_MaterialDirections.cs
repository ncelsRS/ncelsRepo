namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_MaterialDirections
    {
        public Guid Id { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        [StringLength(128)]
        public string Number { get; set; }

        public Guid DrugDeclarationId { get; set; }

        public Guid StatusId { get; set; }

        public Guid? SendEmployeeId { get; set; }

        public DateTime? SendDate { get; set; }

        public Guid? ExecutorEmployeeId { get; set; }

        public DateTime? ReceiveDate { get; set; }

        public DateTime? RejectDate { get; set; }

        public string Comment { get; set; }

        public virtual Dictionary Status { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Employee Employee1 { get; set; }

        public virtual EXP_DrugDeclaration EXP_DrugDeclaration { get; set; }
    }
}
