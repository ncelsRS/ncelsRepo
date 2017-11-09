using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models
{
    public class UnitBankModel
    {
        public Nullable<System.Guid> UnitBankId { get; set; }
        public Guid CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string BankNameRu { get; set; }
        public string BankNameKz { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string KBE { get; set; }
        public string Code { get; set; }
        public string SWIFT { get; set; }
        public string IIK { get; set; }
        public string CorrespondentBank { get; set; }
        public string CorrespondentAccount { get; set; }
        public string SWIFT1 { get; set; }
        public string CorrespondentAccount1 { get; set; }
        public string SWIFT2 { get; set; }
    }
}
