using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PW.Ncels.Database.DataModel;

namespace PW.Prism.ViewModels.Commissions
{
    public class CommissionQuestionsModel
    {
        public Guid Guid { get; set; }
        public int CommissionId { get; set; }
        //public int CommissionKind { get; set; }
        //public int CommissionType { get; set; }
    }
}