using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models.EMP
{
    public class Declarants
    {
        public bool IsFind { get; set; }
        public Guid Id { get; set; }
        public bool IsResident { get; set; }
        public Guid? OrganizationFormId { get; set; }
        public string Bin { get; set; }
        public string NameKz { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }
        public Guid? CountryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Number { get; set; }
        public int Type { get; set; }
        public Guid? ExpertOrganization { get; set; }
        public Guid? Signer { get; set; }
        public int Status { get; set; }
        public Contacts Contact { get; set; }
    }
}
