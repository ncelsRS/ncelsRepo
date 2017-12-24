using System;
using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Database.Models
{
	public class ZBKTransferRegister
    {
        public string Declarer { get; set; }
        public string ConclusionNumber { get; set; }
        public DateTime? SendDate { get; set; }
        public string DrugFormFullName { get; set; }
        public string RequestType { get; set; }
        public DateTime? ExtraditeDate { get; set; }
        public string ReceiverFIO { get; set; }
        public int Order { get; set; }
    }
}