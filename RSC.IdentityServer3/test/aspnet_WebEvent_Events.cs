namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class aspnet_WebEvent_Events
    {
        [Key]
        [StringLength(32)]
        public string EventId { get; set; }

        public DateTime EventTimeUtc { get; set; }

        public DateTime EventTime { get; set; }

        [StringLength(512)]
        public string EventType { get; set; }

        public decimal EventSequence { get; set; }

        public decimal EventOccurrence { get; set; }

        public int EventCode { get; set; }

        public int EventDetailCode { get; set; }

        [StringLength(2048)]
        public string Message { get; set; }

        [StringLength(512)]
        public string ApplicationPath { get; set; }

        [StringLength(512)]
        public string ApplicationVirtualPath { get; set; }

        [StringLength(512)]
        public string MachineName { get; set; }

        [StringLength(2048)]
        public string RequestUrl { get; set; }

        [StringLength(512)]
        public string ExceptionType { get; set; }

        [Column(TypeName = "ntext")]
        public string Details { get; set; }
    }
}
