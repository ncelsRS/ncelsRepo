using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Aspose.Words;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Ncels.Core.ActivityManager;
using Ncels.Helpers;
using Newtonsoft.Json;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Repository.Contract;
using PW.Prism.ViewModels;
using PW.Prism.ViewModels.Visits;

namespace PW.Prism.Controllers
{
    [Authorize]
    public class VisitController : Controller
    {
        private ncelsEntities db = UserHelper.GetCn();

        public ActionResult VisitTypeList()
        {
            Guid guid = Guid.NewGuid();
            return PartialView("VisitTypeList", guid);
        }

        public ActionResult VisitTypeRead([DataSourceRequest] DataSourceRequest request)
        {
            var data = db.VisitTypes.Select(o => new VisitTypeModel
                {
                    Id = o.Id,
                    Name = o.Name,
                    CategoryName = o.Group,
                    Time = o.Time,
                });
            return Json(data.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult VisitTypeCreate([DataSourceRequest] DataSourceRequest request, VisitTypeModel dictionary)
        {
            if (dictionary != null)
            {
                if (dictionary.Time <= 0 || dictionary.Time > 1440)
                {
                    ModelState.AddModelError("Message", Convert.ToString("Время должно быть не более 1440 минут и больше 0 минут"));
                    return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
                }
                var dbVisitType = new VisitType();
                dbVisitType.Group = dictionary.CategoryName;
                dbVisitType.Name = dictionary.Name;
                dbVisitType.Time = dictionary.Time;
                db.VisitTypes.Add(dbVisitType);
                db.SaveChanges();
                dictionary.Id = dbVisitType.Id;
            }
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult VisitTypeUpdate([DataSourceRequest] DataSourceRequest request, VisitTypeModel dictionary)
        {
            if (dictionary != null && ModelState.IsValid)
            {
                if (dictionary.Time <= 0 || dictionary.Time > 1440)
                {
                    ModelState.AddModelError("Message", Convert.ToString("Время должно быть не более 1440 минут и больше 0 минут"));
                    return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
                }
                var dbVisitType = db.VisitTypes.Single(x => x.Id == dictionary.Id);
                dbVisitType.Group = dictionary.CategoryName;
                dbVisitType.Name = dictionary.Name;
                dbVisitType.Time = dictionary.Time;
                db.SaveChanges();
            }
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult VisitTypeDestroy([DataSourceRequest] DataSourceRequest request, VisitTypeModel dictionary)
        {
            if (dictionary != null)
            {
                var dbVisitType = db.VisitTypes.Single(x => x.Id == dictionary.Id);
                db.VisitTypes.Remove(dbVisitType);
                db.SaveChanges();
            }
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult VisitWokringTime()
        {
            var model = new VisitWokringTimeModel();
            model.VisitTypes = new List<VisitTypeSettings>();

            Guid empId = UserHelper.GetCurrentEmployee().Id;
            var dbTypes = db.VisitTypes.ToList();
            var dbEmployeeTypes = db.VisitEmployeeTypes.Where(x => x.EmployeeId == empId).ToList();
            foreach (var dbType in dbTypes)
            {
                var dbEmplType = dbEmployeeTypes.SingleOrDefault(x => x.VisitTypeId == dbType.Id);
                var t = new VisitTypeSettings();
                t.Type = dbType;
                t.IsEnable = dbEmplType != null && dbEmplType.IsEnable;
                model.VisitTypes.Add(t);
            }
            return PartialView("VisitWokringTime", model);
        }

        public ActionResult UpdateVisitTypeEnable(int typeId, bool isEnable)
        {
            Guid empId = UserHelper.GetCurrentEmployee().Id;
            var dbEmplType = db.VisitEmployeeTypes.SingleOrDefault(x => x.EmployeeId == empId && x.VisitTypeId == typeId);
            if(dbEmplType == null)
            {
                dbEmplType = new VisitEmployeeType();
                dbEmplType.EmployeeId = empId;
                dbEmplType.VisitTypeId = typeId;
                db.VisitEmployeeTypes.Add(dbEmplType);
            }
            dbEmplType.IsEnable = isEnable;
            db.SaveChanges();
            return Json(new { success = true });
        }

        public ActionResult GetWorkingDays()
        {
            Guid empId = UserHelper.GetCurrentEmployee().Id;
            var dbWorkingTimes = db.VisitEmployeeWorkingTimes.Where(x => x.EmployeeId == empId).ToList();
            var days = dbWorkingTimes.Select(x => x.Date).Distinct()//.Select(x=>x.ToString("yyyy-MM-dd"))
                .ToList();
            return Json(new { success = true, dates = days });
        }
        public ActionResult GetWorkingDayHours(DateTime date)
        {
            Guid empId = UserHelper.GetCurrentEmployee().Id;
            var dbWorkingTimes = db.VisitEmployeeWorkingTimes.Where(x => x.EmployeeId == empId && x.Date == date).ToList();
            var visitWokringDayHoursModel = new VisitWokringDayHoursModel();
            visitWokringDayHoursModel.Intervals = new List<WorkingDayInterval>();
            foreach (var visitWorkingTime in dbWorkingTimes)
            {
                var interval = new WorkingDayInterval();
                interval.From = visitWorkingTime.TimeBegin;
                interval.To = visitWorkingTime.TimeEnd;
                interval.Id = visitWorkingTime.Id;
                visitWokringDayHoursModel.Intervals.Add(interval);
            }
            visitWokringDayHoursModel.Date = date;
            return PartialView("VisitWokringDayHours", visitWokringDayHoursModel);
        }

        public ActionResult CreateWorkingDayHours(DateTime date, string from, string to)
        {
            Guid empId = UserHelper.GetCurrentEmployee().Id;
            var dbTime = new VisitEmployeeWorkingTime();
            dbTime.EmployeeId = empId;
            dbTime.Date = date;
            dbTime.TimeBegin = (int)TimeSpan.Parse(from).TotalMinutes;
            dbTime.TimeEnd = (int)TimeSpan.Parse(to).TotalMinutes;
            db.VisitEmployeeWorkingTimes.Add(dbTime);
            db.SaveChanges();
            var model = new WorkingDayInterval();
            model.Id = dbTime.Id;
            model.From = dbTime.TimeBegin;
            model.To = dbTime.TimeEnd;
            return PartialView("VisitWokringDayHoursRow", model);
        }

        public ActionResult UpdateWorkingDayHours(int id, string from, string to)
        {
            Guid empId = UserHelper.GetCurrentEmployee().Id;
            var dbTime = db.VisitEmployeeWorkingTimes.SingleOrDefault(x => x.Id == id);
            if(dbTime == null)
            {
                return Json(new { success = false, message = "Время не найдено" });
            }
            if(empId != dbTime.EmployeeId)
            {
                return Json(new { success = false, message = "Время вам не пренадлежит" });
            }
            dbTime.TimeBegin = (int)TimeSpan.Parse(from).TotalMinutes;
            dbTime.TimeEnd = (int)TimeSpan.Parse(to).TotalMinutes;
            db.SaveChanges();
            return Json(new { success = true });
        }

        public ActionResult DeleteWorkingDayHours(int id)
        {
            Guid empId = UserHelper.GetCurrentEmployee().Id;
            var dbTime = db.VisitEmployeeWorkingTimes.SingleOrDefault(x => x.Id == id);
            if (dbTime == null)
            {
                return Json(new { success = false, message = "Время не найдено" });
            }
            if (empId != dbTime.EmployeeId)
            {
                return Json(new { success = false, message = "Время вам не пренадлежит" });
            }
            db.VisitEmployeeWorkingTimes.Remove(dbTime);
            db.SaveChanges();
            return Json(new { success = true });
        }

        public ActionResult VisitListRead([DataSourceRequest] DataSourceRequest request)
        {
            Guid empId = UserHelper.GetCurrentEmployee().Id;
            var data = db.VisitsViews.Where(x=>x.EmployeeId== empId).OrderBy(x=>x.VisitDate).ThenBy(x=>x.VisitTimeBegin)
                .Select(x => new VisitModel
                    {
                        VisitId = x.VisitId,
                        VisitStatusId = x.VisitStatusId,
                        VisitStatusName = x.VisitStatusName,
                        VisitTypeName = x.VisitTypeName,
                        VisitTypeGroup = x.VisitTypeGroup,
                        VisitComment = x.VisitComment,
                        VisitorFirstName = x.VisitorFirstName,
                        VisitorMiddleName = x.VisitorMiddleName,
                        VisitorLastName = x.VisitorLastName,
                        VisitDate = x.VisitDate,
                        VisitTimeBegin = x.VisitTimeBegin,
                        VisitDuration = x.VisitDuration,
                        VisitRatingValue = x.VisitRatingValue,
                        VisitRatingComment = x.VisitRatingComment,
                    //VisitBegin = x.VisitDate.AddMinutes(x.VisitTimeBegin).ToString("HH:mm")+" ("+x.VisitDuration+")",
                });
            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult ConfirmVisit(int visitId)
        {
            Guid empId = UserHelper.GetCurrentEmployee().Id;
            var dbVisit = db.Visits.SingleOrDefault(x => x.Id == visitId);
            if (dbVisit == null)
            {
                return Json(new { success = false, message = "Приём не найдено" });
            }
            if (empId != dbVisit.EmployeeId)
            {
                return Json(new { success = false, message = "Приём вам не пренадлежит" });
            }
            var sendMail = db.Employees.Single(x => x.Id == dbVisit.VisitorId).Email;
            var msg = "Ваш приём " + dbVisit.Date.ToShortDateString() + " в " + dbVisit.Date.AddMinutes(dbVisit.TimeBegin).ToString("HH:mm") + " подтверждён";
            MailSender mailSender = new MailSender();
            mailSender.SendMail(sendMail, "Заявка на приём подтверждена", msg, new MailAddress(sendMail));
            dbVisit.VisitStatusId = (int)VisitStatuses.Planned;
            db.SaveChanges();
            return Json(new { success = true });
        }

        public ActionResult CancelVisit(int visitId, string reason)
        {
            Guid empId = UserHelper.GetCurrentEmployee().Id;
            var dbVisit = db.Visits.SingleOrDefault(x => x.Id == visitId);
            if (dbVisit == null)
            {
                return Json(new { success = false, message = "Приём не найдено" });
            }
            if (empId != dbVisit.EmployeeId)
            {
                return Json(new { success = false, message = "Приём вам не пренадлежит" });
            }
            var sendMail = db.Employees.Single(x => x.Id == dbVisit.VisitorId).Email;
            if (String.IsNullOrEmpty(reason))
            {
                reason = "не указано";
            }
            var msg = "Ваш приём " + dbVisit.Date.ToShortDateString() + " в " + dbVisit.Date.AddMinutes(dbVisit.TimeBegin).ToString("HH:mm") + " отклонён по причине: \r\n"
                + reason;
            MailSender mailSender = new MailSender();
            mailSender.SendMail(sendMail, "Заявка на приём отклонена", msg, new MailAddress(sendMail));
            db.Visits.Remove(dbVisit);
            db.SaveChanges();
            return Json(new { success = true });
        }

        public ActionResult CompleteVisit(int visitId)
        {
            Guid empId = UserHelper.GetCurrentEmployee().Id;
            var dbVisit = db.Visits.SingleOrDefault(x => x.Id == visitId);
            if (dbVisit == null)
            {
                return Json(new { success = false, message = "Приём не найдено" });
            }
            if (empId != dbVisit.EmployeeId)
            {
                return Json(new { success = false, message = "Приём вам не пренадлежит" });
            }
            //var sendMail = db.Employees.Single(x => x.Id == dbVisit.VisitorId).Email;
            //var msg = "Ваш приём " + dbVisit.Date.ToShortDateString() + " в " + dbVisit.Date.AddMinutes(dbVisit.TimeBegin).ToString("HH:mm") + " подтверждён";
            //MailSender mailSender = new MailSender();
            //mailSender.SendMail(sendMail, "Заявка на приём подтверждена", msg, new MailAddress(sendMail));
            dbVisit.VisitStatusId = (int)VisitStatuses.Complete;
            db.SaveChanges();
            return Json(new { success = true });
        }

        public ActionResult NoOneComeVisit(int visitId)
        {
            Guid empId = UserHelper.GetCurrentEmployee().Id;
            var dbVisit = db.Visits.SingleOrDefault(x => x.Id == visitId);
            if (dbVisit == null)
            {
                return Json(new { success = false, message = "Приём не найдено" });
            }
            if (empId != dbVisit.EmployeeId)
            {
                return Json(new { success = false, message = "Приём вам не пренадлежит" });
            }
            //var sendMail = db.Employees.Single(x => x.Id == dbVisit.VisitorId).Email;
            //var msg = "Ваш приём " + dbVisit.Date.ToShortDateString() + " в " + dbVisit.Date.AddMinutes(dbVisit.TimeBegin).ToString("HH:mm") + " подтверждён";
            //MailSender mailSender = new MailSender();
            //mailSender.SendMail(sendMail, "Заявка на приём подтверждена", msg, new MailAddress(sendMail));
            dbVisit.VisitStatusId = (int)VisitStatuses.NoOneCome;
            db.SaveChanges();
            return Json(new { success = true });
        }
    }
}