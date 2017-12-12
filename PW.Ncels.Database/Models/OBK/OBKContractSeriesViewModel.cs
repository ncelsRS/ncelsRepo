using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBKContractSeriesViewModel
    {
        public int? Id { get; set; }
        public string Series { get; set; }
        public string CreateDate { get; set; }
        public string ExpireDate { get; set; }
        public string Part { get; set; }
        public long? UnitId { get; set; }
        public string UnitName { get; set; }
    }
}
