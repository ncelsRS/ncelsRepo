using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Models;

namespace PW.Ncels.Controllers {
	[Authorize()]
	public class ProfileController : ACommonController
    {
		[Authorize()]
		// GET: Profile
		public ActionResult Index() {
			ncelsEntities db = UserHelper.GetCn();
			var employee = db.Employees.FirstOrDefault(x => x.Login == User.Identity.Name);
			var model = new ProfileModel {
				Email = employee.Email,
				Organization = employee.LastName,
				Position = employee.FirstName,
				Name = employee.Iin
			};
			return View(model);
		}

		// GET: Profile
		public ActionResult Edit() {
			return View(new ProfileModel());
		}
	}
}