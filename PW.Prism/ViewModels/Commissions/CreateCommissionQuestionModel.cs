using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PW.Ncels.Database.DataModel;

namespace PW.Prism.ViewModels.Commissions
{
    public class CreateCommissionQuestionModel
    {
        public List<SelectListItem> TypeList { get; set; }
        public int NextNumber { get; set; }
        public int CommissionId { get; set; }
        public int Type { get; set; }
        public string Comment { get; set; }
    }
}