namespace Ncels.Teme.Contracts.ViewModels
{
    public class EmpContractWorkCostViewModel
    {
        public string WorkName { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get { return Price * Count; } }
    }
}
