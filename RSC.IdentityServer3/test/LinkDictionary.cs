namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LinkDictionary
    {
        public Guid Id { get; set; }

        public Guid? DictionaryId { get; set; }

        public Guid? OwnerId { get; set; }

        [StringLength(4000)]
        public string PropertyName { get; set; }
    }
}
