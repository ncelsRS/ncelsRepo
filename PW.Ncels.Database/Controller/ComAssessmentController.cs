using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Repository.Expertise;
using PW.Ncels.Database.Repository.OBK;

namespace PW.Ncels.Database.Controller
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ComAssessmentController : System.Web.Mvc.Controller
    {
        [HttpGet]
        public ActionResult ShowComment(string modelId, string idControl)
        {
            var repository = new SafetyAssessmentRepository();
            var model = repository.GetComments(modelId, idControl);
            if (model == null)
            {
                model = new OBK_AssessmentDeclarationCom();
            }
            model.ObkAssessmentDeclarationFieldHistories = repository.GetFieldHistories(modelId, idControl);
            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }

            return View(model);
        }
        [HttpPost]
        public virtual ActionResult SaveComment(string modelId, string idControl, bool isError, string comment, string fieldValue, string userId, string fieldDisplay)
        {
            new SafetyAssessmentRepository().SaveComment(modelId, idControl, isError, comment, fieldValue, userId, fieldDisplay);
            return Json(new { Success = true });
        }
    }
}
