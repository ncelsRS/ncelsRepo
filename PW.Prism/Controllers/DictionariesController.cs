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
        private NcelsEntities db = UserHelper.GetCn();
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
        
        public ActionResult LimsGetFrpEmployeeList()
        {
            IQueryable<Employee> data = null;
            var currentUserCompany = UserHelper.GetCompany().Id;
            //var list = new List<>();
            if (EmployeePermissionHelper.IsFrpDepartmentTmc || EmployeePermissionHelper.IsFrpCenterTmc)
            {
                
                var roleIds = db.PermissionRoleKeys.Where(prk => prk.PermissionKey == EmployeePermissionHelper.Rights.IsFrpCenterTmc
                                           && prk.PermissionValue == bool.TrueString).Select(prk => prk.PermissionRoleId).ToList();
                data = db.EmployeePermissionRoles.Where(epr => roleIds.Contains(epr.PermissionRoleId))
                    .Select(epr => epr.EmployeeId)
                    //.Join(db.Employees, oks => oks, ink => ink.Id, (empId, emp) => emp)
                    .Join(db.Units, oks => oks, ink => ink.EmployeeId, (empId, unt) => unt)
                    .Where(u => u.Parent != null && u.Parent.ParentId == currentUserCompany)
                    .Select(u=> u.Employee)
                    .OrderBy(o => o.DisplayName);
            }
            else
            {
                var currentUserDepartment = UserHelper.GetDepartment().Id;
                var roleIds = db.PermissionRoleKeys.Where(prk => prk.PermissionKey == EmployeePermissionHelper.Rights.IsFrpDepartmentTmc
                                          && prk.PermissionValue == bool.TrueString).Select(prk => prk.PermissionRoleId).ToList();
                data = db.EmployeePermissionRoles.Where(epr => roleIds.Contains(epr.PermissionRoleId))
                    .Select(epr => epr.EmployeeId)
                    //.Join(db.Employees, oks => oks, ink => ink.Id, (empId, emp) => emp)
                    .Join(db.Units, oks => oks, ink => ink.EmployeeId, (empId, unt) => unt)
                    .Where(u => u.Parent != null && u.ParentId == currentUserDepartment)
                    .Select(u => u.Employee)
                    .OrderBy(o => o.DisplayName);
                var list1 = data.Select(o => new {o.Id, Name = o.DisplayName}).ToList();

                roleIds = db.PermissionRoleKeys.Where(prk => prk.PermissionKey == EmployeePermissionHelper.Rights.IsFrpCenterTmc
                                           && prk.PermissionValue == bool.TrueString).Select(prk => prk.PermissionRoleId).ToList();
                var data1 = db.EmployeePermissionRoles.Where(epr => roleIds.Contains(epr.PermissionRoleId))
                    .Select(epr => epr.EmployeeId)
                    //.Join(db.Employees, oks => oks, ink => ink.Id, (empId, emp) => emp)
                    .Join(db.Units, oks => oks, ink => ink.EmployeeId, (empId, unt) => unt)
                    .Where(u => u.Parent != null && u.Parent.ParentId == currentUserCompany)
                    .Select(u => u.Employee)
                    .OrderBy(o => o.DisplayName);
                var list2 = data1.Select(o => new { o.Id, Name = o.DisplayName }).ToList();
                var list3 = list2.Union(list1);
                return Json(list3, JsonRequestBehavior.AllowGet);
            }
            //var depId = UserHelper.GetCompany().Id;
            var list = data.Select(o => new { o.Id, Name = o.DisplayName }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDictionaryListByCode(string code)
        {
            var dataDic = db.Dictionaries.Where(o => o.Type == code && o.ExpireDate == null)
                .OrderBy(o => o.Name).ToList();
            return Json(dataDic.Select(o => new { o.Id, Name = LocalizationHelper.GetString(o.Name, o.NameKz) }),
                JsonRequestBehavior.AllowGet);
        }
    }
}