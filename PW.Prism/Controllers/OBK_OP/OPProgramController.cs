using Kendo.Mvc.Extensions;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Prism.ViewModels.OBK.OP;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PW.Prism.Controllers.OBK_OP
{
    public class OPProgramController : Controller
    {
        private readonly ncelsEntities repo;

        const int programStageId = 16; // in dbo.OBK_Ref_Stages

        public OPProgramController()
        {
            repo = new ncelsEntities();
        }

        [HttpGet]
        public ActionResult Program(Guid declarationId)
        {
            return PartialView(declarationId);
        }

        [HttpGet]
        public ActionResult LoadProgram(Guid declarationId)
        {
            var res = repo.OBK_AssessmentDeclarationProgram
                .FirstOrDefault(x => x.DeclarationId == declarationId)
                .ToVM();
            res.Attach = FileHelper.GetAttachListEdit(repo, declarationId.ToString(), "assessmentDeclarationOPProgram")
                .ToDataSourceResult(new Kendo.Mvc.UI.DataSourceRequest());
            return Json(new { isSuccess = true, data = res }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveProgram(AssessmentProgramVM program)
        {
            var oldProgram = repo.OBK_AssessmentDeclarationProgram.FirstOrDefault(x => x.DeclarationId == program.DeclarationId);
            if (oldProgram != null)
            {
                program.Id = oldProgram.Id;
                repo.OBK_AssessmentDeclarationProgram.Remove(oldProgram);
            }
            program.Id = Guid.NewGuid();
            repo.OBK_AssessmentDeclarationProgram.Add(program.ToDB());
            repo.SaveChanges();
            return Json(new { isSuccess = true, data = program }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SendToWork(Guid declarationId)
        {
            var stage = repo.OBK_AssessmentStage
                .FirstOrDefault(x => x.StageId == programStageId && x.DeclarationId == declarationId);
            if (stage == null)
                return Json(new { isError = true, data = "Stage not found" });
            stage.StageStatusId = repo.OBK_Ref_StageStatus
                .Where(x => x.Code == "inWork")
                .Select(x => x.Id)
                .FirstOrDefault();
            repo.SaveChanges();
            return Json(new { isSuccess = true }, JsonRequestBehavior.AllowGet);
        }


        #region SelectExecutors
        [HttpGet]
        public ActionResult ListExecutors(Guid declarationId)
        {
            var stage = repo.OBK_AssessmentStage.FirstOrDefault(x => x.DeclarationId == declarationId && x.StageId == programStageId);
            if (stage == null)
            {
                stage = new OBK_AssessmentStage
                {
                    Id = Guid.NewGuid(),
                    StartDate = DateTime.Now,
                    DeclarationId = declarationId,
                    StageId = programStageId,
                    StageStatusId = repo.OBK_Ref_StageStatus.Where(x => x.Code == "inQueue").Select(x => x.Id).FirstOrDefault()
                };
                repo.OBK_AssessmentStage.Add(stage);
                repo.SaveChanges();
                var members = repo.OBK_OP_Commission
                    .Where(x => x.DeclarationId == declarationId)
                    .Select(x => x.EmployeeId)
                    .ToList();
                members.ForEach(member =>
                {
                    var executor = new OBK_AssessmentStageExecutors
                    {
                        AssessmentStageId = stage.Id,
                        ExecutorId = member,
                        ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR
                    };
                    repo.OBK_AssessmentStageExecutors.Add(executor);
                });
                repo.SaveChanges();
            }
            var executors = repo.OBK_AssessmentStageExecutors
                .Where(x => x.AssessmentStageId == stage.Id)
                .Select(x => new
                {
                    x.ExecutorId,
                    x.Employee.FullName
                });

            return Json(new { isSuccess = true, data = executors }, JsonRequestBehavior.AllowGet);
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

        public ActionResult ListEmployees(Guid declarationId, Guid unitId)
        {
            var employeeIds = repo.OBK_AssessmentStageExecutors
                .Where(c => c.OBK_AssessmentStage.DeclarationId == declarationId
                    && c.OBK_AssessmentStage.StageId == 16)
                .Select(e => e.ExecutorId);
            var employees = repo.Employees
                .Where(x => (
                    x.Position.ParentId == unitId
                    || x.Position.Parent.ParentId == unitId
                    || x.Position.Parent.Parent.ParentId == unitId)
                    && !employeeIds.Contains(x.Id))
                .Select(x => new { x.Id, x.FullName, PositionName = x.Position.Name }).ToList();
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpsertStageExecutor(Guid employeeId, Guid declarationId)
        {
            var stageIds = repo.OBK_AssessmentStage
                .Where(x => x.StageId == programStageId && x.DeclarationId == declarationId)
                .Select(x => x.Id);

            var old = repo.OBK_AssessmentStageExecutors
                .FirstOrDefault(x =>
                    stageIds.Contains(x.AssessmentStageId)
                    && x.ExecutorId == employeeId);

            if (old == null)
            {
                var newExecutor = new OBK_AssessmentStageExecutors
                {
                    ExecutorId = employeeId,
                    ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR,
                    AssessmentStageId = stageIds.FirstOrDefault()
                };

                repo.OBK_AssessmentStageExecutors.Add(newExecutor);

                repo.SaveChanges();
            }

            return Json(new { isSuccess = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveStageExecutor(Guid executorId, Guid declarationId)
        {
            var old = repo.OBK_AssessmentStageExecutors.FirstOrDefault(x => x.ExecutorId == executorId);
            repo.OBK_AssessmentStageExecutors.Remove(old);
            repo.SaveChanges();
            return Json(new { isSuccess = true }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}