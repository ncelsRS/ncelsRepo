using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Ncels.Helpers;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models.OBK;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Database.Repository.OBK;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PW.Prism.Controllers.OBKContract
{
    public class OBKContractController : Controller
    {
        private ncelsEntities db = UserHelper.GetCn();
        private OBKContractRepository obkRepo;

        public OBKContractController()
        {
            obkRepo = new OBKContractRepository();
        }

        // GET: OBKContract
        public ActionResult Index()
        {
            return PartialView(Guid.NewGuid());
        }

        public ActionResult ListContract([DataSourceRequest] DataSourceRequest request)
        {
            IQueryable<OBK_ContractRegisterView> query = obkRepo.GetContracts();
            //var xxx = Json(query.ToDataSourceResult(request));
            return Json(query.ToDataSourceResult(request));
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Document document = db.Documents.Find(id);
            //if (document == null)
            //{
            //    return HttpNotFound();
            //}
            ViewBag.UiId = Guid.NewGuid().ToString();

            var contractTypes = db.OBK_Ref_Type.Where(x => x.ViewOption == CodeConstManager.OBK_VIEW_OPTION_SHOW_ON_CREATE).OrderBy(x => x.Id).Select(o => new { o.Id, Name = o.NameRu, o.Code, o.NameKz });
            var expertOrganizations = obkRepo.GetExpertOrganizations();
            var signers = obkRepo.GetSigners();

            var countries = db.Dictionaries.Where(x => x.Type == "Country").ToList();
            var organizationForms = db.Dictionaries.Where(x => x.Type == "OpfType").ToList();
            var docTypes = db.Dictionaries.Where(x => x.Type == "OBKContractDocumentType").ToList();
            var currencies = db.Dictionaries.Where(x => x.Type == "Currency").ToList();


            var contract = obkRepo.LoadContract(id.Value);
            var declarant = obkRepo.GetDeclarant(id.Value);

            //declarant.NameEn
            //

            var productInfo = obkRepo.GetProducts(id.Value);
            var prices = obkRepo.GetContractPrices(id.Value);

            var obkContract = db.OBK_Contract.Where(x => x.Id == id).FirstOrDefault();
            obkContract.ObkRsProductCount = productInfo.Count;
            ViewBag.Contract = obkContract;
            ViewBag.ContractTypes = new SelectList(contractTypes, "Id", "Name", obkContract.Type);
            ViewBag.ExpertOrganizations = new SelectList(expertOrganizations, "Id", "Name", obkContract.ExpertOrganization);
            ViewBag.Signers = new SelectList(signers, "Id", "Name", obkContract.Signer);
            ViewBag.Countries = new SelectList(countries, "Id", "Name", declarant.CountryId);
            ViewBag.OrganizationForms = new SelectList(organizationForms, "Id", "Name", declarant.OrganizationFormId);
            Guid selectedNonResident = Guid.Empty;
            if (declarant.IsConfirmed)
            {
                selectedNonResident = declarant.Id.Value;
            }
            ViewBag.NamesNonResidents = new SelectList((IEnumerable<object>)obkRepo.GetNamesNonResidents(declarant.CountryId), "Id", "Name", selectedNonResident);
            ViewBag.DocTypes = new SelectList(docTypes, "Id", "Name", contract.BossDocType);
            ViewBag.BoolValues = new SelectList(new List<SelectListItem> {
                new SelectListItem { Selected = false, Text="Нет", Value = false.ToString()},
                new SelectListItem { Selected = false, Text="Да", Value = true.ToString()},
            }, "Value", "Text", contract.IsHasBossDocNumber.ToString());
            ViewBag.Currencies = new SelectList(currencies, "Id", "Name", contract.CurrencyId);
            ViewData["Courrency"] = new SelectList(currencies, "Id", "Name");
            ViewBag.declarant = declarant;
            ViewBag.productInfo = productInfo;
            ViewBag.prices = prices;

            ViewBag.ShowProductComments = true;
            ViewBag.HidePriceAndCurrency = true;
            #region Attachments
            var repository = new UploadRepository();
            string type = "";
            if (declarant.IsResident)
            {
                type = CodeConstManager.ATTACH_CONTRACT_FILE_RESIDENT;
            }
            else
            {
                type = CodeConstManager.ATTACH_CONTRACT_FILE_NON_RESIDENT;
            }
            var list = repository.GetAttachListEdit(id, type);
            ViewBag.ListAttachments = list;
            #endregion


            ViewBag.ShowMeetsRequirementsBtn = IsMeetsRequirementsBtnAllowed(id.Value);
            ViewBag.ShowDoesNotMeetRequirementsBtn = ViewBag.ShowMeetsRequirementsBtn;
            ViewBag.ShowReturnToApplicantBtn = IsShowReturnToApplicantBtnAllowed(id.Value);
            ViewBag.ShowSendToBossForApprovalBtn = IsShowSendToBossForApprovalBtnAllowed(id.Value);
            ViewBag.ShowSendToBossForApprovalWithWarningBtn = false;
            string questionMessage = "";
            if (ViewBag.ShowSendToBossForApprovalBtn == false)
            {
                ViewBag.ShowSendToBossForApprovalWithWarningBtn = IsShowSendToBossForApprovalBtnWithWarningAllowed(id.Value, out questionMessage);
            }
            ViewBag.QuestionMessage = questionMessage;
            var isShowDoApprovementBtnAndShowRefuseApprovementBtnAllowed = IsShowDoApprovementBtnAllowed(id.Value);
            ViewBag.ShowDoApprovementBtn = isShowDoApprovementBtnAndShowRefuseApprovementBtnAllowed;
            ViewBag.ShowRefuseApprovementBtn = isShowDoApprovementBtnAndShowRefuseApprovementBtnAllowed;
            ViewBag.ShowShowRefuseReasonBtn = IsShowRefuseReasonBtnAllowed(id.Value);
            ViewBag.ShowRegisterBtn = IsRegisterBtnAllowed(id.Value);
            ViewBag.ShowAttachContractBtn = IsAttachContractBtnAllowed(id.Value);
            ViewBag.ShowSignContractBtn = IsSignContractBtnAllowed(id.Value);


            return PartialView("Contract", contract);
        }

        private bool IsMeetsRequirementsBtnAllowed(Guid contractId)
        {
            bool result = false;

            if (EmployePermissionHelper.CanViewMeetAndNotMeetRqrmntsBtnObkContract)
            {
                var employeeId = UserHelper.GetCurrentEmployee().Id;

                var stages = db.OBK_ContractRegisterView.Where(x => x.ContractId == contractId && x.ExecutorId == employeeId && x.StageStatusCode == OBK_Ref_StageStatus.InWork);

                Guid cozStageId = Guid.Empty;
                Guid uobkStageId = Guid.Empty;
                Guid defStageId = Guid.Empty;
                foreach (var item in stages)
                {
                    if (item.ContractStageStageId == CodeConstManager.STAGE_OBK_COZ)
                    {
                        cozStageId = item.ContractStageId;
                        break;
                    }
                    if (item.ContractStageStageId == CodeConstManager.STAGE_OBK_UOBK)
                    {
                        uobkStageId = item.ContractStageId;
                        break;
                    }
                    if (item.ContractStageStageId == CodeConstManager.STAGE_OBK_DEF)
                    {
                        defStageId = item.ContractStageId;
                        break;
                    }
                }

                if (cozStageId != Guid.Empty)
                {
                    var stageObk = db.OBK_ContractStage.Where(x => x.Id == cozStageId).FirstOrDefault();
                    //var childStages = db.OBK_ContractStage.Where(x => x.ParentStageId == cozStageId).ToList();

                    //int notFinishedCount = 0;
                    //foreach (var childStage in childStages)
                    //{
                    //    if (childStage.ResultId == null || childStage.ResultId == CodeConstManager.OBK_RESULT_ID_NOT_STARTED)
                    //    {
                    //        notFinishedCount++;
                    //    }

                    //    var childStagesOfChild = db.OBK_ContractStage.Where(x => x.ParentStageId == childStage.Id).ToList();
                    //    foreach (var childStageOfChild in childStagesOfChild)
                    //    {
                    //        if (childStageOfChild.ResultId == null || childStageOfChild.ResultId == CodeConstManager.OBK_RESULT_ID_NOT_STARTED)
                    //        {
                    //            notFinishedCount++;
                    //        }
                    //    }
                    //}

                    //if (notFinishedCount == 0)
                    //{
                    if (stageObk.ResultId == null || stageObk.ResultId == CodeConstManager.OBK_RESULT_ID_NOT_STARTED)
                    {
                        result = true;
                    }
                    //}
                }
                if (uobkStageId != Guid.Empty)
                {
                    var stage = db.OBK_ContractStage.Where(x => x.Id == uobkStageId).FirstOrDefault();
                    if (stage.ResultId == null || stage.ResultId == CodeConstManager.OBK_RESULT_ID_NOT_STARTED)
                    {
                        result = true;
                    }
                }
                if (defStageId != Guid.Empty)
                {
                    var stage = db.OBK_ContractStage.Where(x => x.Id == defStageId).FirstOrDefault();
                    if (stage.ResultId == null || stage.ResultId == CodeConstManager.OBK_RESULT_ID_NOT_STARTED)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        private bool IsShowReturnToApplicantBtnAllowed(Guid contractId)
        {
            bool result = false;

            if (EmployePermissionHelper.CanViewReturnToApplicantAndSendToBossForApproval)
            {
                var employeeId = UserHelper.GetCurrentEmployee().Id;

                var stages = db.OBK_ContractRegisterView.Where(x => x.ContractId == contractId && x.ExecutorId == employeeId && (x.StageStatusCode == OBK_Ref_StageStatus.InWork || x.StageStatusCode == OBK_Ref_StageStatus.NotAgreed));
                Guid cozStageId = Guid.Empty;
                foreach (var item in stages)
                {
                    if (item.ContractStageStageId == CodeConstManager.STAGE_OBK_COZ)
                    {
                        cozStageId = item.ContractStageId;
                        break;
                    }
                }

                if (cozStageId != Guid.Empty)
                {
                    var stageObk = db.OBK_ContractStage.Where(x => x.Id == cozStageId).FirstOrDefault();
                    if (stageObk.OBK_Ref_StageStatus.Code == OBK_Ref_StageStatus.InWork)
                    {
                        int notFinishedCount = 0;
                        int notApprovedCount = 0;
                        if (stageObk.ResultId == null || stageObk.ResultId == CodeConstManager.OBK_RESULT_ID_NOT_STARTED)
                        {
                            return result;
                        }
                        if (stageObk.ResultId == CodeConstManager.OBK_RESULT_ID_DOES_NOT_MEET_REQUIREMENTS)
                        {
                            notApprovedCount++;
                        }
                        var childStages = db.OBK_ContractStage.Where(x => x.ParentStageId == cozStageId).ToList();
                        foreach (var childStage in childStages)
                        {
                            if (childStage.ResultId == null || childStage.ResultId == CodeConstManager.OBK_RESULT_ID_NOT_STARTED)
                            {
                                notFinishedCount++;
                            }
                            if (childStage.ResultId == CodeConstManager.OBK_RESULT_ID_DOES_NOT_MEET_REQUIREMENTS)
                            {
                                notApprovedCount++;
                            }

                            var childStagesOfChild = db.OBK_ContractStage.Where(x => x.ParentStageId == childStage.Id).ToList();
                            foreach (var childStageOfChild in childStagesOfChild)
                            {
                                if (childStageOfChild.ResultId == null || childStageOfChild.ResultId == CodeConstManager.OBK_RESULT_ID_NOT_STARTED)
                                {
                                    notFinishedCount++;
                                }
                                if (childStageOfChild.ResultId == CodeConstManager.OBK_RESULT_ID_DOES_NOT_MEET_REQUIREMENTS)
                                {
                                    notApprovedCount++;
                                }
                            }
                        }

                        if (notFinishedCount == 0 && notApprovedCount > 0)
                        {
                            result = true;
                        }
                    }
                    else if (stageObk.OBK_Ref_StageStatus.Code == OBK_Ref_StageStatus.NotAgreed)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        private bool IsShowSendToBossForApprovalBtnAllowed(Guid contractId)
        {
            bool result = false;

            if (EmployePermissionHelper.CanViewReturnToApplicantAndSendToBossForApproval)
            {
                var employeeId = UserHelper.GetCurrentEmployee().Id;

                var stages = db.OBK_ContractRegisterView.Where(x => x.ContractId == contractId && x.ExecutorId == employeeId && (x.StageStatusCode == OBK_Ref_StageStatus.InWork || x.StageStatusCode == OBK_Ref_StageStatus.NotAgreed));

                Guid cozStageId = Guid.Empty;
                foreach (var item in stages)
                {
                    if (item.ContractStageStageId == CodeConstManager.STAGE_OBK_COZ)
                    {
                        cozStageId = item.ContractStageId;
                        break;
                    }
                }

                if (cozStageId != Guid.Empty)
                {
                    var stageObk = db.OBK_ContractStage.Where(x => x.Id == cozStageId).FirstOrDefault();
                    if (stageObk.OBK_Ref_StageStatus.Code == OBK_Ref_StageStatus.InWork)
                    {
                        int notFinishedCount = 0;
                        int notApprovedCount = 0;
                        if (stageObk.ResultId == null || stageObk.ResultId == CodeConstManager.OBK_RESULT_ID_NOT_STARTED)
                        {
                            return result;
                        }
                        if (stageObk.ResultId == CodeConstManager.OBK_RESULT_ID_DOES_NOT_MEET_REQUIREMENTS)
                        {
                            notApprovedCount++;
                        }
                        var childStages = db.OBK_ContractStage.Where(x => x.ParentStageId == cozStageId).ToList();
                        foreach (var childStage in childStages)
                        {
                            if (childStage.ResultId == null || childStage.ResultId == CodeConstManager.OBK_RESULT_ID_NOT_STARTED)
                            {
                                notFinishedCount++;
                            }
                            if (childStage.ResultId == CodeConstManager.OBK_RESULT_ID_DOES_NOT_MEET_REQUIREMENTS)
                            {
                                notApprovedCount++;
                            }

                            var childStagesOfChild = db.OBK_ContractStage.Where(x => x.ParentStageId == childStage.Id).ToList();
                            foreach (var childStageOfChild in childStagesOfChild)
                            {
                                if (childStageOfChild.ResultId == null || childStageOfChild.ResultId == CodeConstManager.OBK_RESULT_ID_NOT_STARTED)
                                {
                                    notFinishedCount++;
                                }
                                if (childStageOfChild.ResultId == CodeConstManager.OBK_RESULT_ID_DOES_NOT_MEET_REQUIREMENTS)
                                {
                                    notApprovedCount++;
                                }
                            }
                        }

                        if (notFinishedCount == 0 && notApprovedCount == 0)
                        {
                            result = true;
                        }
                    }
                    else if (stageObk.OBK_Ref_StageStatus.Code == OBK_Ref_StageStatus.NotAgreed)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        private bool IsShowSendToBossForApprovalBtnWithWarningAllowed(Guid contractId, out string questionMessage)
        {
            questionMessage = "";
            bool result = false;

            var cozRefusedCount = 0;
            var uobkResusedCount = 0;
            var defRefusedCount = 0;

            if (EmployePermissionHelper.CanViewReturnToApplicantAndSendToBossForApproval)
            {
                var employeeId = UserHelper.GetCurrentEmployee().Id;

                var stages = db.OBK_ContractRegisterView.Where(x => x.ContractId == contractId && x.ExecutorId == employeeId && (x.StageStatusCode == OBK_Ref_StageStatus.InWork));

                Guid cozStageId = Guid.Empty;
                foreach (var item in stages)
                {
                    if (item.ContractStageStageId == CodeConstManager.STAGE_OBK_COZ)
                    {
                        cozStageId = item.ContractStageId;
                        break;
                    }
                }

                if (cozStageId != Guid.Empty)
                {
                    var stageObk = db.OBK_ContractStage.Where(x => x.Id == cozStageId).FirstOrDefault();
                    int notFinishedCount = 0;
                    if (stageObk.ResultId == null || stageObk.ResultId == CodeConstManager.OBK_RESULT_ID_NOT_STARTED)
                    {
                        return result;
                    }
                    if (stageObk.ResultId == CodeConstManager.OBK_RESULT_ID_DOES_NOT_MEET_REQUIREMENTS)
                    {
                        cozRefusedCount++;
                    }
                    var childStages = db.OBK_ContractStage.Where(x => x.ParentStageId == cozStageId).ToList();
                    foreach (var childStage in childStages)
                    {
                        if (childStage.ResultId == null || childStage.ResultId == CodeConstManager.OBK_RESULT_ID_NOT_STARTED)
                        {
                            notFinishedCount++;
                        }
                        if (childStage.ResultId == CodeConstManager.OBK_RESULT_ID_DOES_NOT_MEET_REQUIREMENTS)
                        {
                            uobkResusedCount++;
                        }

                        var childStagesOfChild = db.OBK_ContractStage.Where(x => x.ParentStageId == childStage.Id).ToList();
                        foreach (var childStageOfChild in childStagesOfChild)
                        {
                            if (childStageOfChild.ResultId == null || childStageOfChild.ResultId == CodeConstManager.OBK_RESULT_ID_NOT_STARTED)
                            {
                                notFinishedCount++;
                            }
                            if (childStageOfChild.ResultId == CodeConstManager.OBK_RESULT_ID_DOES_NOT_MEET_REQUIREMENTS)
                            {
                                defRefusedCount++;
                            }
                        }
                    }

                    if (notFinishedCount == 0 && (cozRefusedCount + uobkResusedCount + defRefusedCount) > 0)
                    {
                        result = true;

                        StringBuilder messageStr = new StringBuilder();
                        messageStr.Append("По выбранному договору на уровне ");
                        if (cozRefusedCount > 0)
                        {
                            messageStr.Append("ЦОЗ");
                            messageStr.Append(", ");
                        }
                        if (uobkResusedCount > 0)
                        {
                            messageStr.Append("УОБК ");
                            messageStr.Append(", ");
                        }
                        if (defRefusedCount > 0)
                        {
                            messageStr.Append("ДЭФ ");
                            messageStr.Append(", ");
                        }
                        var index = -1;
                        index = messageStr.ToString().LastIndexOf(' ');
                        if (index >= 0)
                        {
                            messageStr.Remove(index, 1);
                        }
                        index = messageStr.ToString().LastIndexOf(',');
                        if (index >= 0)
                        {
                            messageStr.Remove(index, 1);
                        }
                        messageStr.Append(" ");
                        messageStr.Append("было не соответствие требованиям. Вы подтверждаете действие \"Отправить на согласование руководителю\"?");
                        questionMessage = messageStr.ToString();
                    }
                }
            }

            return result;
        }

        private bool IsShowDoApprovementBtnAllowed(Guid contractId)
        {
            bool result = false;
            if (EmployePermissionHelper.CanViewDoApprovementAndRefuseApprovement)
            {
                var employeeId = UserHelper.GetCurrentEmployee().Id;
                var stage = db.OBK_ContractRegisterView.Where(x => x.ContractId == contractId &&
                                                                x.ExecutorId == employeeId &&
                                                                x.StageStatusCode == OBK_Ref_StageStatus.OnAgreement &&
                                                                x.ContractStageStageId == CodeConstManager.STAGE_OBK_COZ).FirstOrDefault();
                if (stage != null)
                {
                    result = true;
                }
            }
            return result;
        }

        private bool IsShowRefuseReasonBtnAllowed(Guid contractId)
        {
            bool result = false;
            if (EmployePermissionHelper.CanViewReturnToApplicantAndSendToBossForApproval)
            {
                var employeeId = UserHelper.GetCurrentEmployee().Id;

                var stages = db.OBK_ContractRegisterView.Where(x => x.ContractId == contractId && x.ExecutorId == employeeId && x.StageStatusCode == OBK_Ref_StageStatus.NotAgreed);

                Guid cozStageId = Guid.Empty;
                foreach (var item in stages)
                {
                    if (item.ContractStageStageId == CodeConstManager.STAGE_OBK_COZ)
                    {
                        cozStageId = item.ContractStageId;
                        break;
                    }
                }

                if (cozStageId != Guid.Empty)
                {
                    var stageObk = db.OBK_ContractStage.Where(x => x.Id == cozStageId).FirstOrDefault();
                    result = true;
                }
            }
            return result;
        }

        private bool IsRegisterBtnAllowed(Guid contractId)
        {
            bool result = false;
            if (EmployePermissionHelper.CanViewRegisterAndAttachContract)
            {
                var employeeId = UserHelper.GetCurrentEmployee().Id;
                var stages = db.OBK_ContractRegisterView.Where(x => x.ContractId == contractId && x.ExecutorId == employeeId && x.StageStatusCode == OBK_Ref_StageStatus.RequiresRegistration && (x.ContractNumber == null || x.ContractNumber == CodeConstManager.OBK_CONTRACT_NO_NUMBER)).ToList();
                if (stages.Count > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        private bool IsAttachContractBtnAllowed(Guid contractId)
        {
            bool result = false;
            if (EmployePermissionHelper.CanViewRegisterAndAttachContract)
            {
                var digitalSign = db.OBK_ContractSignedDatas.Where(x => x.ContractId == contractId).FirstOrDefault();
                if (digitalSign == null)
                {
                    var employeeId = UserHelper.GetCurrentEmployee().Id;
                    var stages = db.OBK_ContractRegisterView.Where(x => x.ContractId == contractId && x.ExecutorId == employeeId && x.StageStatusCode == OBK_Ref_StageStatus.RequiresRegistration).ToList();
                    if (stages.Count > 0)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        private bool IsSignContractBtnAllowed(Guid contractId)
        {
            bool result = false;
            var employeeId = UserHelper.GetCurrentEmployee().Id;
            var stages = db.OBK_ContractRegisterView.Where(x => x.ContractId == contractId && x.ExecutorId == employeeId && x.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_SIGNER && x.StageStatusCode == OBK_Ref_StageStatus.RequiresSigning).ToList();
            if (stages.Count > 0)
            {
                result = true;
            }
            return result;
        }

        public ActionResult ListAttaches([DataSourceRequest] DataSourceRequest request, Guid contractId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            var query = db.FileLinkViews.Include("FileCategory").AsNoTracking().Where(e => e.DocumentId == contractId);
            return Json(query.ToDataSourceResult(request, c => new
            {
                c.Id,
                c.Version,
                c.CreateDate,
                c.Category,
                c.FileName,
                c.FilePath,
                c.CategoryId,
                c.DocumentId,
                c.ParentFileName
            }));
        }

        [HttpGet]
        public ActionResult ShowComment(Guid modelId, string idControl)
        {
            var model = obkRepo.GetComments(modelId, idControl);
            if (model == null)
            {
                model = new OBK_ContractCom();
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }

            return View(model);

            //var repository = new SafetyAssessmentRepository();
            //var model = repository.GetComments(modelId, idControl);
            //if (model == null)
            //{
            //    model = new OBK_AssessmentDeclarationCom();
            //}
            //model.ObkAssessmentDeclarationFieldHistories = repository.GetFieldHistories(modelId, idControl);
            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView(model);
            //}

            //return View(model);
        }
        [HttpPost]
        public virtual ActionResult SaveComment(string modelId, string idControl, bool isError, string comment, string fieldValue, string userId, string fieldDisplay)
        {
            obkRepo.SaveComment(modelId, idControl, isError, comment, fieldValue, userId, fieldDisplay);

            //new SafetyAssessmentRepository().SaveComment(modelId, idControl, isError, comment, fieldValue, userId, fieldDisplay);

            return Json(new { Success = true });

        }

        public ActionResult ShowCommentPrice(Guid contractPriceId)
        {
            var model = obkRepo.GetCommentsPrice(contractPriceId);
            if (model == null)
            {
                model = new OBK_ContractPriceCom();
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }

            return View(model);
        }

        [HttpPost]
        public virtual ActionResult SaveCommentPrice(string contractPriceId, bool isError, string comment, string fieldValue, string userId, string fieldDisplay)
        {
            obkRepo.SaveCommentPrice(contractPriceId, isError, comment, fieldValue, userId, fieldDisplay);

            return Json(new { Success = true });
        }

        public ActionResult ShowCommentProduct(int productId)
        {
            var model = obkRepo.GetCommentsProduct(productId);
            if (model == null)
            {
                model = new OBK_RS_ProductsCom();
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveCommentProduct(string productId, bool isError, string comment, string fieldValue, string userId, string fieldDisplay)
        {
            obkRepo.SaveCommentProduct(productId, isError, comment, fieldValue, userId, fieldDisplay);

            return Json(new { Success = true });
        }

        public ActionResult ShowCommentProductsSerie(int productSerieId)
        {
            var model = obkRepo.GetCommentsProductsSerie(productSerieId);
            if (model == null)
            {
                model = new OBK_Products_SeriesCom();
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveCommentProductsSerie(string productSerieId, bool isError, string comment, string fieldValue, string userId, string fieldDisplay)
        {
            obkRepo.SaveCommentProductsSerie(productSerieId, isError, comment, fieldValue, userId, fieldDisplay);

            return Json(new { Success = true });
        }

        public ActionResult RequestView()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult GetContractPrices(Guid contractId)
        {
            var list = obkRepo.GetContractPrices(contractId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SetExecutor()
        {
            return PartialView(Guid.NewGuid());
        }

        [HttpPost]
        public ActionResult SetExecutor(Guid executorId, Guid stageId)
        {
            obkRepo.SendToWork(stageId, executorId);
            return Json("OK");
        }

        [HttpPost]
        public ActionResult MeetsRequirements(Guid contractId)
        {
            obkRepo.MeetsRequirements(contractId);
            return Json("OK");
        }

        [HttpPost]
        public ActionResult DoesNotMeetRequirements(Guid contractId)
        {
            obkRepo.DoestNotMeetRequirements(contractId);
            return Json("OK");
        }

        [HttpPost]
        public ActionResult ReturnToApplicant(Guid contractId)
        {
            obkRepo.ReturnToApplicant(contractId);
            return Json("OK");
        }

        [HttpPost]
        public ActionResult SendToBossForApproval(Guid contractId)
        {
            obkRepo.SendToBossForApproval(contractId);
            return Json("OK");
        }

        [HttpPost]
        public ActionResult DoApprovement(Guid contractId)
        {
            obkRepo.ApproveContract(contractId);
            return Json("OK");
        }

        [HttpGet]
        public ActionResult RefuseReasonDlg()
        {
            return PartialView(Guid.NewGuid());
        }

        [HttpPost]
        public ActionResult RefuseApprovement(Guid contractId, string reason)
        {
            obkRepo.RefuseApprovement(contractId, reason);
            return Json("OK");
        }

        [HttpGet]
        public ActionResult ShowRefuseReasonDlg(Guid contractId)
        {
            var reason = obkRepo.GetRefuseReason(contractId);
            if (reason != null)
            {
                ViewBag.Reason = reason;
                return PartialView(Guid.NewGuid());
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult RegisterContract(Guid contractId)
        {
            string regNumber = obkRepo.RegisterContract(contractId);
            SendToPay(contractId);
            return Json(regNumber);
        }

        public void SendToPay(Guid contractId)
        {
            new OBKPaymentRepository().SavePayments(contractId);
        }

        [HttpGet]
        public ActionResult UploadContract(Guid? contractId)
        {
            if (EmployePermissionHelper.CanViewRegisterAndAttachContract)
            {
                ViewBag.UiId = Guid.NewGuid().ToString();
                ViewBag.ContractId = contractId;
                return PartialView();
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult UploadContract(Guid contractId)
        {
            if (EmployePermissionHelper.CanViewRegisterAndAttachContract)
            {
                string code = "";
                var contract = db.OBK_Contract.Where(x => x.Id == contractId).FirstOrDefault();
                if (contract.OBK_Declarant.IsResident)
                {
                    var codeId = db.Dictionaries.Where(x => x.Type == CodeConstManager.ATTACH_CONTRACT_FILE_RESIDENT && x.Code == "0").Select(x => x.Id).FirstOrDefault();
                    if (codeId == Guid.Empty)
                    {
                        throw new Exception("CodeId cannot be found");
                    }
                    code = codeId.ToString();
                }
                else
                {
                    var codeId = db.Dictionaries.Where(x => x.Type == CodeConstManager.ATTACH_CONTRACT_FILE_NON_RESIDENT && x.Code == "0").Select(x => x.Id).FirstOrDefault();
                    if (codeId == Guid.Empty)
                    {
                        throw new Exception("CodeId cannot be found");
                    }
                    code = codeId.ToString();
                }
                string path = contractId.ToString();
                bool saveMetadata = true;
                string originField = null;

                var list = FileHelper.GetAttachListByDoc(db, path, code);
                foreach (var item in list)
                {
                    Type t = item.GetType();
                    PropertyInfo p = t.GetProperty("AttachName");
                    object attachName = p.GetValue(item, null);
                    FileHelper.DeleteAttach(path, code, attachName.ToString());
                }
                FileHelper.SaveAttach(code, path, Request, saveMetadata, originField, db);
                obkRepo.UploadContract(contractId);
                return Json("OK");
            }
            return HttpNotFound();
        }

        public ActionResult GetContractTemplatePdf(Guid id)
        {
            var db = new ncelsEntities();
            string name = "Договор_на_проведение_оценки_безопасности_и_качества.pdf";
            StiReport report = new StiReport();
            try
            {
                report.Load(obkRepo.GetContractTemplatePath(id));//(Server.MapPath("~/Reports/Mrts/OBK/ObkContractDec.mrt"));
                foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
                {
                    data.ConnectionString = UserHelper.GetCnString();
                }

                report.Dictionary.Variables["ContractId"].ValueObject = id;

                var price = new OBKContractRepository().GetPriceCount(id);
                report.Dictionary.Variables["PriceCount"].ValueObject = price;

                var priceText = RuDateAndMoneyConverter.ToTextTenge(Convert.ToDouble(price), false);
                report.Dictionary.Variables["PriceCountName"].ValueObject = priceText;

                report.Render(false);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("ex: " + ex.Message + " \r\nstack: " + ex.StackTrace);
            }

            Stream stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Word2007, stream);
            stream.Position = 0;

            Aspose.Words.Document doc = new Aspose.Words.Document(stream);

            try
            {
                var signData = db.OBK_ContractSignedDatas.Where(x => x.ContractId == id).FirstOrDefault();
                if (signData != null && signData.ApplicantSign != null && signData.CeoSign != null)
                {
                    doc.InserQrCodesToEnd("ApplicantSign", signData.ApplicantSign);
                    doc.InserQrCodesToEnd("CeoSign", signData.CeoSign);
                }
            }
            catch (Exception ex)
            {

            }

            var file = new MemoryStream();
            doc.Save(file, Aspose.Words.SaveFormat.Pdf);
            file.Position = 0;

            //return new FileStreamResult(stream, "application/pdf");
            return File(file, "application/pdf", name);
        }

        [HttpGet]
        public ActionResult SignData(Guid contractId)
        {
            var _data = obkRepo.GetDataForSign(contractId);
            return Json(new { data = _data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveSignedContract(Guid contractId, string signedData)
        {
            obkRepo.SignContractCeo(contractId, signedData);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListHistory([DataSourceRequest] DataSourceRequest request, Guid contractId)
        {
            var query = obkRepo.GetHistory(contractId);
            return Json(query.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult GetInstuctions(int registerId)
        {
            var instructions = obkRepo.GetInstructions(registerId);
            return Json(instructions, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetInstruction(int registerId)
        {
            var name = "doc_" + registerId + ".zip";
            var file = obkRepo.GetInstructionFile(registerId);
            return File(file, System.Net.Mime.MediaTypeNames.Application.Octet, name);
        }
    }
}