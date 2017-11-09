using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Database.Repository.Expertise;

namespace PW.Prism.Controllers.Expertise
{
    public class DrugStageHistoryController : APrismDrugDeclaraionController
    {
        // GET: DrugStageHistory
        public ActionResult HistoryStageView(Guid id)
        {
            var list = new ExpertiseStageRepository().GetExpExpertiseStagesByDeclarationId(id);
            return PartialView("~/Views/DrugDeclaration/DrugStageHistory/HistoryStageView.cshtml", list.Where(e=>e.StageId!=7).ToList());
        }

        
    }
}