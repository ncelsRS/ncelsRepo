using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PW.Prism.Controllers.OBK_OP
{
    public class OPController : Controller
    {
        public readonly Guid UOBK = new Guid("f5eb95b6-fdf0-4f5a-afb6-85839419aa93");


        public ncelsEntities repo = new ncelsEntities();

        // GET: OP
        public ActionResult Index(Guid id)
        {
            var userId = UserHelper.GetCurrentEmployee().Id;
            ViewBag.IsExecutor = repo.OBK_AssessmentStageExecutors
                .Any(x => x.OBK_AssessmentStage.DeclarationId == id
                          && x.OBK_AssessmentStage.OBK_Ref_Stage.Code == "15"
                          && x.ExecutorId == userId);
            return PartialView(id);
        }

        [HttpPost]
        public ActionResult SendToUOBK(Guid declarationId)
        {
            var declarationStage = repo.OBK_AssessmentStage.FirstOrDefault(x => x.DeclarationId == declarationId && x.StageId == 3);
            if (declarationStage == null)
            {
                declarationStage = new OBK_AssessmentStage
                {
                    Id = Guid.NewGuid(),
                    StageId = 2,
                    StageStatusId = 1,
                    DeclarationId = declarationId,
                    StartDate = DateTime.Now
                };
                repo.OBK_AssessmentStage.Add(declarationStage);
            }
            else
            {
                declarationStage.StageStatusId = 1;
                declarationStage.StartDate = DateTime.Now;
            }

            var executor = repo.OBK_AssessmentStageExecutors.FirstOrDefault(x => x.AssessmentStageId == declarationStage.Id && x.ExecutorType == 1);

            var employeeIdstr = repo.Units.Where(x => x.Id == UOBK).Select(x => x.BossId).FirstOrDefault();
            var employeeId = new Guid(employeeIdstr);

            if (executor == null)
            {
                executor = new OBK_AssessmentStageExecutors
                {
                    AssessmentStageId = declarationStage.Id,
                    ExecutorId = employeeId,
                    ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_ASSIGNING
                };
                repo.OBK_AssessmentStageExecutors.Add(executor);
            }
            else
            {
                executor.ExecutorId = employeeId;
            }

            var stage = repo.OBK_AssessmentStage.FirstOrDefault(x => x.DeclarationId == declarationId && x.StageId == 15);
            stage.StageStatusId = 14;
            stage.EndDate = DateTime.Now;
            stage.FactEndDate = stage.EndDate;
            var declaration = repo.OBK_AssessmentDeclaration.FirstOrDefault(x => x.Id == declarationId);
            var status = repo.OBK_Ref_Status.FirstOrDefault(x => x.Code == "6");
            declaration.StatusId = status.Id;

            var reportOP = repo.OBK_AssessmentReportOP.FirstOrDefault(x => x.DeclarationId == declarationId);
            reportOP.StageStatusId = repo.OBK_Ref_StageStatus.Where(x => x.Code == "OPReportCompleted").Select(x => x.Id).FirstOrDefault();

            repo.SaveChanges();
            return Json(new { isSuccess = true });
        }

        public ActionResult SendToEC(Guid declarationId, int expertCouncilId)
        {
            try
            {
                var stage = repo.OBK_AssessmentStage.FirstOrDefault(x => x.DeclarationId == declarationId && x.OBK_Ref_Stage.Code == "15");
                stage.StageStatusId = repo.OBK_Ref_StageStatus.Where(x => x.Code == "OPReportOnEC").Select(x => x.Id).Single();

                var relationship = repo.OBK_AssessmentDeclaration__OBK_ExpertCouncil
                    .FirstOrDefault(x => x.DeclarationId == declarationId && x.ExpertCouncilId == expertCouncilId);
                if (relationship == null)
                {
                    var newRel = new OBK_AssessmentDeclaration__OBK_ExpertCouncil
                    {
                        DeclarationId = declarationId,
                        ExpertCouncilId = expertCouncilId
                    };
                    repo.OBK_AssessmentDeclaration__OBK_ExpertCouncil.Add(newRel);
                }

                var report = repo.OBK_AssessmentReportOP.FirstOrDefault(x => x.DeclarationId == declarationId);
                report.StageStatusId = repo.OBK_Ref_StageStatus
                    .Where(x => x.Code == "OPReportCompleted")
                    .Select(x => x.Id)
                    .FirstOrDefault();

                repo.SaveChanges();

                return Json(new { isSuccess = true }, JsonRequestBehavior.AllowGet);
            }
            catch (ArgumentException ex)
            {
                Response.StatusCode = 400;
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
        }

    }
}