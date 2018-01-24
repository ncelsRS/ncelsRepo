namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Log
    {
        public long Id { get; set; }

        [StringLength(4000)]
        public string Message { get; set; }

        [StringLength(4000)]
        public string InnerException { get; set; }

        [StringLength(4000)]
        public string StackTrace { get; set; }

        [StringLength(4000)]
        public string CurrentUser { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
