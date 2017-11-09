using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Enums;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;

namespace PW.Ncels.Controllers
{
    [Authorize()]
    public class NotificationController : ACommonController
    {

        private ncelsEntities db = UserHelper.GetCn();

        // GET: Notification
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetList(ModelRequest request)
        {
            return Json(await NotificationServices.Instance.GetCurrentList(db, request), JsonRequestBehavior.AllowGet);
        }


        public async Task<JsonResult> GetNewNotification()
        {
            return Json(await NotificationServices.Instance.GetNewNotification(db), JsonRequestBehavior.AllowGet);

        }

        public JsonResult SetViewed(List<int> list)
        {
            return Json(NotificationServices.Instance.SetViewed(db, list), JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public async Task<JsonResult> GetListAllNotification()
        {
            return Json(await NotificationServices.Instance.GetListAllNotification(db), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowDetails(long id)
        {
            var model = db.Notifications.FirstOrDefault(e => e.Id == id);
            if (model != null)
            {
                model.IsRead = true;
                db.SaveChanges();
                switch (model.TableName)
                {
                    case "Letter":
                        {
                            var project = db.PriceProjects.FirstOrDefault(e => e.Id == new Guid(model.ObjectId));
                            if (project != null)
                            {
                                switch (project.Type)
                                {
                                    case (int)PriceProjectType.PriceLs:
                                        {
                                            return RedirectToAction("PriceLsDetails", "Project", new { id = project.Id });
                                        }
                                    case (int)PriceProjectType.PriceImn:
                                        {
                                            return RedirectToAction("PriceImnDetails", "Project", new { id = project.Id });
                                        }
                                    case (int)PriceProjectType.RePriceLs:
                                        {
                                            return RedirectToAction("RePriceLsDetails", "Project", new { id = project.Id });
                                        }
                                    case (int)PriceProjectType.RePriceImn:
                                        {
                                            return RedirectToAction("RePriceImnDetails", "Project", new { id = project.Id });
                                        }
                                }
                            }
                            break;
                        }
                    case "PriceProjects":
                        {
                            var project = db.PriceProjects.FirstOrDefault(e => e.Id == new Guid(model.ObjectId));
                            if (project != null)
                            {
                                switch (project.Type)
                                {
                                    case (int) PriceProjectType.PriceLs:
                                    {
                                        return RedirectToAction("PriceLsDetails", "Project", new {id = project.Id});
                                    }
                                    case (int) PriceProjectType.PriceImn:
                                    {
                                        return RedirectToAction("PriceImnDetails", "Project", new {id = project.Id});
                                    }
                                    case (int) PriceProjectType.RePriceLs:
                                    {
                                        return RedirectToAction("RePriceLsDetails", "Project", new {id = project.Id});
                                    }
                                    case (int) PriceProjectType.RePriceImn:
                                    {
                                        return RedirectToAction("RePriceImnDetails", "Project", new {id = project.Id});
                                    }
                                }
                            }
                            break;
                        }
                    case "ObkDeclaration":
                    {
                        var safetyAssessment =
                            db.OBK_AssessmentDeclaration.FirstOrDefault(e => e.Id == new Guid(model.ObjectId));
                        if (safetyAssessment != null)
                        {
                            return RedirectToAction("Edit", "SafetyAssessment", new { id = safetyAssessment.Id });
                        }
                        break;
                    }
                    case "ObkContract":
                    {
                        var obkContract = db.OBK_Contract.FirstOrDefault(e => e.Id == new Guid(model.ObjectId));
                        if (obkContract != null)
                        {
                            return RedirectToAction("Contract", "OBKContract", new { obkContract.Id });
                        }
                        break;
                    }

                }

            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public virtual ActionResult SetReadNotification(long id)
        {
            var price = db.Notifications.FirstOrDefault(e => e.Id == id);
            if (price != null)
            {
                price.IsRead = true;

            }
            db.SaveChanges();
            return Json(new { Success = true });
        }

    }
}