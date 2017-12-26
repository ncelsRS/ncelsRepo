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

        public ActionResult SelectCommissionOP(Guid id)
        {
            var model = repo.OBK_AssessmentDeclaration.FirstOrDefault(x => x.Id == id);
            return PartialView(model);
        }

        public ActionResult GetOBK_OP_Commission(Guid id)
        {
            var members = repo.OBK_OP_Commission.Where(x => x.DeclarationId == id).ToList();
            var result = members.Select(x =>
            {
                var employee = x.Employee;
                return new
                {
                    x.Id,
                    employee.OrganizationId,
                    Organization = employee.Organization.Name,
                    UnitId = employee.Position.ParentId,
                    Unit = repo.Units.FirstOrDefault(u => u.Id == employee.Position.ParentId)?.Name,
                    Position = employee.Position.Name,
                    EmployeeId = employee.Id,
                    FIO = employee.FullName,
                    RoleId = x.RoleId,
                    RoleName = x.OBK_OP_CommissionRoles.NameRu
                };
            });
            return Json(new { isSuccess = true, result });
        }

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

        public ActionResult ListPositions(Guid employeeId = new Guid())
        {
            var employee = repo.Employees.FirstOrDefault(x => x.Id == employeeId);
            var positions = repo.Units
                .Where(x => x.PositionState == 1 && x.Id == employee.PositionId)
                .Select(x => new { x.Id, x.Name }).ToList();
            return Json(positions, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListEmployees(Guid unitId)
        {
            var positions = repo.Units
                .Where(x => x.PositionState == 1 && x.ParentId == unitId)
                .Select(x => x.Id).ToList();
            var employees = repo.Employees
                .Where(x => positions.Contains(x.PositionId ?? new Guid()))
                .Select(x => new { x.Id, x.FullName, PositionName = x.Position.Name }).ToList();
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListRoles()
        {
            var roles = repo.OBK_OP_CommissionRoles.Select(x => new { x.Id, Name = x.NameRu }).ToList();
            return Json(roles, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveMember(OBK_OP_Commission member)
        {
            if (member.Id == new Guid())
            {
                member.Id = Guid.NewGuid();
                member.DateOfCreation = DateTime.Now;
                repo.OBK_OP_Commission.Add(member);
            }
            else
            {
                var memberOld = repo.OBK_OP_Commission.FirstOrDefault(x => x.Id == member.Id);
                memberOld.RoleId = member.RoleId;
            }
            repo.SaveChanges();
            return Json(new { });
        }

        [HttpPost]
        public ActionResult RemoveMember(OBK_OP_Commission member)
        {
            var old = repo.OBK_OP_Commission.FirstOrDefault(x => x.Id == member.Id);
            repo.OBK_OP_Commission.Remove(old);
            repo.SaveChanges();
            return Json(new { isSuccess = true });
        }

        [HttpPost]
        public ActionResult SendToOP(OBK_AssessmentDeclaration declaration)
        {
            var declarationStage = repo.OBK_AssessmentStage.FirstOrDefault(x => x.DeclarationId == declaration.Id && x.StageId == 15);
            if (declarationStage == null)
            {
                declarationStage = new OBK_AssessmentStage
                {
                    Id = Guid.NewGuid(),
                    StageId = 15,
                    StageStatusId = 2,
                    DeclarationId = declaration.Id,
                    StartDate = DateTime.Now
                };
                repo.OBK_AssessmentStage.Add(declarationStage);
            }
            else
            {
                declarationStage.StageStatusId = 2;
                declarationStage.StartDate = DateTime.Now;
            }
            var stage = repo.OBK_AssessmentStage.FirstOrDefault(x => x.DeclarationId == declaration.Id && x.StageId == 2);
            repo.SaveChanges();
            return Json(new { isSuccess = true });
        }
    }
}