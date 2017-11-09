using System;

namespace PW.Ncels.Database.Models
{
    public class ContractAgreementHistory
    {
        public DateTime? OperationDate { get; set; }
        public string StatusCode { get; set; }

        public string Result
        {
            get
            {
                if (StatusCode == "1") return "Согласованно";
                if (StatusCode == "2") return "Отказанно";
                return "";
            }
        }
        public string Comment { get; set; }
    }
}