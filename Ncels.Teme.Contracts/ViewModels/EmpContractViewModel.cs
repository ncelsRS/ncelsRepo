using System;

namespace Ncels.Teme.Contracts.ViewModels
{
    public class EmpContractViewModel
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Declarant { get; set; }
        public string ContractType { get; set; }
        public string ValidatinGroup { get; set; }
        public string Def { get; set; }
        public string Lawyer { get; set; }
        public string StageStatusCode { get; set; }
        public Guid ContractStageId { get; set; }
        public string ContractStatusId { get; set; }
    }
}
