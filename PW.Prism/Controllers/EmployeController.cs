using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls.Expressions;
using Antlr.Runtime.Misc;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models.Expertise;

namespace PW.Prism.Controllers
{
	public class EmployeController : Controller
	{
		private ncelsEntities db = UserHelper.GetCn();


		// GET: /Employe/
		public ActionResult Index()
		{
			Guid guid = Guid.NewGuid();
			return PartialView(guid);
		}

		public ActionResult DismissedList()
		{
			Guid guid = Guid.NewGuid();
			return PartialView(guid);
		}
		public ActionResult DismissedListDep()
		{
			Guid guid = Guid.NewGuid();
			return PartialView(guid);
		}
		public ActionResult Delete(Guid? id, string type, Guid? parentId, Guid? modelId)
		{
			try
			{
				Unit unit = db.Units.Find(id);
				if (unit.EmployeeId != null)
				{
					Employee employee = db.Employees.Find(unit.EmployeeId);
					unit.EmployeeId = null;
					employee.PositionId = null;
					db.SaveChanges();
					db.Employees.Remove(employee);
					if (Membership.GetUser(employee.Login) != null)
					{
						Membership.DeleteUser(employee.Login);
					}
				}
				db.Units.Remove(unit);

				db.SaveChanges();

				return Content(bool.TrueString);
			}
			catch (Exception)
			{

				return Content(bool.FalseString);
			}

		}

		public ActionResult Edit(Guid? id, string type, Guid? parentId, Guid? modelId, string uid)
		{
			ViewBag.ModelId = modelId;
			ViewBag.ModeUid = uid;
			List<Guid> items = new List<Guid>();
			items.Add(id.Value);
			if (parentId.HasValue)
			{
				items.Add(parentId.Value);
			}
			if (type == "0" && !parentId.HasValue)
			{
				return PartialView("EditOrganization", items.ToArray());
			}
			if (type == "0" && parentId.HasValue)
			{
				return PartialView("EditDepartment", items.ToArray());
			}

			if (type == "9")
			{
				return PartialView("EditDepartment", items.ToArray());
			}

			if (type == "1" && !parentId.HasValue)
			{
				return PartialView("EditDepartment", items.ToArray());
			}

			if (type == "1" && parentId.HasValue)
			{
				return PartialView("EditPosition", items.ToArray());
			}
			if (type == "2" && !parentId.HasValue)
			{
				return PartialView("EditPosition", items.ToArray());
			}
			if (type == "2" && parentId.HasValue)
			{
				Unit unit = db.Units.Find(parentId.Value);
				if (unit.EmployeeId.HasValue)
				{
					return Content(bool.FalseString);
				}
				return PartialView("EditEmploye", items.ToArray());
			}
			if (type == "3")
			{
				return PartialView("EditEmploye", items.ToArray());
			}

            return PartialView(id.Value);
        }

        public ActionResult SendEmploye(Guid id)
		{
			Employee employee = db.Employees.Find(id);
			string[] models = new[] {id.ToString(), employee.FullName};
			return PartialView(models);
		}

		[HttpPost]
		public ActionResult SendEmployeConfirm(Guid id, Guid positionId, Guid organizationId)
		{
			Employee employee = db.Employees.Find(id);
			Unit oldUnit = db.Units.Find(employee.PositionId);
			Unit unit = db.Units.Find(positionId);
			employee.PositionId = positionId;
			employee.OrganizationId = organizationId;
			unit.EmployeeId = id;
			unit.PositionState = 1;
			oldUnit.PositionState = 2;
			oldUnit.EmployeeId = null;
			db.SaveChanges();
			return Content(bool.TrueString);
		}

		public ActionResult PermissionValueList(string permissionKey)
		{
			var data =
				db.PermissionValues.Where(o => o.PermissionKey == permissionKey).Select(o => new {o.Name, Id = o.Value}).ToList();
			return Json(data, JsonRequestBehavior.AllowGet);
		}

        public ActionResult PermissionRoleValueList()
        {
            ActionLogger.WriteInt("Получение списка ролей доступа");
            var data = db.PermissionRoles.Select(o => new { Id = o.Id, Name = o.Name }).OrderBy(x => x.Name).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmployeePermissionRoleRead([DataSourceRequest] DataSourceRequest request, Guid employeeId)
        {
            ActionLogger.WriteInt("Получение списка ролей доступа сотрудника", "EmployeeId: " + employeeId);
            var data = db.EmployeePermissionRoles.Where(o => o.EmployeeId == employeeId)
                .Join(db.PermissionRoles, x => x.PermissionRoleId, x => x.Id, (ep, r) => new {EmpRole = ep, Role = r})
                .Select(o => new PermissionRoleModel
                {
                    EmployeeRoleId = o.EmpRole.Id,
                    RoleId = o.Role.Id,
                    RoleName = o.Role.Name,
                }).OrderBy(x=>x.RoleName);
            return Json(data.ToDataSourceResult(request));
        }

	    [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EmployeePermissionRoleCreate([DataSourceRequest] DataSourceRequest request, PermissionRoleModel dictionary, Guid employeeId)
        {
            if (dictionary != null)
            {
                var dbEmployeeRole = db.EmployeePermissionRoles.SingleOrDefault(x => x.PermissionRoleId == dictionary.RoleId && x.EmployeeId == employeeId);
                if (dbEmployeeRole != null)
                {
                    ModelState.AddModelError("Message", Convert.ToString("Роль уже присвоена"));
                    return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
                }
                dbEmployeeRole = new EmployeePermissionRole();
                dbEmployeeRole.EmployeeId = employeeId;
                dbEmployeeRole.PermissionRoleId = dictionary.RoleId;
                db.EmployeePermissionRoles.Add(dbEmployeeRole);
                ActionLogger.WriteInt(db, "Добавление сотруднику роли прав доступа", "EmployeeId: " + employeeId+ "; RoleId: " + dictionary.RoleId);
                db.SaveChanges();
                EmployePermissionHelper.ClearEmployeePermission();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EmployeePermissionRoleUpdate([DataSourceRequest] DataSourceRequest request, PermissionRoleModel dictionary, Guid employeeId)
        {
            if (dictionary != null && ModelState.IsValid)
            {
                var dbEmployeeRole = db.EmployeePermissionRoles.SingleOrDefault(x => x.PermissionRoleId == dictionary.RoleId && x.EmployeeId == employeeId && x.Id != dictionary.EmployeeRoleId);
                if (dbEmployeeRole != null)
                {
                    ModelState.AddModelError("Message", Convert.ToString("Роль уже присвоена"));
                    return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
                }
                dbEmployeeRole = db.EmployeePermissionRoles.Single(x => x.Id == dictionary.EmployeeRoleId);
                var prevRoleId = dbEmployeeRole.PermissionRoleId;
                dbEmployeeRole.PermissionRoleId = dictionary.RoleId;
                ActionLogger.WriteInt(db, "Обновление сотруднику роли прав доступа", "PrevRoleId: " + prevRoleId + "; CurrentRoleId: " + dictionary.RoleId);
                db.SaveChanges();
                EmployePermissionHelper.ClearEmployeePermission();
            }
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EmployeePermissionRoleDestroy([DataSourceRequest] DataSourceRequest request, PermissionRoleModel dictionary, Guid employeeId)
        {
            if (dictionary != null)
            {
                var dbEmployeeRole = db.EmployeePermissionRoles.Single(x => x.Id == dictionary.EmployeeRoleId);
                db.EmployeePermissionRoles.Remove(dbEmployeeRole);
                ActionLogger.WriteInt(db, "Удаление сотруднику роли прав доступа","RoleId: " + dbEmployeeRole.PermissionRoleId+ "; EmployeeId: " + dbEmployeeRole.EmployeeId);
                db.SaveChanges();
                EmployePermissionHelper.ClearEmployeePermission();
            }
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PermissionRoleCreate([DataSourceRequest] DataSourceRequest request, PermissionRole dictionary)
        {
            if (dictionary != null)
            {
                db.PermissionRoles.Add(dictionary);
                ActionLogger.WriteInt(db, "Создание роли прав доступа", "Name: " + dictionary.Name);
                db.SaveChanges();
                EmployePermissionHelper.AddRoleDefaultPermissionKeys(dictionary.Id);
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PermissionRoleUpdate([DataSourceRequest] DataSourceRequest request, PermissionRole dictionary)
        {
            if (dictionary != null && ModelState.IsValid)
            {
                var dbPermissionRole = db.PermissionRoles.Single(o => o.Id == dictionary.Id);
                var prevName = dbPermissionRole.Name;
                dbPermissionRole.Name = dictionary.Name;
                ActionLogger.WriteInt(db, "Обновление роли прав доступа", "RoleId: " + dbPermissionRole.Id + "; PrevName: " + prevName + "; CurrentName: " + dictionary.Name);
                db.SaveChanges();
            }
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PermissionRoleDestroy([DataSourceRequest] DataSourceRequest request, PermissionRole dictionary)
        {
            if (dictionary != null)
            {
                var roleId = dictionary.Id;
                var dbRole = db.PermissionRoles.SingleOrDefault(x => x.Id == roleId);
                if (dbRole == null)
                {
                    ModelState.AddModelError("Message", Convert.ToString("Роль не найдена"));
                    return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
                }
                var dbEmployees = db.EmployeePermissionRoles.Where(x => x.PermissionRoleId == roleId)
                    .Join(db.Employees, x => x.EmployeeId, x => x.Id, (r, e) => e).ToList();
                if (dbEmployees.Count > 0)
                {
                    var empl = dbEmployees[0];
                    var employeeNames = empl.LastName + " " + empl.FirstName + " " + empl.MiddleName;
                    var otherCount = dbEmployees.Count - 1;
                    if (otherCount > 0)
                    {
                        employeeNames += " и ещё " + otherCount + " сотрудник(ов)";
                    }
                    ModelState.AddModelError("Message", Convert.ToString("Роль использует " + employeeNames));
                    return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
                }
                db.PermissionRoles.Remove(dbRole);
                ActionLogger.WriteInt(db, "Удаление роли прав доступа", "RoleId: " + dbRole.Id+ "; Name: " + dbRole.Name);
                db.SaveChanges();
                EmployePermissionHelper.RemoveRolePermissionKeys(dbRole.Id);
            }
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult PermissionRoleList()
        {
            Guid guid = Guid.NewGuid();
            return PartialView("PermissionRoleList", guid);
        }

        public ActionResult PermissionRoleRead([DataSourceRequest] DataSourceRequest request)
        {
            ActionLogger.WriteInt(db, "Получение списка ролей прав доступа");
            var data = db.PermissionRoles.OrderByDescending(m => m.Id).Select(o => new
            {
                o.Id,
                o.Name,
            });
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PermissionRoleKeysUpdate([DataSourceRequest] DataSourceRequest request, PermissionModel permissionModel, int roleId)
        {
            if (permissionModel != null && ModelState.IsValid)
            {

                var employeePermission = db.PermissionRoleKeys.First(o => o.PermissionRoleId == roleId && o.PermissionKey == permissionModel.KeyType);
                var prevValue = employeePermission.PermissionValue;
                employeePermission.PermissionValue = permissionModel.KeyValue;
                ActionLogger.WriteInt(db, "Обновление разрешений у роли прав доступа", 
                    "RoleId: " + roleId + "; Key: " + permissionModel.KeyType + "; PrevValue: " + prevValue + "; CurrentValue: " + permissionModel.KeyValue);
                db.SaveChanges();
                EmployePermissionHelper.ClearEmployeePermission();
            }
            return Json(new[] { permissionModel }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult DeletePermissionRole(int roleId)
        {
            var dbRole = db.PermissionRoles.SingleOrDefault(x => x.Id == roleId);
            if (dbRole == null)
            {
                return Json(new { success = false, message = "Роль не найдена" });
            }
            var dbEmployees = db.EmployeePermissionRoles.Where(x => x.PermissionRoleId == roleId)
                .Join(db.Employees, x => x.EmployeeId, x => x.Id, (r, e) => e).ToList();
            if (dbEmployees.Count > 0)
            {
                var empl = dbEmployees[0];
                var employeeNames = empl.LastName + " " + empl.FirstName + " " + empl.MiddleName;
                var otherCount = dbEmployees.Count - 1;
                if (otherCount > 0)
                {
                    employeeNames += " и ещё " + otherCount + " сотрудник(ов)";
                }
                return Json(new { success = false, message = "Роль использует " + employeeNames });
            }
            db.PermissionRoles.Remove(dbRole);
            ActionLogger.WriteInt(db, "Удаление роли прав доступа", "RoleId: " + roleId + "; RoleName" + dbRole.Name);
            db.SaveChanges();
            return Json(new { success = true, message = "Роль успешно удалена" });
        }

        public ActionResult PermissionRoleKeysList(int roleId)
        {
            return PartialView("PermissionRoleKeysList", roleId);
        }

        public ActionResult PermissionRoleKeysRead([DataSourceRequest] DataSourceRequest request, int roleId)
        {
            ActionLogger.WriteInt("Получение списка разрешений роли прав доступа", "RoleId: " + roleId);
            var data = db.PermissionRoleKeys.Where(o => o.PermissionRoleId == roleId)
                    .Join(db.PermissionKeys, x => x.PermissionKey, x => x.Key, (r, k) => new { RoleKeys = r, Key = k })
                    .ToList().Select(o => new PermissionModel()
                    {
                        Id = o.RoleKeys.Id,
                        GroupName = o.Key.GroupName,
                        KeyDescription = EmployePermissionHelper.GeKeys().First(x => x.Key == o.RoleKeys.PermissionKey).KeyDescription,
                        KeyName = EmployePermissionHelper.GeKeys().First(x => x.Key == o.RoleKeys.PermissionKey).KeyName,
                        KeyType = o.RoleKeys.PermissionKey,
                        KeyValue = o.RoleKeys.PermissionValue,
                        KeyPermission =
                        EmployePermissionHelper.GeKeysValue()
                            .Where(x => x.PermissionKey == o.RoleKeys.PermissionKey)
                            .First(y => y.Value == o.RoleKeys.PermissionValue)
                            .Name
                    });
            return Json(data.ToDataSourceResult(request));
        }


	    public ActionResult UnitsSignerRead([DataSourceRequest] DataSourceRequest request, Guid modelId)
	    {
            var data = db.UnitSigners.Where(o=>o.UnitsId==modelId && o.IsDeleted == false)
                .Join(db.Employees, x=>x.SignerId, x=>x.Id, (us, e)=>new {USigner = us, Emp = e})
                .Select(o=> new UnitSignerModel
                {
                    UnitSignerId = o.USigner.Id,
                    DocNumber = o.USigner.DocNumber,
                    DocumentType = o.USigner.DocumentType,
                    StartDate = o.USigner.StartDate,
                    EndDate = o.USigner.EndDate,
                    SignerId = o.Emp.Id,
                    Signer = o.Emp.DisplayName
                });
	        return Json(data.ToDataSourceResult(request, ModelState));
        }

	    public ActionResult UnitsSignerValueList(Guid modelId)
	    {
	        var data = db.Employees.Where(e => e.OrganizationId == modelId)
	            .Select(o => new {Id = o.Id, Name = o.DisplayName})
	            .OrderBy(x => x.Name)
	            .ToList();
	        return Json(data, JsonRequestBehavior.AllowGet);
	    }

	    public ActionResult UnitsSignerDocTypeList()
	    {
	        var data = db.OBK_Ref_ContractDocumentType.Select(o => new { Id = o.Id, Name = o.NameRu }).OrderBy(x => x.Name).ToList();
	        return Json(data, JsonRequestBehavior.AllowGet);
        }

	    public ActionResult UnitsSignerDocAvailableList()
	    {
            var list = new List<BoolDocType>();
	        list.Add(new BoolDocType { Type = false, Name = "Нет" });
	        list.Add(new BoolDocType { Type = true, Name = "Да" });
	        return Json(list, JsonRequestBehavior.AllowGet);
	    }

        public class BoolDocType {
            public bool Type { get; set; }
            public string Name { get; set; }
        }

	    [AcceptVerbs(HttpVerbs.Post)]
	    public ActionResult UnitsSignerCreate([DataSourceRequest] DataSourceRequest request,
	        UnitSignerModel unitSignerModel, Guid modelId)
	    {
	        //var dbUSigner = db.UnitSigners.Where(x => x.UnitsId == modelId && x.StartDate<= DateTime.Now && x.EndDate>=DateTime.Now).ToList();
	        //if (dbUSigner.Count > 1)
	        //    return Content("Подписывающий уже определен");

            if (unitSignerModel != null)
	        {
	            var dbUnitsSigner = new UnitSigner();
	            dbUnitsSigner.Id = Guid.NewGuid();
	            dbUnitsSigner.UnitsId = modelId;
	            dbUnitsSigner.IsDeleted = false;
	            dbUnitsSigner.CreatedDate = DateTime.Now;
	            dbUnitsSigner.SignerId = unitSignerModel.SignerId;
	            dbUnitsSigner.DocNumber = unitSignerModel.DocNumber;
	            dbUnitsSigner.DocumentType = unitSignerModel.DocumentType;
	            dbUnitsSigner.StartDate = unitSignerModel.StartDate;
	            dbUnitsSigner.EndDate = unitSignerModel.EndDate;
	            db.UnitSigners.Add(dbUnitsSigner);
	            db.SaveChanges();
	        }
	        return Json(new[] { unitSignerModel }.ToDataSourceResult(request, ModelState));
	    }

	    [AcceptVerbs(HttpVerbs.Post)]
	    public ActionResult UnitsSignerUpdate([DataSourceRequest] DataSourceRequest request,
	        UnitSignerModel unitSignerModel, Guid modelId)
	    {
	        if (unitSignerModel != null && ModelState.IsValid)
	        {
	            var dbUnitsSigner = db.UnitSigners.SingleOrDefault(x => x.Id == unitSignerModel.UnitSignerId);
	            if (dbUnitsSigner != null)
	            {
	                dbUnitsSigner.DocNumber = unitSignerModel.DocNumber;
	                dbUnitsSigner.DocumentType = unitSignerModel.DocumentType;
	                dbUnitsSigner.SignerId = unitSignerModel.SignerId;
	                dbUnitsSigner.StartDate = unitSignerModel.StartDate;
	                dbUnitsSigner.EndDate = unitSignerModel.EndDate;
	                db.SaveChanges();
	            }
	        }
	        return Json(new[] { unitSignerModel }.ToDataSourceResult(request, ModelState));
	    }

	    [AcceptVerbs(HttpVerbs.Post)]
	    public ActionResult UnitsSignerDestroy([DataSourceRequest] DataSourceRequest request,
	        UnitSignerModel unitSignerModel, Guid modelId)
	    {
	        if (unitSignerModel != null)
	        {
	            var dbUnitsSigner = db.UnitSigners.SingleOrDefault(x => x.Id == unitSignerModel.UnitSignerId);
	            if (dbUnitsSigner != null)
	            {
	                dbUnitsSigner.IsDeleted = true;
	                db.SaveChanges();
	            }
	        }
	        return Json(new[] { unitSignerModel }.ToDataSourceResult(request, ModelState));
	    }


        public ActionResult UnitsBankRead([DataSourceRequest] DataSourceRequest request, Guid modelId)
	    {
	        var data = db.UnitsBanks.Where(o => o.UnitsId == modelId && o.IsDeleted == false)
	            .Join(db.Dictionaries, x => x.CurrencyId, x => x.Id, (ub, d) => new {UBank = ub, Dic = d})
	            .Select(o => new UnitBankModel
	            {
	                UnitBankId = o.UBank.Id,
                    BankNameRu = o.UBank.BankNameRu,
                    BankNameKz = o.UBank.BankNameKz,
	                KBE = o.UBank.KBE,
	                Code = o.UBank.Code,
	                SWIFT = o.UBank.SWIFT,
	                IIK = o.UBank.IIK,
	                CorrespondentBank = o.UBank.CorrespondentBank,
	                CorrespondentAccount = o.UBank.CorrespondentAccount,
	                SWIFT1 = o.UBank.SWIFT1,
	                CorrespondentAccount1 = o.UBank.CorrespondentAccount1,
	                SWIFT2 = o.UBank.SWIFT2,
                    CurrencyId = o.Dic.Id,
                    CurrencyName = o.Dic.Name,
                    StartDate = o.UBank.StartDate,
                    EndDate = o.UBank.EndDate
                });
	        return Json(data.ToDataSourceResult(request, ModelState));
        }

	    public ActionResult UnitsBankValueList()
	    {
	        var data = db.Dictionaries.Where(e => e.Type == "Currency").Select(o => new { Id = o.Id, Name = o.Name }).OrderBy(x => x.Name).ToList();
	        return Json(data, JsonRequestBehavior.AllowGet);
        }

	    [AcceptVerbs(HttpVerbs.Post)]
	    public ActionResult UnitsBankCreate([DataSourceRequest] DataSourceRequest request,
	        UnitBankModel unitBankModel, Guid modelId)
	    {
	        if (unitBankModel != null)
	        {
	            var dbUnitsBanks = new UnitsBank();
	            dbUnitsBanks.Id = Guid.NewGuid();
	            dbUnitsBanks.UnitsId = modelId;
	            dbUnitsBanks.IsDeleted = false;
	            dbUnitsBanks.CreatedDate = DateTime.Now;
	            dbUnitsBanks.BankNameRu = unitBankModel.BankNameRu;
	            dbUnitsBanks.BankNameKz = unitBankModel.BankNameKz;
	            dbUnitsBanks.CurrencyId = unitBankModel.CurrencyId;
	            dbUnitsBanks.Code = unitBankModel.Code;
	            dbUnitsBanks.CorrespondentAccount1 = unitBankModel.CorrespondentAccount1;
	            dbUnitsBanks.CorrespondentAccount = unitBankModel.CorrespondentAccount;
	            dbUnitsBanks.CorrespondentBank = unitBankModel.CorrespondentBank;
	            dbUnitsBanks.IIK = unitBankModel.IIK;
	            dbUnitsBanks.KBE = unitBankModel.KBE;
	            dbUnitsBanks.SWIFT = unitBankModel.SWIFT;
	            dbUnitsBanks.SWIFT1 = unitBankModel.SWIFT1;
	            dbUnitsBanks.SWIFT2 = unitBankModel.SWIFT2;
                dbUnitsBanks.StartDate = unitBankModel.StartDate;
	            dbUnitsBanks.EndDate = unitBankModel.EndDate;
	            db.UnitsBanks.Add(dbUnitsBanks);
	            db.SaveChanges();
	        }
	        return Json(new[] { unitBankModel }.ToDataSourceResult(request, ModelState));
	    }

	    [AcceptVerbs(HttpVerbs.Post)]
	    public ActionResult UnitsBankUpdate([DataSourceRequest] DataSourceRequest request,
	        UnitBankModel unitBankModel, Guid modelId)
	    {
	        if (unitBankModel != null && ModelState.IsValid)
	        {
	            var dbUnitsBank = db.UnitsBanks.SingleOrDefault(x => x.Id == unitBankModel.UnitBankId);
	            if (dbUnitsBank != null)
	            {
	                dbUnitsBank.BankNameRu = unitBankModel.BankNameRu;
	                dbUnitsBank.BankNameKz = unitBankModel.BankNameKz;
                    dbUnitsBank.CurrencyId = unitBankModel.CurrencyId;
	                dbUnitsBank.Code = unitBankModel.Code;
	                dbUnitsBank.CorrespondentAccount1 = unitBankModel.CorrespondentAccount1;
	                dbUnitsBank.CorrespondentAccount = unitBankModel.CorrespondentAccount;
	                dbUnitsBank.CorrespondentBank = unitBankModel.CorrespondentBank;
	                dbUnitsBank.IIK = unitBankModel.IIK;
	                dbUnitsBank.KBE = unitBankModel.KBE;
	                dbUnitsBank.SWIFT = unitBankModel.SWIFT;
	                dbUnitsBank.SWIFT1 = unitBankModel.SWIFT1;
	                dbUnitsBank.SWIFT2 = unitBankModel.SWIFT2;
                    dbUnitsBank.StartDate = unitBankModel.StartDate;
	                dbUnitsBank.EndDate = unitBankModel.EndDate;
                    db.SaveChanges();
	            }
	        }
	        return Json(new[] { unitBankModel }.ToDataSourceResult(request, ModelState));
	    }

	    [AcceptVerbs(HttpVerbs.Post)]
	    public ActionResult UnitsBankDestroy([DataSourceRequest] DataSourceRequest request,
	        UnitBankModel unitBankModel, Guid modelId)
	    {
	        if (unitBankModel != null)
	        {
	            var dbUnitsBank = db.UnitsBanks.SingleOrDefault(x => x.Id == unitBankModel.UnitBankId);
	            if (dbUnitsBank != null)
	            {
	                dbUnitsBank.IsDeleted = true;
	                db.SaveChanges();
	            }
	        }
	        return Json(new[] { unitBankModel }.ToDataSourceResult(request, ModelState));
	    }


        public ActionResult UnitsAddressRead([DataSourceRequest] DataSourceRequest request, Guid modelId)
	    {
	        var data = db.UnitsAddresses.Where(o => o.UnitsId == modelId && o.IsDeleted == false)
	            .Join(db.Dictionaries, x => x.RegionId, x => x.Id, (ua, d) => new { UAddress = ua, Dic = d })
	            .Select(o => new UnitAddress
	            {
	                UnitAddressId = o.UAddress.Id,
	                RegionId = o.Dic.Id,
	                RegionName = o.Dic.Name,
	                AddressNameRu = o.UAddress.AddressNameRu,
	                AddressNameKz = o.UAddress.AddressNameKz
	            });
	        return Json(data.ToDataSourceResult(request, ModelState));
	    }

	    public ActionResult UnitsAddressValueList()
	    {
	        ActionLogger.WriteInt("Получение списка регионов");
	        var data = db.Dictionaries.Where(e => e.Type == "Kato").Select(o => new { Id = o.Id, Name = o.Name }).OrderBy(x => x.Name).ToList();
	        return Json(data, JsonRequestBehavior.AllowGet);
	    }

	    [AcceptVerbs(HttpVerbs.Post)]
	    public ActionResult UnitsAddressCreate([DataSourceRequest] DataSourceRequest request,
	        UnitAddress unitsAddress, Guid modelId)
	    {
	        if (unitsAddress != null)
	        {
	            var dbUnitsAddress = new UnitsAddress();
	            dbUnitsAddress.Id = Guid.NewGuid();
	            dbUnitsAddress.UnitsId = modelId;
	            dbUnitsAddress.IsDeleted = false;
	            dbUnitsAddress.CreatedDate = DateTime.Now;
	            dbUnitsAddress.AddressNameRu = unitsAddress.AddressNameRu;
	            dbUnitsAddress.AddressNameKz = unitsAddress.AddressNameKz;
	            dbUnitsAddress.RegionId = unitsAddress.RegionId;
	            db.UnitsAddresses.Add(dbUnitsAddress);
	            db.SaveChanges();
	        }
	        return Json(new[] { unitsAddress }.ToDataSourceResult(request, ModelState));
	    }

	    [AcceptVerbs(HttpVerbs.Post)]
	    public ActionResult UnitsAddressUpdate([DataSourceRequest] DataSourceRequest request,
	        UnitAddress unitsAddress, Guid modelId)
	    {
	        if (unitsAddress != null && ModelState.IsValid)
	        {
	            var dbUnitsAddress = db.UnitsAddresses.SingleOrDefault(x => x.Id == unitsAddress.UnitAddressId);
	            if (dbUnitsAddress != null)
	            {
	                dbUnitsAddress.AddressNameRu = unitsAddress.AddressNameRu;
	                dbUnitsAddress.AddressNameKz = unitsAddress.AddressNameKz;
	                dbUnitsAddress.RegionId = unitsAddress.RegionId;
	                db.SaveChanges();
	            }

	        }
	        return Json(new[] { unitsAddress }.ToDataSourceResult(request, ModelState));
	    }
	    [AcceptVerbs(HttpVerbs.Post)]
	    public ActionResult UnitsAddressDestroy([DataSourceRequest] DataSourceRequest request,
	        UnitAddress unitsAddress, Guid modelId)
	    {
	        if (unitsAddress != null)
	        {
	            var dbUnitsAddress = db.UnitsAddresses.SingleOrDefault(x => x.Id == unitsAddress.UnitAddressId);
	            if (dbUnitsAddress != null)
	            {
	                dbUnitsAddress.IsDeleted = true;
	                db.SaveChanges();
	            }
	        }
	        return Json(new[] { unitsAddress }.ToDataSourceResult(request, ModelState));
	    }

        public ActionResult ActionLogsList()
        {
            return PartialView("ActionLogsList", Guid.NewGuid());
        }

        public ActionResult ActionLogsRead([DataSourceRequest] DataSourceRequest request)
        {
            ActionLogger.WriteInt("Получение списка логов действий");
            var data = db.ActionLogsViews;
            return Json(data.ToDataSourceResult(request));
        }

        //return all employees which contains search text
        public List<UnitTreeItemModel> GetEmployees(string lastName)
		{
			string permissionValue = EmployePermissionHelper.IsMenuUnitEmployeeListPermission;

			List<UnitTreeItemModel> employees = new List<UnitTreeItemModel>();
			if (permissionValue == "all")
			{
				employees = (from user in db.Employees.Include(o => o.Position)
					where (user.LastName.Contains(lastName) && user.Position.PositionState != 5 && user.PositionId != null)
					select new UnitTreeItemModel
					{
						id = user.Id,
						Type = 3,
						DataId = user.Id,
						Name = user.DisplayName,
						Sex = user.Sex,
						hasChildren = false,
						ParentId = user.PositionId
					}).ToList();
			} else if (permissionValue == "departament")
			{

				Guid? positionId = UserHelper.GetCurrentEmployee().PositionId;
				Guid? departamentId = db.Units.Where(m => m.Id == positionId).Select(m => m.ParentId).First();

				employees = (from user in db.Employees.Include(o => o.Position)
					where
						(user.LastName.Contains(lastName) && user.Position.PositionState != 5 && user.Position.ParentId == departamentId &&
						 user.PositionId != null)
					select new UnitTreeItemModel
					{
						id = user.Id,
						Type = 3,
						DataId = user.Id,
						Name = user.DisplayName,
						Sex = user.Sex,
						hasChildren = false,
						ParentId = user.PositionId
					}).ToList();

				var childEmployees = (from user in db.Employees.Include(o => o.Position)
									  where
										  (user.LastName.Contains(lastName) && user.Position.PositionState != 5 && user.Position.ParentId != departamentId &&
										   user.PositionId != null)
									  select new UnitTreeItemModel {
										  id = user.Id,
										  Type = 3,
										  DataId = user.Id,
										  Name = user.DisplayName,
										  Sex = user.Sex,
										  hasChildren = false,
										  ParentId = user.PositionId
									  }).ToList();
				List<UnitTreeItemModel> child = GetChildrenEmployeeList(childEmployees, departamentId);
				employees.AddRange(child);
			}
			else
			{
				Guid employeeId = UserHelper.GetCurrentEmployee().Id;
				employees = (from user in db.Employees.Include(o => o.Position)
							 where
								 (user.LastName.Contains(lastName) && user.Id == employeeId && user.Position.PositionState != 5)
							 select new UnitTreeItemModel {
								 id = user.Id,
								 Type = 3,
								 DataId = user.Id,
								 Name = user.DisplayName,
								 Sex = user.Sex,
								 hasChildren = false,
								 ParentId = user.PositionId
							 }).ToList();
			}
			return employees;
		}

		public List<UnitTreeItemModel> GetChildrenEmployeeList(List<UnitTreeItemModel> childEmployees, Guid ? departamentId)
		{
			List<List<UnitTreeItemModel>> BranchList = new List<List<UnitTreeItemModel>>();
			List<UnitTreeItemModel> unitBranch;
			UnitTreeItemModel parentUnit = new UnitTreeItemModel();
			List<UnitTreeItemModel> result = new ListStack<UnitTreeItemModel>();

			int index = 0;
			foreach (var employee in childEmployees) {
				unitBranch = new List<UnitTreeItemModel>();
				parentUnit = employee;
				unitBranch.Add(parentUnit);
				while (parentUnit.ParentId != null || parentUnit.id==departamentId) {
					parentUnit = (from e in db.Units
								  where (e.Id == parentUnit.ParentId)
								  select new UnitTreeItemModel() {
									  id = e.Id,
									  Type = e.Type,
									  DataId = e.Id,
									  expanded = true,
									  Name = e.DisplayName,
									  hasChildren = true,
									  ParentId = e.ParentId,
								  }).First();
					unitBranch.Add(parentUnit);
				}
				BranchList.Add(unitBranch);
			}
			foreach (var item in BranchList)
			{
				foreach (var unit in item)
				{
					if (unit.id == departamentId)
					{
						index = item.IndexOf(unit);
						if (item[index - 1].Type == 1)
						{
							result.Add(item.First());
						}
					}
				}
			}
			return result;
		}

		//return full treeview by search
		public List<UnitTreeItemModel> BuildTree(List<UnitTreeItemModel> employees)
		{
			List<UnitTreeItemModel> tree = new List<UnitTreeItemModel>();
			List<List<UnitTreeItemModel>> BranchList = new List<List<UnitTreeItemModel>>();
			List<UnitTreeItemModel> unitBranch;
			UnitTreeItemModel parentUnit = new UnitTreeItemModel();

			foreach (var employee in employees)
			{
				unitBranch = new List<UnitTreeItemModel>();
				parentUnit = employee;
				unitBranch.Add(parentUnit);
				while (parentUnit.ParentId != null)
				{
					parentUnit = (from e in db.Units
						where (e.Id == parentUnit.ParentId)
						select new UnitTreeItemModel()
						{
							id = e.Id,
							Type = e.Type,
							DataId = e.Id,
							expanded = true,
							Name = e.DisplayName,
							hasChildren = true,
							ParentId = e.ParentId
						}).First();
					unitBranch.Add(parentUnit);
				}
				unitBranch.Reverse();
				BranchList.Add(unitBranch);
			}
			foreach (var item in BranchList)
			{
				BuildChild(tree, item);
			}
			return tree;
		}

		//build child in treview by search
		public List<UnitTreeItemModel> BuildChild(List<UnitTreeItemModel> tree, List<UnitTreeItemModel> child)
		{
			bool hasChild = false;

			foreach (var item in child)
			{
				foreach (var branch in tree)
				{
					if (item.id == branch.id)
					{
						tree = branch.items;
						hasChild = true;
					}
				}
				if (hasChild == false)
				{
					for (int i = child.IndexOf(item); i < child.Count; i++)
					{
						tree.Add(child[i]);
						foreach (var val in tree)
						{
							tree = val.items;
						}
					}
					return tree;
				}
				hasChild = false;
			}
			return tree;
		}

		public object List(Guid? id, string text)
		{
			if (text != null)
			{
				List<UnitTreeItemModel> employeesList = GetEmployees(text);
				List<UnitTreeItemModel> treeView = BuildTree(employeesList);
				return Json(treeView, JsonRequestBehavior.AllowGet);
			}

			string permissionValue = EmployePermissionHelper.IsMenuUnitEmployeeListPermission;



			if (permissionValue == "departament")
			{
				return GetOwnDepartmentList(id);
			}
			if (permissionValue == "all") {
				return GetAllOrganizationsList(id);
			}
			
			return GetEmployeeList(id);
		}

		public object GetEmployeeList(Guid? id)
		{
			Guid orgId = UserHelper.GetCurrentEmployee().OrganizationId;
			Guid empId = UserHelper.GetCurrentEmployee().Id;
			Guid? posId = db.Employees.Where(m => m.Id == empId).Select(m => m.PositionId).First();

			int cureentDepType = 0;
			Guid? currentDep = Guid.Empty;
			List<Guid?> depList = new List<Guid?>();
			depList.Add(posId);
			Guid? depBuf = posId;
			if (id != null) {
				cureentDepType = db.Units.Where(m => m.Id == id).Select(m => m.Type).First();

			}

			if (cureentDepType == 2)
			{
				currentDep = db.Units.Where(m => m.Id == currentDep).Select(m => m.ParentId).FirstOrDefault();

			}
			else
			{
				while (depBuf != orgId)
				{
					depBuf = db.Units.Where(m => m.Id == depBuf).Select(m => m.ParentId).FirstOrDefault();
					depList.Add(depBuf);
				}
				for (int i = 0; i < depList.Count; i++)
				{
					if (id == depList[i])
					{
						currentDep = depList[i - 1];
					}
				}
			}
			var employees = from e in db.Units
							where
								(id.HasValue
									? (e.Id == currentDep && e.PositionState != 5)
									: e.Id == orgId && e.PositionState != 5)

							orderby e.Rank, e.DisplayName
							select new UnitTreeItemModel {
								id = e.Id,
								Type = e.Type,
								DataId = e.Id,
								expanded = false,
								Name = e.DisplayName,
								hasChildren = e.Units1.Any() || db.Employees.Any(o => o.PositionId == e.Id)
							};
			if (employees.Any()) {
				return Json(employees, JsonRequestBehavior.AllowGet);
			} else {
				var users = from user in db.Employees.Include(o => o.Position)
							where (id.HasValue && user.PositionId == id.Value && user.Position.PositionState != 5)
							select new UnitTreeItemModel {
								id = user.Id,
								Type = 3,
								DataId = user.Id,
								Name = user.DisplayName,
								Sex = user.Sex,
								hasChildren = false
							};
				return Json(users, JsonRequestBehavior.AllowGet);
			}
		}

		public object GetOwnDepartmentList(Guid? id) {
			Guid orgId = UserHelper.GetCurrentEmployee().OrganizationId;
			Guid empId = UserHelper.GetCurrentEmployee().Id;
			Guid? posId = db.Employees.Where(m => m.Id == empId).Select(m => m.PositionId).First();
			int cureentDepType = 0;
			Guid? currentDep = Guid.Empty;
			List<Guid?> depList = new List<Guid?>();
			depList.Add(posId);
			Guid? depBuf = posId;
			bool isContain = false;
			bool isLast = false;

			Guid? parentPositionId = db.Units.Where(m => m.Id == posId).Select(m => m.ParentId).FirstOrDefault();
			if (id != null) {
				cureentDepType = db.Units.Where(m => m.Id == id).Select(m => m.Type).First();

			}

			if (cureentDepType == 2) {
				currentDep = db.Units.Where(m => m.Id == currentDep).Select(m => m.ParentId).FirstOrDefault();

			} else {
				while (depBuf != orgId) {
					depBuf = db.Units.Where(m => m.Id == depBuf).Select(m => m.ParentId).FirstOrDefault();
					depList.Add(depBuf);
				}
				for (int i = 0; i < depList.Count; i++) {
					if (id == depList[i]) {
						currentDep = depList[i - 1];
						isContain = true;
					}
					if (id == parentPositionId) {
						isLast = true;
					}
				}
			}
			var employees = from e in db.Units
							where
								(id.HasValue ? (isLast || !isContain ? e.ParentId==id && e.PositionState != 5 :  e.Id == currentDep && e.PositionState != 5)
									: e.Id == orgId && e.PositionState != 5)

							orderby e.Rank, e.DisplayName
							select new UnitTreeItemModel {
								id = e.Id,
								Type = e.Type,
								DataId = e.Id,
								expanded = false,
								Name = e.DisplayName,
								hasChildren = e.Units1.Any() || db.Employees.Any(o => o.PositionId == e.Id)
							};
			if (employees.Any()) {
				return Json(employees, JsonRequestBehavior.AllowGet);
			} else {
				var users = from user in db.Employees.Include(o => o.Position)
							where (id.HasValue && user.PositionId == id.Value && user.Position.PositionState != 5)
							select new UnitTreeItemModel {
								id = user.Id,
								Type = 3,
								DataId = user.Id,
								Name = user.DisplayName,
								Sex = user.Sex,
								hasChildren = false
							};
				return Json(users, JsonRequestBehavior.AllowGet);
			}
		}

		public object GetAllOrganizationsList(Guid? id) {
			Guid rcez = new Guid("8F0B91F3-AF29-4D3C-96D6-019CBBDFC8BE");
			Guid orgId = UserHelper.GetCurrentEmployee().OrganizationId;
			var employees = from e in db.Units
							where (id.HasValue
								? e.ParentId == id && e.PositionState != 5
								: e.ParentId == null
								  && e.Type == 0 && (orgId != rcez ? e.Id == orgId : e.Id != null) && e.PositionState != 5)

							orderby e.Rank, e.DisplayName
							select new UnitTreeItemModel {
								id = e.Id,
								Type = e.Type,
								DataId = e.Id,
								expanded = false,
								Name = e.DisplayName,
								hasChildren = e.Units1.Any() || db.Employees.Any(o => o.PositionId == e.Id)
							};
			if (employees.Any()) {
				return Json(employees, JsonRequestBehavior.AllowGet);
			} else {
				var users = from user in db.Employees.Include(o => o.Position)
							where (id.HasValue && user.PositionId == id.Value && user.Position.PositionState != 5)
							select new UnitTreeItemModel {
								id = user.Id,
								Type = 3,
								DataId = user.Id,
								Name = user.DisplayName,
								Sex = user.Sex,
								hasChildren = false
							};
				return Json(users, JsonRequestBehavior.AllowGet);
			}
		}

		public ActionResult OrganizationRead(Guid id, Guid? parentId) {
			Unit unit = db.Units.Find(id);
			if (unit == null || parentId.HasValue) {
				unit = new Unit() {
					Id = Guid.NewGuid(),
					ParentId = parentId,
					CreatedDate = DateTime.Now,					
				};
			}
			UnitModel model = new UnitModel(unit);
			return Content(JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy" }));
		}

		public ActionResult EmployeRead(Guid id, Guid? parentId) {
			Employee employee = db.Employees.Find(id);
			if (employee == null || parentId.HasValue) {
				employee = new Employee() {
					Id = Guid.NewGuid(),
					PositionId = parentId,
					CreatedDate = DateTime.Now,
					
				};
			}
			EmployeeModel model = new EmployeeModel(employee);

			return Content(JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy" }));
		}

		[HttpPost]
		public ActionResult OrganizationUpdate(UnitModel unit) {
			if (unit != null) {
				Unit baseUnit = db.Units.Find(unit.Id);
				if (baseUnit == null) {
					baseUnit = new Unit() {
						Id = unit.Id,
						Type = 0,
						CreatedDate = DateTime.Now,
						ModifiedDate = DateTime.Now
					};
					db.Units.Add(baseUnit);
				}
				baseUnit = unit.GetUnit(baseUnit);
				baseUnit.DisplayName = BuildDisplayName(baseUnit);
				db.SaveChanges();

				MembershipUser user = null;
				if (unit.Type == 2)
					user = Membership.GetUser(baseUnit.Employee.Login);

				if (unit.Type == 2 && unit.PositionState != 1 && user != null && user.IsApproved) {
						user.IsApproved = false;
						Membership.UpdateUser(user);
				}
				if (unit.Type == 2 && unit.PositionState == 1 && user != null && !user.IsApproved) {
					user.IsApproved = true;
					Membership.UpdateUser(user);
				}

				return Content(bool.TrueString);
			}
			return Content(bool.FalseString);
		}
		public ActionResult DepartamentUpdate(UnitModel unit) {
			if (unit != null) {
				Unit baseUnit = db.Units.Find(unit.Id);
				if (baseUnit == null) {
					baseUnit = new Unit() {
						Id = unit.Id,
						Type = 1,
						CreatedDate = DateTime.Now,
						ModifiedDate = DateTime.Now
					};
					db.Units.Add(baseUnit);
				}
				baseUnit = unit.GetUnit(baseUnit);
				baseUnit.Type = 1;
				baseUnit.DisplayName = BuildDisplayName(baseUnit);
				db.SaveChanges();

				MembershipUser user = null;
				if (unit.Type == 2)
					user = Membership.GetUser(baseUnit.Employee.Login);

				if (unit.Type == 2 && unit.PositionState != 1 && user != null && user.IsApproved) {
					user.IsApproved = false;
					Membership.UpdateUser(user);
				}
				if (unit.Type == 2 && unit.PositionState == 1 && user != null && !user.IsApproved) {
					user.IsApproved = true;
					Membership.UpdateUser(user);
				}

				return Content(bool.TrueString);
			}
			return Content(bool.FalseString);
		}
		public ActionResult PositionUpdate(UnitModel unit) {
			if (unit != null) {
				Unit baseUnit = db.Units.Find(unit.Id);
				if (baseUnit == null) {
					baseUnit = new Unit() {
						Id = unit.Id,
						Type = 2,
						CreatedDate = DateTime.Now,
						ModifiedDate = DateTime.Now
					};
					db.Units.Add(baseUnit);
				}
				baseUnit = unit.GetUnit(baseUnit);
				baseUnit.DisplayName = BuildDisplayName(baseUnit);
				baseUnit.Type = 2;
				db.SaveChanges();

				MembershipUser user = null;
				if (unit.Type == 2 && baseUnit.Employee != null && !string.IsNullOrEmpty(baseUnit.Employee.Login))
					user = Membership.GetUser(baseUnit.Employee.Login);

				if (unit.Type == 2 && unit.PositionState != 1 && user != null && user.IsApproved) {
					user.IsApproved = false;
					Membership.UpdateUser(user);
				}
				if (unit.Type == 2 && unit.PositionState == 1 && user != null && !user.IsApproved) {
					user.IsApproved = true;
					Membership.UpdateUser(user);
				}

				return Json( new {  result = bool.TrueString, ParentId = baseUnit.ParentId });
			}
			return Content(bool.FalseString);
		}
		private string BuildDisplayName(Unit unit) {
			switch (unit.Type) {
				// Organization = 0,
				case 0:
					Guid bossId;
					if (Guid.TryParse(unit.BossId, out bossId)) {
						Employee boss = db.Employees.Find(bossId);
						if (boss != null && !string.IsNullOrEmpty(boss.ShortName))
							return string.Format("{0} ({1})", unit.Name, boss.ShortName);
					}
					return unit.Name;
				// Department = 1
				case 1:
					Guid managerId;
					if (Guid.TryParse(unit.ManagerId, out managerId)) {
						Employee manager = db.Employees.Find(managerId);
						if (manager != null && !string.IsNullOrEmpty(manager.ShortName))
							return string.Format("{0} ({1})", unit.Name, manager.ShortName);
					}
					return unit.Name;
				// Position = 2
				case 2:
					if (unit.EmployeeId != null) {
						Employee employee = db.Employees.Find(unit.EmployeeId.Value);
						if (employee != null && !string.IsNullOrEmpty(employee.ShortName)) {
							employee.ShortName = BuildShortName(employee);
							employee.FullName = BuildFullName(employee);
							employee.DisplayName = BuildDisplayName(employee);
							return string.Format("{0} ({1})", unit.Name, employee.ShortName);
						}

					}
					return unit.Name;
			}
			return string.Empty;
		}
		private string BuildDisplayName(Employee employee) {
			if (employee != null) {
				if (employee.PositionId != null) {
					Unit unit = db.Units.Find(employee.PositionId);// GetUnit(employee.PositionId.ToString());

					Unit otdel = db.Units.Find(unit.ParentId);
					string otdelName = string.Empty;
					if (otdel != null)
						otdelName = otdel.ShortName;
					if (unit != null && !string.IsNullOrEmpty(unit.Name))
						return string.Format("{0} ({1} - {2})", employee.ShortName, otdelName, unit.Name);
				}
				return employee.ShortName;
			}
			return string.Empty;
		}

		private static string BuildFullName(Employee employee) {
			string fullName = string.Empty;
			if (!string.IsNullOrEmpty(employee.LastName)) {
				fullName += employee.LastName + " ";
			}
			if (!string.IsNullOrEmpty(employee.FirstName)) {
				fullName += employee.FirstName + " ";
			}
			if (!string.IsNullOrEmpty(employee.MiddleName)) {
				fullName += employee.MiddleName + " ";
			}
			return fullName.TrimEnd(' ');
		}

		private static string BuildShortName(Employee employee) {
			string shortName = string.Empty;
			if (!string.IsNullOrEmpty(employee.LastName)) {
				shortName += employee.LastName + " ";
			}
			if (!string.IsNullOrEmpty(employee.FirstName)) {
				shortName += employee.FirstName[0] + ". ";
			}
			if (!string.IsNullOrEmpty(employee.MiddleName)) {
				shortName += employee.MiddleName[0] + ". ";
			}
			return shortName.TrimEnd(' ');
		}
		[HttpPost]
		public ActionResult EmployeUpdate(EmployeeModel employee) {
			StringBuilder errorMessage = new StringBuilder();
			if (employee != null) {
				Employee baseEmployee = db.Employees.Find(employee.Id);
				if (baseEmployee == null) {
					baseEmployee = new Employee() {
						Id = employee.Id,
						CreatedDate = DateTime.Now,
						ModifiedDate = DateTime.Now
					};
					
					Unit unit = db.Units.Find(employee.PositionId);
					unit.EmployeeId = employee.Id;
					baseEmployee.OrganizationId = OrgId(unit);
					db.Employees.Add(baseEmployee);
				}
			

				baseEmployee = employee.GetEmployee(baseEmployee);
				baseEmployee.ShortName = BuildShortName(baseEmployee);
				baseEmployee.FullName = BuildFullName(baseEmployee);
				baseEmployee.DisplayName = BuildDisplayName(baseEmployee);
				if (string.IsNullOrEmpty(baseEmployee.FirstName))
					errorMessage.AppendLine("Имя не может быть пустым.<br/>");
				if (string.IsNullOrEmpty(baseEmployee.LastName))
					errorMessage.AppendLine("Фамилия не может быть пустой.<br/>");
				if (errorMessage.Length == 0)
				{
					try {
						db.SaveChanges();
					}
					catch (Exception ex) {
						
						
					}
					


					return Json(new { IsValid = true, Text = "Пользователь успешно сохранен." });
				}
			}
			return Json(new { IsValid = false, Text = errorMessage.ToString() });
		}

	    public Guid OrgId(Unit unit)
	    {
		    if (!unit.ParentId.HasValue)
		    {
			    return unit.Id;
		    }
		    unit = db.Units.Find(unit.ParentId);
			return OrgId(unit);
	    }

		[HttpPost]
		public ActionResult EmployeReset(EmployeeModel employee) {
		    string newPassword = Membership.GeneratePassword(6, 0);
            newPassword = Regex.Replace(newPassword, @"[^a-zA-Z0-9]", m => "9");
            if (employee != null) {
				MembershipUser user = Membership.GetUser(employee.Login);
				if (user != null) {
					string oldPassword = user.ResetPassword();
					user.ChangePassword(oldPassword, newPassword);
				} else {
					Membership.CreateUser(employee.Login, newPassword);
				}
				return Content("Пароль по умолчанию: " + newPassword);
			}
			return Content(bool.FalseString);
		}

		[HttpPost]
		public ActionResult EmployeLoginReset(EmployeeModel employee) {
		    string newPassword = Membership.GeneratePassword(6, 0);
            newPassword = Regex.Replace(newPassword, @"[^a-zA-Z0-9]", m => "9");
            if (employee != null)
            {
				MembershipUser user = Membership.GetUser(employee.Login);
			    if (user != null)
			        return Content("Пользователь с таким логином уже существует");
			    Employee item = db.Employees.Find(employee.Id);
			    item.Login = employee.Login;
			    db.SaveChanges();
			    Membership.CreateUser(employee.Login, newPassword);
			    return Content("Пароль по умолчанию: " + newPassword);
			}
			return Content(bool.FalseString);
		}

		[HttpPost]
		public ActionResult EmployeRelease(EmployeeModel employee) {
			if (employee != null) {
				MembershipUser user = Membership.GetUser(employee.Login);
				if (user != null)
					if (user.IsLockedOut)
						user.UnlockUser();
				return Content(bool.TrueString);
			}
			return Content(bool.FalseString);
		}

		public ActionResult EmployeDismissedList([DataSourceRequest] DataSourceRequest request)
		{
			Guid orgId = UserHelper.GetCurrentEmployee().OrganizationId;
			var list = db.Employees.Include(o => o.Position).Include(c=>c.Position.Parent).Where(o => o.Position.PositionState == 5 && o.OrganizationId == orgId);
			return Json(list.Select(o => new { o.Id, o.DisplayName, o.TerminationDate, LastName = o.Position.Parent.DisplayName }).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
		}

		public ActionResult EmployeDismissedListDep([DataSourceRequest] DataSourceRequest request)
		{
			Guid orgId = UserHelper.GetCurrentEmployee().OrganizationId;
			Guid depId = UserHelper.GetDepartment().Id;
			var list = db.Employees.Include(o => o.Position).Include(c => c.Position.Parent).Include("Position.Parent.Parent").Include("Position.Parent.Parent.Parent").Where(o => o.Position.PositionState == 5 && o.OrganizationId == orgId && (o.Position.ParentId== depId || (o.Position.Parent!=null && o.Position.Parent.ParentId == depId ) || (o.Position.Parent!=null && o.Position.Parent.Parent!=null && o.Position.Parent.Parent.ParentId == depId)));
			return Json(list.Select(o => new { o.Id, o.DisplayName, o.TerminationDate, LastName = o.Position.Parent.DisplayName }).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
		}

		protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
