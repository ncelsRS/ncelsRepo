using System;

namespace PW.Ncels.Database.Models
{
    public class ContractSignData
    {
        public Guid ContractId { get; set; }
        public string ContractNumber { get; set; }
        public DateTime ContractDate { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
    }
}