using System;

namespace Ncels.Teme.Contracts.ViewModels
{
    public class EmpContractViewModel
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public DateTime CreateDate { get; set; }
        public string StageStatusCode { get; set; }
    }
}
