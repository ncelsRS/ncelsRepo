using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ncels.Teme.Contracts.ViewModels
{
    public class EmpContractDeclarantViewModel
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public bool IsResident { get; set; }

        public IEnumerable<SelectListItem> OrganizationForms { get; set; }

        public string NameKz { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

        public string Bin { get; set; }
        public IEnumerable<SelectListItem> NonResidentsNames { get; set; }

        public string BossLastName { get; set; }
        public string BossFirstName { get; set; }
        public string BossMiddleName { get; set; }
        public string BossPositionRu { get; set; }
        public string BossPositionKz { get; set; }

        public string AddressLegal { get; set; }
        public string AddressFact { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string BankName { get; set; }
        public string BankIik { get; set; }
        public IEnumerable<SelectListItem> Currencies { get; set; }
        public string BankBik { get; set; }
        public string Iin { get; set; }
        public string BankAccount { get; set; }

        public string BossDocType { get; set; }
        public string BossDocTypeCode { get; set; }
        public string IsHasBossDocNumber { get; set; }
        public string BossDocNumber { get; set; }
        public string BossDocUnlimited { get; set; }
        public string DocumentType { get; set; }
        public string BossDosCreateDate { get; set; }
        public string BossDocEndDate { get; set; }

        public bool CanApprove { get; set; }
    }
}
