using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PW.Ncels.Database.Models;
using PW.Ncels.Models;

namespace PW.Ncels.Controllers {
	[Authorize()]
	public class ConclusionController : ACommonController
    {
		// GET: Conclusion
		public ActionResult Index() {
			return View();
		}

        [HttpPost]
        public JsonResult ReadConclusion(ModelRequest request )
        {
            var data = new
            {
                draw = request.Draw,
                recordsFiltered = 4,
                recordsTotal = 4,
                Data =
                    new List<object>()
                    {
                        new
                        {
                            Number = "1",
                            ApplicationDate = DateTime.Now,
                            NameRu = "test",
                            ProjectStateName = "test",
                            ExecutorName = "test",
                            SignEmployeeName = "test"
						},
						new
						{
							Number = "2",
							ApplicationDate = DateTime.Now,
							NameRu = "test",
							ProjectStateName = "test",
							ExecutorName = "test",
							SignEmployeeName = "test"
						}
					}
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Details() {
			return View();
		}
	}
}