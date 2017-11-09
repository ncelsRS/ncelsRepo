using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Models
{
    public class ContractModel
    {
        public Guid Id { get; set; }
        public OrganizationsView Manufaturer { get; set; }
        public OrganizationsView Holder { get; set; }
        public OrganizationsView Payer { get; set; }
        public OrganizationsView Applicant { get; set; }
        public OrganizationsView PayerTranslation { get; set; }
        public ContractsView ApplicantContract { get; set; }
        public ContractsView HolderContract { get; set; }
        public bool IsEdit { get; set; }
    }
}