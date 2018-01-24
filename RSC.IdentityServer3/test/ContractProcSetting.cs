namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ContractProcSetting
    {
        public Guid Id { get; set; }

        public Guid ProcCenterHeadId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
