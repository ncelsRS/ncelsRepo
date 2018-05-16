using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Entities
{
    public class ConclusionSafetyReport
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public string Category { get; set; }
        public string Remark { get; set; }
        public string StatusName { get; set; }
        public bool IsSigned { get; set; }
        public bool? IsAccepted { get; set; }

    }
}
