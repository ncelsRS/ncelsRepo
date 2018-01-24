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
        private NcelsEntities db = UserHelper.GetCn();
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
            var reference = db.EXP_DIC_Type.First(x => x.Id == regType.RefId);
            newRegType.Name = reference.NameRu;
            newRegType.RefId = regType.RefId;
            newRegType.ParentId = regType.ParentId;
            db.EXP_RegistrationTypes.Add(newRegType);
            db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateExpRegType(EXP_RegistrationTypes regType)
        {
            var dbRegType = db.EXP_RegistrationTypes.First(x => x.Id == regType.Id);
            var reference = db.EXP_DIC_Type.First(x => x.Id == regType.RefId);
            dbRegType.Name = reference.NameRu;
            dbRegType.RefId = regType.RefId;
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

        public ActionResult GetAviableRegExpSteps(int? refId)
        {
            var exists = db.EXP_RegistrationTypes.Where(x => x.RefId != null && refId!=x.RefId).Select(x => x.RefId).Distinct();

            var available = db.EXP_DIC_Type.Where(x => !exists.Contains(x.Id)).Select(y => new
            {
                id = y.Id,
                Name = y.NameRu
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
            if (db.EXP_RegistrationExpSteps.Count(e =>
                    e.RegistrationId == registrationTypeId && e.RefId == step.RefId) > 1)
            {
                return Json(new { success = false, message = "Для данного типа регистрации уже задан этап." }, JsonRequestBehavior.AllowGet);
            }            
            if (!isUpdate)
            {
                step.Id = Guid.NewGuid();
                step.Priority = priority;
                step.RegistrationId = registrationTypeId;
                step.Name = db.EXP_DIC_Stage.FirstOrDefault(e => e.Id == step.RefId).NameRu;
                step.Executors.AddRange(db.Employees.Where(e=>step.ExecutorsIds.Contains(e.Id)));
                db.EXP_RegistrationExpSteps.Add(step);
            }
            else
            {
                var curStep = db.EXP_RegistrationExpSteps.First(x => x.Id == step.Id);
                curStep.Priority = priority;
                curStep.Duration = step.Duration;
                curStep.SupervisingEmployeeId = step.SupervisingEmployeeId;
                curStep.Executors.Clear();
                db.SaveChanges();
                curStep.Executors.AddRange(db.Employees.Where(e => step.ExecutorsIds.Contains(e.Id)));
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
                    x.Id,
                    x.Name
                }).ToList();
            }
            else
            {
                steps = db.EXP_RegistrationExpSteps.Where(x => x.RegistrationId == regId).OrderBy(x => x.Priority).Select(x => new
                {
                    x.Id,
                    x.Name
                }).ToList();
            }
            return Json(steps, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetExpertiseStages()
        {
            return Json(db.EXP_DIC_Stage.Where(e => !e.IsDeleted).Select(e => new {e.Id, Name = e.NameRu}), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetResponsibleSubdivisions()
        {
            return Json(db.Units.Where(e => e.Parent.Code=="00" || e.Parent.Code== "ES-DEPARTMENT").Select(e => new { e.Id, e.Name }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetResponsibleEmployees(Guid? responsibleSubdivId=null)
        {
            if (responsibleSubdivId != null)
            {
                return Json(db.Employees.Where(e => e.Position.Parent.Id==responsibleSubdivId && e.Position.Type == 2 && e.Position.PositionState == 1)
                    .Select(e => new { e.Id, Name=e.FullName }), JsonRequestBehavior.AllowGet);
            }
            return Json(new Guid[] { }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSupervisingEmployees()
        {
            var data = db.Units.Where(
                    x => x.Type == 2 && x.PositionState == 1 && x.Employee != null)
                .Select(o => o.Employee)
                .OrderBy(o => o.DisplayName);
            return Json(data.Select(o => new { o.Id, Name = o.DisplayName }), JsonRequestBehavior.AllowGet);
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