using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Aspose.Pdf;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace PW.Prism.Controllers {
	public class HolidaysController : Controller {
		private ncelsEntities db = UserHelper.GetCn();

		public ActionResult ListByEmployee([DataSourceRequest] DataSourceRequest request, string employeId) {
			var data = db.Holidays
				.Where(o => o.EmployeeId.ToLower() == employeId.ToLower())
				.OrderBy(o => o.PeriodStart).ToList()
				.Select(o => new HolidayModel {
					Id = o.Id,
					DateEnd = o.DateEnd,
					DateStart = o.DateStart,
					PeriodEnd = o.PeriodEnd,
					PeriodStart = o.PeriodStart,
					DocumentId = DictionaryHelper.GetItems(o.DocumentId, o.DocumentValue),
					EmployeeId = DictionaryHelper.GetItems(o.EmployeeId, o.EmployeeValue),
					HolidayTypeDictionaryId = DictionaryHelper.GetItems(o.HolidayTypeDictionaryId, o.HolidayTypeDictionaryValue),
					Note = o.Note,
					Count = o.Count
				});
			return Json(data.ToDataSourceResult(request));
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult CreateHoliday([DataSourceRequest] DataSourceRequest request, HolidayModel holiday, string employeId) { 
			Guid id;
			if (holiday != null && ModelState.IsValid && Guid.TryParse(employeId, out id) && db.Employees.FirstOrDefault(x => x.Id == id) != null) {
				Holiday h = new Holiday {
					EmployeeId = employeId,
					EmployeeValue = db.Employees.FirstOrDefault(x => x.Id == id).DisplayName,
					DateEnd = holiday.DateEnd,
					DateStart = holiday.DateStart,
					PeriodEnd = holiday.PeriodEnd,
					PeriodStart = holiday.PeriodStart,
					DocumentId = DictionaryHelper.GetItemsId(holiday.DocumentId),
					DocumentValue = DictionaryHelper.GetItemsName(holiday.DocumentId),
					HolidayTypeDictionaryId = DictionaryHelper.GetItemsId(holiday.HolidayTypeDictionaryId),
					HolidayTypeDictionaryValue = DictionaryHelper.GetItemsName(holiday.HolidayTypeDictionaryId),
					Note = holiday.Note,
					Count = holiday.Count
				};
				db.Holidays.Add(h);
				db.SaveChanges();
				holiday.Id = h.Id;
			}

			return Json(new[] {holiday}.ToDataSourceResult(request, ModelState));
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult EditHoliday([DataSourceRequest] DataSourceRequest request, HolidayModel holiday, string employeId) {
			Guid id;
			if (holiday != null && ModelState.IsValid && Guid.TryParse(employeId, out id)) {
				Holiday h = db.Holidays.First(o => o.Id == holiday.Id);
				h.DateEnd = holiday.DateEnd;
				h.DateStart = holiday.DateStart;
				h.PeriodEnd = holiday.PeriodEnd;
				h.PeriodStart = holiday.PeriodStart;
				h.DocumentId = DictionaryHelper.GetItemsId(holiday.DocumentId);
				h.DocumentValue = DictionaryHelper.GetItemsName(holiday.DocumentId);
				h.EmployeeId = DictionaryHelper.GetItemsId(holiday.EmployeeId);
				h.EmployeeValue = DictionaryHelper.GetItemsName(holiday.EmployeeId);
				h.HolidayTypeDictionaryId = DictionaryHelper.GetItemsId(holiday.HolidayTypeDictionaryId);
				h.HolidayTypeDictionaryValue = DictionaryHelper.GetItemsName(holiday.HolidayTypeDictionaryId);
				h.Note = holiday.Note;
				h.Count = holiday.Count;
				db.SaveChanges();
			}

			return Json(new[] { holiday }.ToDataSourceResult(request, ModelState));
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult DeleteHoliday([DataSourceRequest] DataSourceRequest request, HolidayModel holiday, string employeId) {
			if (holiday != null) {
				Holiday d = db.Holidays.First(o => o.Id == holiday.Id);
				db.Holidays.Remove(d);
				db.SaveChanges();
			}

			return Json(new[] { holiday }.ToDataSourceResult(request, ModelState));
		}

		private int CalculateHolidayDays(DateTime? start, DateTime? end) {
			if (start.HasValue && end.HasValue && end.Value > start.Value) {
				return (end.Value - start.Value).Days +1;
			}
			return 0;
		}

		protected override void Dispose(bool disposing) {
			if (disposing) {
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
