using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models
{
    public class TmcOutCountViewModel
    {
        public System.Guid ? Id { get; set; }
        public System.Guid ? TmcOutId { get; set; }
        public string TmcOutIdString { get; set; }
        public System.Guid ? TmcId { get; set; }
        public string Name { get; set; }
        public Nullable<System.Guid> MeasureTypeConvertDicId { get; set; }
        public string MeasureTypeConvertDicValue { get; set; }
        public decimal Count { get; set; }
        public decimal CountFact { get; set; }
        public int StateType { get; set; }
        public string StateTypeValue { get; set; }
        public string Note { get; set; }
        public string OwnerEmployeeValue { get; set; }
        public string StorageDicValue { get; set; }
        public string Safe { get; set; }
        public string Rack { get; set; }
    }
}
