namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LinkDicOrphanDrugsIcdDeseases")]
    public partial class LinkDicOrphanDrugsIcdDeseas
    {
        public long Id { get; set; }

        public long refDicOrphanDrugs { get; set; }

        public long refDicIcdDeseases { get; set; }
    }
}
