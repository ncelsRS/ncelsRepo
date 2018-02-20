using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
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
                var unit = repo.Units.FirstOrDefault(u => u.ParentId == employee.OrganizationId
                        && (u.Id == employee.Position.ParentId
                            || u.Id == employee.Position.Parent.ParentId
                            || u.Id == employee.Position.Parent.Parent.ParentId));
                return new
                {
                    x.Id,
                    employee.OrganizationId,
                    Organization = employee.Organization.Name,
                    UnitId = unit?.Id,
                    Unit = unit?.Name,
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
                .Where(x => x.Code == OrganizationConsts.NCELS)
                .Select(x => new { x.Id, x.Name })
                .OrderBy(x => x.Name)
                .ToList();
            return Json(orgs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListUnits(Guid organizationId)
        {
            var units = repo.Units
                .Where(x => x.ParentId == organizationId)
                .Select(x => new { x.Id, x.Name })
                .OrderBy(x => x.Name)
                .ToList();
            return Json(units, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListPositions(Guid employeeId = new Guid())
        {
            var employee = repo.Employees.FirstOrDefault(x => x.Id == employeeId);
            var positions = repo.Units
                .Where(x => x.PositionState == 1 && x.Id == employee.PositionId)
                .Select(x => new { x.Id, x.Name })
                .OrderBy(x => x.Name)
                .ToList();
            return Json(positions, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListEmployees(Guid declarationId, Guid unitId, string mode)
        {
            var employeeIds = repo.OBK_OP_Commission.Where(c => c.DeclarationId == declarationId).Select(e => e.EmployeeId);
            var employees = repo.Employees
                .Where(x => (
                    x.Position.ParentId == unitId
                    || x.Position.Parent.ParentId == unitId
                    || x.Position.Parent.Parent.ParentId == unitId)
                    && (!employeeIds.Contains(x.Id) || mode == "2"))
                .Select(x => new { x.Id, x.FullName, PositionName = x.Position.Name })
                .OrderBy(x => x.FullName)
                .ToList();
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListRoles(Guid declarationId, string mode)
        {
            var chairManeRoleId = repo.OBK_OP_CommissionRoles.Single(x => x.Code == "Chairman").Id;
            var isHasChairman = mode == "2" 
                ? false
                : repo.OBK_OP_Commission.Any(c => c.DeclarationId == declarationId && c.RoleId == chairManeRoleId);
            var roles = repo.OBK_OP_CommissionRoles
                .Where(r => !isHasChairman || r.Id != chairManeRoleId)
                .Select(x => new { x.Id, Name = x.NameRu }).ToList();
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
                memberOld.EmployeeId = member.EmployeeId;
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

            var executor = repo.OBK_AssessmentStageExecutors.FirstOrDefault(x => x.AssessmentStageId == declarationStage.Id && x.ExecutorType == 2);
            if (executor == null)
            {
                executor = new OBK_AssessmentStageExecutors
                {
                    AssessmentStageId = declarationStage.Id,
                    ExecutorId = UserHelper.GetCurrentEmployee().Id,
                    ExecutorType = 2
                };
                repo.OBK_AssessmentStageExecutors.Add(executor);
            }
            else
            {
                executor.ExecutorId = UserHelper.GetCurrentEmployee().Id;
            }

            var stage = repo.OBK_AssessmentStage.FirstOrDefault(x => x.DeclarationId == declaration.Id && x.StageId == 2);
            stage.StageStatusId = 13;
            stage.EndDate = DateTime.Now;
            stage.FactEndDate = stage.EndDate;
            declaration = repo.OBK_AssessmentDeclaration.FirstOrDefault(x => x.Id == declaration.Id);
            var status = repo.OBK_Ref_Status.FirstOrDefault(x => x.Code == "30");
            declaration.StatusId = status.Id;
            repo.SaveChanges();
            return Json(new { isSuccess = true });
        }
    }
}