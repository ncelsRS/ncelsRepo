using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PW.Prism.Controllers.OBKExpDocument
{
    public class SelectOPCommissionController : Controller
    {
        public ncelsEntities repo = new ncelsEntities();

        public ActionResult ListOrganization()
        {
            var orgs = repo.Units
                .Where(x => x.Id == new Guid("8f0b91f3-af29-4d3c-96d6-019cbbdfc8be"))
                .Select(x => new { x.Id, x.Name }).ToList();
            return Json(orgs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListUnits(Guid organizationId)
        {
            var units = repo.Units
                .Where(x => x.ParentId == organizationId)
                .Select(x => new { x.Id, x.Name }).ToList();
            return Json(units, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListPositions(Guid unitId)
        {
            var positions = repo.Units
                .Where(x => x.PositionState == 1 && x.ParentId == unitId)
                .Select(x => new { x.Id, x.Name }).ToList();
            return Json(positions, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListEmployees(Guid unitId)
        {
            var employees = repo.Employees
                .Where(x => x.Units.Any(u => u.Id == unitId))
                .Select(x => new { x.Id, x.FullName }).ToList();
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListRoles()
        {
            var roles = repo.OBK_OP_CommissionRoles.Select(x => new { x.Id, Name = x.NameRu }).ToList();
            return Json(roles, JsonRequestBehavior.AllowGet);
        }
    }
}