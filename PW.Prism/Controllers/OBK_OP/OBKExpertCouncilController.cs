using PW.Ncels.Database.DataModel;
using PW.Prism.ViewModels.OBK.ExpertCouncil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PW.Prism.Controllers.OBK_OP
{
    public class OBKExpertCouncilController : Controller
    {
        private NcelsEntities repo = new NcelsEntities();

        // GET: OBKExpertCouncil
        public ActionResult Index()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult GetLeftPane()
        {
            var model = repo.OBK_ExpertCouncil.Where(c => c.Date.Year == DateTime.Now.Year).ToList();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult SaveExpertCouncil(ExpertCouncilMV council)
        {
            var entity = new OBK_ExpertCouncil
            {
                Name = council.Name,
                Date = council.Date
            };
            repo.OBK_ExpertCouncil.Add(entity);
            repo.SaveChanges();
            return Json(new { isSuccess = true });
        }
    }
}