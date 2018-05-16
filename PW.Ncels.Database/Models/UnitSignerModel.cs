using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models
{
    public class UnitSignerModel
    {
        public Nullable<System.Guid> UnitSignerId { get; set; }
        public Guid SignerId { get; set; }
        public string Signer { get; set; }
        public int? DocType { get; set; }
        public Nullable<System.Guid> DocumentType { get; set; }
        public bool DocAvailable { get; set; }
        public string DocNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
