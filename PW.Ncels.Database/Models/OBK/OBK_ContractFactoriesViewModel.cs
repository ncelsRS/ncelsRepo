using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBK_ContractFactoriesViewModel
    {
        public Guid Id { get; internal set; }
        public string Name { get; set; }
        public string LegalLocation { get; set; }
        public string ActualLocation { get; set; }
        public int Count { get; set; }
    }
}
