using System;
using System.Collections.Generic;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace PW.Prism.ViewModels.Commissions
{
    public class CommissionListItemModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string KindShortName { get; set; }
        public DateTime Date { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public bool IsComplete { get; set; }
        public string Comment { get; set; }
    }
}