namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ZBKCopyBlank
    {
        public Guid Id { get; set; }

        public Guid? ZBKCopyId { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? StartNumber { get; set; }

        public int? EndPrimeNumber { get; set; }

        public int? StartApplicationNumber { get; set; }

        public int? EndApplicationNumber { get; set; }

        public Guid? EmployeeId { get; set; }

        public virtual OBK_ZBKCopy OBK_ZBKCopy { get; set; }
    }
}
