using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBKContractServiceViewModel
    {
        public Guid? Id { get; set; }
        public string ServiceName { get; set; }
        public Guid ServiceId { get; set; }
        public string UnitOfMeasurementName { get; set; }
        public Guid UnitOfMeasurementId { get; set; }
        public double PriceWithoutTax { get; set; }
        public int Count { get; set; }
        public double FinalCostWithoutTax { get; set; }
        public double FinalCostWithTax { get; set; }
        public int? ProductId { get; set; }

        public string ProductName { get; set; }
    }
}
