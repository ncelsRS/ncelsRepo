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
        private ncelsEntities repo = new ncelsEntities();

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

        public ActionResult ListFutureCouncils()
        {
            try
            {
                var res = repo.OBK_ExpertCouncil
                    .Where(x => x.Date > DateTime.Now.AddDays(1))
                    .Select(x => new
                    {
                        Value = x.Id,
                        Text = x.Name
                    })
                    .AsEnumerable();
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (ArgumentException ex)
            {
                Response.StatusCode = 400;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}