using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ncels.Teme.Contracts.ViewModels
{
    public class EmpContractPaymentDetailsViewModel
    {
        public string InvoiceNumber1C { get; set; }
        public string Provider { get; set; }
        public string Contract { get; set; }
        public Guid ContractId { get; set; }
        public DateTime? InvoiceDate1C { get; set; }
        public string Buyer { get; set; }
    }
}
