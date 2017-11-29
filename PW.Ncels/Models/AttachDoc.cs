using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW.Ncels.Models
{
    public class AttachDoc
    {
        public long ID { get; set; }
        public string AttachmentName { get; set; }
        public Nullable<long> LetterId { get; set; }
        public string SignXml { get; set; }
        public bool isSigned { get; set; }
    }
}