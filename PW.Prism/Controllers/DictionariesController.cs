using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Repository.Common;

namespace PW.Prism.Controllers
{
    public class DictionariesController : Controller
    {
        private ncelsEntities db = UserHelper.GetCn();
        // GET: Dictionaries
        private ReadOnlyDictionaryRepository rodRepository = new ReadOnlyDictionaryRepository();


        [Authorize()]
        [HttpGet]
        public async Task<ActionResult> GetReference(string type)
        {
            return Json(await db.Dictionaries.Where(o => o.Type == type)
                .Select(o => new { o.Id, o.Name, o.Code, o.NameKz })
                .OrderBy(m => m.Code)
                .ToListAsync(), JsonRequestBehavior.AllowGet);
        }

        [Authorize()]
        [HttpGet]
        public async Task<ActionResult> GetOrganizations()
        {
            return Json(await db.Organizations.Where(o => o.OriginalOrgId == null && o.NameRu != null)
                .Select(o => new { o.Id, Name = o.NameRu, o.NameEn, o.NameKz })
                .OrderBy(m => m.Name)
                .ToListAsync(), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetOrganization(Guid id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Organizations.AsNoTracking().FirstOrDefault(e => e.Id == id), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GetEmployeeList()
        {
            //Guid organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
            //var data = db.Units.Where(
            //	x => x.Type == 2 && x.PositionState == 1 && x.Employee != null && x.Employee.OrganizationId == organizationId)
            //	.Select(o => o.Employee)
            //	.OrderBy(o => o.DisplayName);
            var data = db.Units.Where(
                x => x.Type == 2 && x.PositionState == 1 && x.Employee != null)
                .Select(o => o.Employee)
                .OrderBy(o => o.DisplayName);
            return Json(data.Select(o => new { o.Id, Name = o.DisplayName }), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult LimsGetFrpEmployeeList(Guid depId)
        {
            var roleIds = db.PermissionRoleKeys.Where(prk => prk.PermissionKey == "IsFrpCenterTmc" && prk.PermissionValue == bool.TrueString).Select(prk => prk.PermissionRoleId).ToList();
            var data = db.EmployeePermissionRoles.Where(epr => roleIds.Contains(epr.PermissionRoleId))
                .Select(epr => epr.EmployeeId)
                .Join(db.Employees, oks => oks, ink => ink.Id, (empId, emp) => emp)
                .OrderBy(o => o.DisplayName);


//            var data = db.Units.Where(x => x.Type == 2
//                                           && x.Employee != null
//                                           && x.Id != null
//                                           && x.Parent != null
//                                           && x.ParentId == depId || x.Parent.ParentId == depId)
//                .Select(o => o.Employee)
//                .Where(o => o.Id != null)
//                .OrderBy(o => o.DisplayName);

            var list = data.Select(o => new { o.Id, Name = o.DisplayName }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}