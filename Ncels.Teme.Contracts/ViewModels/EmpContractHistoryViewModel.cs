using System;

namespace Ncels.Teme.Contracts.ViewModels
{
    public class EmpContractHistoryViewModel
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public Guid EmployeeId { get; set; }
        public string UnitName { get; set; }
        public Guid StatusId { get; set; }
        public string RefuseReason { get; set; }
        public Guid ContractId { get; set; }
        public string EmployeeFullName { get; set; }
        public string EmployeeShortName { get; set; }
        public string StatusCode { get; set; }
        public string StatusNameRu { get; set; }
        public string StatusNameKz { get; set; }
    }
}
