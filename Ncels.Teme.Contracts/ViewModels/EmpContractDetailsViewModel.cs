using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Ncels.Teme.Contracts.ViewModels
{
    public class EmpContractDetailsViewModel
    {
        public Guid Id { get; set; }
        public string ContractScope { get; set; }
        public string ContractScopeName { get; set; }
        public string HolderType { get; set; }
        public string ContractType { get; set; }
        public string StatemantNumber { get; set; }

        public EmpContractDeclarantViewModel Manufacturer { get; set; }
        public EmpContractDeclarantViewModel Declarant { get; set; }
        public EmpContractDeclarantViewModel Payer { get; set; }

        public IEnumerable<SelectListItem> Payers { get; set; }
        public string ChoosPayer { get; set; }
        public string MedicalDeviceName { get; set; }
        public string MedicalDeviceNameKz { get; set; }
        public IList<EmpContractWorkCostViewModel> WorkCosts { get; set; }

        public IEnumerable<EmpContractFileAttachmentViewModel> Attachments { get; set; }

        public Guid StageId { get; set; }
        public bool CanApprove { get; set; }

    }
}
