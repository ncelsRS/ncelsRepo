using PW.Ncels.Database.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PW.Prism.Controllers.OBK_OP
{
    public class OPProgramController : Controller
    {
        public ncelsEntities repo = new ncelsEntities();

        [HttpGet]
        public ActionResult Program(Guid declarationId)
        {
            return PartialView(declarationId);
        }

        [HttpPost]
        public ActionResult SaveProgram(OBK_AssessmentDeclarationProgram program)
        {
            var oldProgram = repo.OBK_AssessmentDeclarationProgram.FirstOrDefault(x => x.DeclarationId == program.DeclarationId);
            if (oldProgram != null)
            {
                program.Id = oldProgram.Id;
                repo.OBK_AssessmentDeclarationProgram.Remove(oldProgram);
            }
            if (program.Id == null)
                program.Id = Guid.NewGuid();
            repo.OBK_AssessmentDeclarationProgram.Add(program);
            repo.SaveChanges();
            return Json(new { isSuccess = true, data = program });
        }

        [HttpPost]
        public ActionResult ProgramSendToConfirm(OBK_AssessmentDeclarationProgram program)
        {
            return Json(new { isSuccess = true, data = program });
        }
    }
}