using System;

namespace Ncels.Teme.Contracts.ViewModels
{
    public class EmpContractPaymentViewModel
    {
        public Guid Id { get; set; }
        public string InvoiceNumber1C { get; set; }
        public DateTime CreateDate { get; set; }
        public string PayerValue { get; set; }
        public string ContractNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public string ChiefName { get; set; }
        public string ChiefAccountantSign { get; set; }
        public string ExecutorName { get; set; }
        public string ExecutorSign { get; set; }
        public Guid ContractId { get; set; }
        public string StatusCode { get; set; }
    }
}