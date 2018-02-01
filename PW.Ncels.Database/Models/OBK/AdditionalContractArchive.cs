using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models.OBK
{
    public class AdditionalContractArchive
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public DateTime? StartDate { get; set; }
        public string additionalType { get; set; }
    }
}
