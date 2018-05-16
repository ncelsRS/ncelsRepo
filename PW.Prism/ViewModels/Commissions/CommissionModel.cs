using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PW.Ncels.Database.DataModel;

namespace PW.Prism.ViewModels.Commissions
{
    public class CommissionModel
    {
        public int? Id { get; set; }
        public string Number { get; set; }
        public DateTime? Date { get; set; }
        public bool IsComplete { get; set; }
        public string Comment { get; set; }
        public int? Kind { get; set; }
        public int? Type { get; set; }

        public List<SelectListItem> TypeList { get; set; }
        public List<SelectListItem> KindList { get; set; }
        public List<CommissionUnitType> CommissionUnitTypes { get; set; }
        public List<CommissionEmployeeDepartment> Units { get; set; }
        public List<CommissionQuestion> Questions { get; set; }
    }
}