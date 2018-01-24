namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LinkUnit
    {
        public Guid Id { get; set; }

        public Guid? EmployeeId { get; set; }

        public Guid? PositionId { get; set; }

        public Guid? Department1Id { get; set; }

        public Guid? Department2Id { get; set; }

        public Guid? Department3Id { get; set; }

        public Guid? OrganizationId { get; set; }

        [StringLength(4000)]
        public string PropertyName { get; set; }

        public Guid? OwnerId { get; set; }
    }
}
