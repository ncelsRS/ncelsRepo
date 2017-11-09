using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models
{
  public class TmcViewModel
    {
        public System.Guid ? Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.Guid? CreatedEmployeeId { get; set; }
        public int StateType { get; set; }
        public string StateTypeValue { get; set; }
        public System.Guid ? TmcInId { get; set; }
        public string TmcInIdString { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Manufacturer { get; set; }
        public string Serial { get; set; }
        public decimal Count { get; set; }
        public Nullable<System.Guid> MeasureTypeDicId { get; set; }
        public string MeasureTypeDicValue { get; set; }
        public decimal CountFact { get; set; }
        public decimal CountConvert { get; set; }
        public Nullable<System.Guid> MeasureTypeConvertDicId { get; set; }
        public string MeasureTypeConvertDicValue { get; set; }
        public decimal CountActual { get; set; }
        public Nullable<System.DateTime> ManufactureDate { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public Nullable<System.Guid> PackageDicId { get; set; }
        public string PackageDicValue { get; set; }
        public Nullable<System.Guid> TmcTypeDicId { get; set; }
        public string TmcTypeDicValue { get; set; }
        public Nullable<System.Guid> StorageDicId { get; set; }
        public string StorageDicValue { get; set; }
        public string Safe { get; set; }
        public string Rack { get; set; }
        public Nullable<System.Guid> OwnerEmployeeId { get; set; }
        public string OwnerEmployeeValue { get; set; }
        public Nullable<System.DateTime> ReceivingDate { get; set; }

    }
}
