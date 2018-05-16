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
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace PW.Prism.Controllers {
	public class ExperiencesController : Controller {
		private ncelsEntities db = UserHelper.GetCn();

		public ActionResult ListByExperience([DataSourceRequest] DataSourceRequest request, string employeId)
		{
			var data = db.Experiences
				.Where(o => o.EmployeeId== employeId)
				.OrderBy(o => o.DateStart).Select(o=>new
				{
					Kod = o.Id,
					o.DateStart,
					o.DateEnd,
					o.Country,
					o.EmployeeId,
					o.EmployeeValue,
					o.Note,
					o.Organization,
					o.Position,
					
				}).ToList(); 
			return Json(data.ToDataSourceResult(request));
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult CreateExperience([DataSourceRequest] DataSourceRequest request, Experience experience, string employeId)
		{
			ModelState.Remove("Id");
			Guid idGuid = new Guid(employeId);
			experience.Kod = Guid.NewGuid();
			experience.EmployeeId = employeId;
			experience.EmployeeValue = db.Employees.First(x => x.Id == idGuid).DisplayName;
			db.Experiences.Add(experience);
			db.SaveChanges();
			return Json(new[] { experience }.ToDataSourceResult(request, ModelState));
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult EditExperience([DataSourceRequest] DataSourceRequest request, Experience experience, string employeId) {

			Experience oldExperience = db.Experiences.Find(experience.Id);
			oldExperience.Note = experience.Note;
			oldExperience.Position = experience.Position;
			oldExperience.Organization = experience.Organization;
			oldExperience.Country = experience.Country;
			oldExperience.DateStart = experience.DateStart;
			oldExperience.DateEnd = experience.DateEnd;
			db.SaveChanges();
			return Json(new[] { experience }.ToDataSourceResult(request, ModelState));
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult DeleteExperience([DataSourceRequest] DataSourceRequest request, Experience experience, string employeId) {
			if (experience != null) {
				Experience d = db.Experiences.First(o => o.Id == experience.Id);
				db.Experiences.Remove(d);
				db.SaveChanges();
			}

			return Json(new[] { experience }.ToDataSourceResult(request, ModelState));
		}



		protected override void Dispose(bool disposing) {
			if (disposing) {
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
