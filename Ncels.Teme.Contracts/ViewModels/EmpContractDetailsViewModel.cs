using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Ncels.Teme.Contracts.ViewModels
{
    public class EmpContractDetailsViewModel
    {
        public Guid Id { get; set; }

        public EmpContractDeclarantViewModel Manufacturer { get; set; }
        public EmpContractDeclarantViewModel Declarant { get; set; }
        public EmpContractDeclarantViewModel Payer { get; set; }

        public IEnumerable<SelectListItem> Payers { get; set; }
        public string ChoosPayer { get; set; }
        public string MedicalDeviceName { get; set; }
        public IList<EmpContractWorkCostViewModel> WorkCosts { get; set; }

        //public Guid? ParentId { get; set; }

        //public bool DeclarantIsResident { get; set; }
        //public string DeclarantBin { get; set; }
        //public string DeclarantNameRu { get; set; }
        //public string DeclaranrNameKz { get; set; }
        //public string DeclarantNameEn { get; set; }

        //public string AddressLegalRu { get; set; }
        //public string AddressLegalKz { get; set; }
        //public string AddressFact { get; set; }
        //public string Phone { get; set; }
        //public string Email { get; set; }
        //public string BossLastName { get; set; }
        //public string BossFirstName { get; set; }
        //public string BossMiddleName { get; set; }
        //public string BossPositionRu { get; set; }
        //public string BossPositionKz { get; set; }
        //public bool BossDocIsUnlimited { get; set; }
        //public string BossDocumentNumber { get; set; }
        //public DateTime? BossDocumentCreateDate { get; set; }
        //public DateTime? BossDocumentEndDate { get; set; }
        //public string BankIik { get; set; }
        //public string BankBik { get; set; }
        //public string BankNameRu { get; set; }
        //public string BankNameKz { get; set; }



        //public IEnumerable<SelectListItem> ContractTypes { get; set; }
        //public IEnumerable<SelectListItem> Countries { get; set; }
        //public IEnumerable<SelectListItem> NonResidentsNames { get; set; }
        //public IEnumerable<SelectListItem> ExpertOrganizations { get; set; }
        //public IEnumerable<SelectListItem> Signers { get; set; }
        //public IEnumerable<SelectListItem> OrganizationForms { get; set; }
        //public IEnumerable<SelectListItem> DocumentTypes { get; set; }
        //public IEnumerable<SelectListItem> BoolValues { get; set; }
        //public IEnumerable<SelectListItem> Currencies { get; set; }
    }
}
