using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBKPaymentSignData
    {
        public Guid? Id { get; set; }
        public Guid? ContractId { get; set; }
        //
        public string UnitsName { get; set; }
        public string UnitsAddress { get; set; }
        public string UnitsPhone { get; set; }
        public string UnitsBin { get; set; }
        public string UnitsIIk { get; set; }
        public string UnitsKbe { get; set; }
        public string UnitsBankName { get; set; }
        public string UnitsBankSwift { get; set; }
        public string UnitsBankCode { get; set; }
        //
        public string InvoiceNuber1C { get; set; }
        public DateTime? InvoiceDate1C { get; set; }
        //
        public string DeclarantBin { get; set; }
        public string DeclarantOrgName { get; set; }
        public string DeclarantName { get; set; }
        public string DeclarantCountryName { get; set; }
        public string DeclarantAddressLegal { get; set; }
        public string ContactNumber { get; set; }
        public DateTime? ContactStartDate { get; set; }
        public string ContactTypeName { get; set; }
        //
        public decimal ContractPriceNds { get; set; }
        public string ContractPriceTotalText { get; set; }
        //
        public string ChiefAccountant { get; set; }
        public string Executor { get; set; }

        public List<ContractPriceSignData> ContractPriceSignDatas { get; set; }
    }

    public class ContractPriceSignData
    {
        public string ContractPriceName { get; set; }
        public int ContractPriceCount { get; set; }
        public string ContractPriceDicName { get; set; }
        public double ContractPrice { get; set; }
        public double ContractPriceTotal { get; set; }
    }
}
