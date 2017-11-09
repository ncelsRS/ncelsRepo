using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PW.Prism.ViewModels.Commissions
{
    public class ConclusionCommissionDrugDeclarationModel
    {
        public int Id { get; set; }
        public int CommissionId { get; set; }
        //public int DeclarationId { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public List<SelectListItem> ConclusionList { get; set; }
        public int Type { get; set; }
        public string Comment { get; set; }
    }
}