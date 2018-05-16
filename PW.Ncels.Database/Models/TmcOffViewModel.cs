using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models
{
    public class TmcOffViewModel
    {
        public System.Guid ? Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.Guid ? CreatedEmployeeId { get; set; }
        public string CreatedEmployeeValue { get; set; }
        public string TmcOutIdString { get; set; }
        public int StateType { get; set; }
        public Guid? TmcOutId { get; set; }
        public string Note { get; set; }
        public string StateTypeValue { get; set; }
        public decimal Count { get; set; }

        public string TmcName { get; set; }
        public decimal? TmcCount { get; set; }

        public Guid? RefExtertiseStatement { get; set; }
        public string ExtertiseStatementNumber { get; set; }
    }
}
