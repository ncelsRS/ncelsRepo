using PW.Ncels.Database.DataModel;

namespace PW.Prism.ViewModels
{
    public class ContractViewModel
    {
        public Contract Contract { get; set; }
        public bool HasAgreementTask { get; set; }
        public bool HasSigningTask { get; set; }
        public bool IsProcHeadContract { get; set; }
        public bool IsExecutor { get; set; }
        public bool IsSigner { get; set; }
        public bool IsContractWithoutDc { get; set; }
    }
}