namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PP_ProtocolComissionMembers
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid ProtocolId { get; set; }

        public Guid EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual PP_Protocols PP_Protocols { get; set; }
    }
}
