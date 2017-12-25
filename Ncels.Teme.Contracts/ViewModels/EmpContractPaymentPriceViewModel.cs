namespace Ncels.Teme.Contracts.ViewModels
{
    public class EmpContractPaymentPriceViewModel
    {
        public int Line { get; set; }
        public string ServiceName { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public decimal PriceWithTax { get; set; }
        public decimal Price { get { return Count * PriceWithTax; } }
    }
}
