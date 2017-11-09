using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ncels.Helpers;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Helpers;
using PW.Ncels.Models.Visit;

namespace PW.Ncels.Controllers
{
    [LogInfo]
    public class VisitController : ACommonController
    {
        private ncelsEntities db = UserHelper.GetCn();

        public ActionResult Index()
        { 
            var visitModel = new VisitListModels();
            visitModel.Type = VisitListTypes.All;
            visitModel.DirectoryText = "Все";
            return View("VisitList", visitModel);
        }

        public ActionResult ActualVisitList()
        {
            var visitModel = new VisitListModels();
            visitModel.Type = VisitListTypes.Actual;
            visitModel.DirectoryText = "Предстоящие";
            return View("VisitList", visitModel);
        }

        public ActionResult ArchiveVisitList()
        {
            var visitModel = new VisitListModels();
            visitModel.Type = VisitListTypes.Archive;
            visitModel.DirectoryText = "Прошедние";
            return View("VisitList", visitModel);
        }


        public ActionResult CreateVisit(int? id) {
            return View("CreateVisit", id);
        }

        //public ActionResult Detail(Guid? id) {
        //    return View(id ?? Guid.NewGuid());
        //}

        [HttpPost]
        public async Task<JsonResult> GetList(ModelRequest request,int type) {
            return Json(await VisitServices.Instance.GetOutgoing(db, request, type), JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        //public async Task<JsonResult> GetCurrentList(ModelRequest request,Guid id) {
        //    return Json(await CorrespondenceServices.Instance.GetCurrentList(db, request, id), JsonRequestBehavior.AllowGet);
        //}

        public ActionResult Create(Visit model) {
            var dbVisit = db.Visits.SingleOrDefault(m => m.Id == model.Id);
            if (dbVisit != null)
            {
                return Json(new { success = false, message = "Обновление визита не реализовано" }, JsonRequestBehavior.AllowGet);
            }
            var dbType = db.VisitTypes.Single(x => x.Id == model.VisitTypeId);
            var employeeId = UserHelper.GetCurrentEmployee().Id;
            var hasVisit = db.Visits.Any(x => x.Date == model.Date && x.VisitorId == employeeId && x.VisitTypeId == model.VisitTypeId);
            if (hasVisit)
            {
                return Json(new { success = false, message = "У вас уже есть запись на текущий день по данному типу" }, JsonRequestBehavior.AllowGet);
            }
            var intervals = GetIntervals(dbType.Id, model.Date);
            var intervalByTime = intervals.GroupBy(x => x.From).SingleOrDefault(x => x.Key == model.Date.AddMinutes(model.TimeBegin));
            if(intervalByTime == null)
            {
                return Json(new { success = false, message = "Рабочее время не найдено" }, JsonRequestBehavior.AllowGet);
            }
            var freeIntervals = intervalByTime.Where(x => !x.IsExistsVisit).ToList();
            if(freeIntervals.Count == 0)
            {
                return Json(new { success = false, message = "Время уже занято, выберите другое" }, JsonRequestBehavior.AllowGet);
            }
            var workerId = freeIntervals.Random().EmployeeId;

            dbVisit = new Visit();
            db.Visits.Add(dbVisit);
            dbVisit.Comment = model.Comment;
            dbVisit.VisitTypeId = model.VisitTypeId;
            dbVisit.EmployeeId = workerId;
            dbVisit.VisitorId = employeeId;
            dbVisit.Date = model.Date;
            dbVisit.TimeBegin = model.TimeBegin;
            dbVisit.Duration = dbType.Time;
            dbVisit.VisitStatusId = (int)VisitStatuses.NeedConfirm;
            db.SaveChanges();
            return Json(new {success = true, visitId = dbVisit.Id }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Load(int? id= null)
        {
            var visitTypes = db.VisitTypes.Select(x => new { Id = x.Id, Name = x.Name + "(" + x.Group + ")", Duration = x.Time });
            if (id == null)
            {
                return Json(new { Object = new { VisitId = -1 }, Types = visitTypes }, JsonRequestBehavior.AllowGet);
            }
            var dbVisit = db.Visits.Single(m => m.Id == id);
            var data = new
            {
                VisitId = dbVisit.Id,
                VisitComment = dbVisit.Comment,
                VisitTypeId = dbVisit.VisitTypeId,
            };
            return Json(new { Object = data, Types = visitTypes }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDatesForCreateVisits(int type)
        {
            var typeEmployeeIds = db.VisitEmployeeTypes.Where(x => x.VisitTypeId == type && x.IsEnable).Select(x=>x.EmployeeId);
            var date = DateTime.Now.Date;
            var dates = db.VisitEmployeeWorkingTimes.Where(x=>typeEmployeeIds.Contains(x.EmployeeId) && x.Date >= date).Select(x=>x.Date).Distinct().ToList();
            return Json(dates, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTimesForCreateVisits(int type, DateTime date)
        {
            var model = new TimeListModels();
            model.Intervals = GetIntervals(type, date);
            return PartialView("TimeList", model);
        }

        private List<VisitInterval> GetIntervals(int type, DateTime date)
        {
            var dbType = db.VisitTypes.Single(x => x.Id == type);
            var duration = dbType.Time;
            var typeEmployeeIds = db.VisitEmployeeTypes.Where(x => x.VisitTypeId == type && x.IsEnable).Select(x => x.EmployeeId);
            var dbWorkingTimes = db.VisitEmployeeWorkingTimes.Where(x => typeEmployeeIds.Contains(x.EmployeeId) && x.Date == date).ToList();
            var workingEmployeeIds = dbWorkingTimes.Select(x => x.EmployeeId).Distinct().ToList();
            var dbVisits = db.Visits.Where(x => x.Date == date && workingEmployeeIds.Contains(x.EmployeeId)).ToList();
            var intervals = new List<VisitInterval>();
            foreach (var dbEmployeeTime in dbWorkingTimes.GroupBy(x=>x.EmployeeId))
            {
                var employeeId = dbEmployeeTime.Key;
                var dbEmployeeVisits = dbVisits.Where(x => x.EmployeeId == employeeId).ToList();
                foreach (var dbTime in dbEmployeeTime)
                {
                    var startDate = date.Date.AddMinutes(dbTime.TimeBegin);
                    var endDate = date.Date.AddMinutes(dbTime.TimeEnd);
                    for (int i = 0; i < 1217; i++) //sdelal bi while, no vdrug chego i zaciklitsya :)
                    {
                        var intervalEnd = startDate.AddMinutes(duration);
                        if (intervalEnd > endDate)
                        {
                            break;
                        }
                        var interval = new VisitInterval();
                        interval.From = startDate;
                        interval.To = intervalEnd;
                        interval.EmployeeId = dbTime.EmployeeId;
                        intervals.Add(interval);
                        startDate = intervalEnd;
                    }
                }
                foreach (var dbEmployeeVisit in dbEmployeeVisits)
                {
                    var from = date.Date.AddMinutes(dbEmployeeVisit.TimeBegin);
                    var to = from.AddMinutes(dbEmployeeVisit.Duration);
                    var crossedIntervals = intervals.Where(x => (x.From <= from && x.To > from) || (x.To <= to && x.To > to));
                    foreach (var crossedInterval in crossedIntervals)
                    {
                        crossedInterval.IsExistsVisit = true;
                    }
                }
            }
            return intervals;
        }

        public ActionResult Delete(int id) {
            var dbVisit = db.Visits.SingleOrDefault(x=>x.Id == id);
            if(dbVisit == null)
            {
                return Json(new { success = false, message = "Запись на приём не найдена" }, JsonRequestBehavior.AllowGet);
            }
            var employeeId = UserHelper.GetCurrentEmployee().Id;
            if(dbVisit.VisitorId != employeeId)
            {
                return Json(new { success = false, message = "Запись вам не пренадлежит" }, JsonRequestBehavior.AllowGet);
            }
            db.Visits.Remove(dbVisit);
            db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult VisitRating(int id)
        {
            var model = new VisitRatingModel();
            model.Comment = "";
            model.VisitId = id;
            return PartialView("RatingView", model);
        }

        public ActionResult SaveRatingValue(int visitId, int value, string comment)
        {
            var dbVisit = db.Visits.SingleOrDefault(x => x.Id == visitId);
            if (dbVisit == null)
            {
                return Json(new { success = false, message = "Запись на приём не найдена" }, JsonRequestBehavior.AllowGet);
            }
            var employeeId = UserHelper.GetCurrentEmployee().Id;
            if (dbVisit.VisitorId != employeeId)
            {
                return Json(new { success = false, message = "Запись вам не пренадлежит" }, JsonRequestBehavior.AllowGet);
            }
            dbVisit.RatingValue = value;
            dbVisit.RatingComment = comment;
            db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}