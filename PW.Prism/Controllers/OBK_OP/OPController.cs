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
        public ncelsEntities repo = new ncelsEntities();

        // GET: OP
        public ActionResult Index(Guid id)
        {
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
                    StageId = 3,
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

            if (executor == null)
            {
                executor = new OBK_AssessmentStageExecutors
                {
                    AssessmentStageId = declarationStage.Id,
                    ExecutorId = new Guid("14D1A1F0-9501-4232-9C29-E9C394D88784"),
                    ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_ASSIGNING
                };
                repo.OBK_AssessmentStageExecutors.Add(executor);
            }
            else
            {
                executor.ExecutorId = new Guid("14D1A1F0-9501-4232-9C29-E9C394D88784");
            }

            var stage = repo.OBK_AssessmentStage.FirstOrDefault(x => x.DeclarationId == declarationId && x.StageId == 15);
            stage.StageStatusId = 14;
            stage.EndDate = DateTime.Now;
            stage.FactEndDate = stage.EndDate;
            var declaration = repo.OBK_AssessmentDeclaration.FirstOrDefault(x => x.Id == declarationId);
            var status = repo.OBK_Ref_Status.FirstOrDefault(x => x.Code == "6");
            declaration.StatusId = status.Id;
            repo.SaveChanges();
            return Json(new { isSuccess = true });
        }

        [HttpGet]
        public ActionResult Program(Guid declarationId)
        {
            return PartialView(declarationId);
        }

    }
}