using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Repository.CertificateOfCompletion;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Database.Repository.DictionaryRep;
using PW.Ncels.Database.Repository.DirectionToPay;
using PW.Prism.ViewModels.CertificateOfCompletion;

namespace PW.Prism.Controllers
{
    public class CertificateOfCompletionController : Controller
    {
        // GET: CertificateOfCompletion
        public ActionResult Index(int stageId, Guid drugDeclarationId)
        {
            var viewModel = new CertificateOfCompletionViewModel()
            {
                Id = Guid.NewGuid(),
                StageId = stageId,
                DrugDeclarationId = drugDeclarationId
            };

            return PartialView(viewModel);
        }
        
        public JsonResult ReadCertificateOfCompletionList([DataSourceRequest] DataSourceRequest request, int stageId, Guid drugDeclarationId)
        {
            CertificateOfCompletionRepository repository = new CertificateOfCompletionRepository();
            var currentUserId = UserHelper.GetCurrentEmployee().Id;
            var data = repository.GetCocViewAsQuarable(coc => coc.DicStageId == stageId && coc.DrugDeclarationId == drugDeclarationId && coc.DeleteDate == null
            &&(coc.CreateEmployeeId == currentUserId || coc.TaskExecutorId == currentUserId));

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult CreateCertificateOfCompletion(int stageId, Guid drugDeclarationId)
        {
            CertificateOfCompletionRepository repository = new CertificateOfCompletionRepository();
            DictionaryRepository dictionary = new DictionaryRepository(false);
            var newStatusId = dictionary.GetDictionaryElementIdByTypeAndCode(Dictionary.CertificateOfCompletionStatusDic.DicCode,
                Dictionary.CertificateOfCompletionStatusDic.New);

            DirectionToPayRepository directionRepository = new DirectionToPayRepository(false);
            var directionToPay =
                directionRepository.GetAsQuarable(
                    d => d.EXP_DrugDeclaration.Any(dd => dd.Id == drugDeclarationId) &&
                        d.Type == EXP_DirectionToPaysView.ExpertWorkType && d.DeleteDate == null).FirstOrDefault();

            if (directionToPay != null)
            {
                var cert = repository.GetAsQuarable(coc => coc.DicStageId == stageId && coc.DrugDeclarationId == drugDeclarationId && coc.DeleteDate == null).FirstOrDefault();
                bool isExist = cert != null;

                if (!isExist)
                    cert = new EXP_CertificateOfCompletion()
                    {
                        Id = Guid.NewGuid(),
                        Number = Registrator.GetNumber("CertificateOfCompletion").ToString(),
                        CreateEmployeeId = UserHelper.GetCurrentEmployee().Id,
                        CreateDate = DateTime.Now,
                        DeleteDate = null,
                        ModifyDate = null,
                        SendDate = null,
                        StatusId = newStatusId,
                        DicStageId = stageId,
                        DrugDeclarationId = drugDeclarationId
                    };

                switch (stageId)
                {
                    case CodeConstManager.STAGE_PRIMARY:
                        cert.TotalPrice = directionToPay.TotalPrice * (decimal)0.7;
                        break;
                    case CodeConstManager.STAGE_SAFETYREPORT:
                        cert.TotalPrice = directionToPay.TotalPrice * (decimal)0.3;
                        break;
                    default:
                        break;
                }

                if (isExist)
                {
                    repository.Update(cert);
                }
                else
                {
                    repository.Insert(cert);
                }
                repository.Save();
            }
            else
            {
                return Json(new {IsSuccess = false, Message="Отсутствует направление на оплату"}, JsonRequestBehavior.AllowGet);
            }

            return Json(new { IsSuccess = true, Message = "" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(Guid id)
        {
            bool isOk = true;
            try
            {
                CertificateOfCompletionRepository repository = new CertificateOfCompletionRepository();
                repository.Delete(id);
                repository.Save();
            }
            catch (Exception e)
            {
                isOk = false;
            }
            return Json(isOk, JsonRequestBehavior.AllowGet);
        }
    }
}