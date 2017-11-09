using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace PW.Prism.Controllers
{
    public class RegistrationStepsController : Controller
    {
        private ncelsEntities db = UserHelper.GetCn();
        // GET: RegistrationSteps

        public ActionResult EditRegNomer()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }
        public ActionResult GetExpRegistrationsTypes(Guid? id)
        {
            var res = db.EXP_RegistrationTypes.Where(x => x.ParentId == id).Select(y => new
            {
                id = y.Id,
                Name = y.Name,
                hasChildren = y.EXP_RegistrationTypes1.Any()
            });
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateExpRegType(EXP_RegistrationTypes regType)
        {
            var newRegType = new EXP_RegistrationTypes();
            newRegType.Id = Guid.NewGuid();
            newRegType.Name = regType.Name;
            newRegType.ParentId = regType.ParentId;
            db.EXP_RegistrationTypes.Add(newRegType);
            db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateExpRegType(EXP_RegistrationTypes regType)
        {
            var dbRegType = db.EXP_RegistrationTypes.First(x => x.Id == regType.Id);
            dbRegType.Name = regType.Name;
            dbRegType.ParentId = regType.ParentId;
            db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAvailableParentsToExpRegType(Guid id)
        {
            var available = db.EXP_RegistrationTypes.Where(x => x.Id != id && x.ParentId != id).Select(y => new
            {
                id = y.Id,
                Name = y.Name
            });
            return Json(available, JsonRequestBehavior.AllowGet);
        }

        public bool DeleteRegistrationType(Guid id)
        {
            var removable = db.EXP_RegistrationTypes.FirstOrDefault(x => x.Id == id);
            if (removable != null)
            {
                db.EXP_RegistrationTypes.Remove(removable);
                db.SaveChanges();
            }
            return true;
        }

        public PartialViewResult ShowCreateRegistrationType()
        {
            var regType = new EXP_RegistrationTypes();
            ViewBag.IsEdit = false;
            return PartialView("RegTypesAddEditPartial", regType);
        }

        public PartialViewResult ShowUpdateRegistrationType(Guid id)
        {
            var regType = db.EXP_RegistrationTypes.First(x => x.Id == id);
            ViewBag.IsEdit = true;
            return PartialView("RegTypesAddEditPartial", regType);
        }

        public PartialViewResult ShowRegistrationSteps(Guid id)
        {
            ViewBag.RegistrationTypeId = id;
            var regSteps = db.EXP_RegistrationExpSteps.Where(x => x.RegistrationId == id).ToList();
            return PartialView("RegistrationStepsPartial", regSteps);
        }
        public bool DeleteRegistrationStep(Guid id)
        {
            var removable = db.EXP_RegistrationExpSteps.FirstOrDefault(x => x.Id == id);
            if (removable != null)
            {
                db.EXP_RegistrationExpSteps.Remove(removable);
                db.SaveChanges();
            }
            return true;
        }

        public PartialViewResult ShowCreateRegistrationStep()
        {
            var step = new EXP_RegistrationExpSteps();
            ViewBag.IsUpdate = false;
            return PartialView("CreateUpdateRegistrationStepPartial", step);
        }

        public PartialViewResult ShowUpdateRegistrationStep(Guid id)
        {
            var step = db.EXP_RegistrationExpSteps.First(x => x.Id == id);
            var prevSteps = db.EXP_RegistrationExpSteps.Where(x => x.RegistrationId == step.RegistrationId && x.Priority < step.Priority).OrderBy(x => x.Priority);
            if (prevSteps.Any())
            {
                ViewBag.PrevStepId = prevSteps.OrderByDescending(x => x.Priority).First().Id;
            }
            var nextSteps = db.EXP_RegistrationExpSteps.Where(x => x.RegistrationId == step.RegistrationId && x.Priority > step.Priority).OrderByDescending(x => x.Priority);
            if (nextSteps.Any())
            {
                ViewBag.NextStepName = nextSteps.OrderBy(x => x.Priority).First().Name;
            }
            ViewBag.IsUpdate = true;
            return PartialView("CreateUpdateRegistrationStepPartial", step);
        }

        public JsonResult CreateRegistrationStep(EXP_RegistrationExpSteps step, bool needParallelWithNext, Guid? prevStepId, Guid registrationTypeId, bool isUpdate)
        {
            int priority = 1;
            if (prevStepId != null)
            {
                var prevStep = db.EXP_RegistrationExpSteps.First(x => x.Id == prevStepId);
                var prevPriority = prevStep.Priority;
                priority = prevPriority + 1;
                var moreThenPrevPrioritySteps =
                    db.EXP_RegistrationExpSteps.Where(
                        x => x.RegistrationId == registrationTypeId && x.Priority > prevPriority);
                if (moreThenPrevPrioritySteps.Any() && !needParallelWithNext)
                {
                    var nextMinPriority = moreThenPrevPrioritySteps.Select(x => x.Priority).Min();
                    var diff = nextMinPriority - prevPriority;

                    if (diff == 1)
                    {
                        moreThenPrevPrioritySteps.Each(x => x.Priority++);
                    }
                }
                else
                {
                    if (!moreThenPrevPrioritySteps.Any() && needParallelWithNext)
                    {
                        priority = prevPriority;
                    }
                }
            }
            else
            {
                if (!needParallelWithNext)
                {
                    db.EXP_RegistrationExpSteps.Where(x => x.RegistrationId == registrationTypeId).Each(x => x.Priority++);
                }
            }

            if (!isUpdate)
            {
                step.Id = Guid.NewGuid();
                step.Priority = priority;
                step.RegistrationId = registrationTypeId;
                db.EXP_RegistrationExpSteps.Add(step);
            }
            else
            {
                var curStep = db.EXP_RegistrationExpSteps.First(x => x.Id == step.Id);
                curStep.Priority = priority;
                curStep.Duration = step.Duration;
            }

            db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAvailablePrevRegStepsToCreate(Guid? id, Guid regId)
        {
            dynamic steps = null;
            if (id != null)
            {
                steps = db.EXP_RegistrationExpSteps.Where(x => x.Id != id && x.RegistrationId == regId).OrderBy(x => x.Priority).Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
            }
            else
            {
                steps = db.EXP_RegistrationExpSteps.Where(x => x.RegistrationId == regId).OrderBy(x => x.Priority).Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
            }
            return Json(steps, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetNextExpRegStepName(Guid? id, Guid regId)
        {
            EXP_RegistrationExpSteps step = null;
            if (id != null)
            {
                var prevStep = db.EXP_RegistrationExpSteps.FirstOrDefault(x => x.Id == id);
                if (prevStep != null)
                {
                    step =
                        db.EXP_RegistrationExpSteps.Where(x => x.RegistrationId == regId && x.Id != id && x.Priority > prevStep.Priority)
                            .OrderBy(x => x.Priority)
                            .FirstOrDefault();
                }
            }
            else
            {
                step = db.EXP_RegistrationExpSteps.Where(x => x.RegistrationId == regId).OrderBy(x => x.Priority).FirstOrDefault();
            }
            var stepName = step != null ? step.Name : "";
            return Json(new { NextStepName = stepName });
        }
    }
}