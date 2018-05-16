using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Aspose.Cells;
using Aspose.Cells.Rendering;
using Aspose.Pdf;
using Kendo.Mvc.Extensions;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models.OBK;
using PW.Ncels.Database.Notifications;
using static PW.Ncels.Database.Constants.CodeConstManager;

namespace PW.Ncels.Database.Repository.OBK
{
    public class OBKExpDocumentRepository : ARepository
    {
        public virtual OBK_AssessmentStage GetAssessmentStage(Guid id)
        {
            return new AssessmentStageRepository().GetById(id);
        }

        public OBK_AssessmentDeclaration GetAssessmentDeclaration(Guid id)
        {
            return AppContext.OBK_AssessmentDeclaration.FirstOrDefault(e => e.Id == id);
        }

        public OBK_StageExpDocumentResult GetStageExpDocResult(Guid modelId)
        {
            return AppContext.OBK_StageExpDocumentResult.FirstOrDefault(e => e.AssessmetDeclarationId == modelId);
        }

        public OBK_Ref_StageStatus GetRefStageStatus(string code)
        {
            return AppContext.OBK_Ref_StageStatus.FirstOrDefault(e => e.Code == code);
        }

        public OBK_Ref_Status GetRefStatus(string code)
        {
            return AppContext.OBK_Ref_Status.FirstOrDefault(e => code.Equals(e.Code));
        }

        public Dictionary GetDictionary(Guid? id)
        {
            return AppContext.Dictionaries.FirstOrDefault(e => e.Id == id);
        }

        public double GetValueAddedTax()
        {
            var reslutvalue = AppContext.OBK_Ref_ValueAddedTax.FirstOrDefault(e => e.Year == DateTime.Now.Year);
            if (reslutvalue == null)
                return 12;
            return reslutvalue.Value;
        }

        public decimal GetContractPrice(Guid? contractId)
        {
            var results = AppContext.OBK_ContractPrice.Where(e => e.ContractId == contractId).ToList();
            decimal totalCount = results.Sum(e => Math.Round(Convert.ToDecimal(TaxHelper.GetCalculationTax(e.OBK_Ref_PriceList.Price) * e.Count), 2));
            return totalCount;
        }

        public decimal GetContractPriceMotivationRefuse(Guid? contractId)
        {
            var results = AppContext.OBK_ContractPrice.Where(e => e.ContractId == contractId).ToList();
            decimal totalCount = results.Sum(e => Math.Round(Convert.ToDecimal(TaxHelper.GetCalculationTax(e.OBK_Ref_PriceList.Price) * e.Count * 0.3), 2));
            return totalCount;
        }

        public void SaveExpDocumentResult(bool expResult, Guid modelId)
        {
            var expDocResult =
                AppContext.OBK_StageExpDocumentResult.FirstOrDefault(e => e.AssessmetDeclarationId == modelId);
            if (expDocResult == null)
            {
                var result = new OBK_StageExpDocumentResult
                {
                    Id = Guid.NewGuid(),
                    AssessmetDeclarationId = modelId,
                    ExpResult = expResult
                };
                AppContext.OBK_StageExpDocumentResult.Add(result);
                AppContext.SaveChanges();
            }
            else
            {
                var result = new OBK_StageExpDocumentResult
                {
                    Id = expDocResult.Id,
                    AssessmetDeclarationId = modelId,
                    ExpResult = expResult
                };
                AppContext.OBK_StageExpDocumentResult.AddOrUpdate(result);
                AppContext.SaveChanges();
            }
        }

        public bool GetReturnToExecutor(Guid id, int stage)
        {
            var assessmentStageStage =
                AppContext.OBK_AssessmentStage.FirstOrDefault(e => e.DeclarationId == id && e.StageId == stage);
            if (assessmentStageStage == null)
                return false;
            assessmentStageStage.StageStatusId = GetRefStageStatus(OBK_Ref_StageStatus.InWork).Id;
            AppContext.SaveChanges();
            return true;
        }

        public bool checkSignData(Guid stageId)
        {
            var current = UserHelper.GetCurrentEmployee().Id;

            var stageSignData =  AppContext.OBK_AssessmentStageSignData.FirstOrDefault(
                o => o.SignerId == current && o.AssessmentStageId == stageId);
            if (stageSignData != null)
            {
                return true;
            }

            return false;
        }

        public string GetSignData(Guid id)
        {
            var reslutStage = AppContext.OBK_AssessmentStage.FirstOrDefault(
                e => e.DeclarationId == id && e.StageId == CodeConstManager.STAGE_OBK_EXPERTISE_DOC);

            if (reslutStage == null)
                return null;

            OBK_AssessmentStage ad = new OBK_AssessmentStage
            {
                Id = reslutStage.Id,
                DeclarationId = reslutStage.DeclarationId
            };
            var xmlData = SerializeHelper.SerializeDataContract(ad);
            return xmlData.Replace("utf-16", "utf-8");
        }

        public List<OBK_Ref_Reason> OBKRefReasonList()
        {
            return AppContext.OBK_Ref_Reason.Where(o => o.Code == "Party" && o.ExpertiseResult == false).ToList();
        }

        public Guid SaveMotivationRefuse(int? OBKRefReason, string motivationRefuseRu, string motivationRefuseKz,
            Guid declarationId, Guid? OBK_StageExpDocumentId)
        {
            var doc = AppContext.OBK_StageExpDocument.FirstOrDefault(o => o.Id == OBK_StageExpDocumentId);

            if (doc == null)
            {
                doc = new OBK_StageExpDocument();
                doc.Id = Guid.NewGuid();
                AppContext.OBK_StageExpDocument.Add(doc);
            }

            doc.ExpResult = false;
            doc.ExpReasonNameRu = motivationRefuseRu;
            doc.ExpReasonNameKz = motivationRefuseKz;
            doc.RefReasonId = OBKRefReason;
            doc.ExecutorId = UserHelper.GetCurrentEmployee().Id;
            doc.AssessmentDeclarationId = declarationId;

            AppContext.SaveChanges();

            return doc.Id;
        }

        public bool GetMotivationRefuseFields(Guid? declarationId)
        {
            var expDoc = AppContext.OBK_StageExpDocument.FirstOrDefault(o => o.AssessmentDeclarationId == declarationId);
            if (expDoc == null)
            {
                return false;
            }

            if (expDoc.RefReasonId == null && expDoc.ExpReasonNameRu == null && expDoc.ExpReasonNameKz == null)
            {
                return false;
            }

            return true;
        }

        public OBK_StageExpDocument GetMotivationRefuse(Guid declarationId)
        {
            return AppContext.OBK_StageExpDocument.FirstOrDefault(o => o.AssessmentDeclarationId == declarationId);
        }

        public void SavePlace(DateTime selectionDate, DateTime selectionTime, string selectionAddress, Guid? assessmentId)
        {
            OBK_StageExpDocumentResult result = AppContext.OBK_StageExpDocumentResult.First(o => o.AssessmetDeclarationId == assessmentId);
            NotificationManager notification = new NotificationManager();

            if (result != null)
            {
                result.SelectionDate = selectionDate;
                result.SelectionTime = selectionTime;
                result.SelectionPlace = selectionAddress;

                OBK_AssessmentDeclaration declaration = AppContext.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == assessmentId);
                declaration.ExpertRequest = true;

                var text = "Заявка №" + declaration.Number + ". Вы прошли этап экспертизы документов. Просим согласовать дату и место выезда на отбор образцов.";
                notification.SendNotificationFromCompany(text, ObjectType.ObkDeclaration, declaration.Id.ToString(), declaration.EmployeeId);

                SafetyAssessmentRepository repository = new SafetyAssessmentRepository();
                OBK_AssessmentStage stage = declaration.OBK_AssessmentStage.FirstOrDefault(o => o.StageId == 2);
                stage.StageStatusId = repository.GetStageStatusByCode(OBK_Ref_StageStatus.DocumentReviewCompleted).Id;

            }

            AppContext.SaveChanges();

        }

        public int? ExecutorType(Guid declarationId)
        {
            var stage = AppContext.OBK_AssessmentStage.FirstOrDefault(e =>
              e.OBK_Ref_Stage.Code == CodeConstManager.STAGE_OBK_EXPERTISE_DOC.ToString()
              && e.OBK_AssessmentDeclaration.Id == declarationId);

            var executor = stage.OBK_AssessmentStageExecutors.FirstOrDefault(o => o.ExecutorId == UserHelper.GetCurrentEmployee().Id);

            if (executor == null)
            {
                return null;
            }

            return executor.ExecutorType;
        }

        public string SaveSignExpDocParty(Guid id, string signedData)
        {
            var msg = "";
            var stage = AppContext.OBK_AssessmentStage.FirstOrDefault(e =>
                e.OBK_Ref_Stage.Code == CodeConstManager.STAGE_OBK_EXPERTISE_DOC.ToString()
                && e.OBK_AssessmentDeclaration.Id == id);
            var stageCoz = AppContext.OBK_AssessmentStage.FirstOrDefault(e =>
                e.OBK_Ref_Stage.Code == CodeConstManager.STAGE_OBK_COZ.ToString()
                && e.OBK_AssessmentDeclaration.Id == id);

            if (stage == null)
                return "Ошибка этап не найден";

            foreach (var executor in stage.OBK_AssessmentStageExecutors)
            {
                if (executor.ExecutorId == UserHelper.GetCurrentEmployee().Id)
                {
                    // руководитель
                    if (executor.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_ASSIGNING)
                    {
                        //результат экпертизы документов
                        var expResult = stage.OBK_AssessmentDeclaration.OBK_StageExpDocumentResult.FirstOrDefault();
                        var stageSign =
                            AppContext.OBK_AssessmentStageSignData.FirstOrDefault(
                                e => e.AssessmentStageId == stage.Id &&
                                     e.SignerId == executor.ExecutorId);
                        if (stageSign == null)
                        {
                            OBK_AssessmentStageSignData stageSignData = new OBK_AssessmentStageSignData
                            {
                                Id = Guid.NewGuid(),
                                AssessmentStageId = stage.Id,
                                SignerId = UserHelper.GetCurrentEmployee().Id,
                                SignXmlData = signedData,
                                SignDateTime = DateTime.Now
                            };
                            stage.StageStatusId = GetRefStageStatus(OBK_Ref_StageStatus.Completed).Id;

                            stage.ResultId = expResult.ExpResult ? CodeConstManager.STAGE_OBK_COMPLETED_POSITIVE : CodeConstManager.STAGE_OBK_COMPLETED_NEGATIVE;
                            stage.OBK_AssessmentStageSignData.Add(stageSignData);

                            var declaration = AppContext.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == id);
                            declaration.StatusId = GetRefStatus(CodeConstManager.STATUS_OBK_MOTIVATION_REFUSE.ToString()).Id;

                            stageCoz.StageStatusId = GetRefStageStatus(OBK_Ref_StageStatus.RequiresConclusion).Id;
                            AppContext.SaveChanges();
                        }
                        else
                        {
                            stageSign.SignXmlData = signedData;
                            stageSign.SignDateTime = DateTime.Now;
                            stage.StageStatusId = GetRefStageStatus(OBK_Ref_StageStatus.Completed).Id;

                            stage.ResultId = expResult.ExpResult ? CodeConstManager.STAGE_OBK_COMPLETED_POSITIVE : CodeConstManager.STAGE_OBK_COMPLETED_NEGATIVE;

                            var declaration = AppContext.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == id);
                            declaration.StatusId = GetRefStatus(CodeConstManager.STATUS_OBK_MOTIVATION_REFUSE.ToString()).Id;

                            stageCoz.StageStatusId = GetRefStageStatus(OBK_Ref_StageStatus.RequiresConclusion).Id;
                            AppContext.SaveChanges();
                        }
                        //отправка акта выполненных работ в 1с
                        new OBKPaymentRepository().SaveCertificateOfCompletionParty(id, expResult.ExpResult);
                        msg = "Документ успешно подписан";
                    }
                    // исполнитель
                    else if (executor.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR)
                    {
                        var stageSign =
                            AppContext.OBK_AssessmentStageSignData.FirstOrDefault(
                                e => e.AssessmentStageId == stage.Id &&
                                     e.SignerId == executor.ExecutorId);
                        if (stageSign == null)
                        {
                            OBK_AssessmentStageSignData stageSignData = new OBK_AssessmentStageSignData
                            {
                                Id = Guid.NewGuid(),
                                AssessmentStageId = stage.Id,
                                SignerId = UserHelper.GetCurrentEmployee().Id,
                                SignXmlData = signedData,
                                SignDateTime = DateTime.Now
                            };
                            stage.StageStatusId = GetRefStageStatus(OBK_Ref_StageStatus.RequiresSigning).Id;
                            stage.OBK_AssessmentStageSignData.Add(stageSignData);
                            AppContext.SaveChanges();
                        }
                        else
                        {
                            stageSign.SignXmlData = signedData;
                            stageSign.SignDateTime = DateTime.Now;
                            stage.StageStatusId = GetRefStageStatus(OBK_Ref_StageStatus.RequiresSigning).Id;
                            AppContext.SaveChanges();
                        }

                        var chiefId = stage.OBK_AssessmentStageExecutors.FirstOrDefault(e =>
                                e.AssessmentStageId == stage.Id &&
                                    e.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_ASSIGNING)
                                        .ExecutorId;

                        SendNotificationToBoss(stage.OBK_AssessmentDeclaration.Number, stage.Id, chiefId);
                        msg = "Документ успешно подписан";
                    }
                }
            }
            return msg;
        }

        public string SaveSignExpDoc(Guid id, string signedData)
        {
            var msg = "";
            var stage = AppContext.OBK_AssessmentStage.FirstOrDefault(e =>
                e.OBK_Ref_Stage.Code == CodeConstManager.STAGE_OBK_EXPERTISE_DOC.ToString()
                && e.OBK_AssessmentDeclaration.Id == id);
            var stageCoz = AppContext.OBK_AssessmentStage.FirstOrDefault(e =>
                e.OBK_Ref_Stage.Code == CodeConstManager.STAGE_OBK_COZ.ToString()
                && e.OBK_AssessmentDeclaration.Id == id);

            if (stage == null)
                return "Ошибка этап не найден";

            foreach (var executor in stage.OBK_AssessmentStageExecutors)
            {
                if (executor.ExecutorId == UserHelper.GetCurrentEmployee().Id)
                {
                    // руководитель
                    if (executor.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_ASSIGNING)
                    {
                        //результат экпертизы документов
                        var expResult = stage.OBK_AssessmentDeclaration.OBK_StageExpDocumentResult.FirstOrDefault();
                        var stageSign =
                            AppContext.OBK_AssessmentStageSignData.FirstOrDefault(
                                e => e.AssessmentStageId == stage.Id &&
                                     e.SignerId == executor.ExecutorId);
                        if (stageSign == null)
                        {
                            OBK_AssessmentStageSignData stageSignData = new OBK_AssessmentStageSignData
                            {
                                Id = Guid.NewGuid(),
                                AssessmentStageId = stage.Id,
                                SignerId = UserHelper.GetCurrentEmployee().Id,
                                SignXmlData = signedData,
                                SignDateTime = DateTime.Now
                            };
                            stage.StageStatusId = GetRefStageStatus(OBK_Ref_StageStatus.Completed).Id;

                            stage.ResultId = expResult.ExpResult ? CodeConstManager.STAGE_OBK_COMPLETED_POSITIVE : CodeConstManager.STAGE_OBK_COMPLETED_NEGATIVE;
                            stage.OBK_AssessmentStageSignData.Add(stageSignData);

                            stageCoz.StageStatusId = GetRefStageStatus(OBK_Ref_StageStatus.RequiresConclusion).Id;
                            AppContext.SaveChanges();
                        }
                        else
                        {
                            stageSign.SignXmlData = signedData;
                            stageSign.SignDateTime = DateTime.Now;
                            stage.StageStatusId = GetRefStageStatus(OBK_Ref_StageStatus.Completed).Id;

                            stage.ResultId = expResult.ExpResult ? CodeConstManager.STAGE_OBK_COMPLETED_POSITIVE : CodeConstManager.STAGE_OBK_COMPLETED_NEGATIVE;

                            stageCoz.StageStatusId = GetRefStageStatus(OBK_Ref_StageStatus.RequiresConclusion).Id;
                            AppContext.SaveChanges();
                        }
                        //отправка акта выполненных работ в 1с
                        new OBKPaymentRepository().SaveCertificateOfCompletion(id);
                        msg = "Документ успешно подписан";
                    }
                    // исполнитель
                    else if (executor.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR)
                    {
                        var stageSign =
                            AppContext.OBK_AssessmentStageSignData.FirstOrDefault(
                                e => e.AssessmentStageId == stage.Id &&
                                     e.SignerId == executor.ExecutorId);
                        if (stageSign == null)
                        {
                            OBK_AssessmentStageSignData stageSignData = new OBK_AssessmentStageSignData
                            {
                                Id = Guid.NewGuid(),
                                AssessmentStageId = stage.Id,
                                SignerId = UserHelper.GetCurrentEmployee().Id,
                                SignXmlData = signedData,
                                SignDateTime = DateTime.Now
                            };
                            stage.StageStatusId = GetRefStageStatus(OBK_Ref_StageStatus.RequiresSigning).Id;
                            stage.OBK_AssessmentStageSignData.Add(stageSignData);
                            AppContext.SaveChanges();
                        }
                        else
                        {
                            stageSign.SignXmlData = signedData;
                            stageSign.SignDateTime = DateTime.Now;
                            stage.StageStatusId = GetRefStageStatus(OBK_Ref_StageStatus.RequiresSigning).Id;
                            AppContext.SaveChanges();
                        }

                        var chiefId = stage.OBK_AssessmentStageExecutors.FirstOrDefault(e =>
                                e.AssessmentStageId == stage.Id &&
                                    e.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_ASSIGNING)
                                        .ExecutorId;

                        SendNotificationToBoss(stage.OBK_AssessmentDeclaration.Number, stage.Id, chiefId);
                        msg = "Документ успешно подписан";
                    }
                }
            }
            return msg;
        }

        #region CommissionOP
        public List<OBK_OP_Commission> GetOBK_OP_Commission(Guid declarationId)
        {
            return AppContext.OBK_OP_Commission.Where(x => x.DeclarationId == declarationId).ToList();
        }
        #endregion

        #region Notification

        private void SendNotificationToBoss(string number, Guid stageId, Guid userId)
        {
            NotificationManager notifManager = new NotificationManager();
            var message = string.Format("Вам поступила заявка №{0} на подписание.", number);

            notifManager.SendNotification(message, ObjectType.ObkDeclaration, stageId, userId);
        }

        #endregion

        #region GenerateNumber

        private string KZ = "KZ";
        private string ZZ = "01";
        private string template = "00000000";

        /// <summary>
        /// Генерация уникальных номеров для сертификатов ОБК 
        /// KZ.XXXX.YY.ZZ.NNNNNNNN
        /// KZ – постоянная часть
        /// XXXX – код филиала
        /// YY – 01 – декларирование
        ///     02 – партия
        ///     03 - серийная
        /// ZZ - 01
        /// NNNNNNNN – порядковый номер в каждом филиале свой сквозной
        /// </summary>
        /// <param name="id">уникальный номер заявления</param>
        /// <param name="productSeriesId">id серии продукции</param>
        /// <returns></returns>
        public string GenerateNumber(Guid id, int productSeriesId)
        {
            var declaration = AppContext.OBK_AssessmentDeclaration.FirstOrDefault(e => e.Id == id);
            if (declaration == null)
                return null;

            //тип заявки
            var typeCode = "";
            switch (declaration.OBK_Ref_Type.Code)
            {
                case "3":
                    typeCode = "01";
                    break;
                case "2":
                    typeCode = "02";
                    break;
                case "1":
                    typeCode = "03";
                    break;
            }

            //код региона
            var regionCode = "";
            switch (declaration.OBK_Contract.Unit.Code)
            {
                case "00":
                    regionCode = "7500";
                    break;
            }
            //поиск присвоенного номера
            var productNumber =
                AppContext.OBK_UniqueNumber.FirstOrDefault(e => e.ProductSeriesId == productSeriesId && e.DeclarantId == id);
            if (productNumber == null)
            {
                var uniNumber = AppContext.OBK_UniqueNumber.Max(e => e.Number);
                var result = uniNumber + 1;
                var newUniNumber = template.Substring(0, template.Length - result.ToString().Length) + result;
                var newFullNumber = KZ + "." + regionCode + "." + typeCode + "." + ZZ + "." + newUniNumber;
                OBK_UniqueNumber firstUniqueNumber = new OBK_UniqueNumber
                {
                    Id = Guid.NewGuid(),
                    DeclarantId = id,
                    ProductSeriesId = productSeriesId,
                    Code = KZ + "." + regionCode + "." + typeCode + "." + ZZ + ".",
                    Number = result
                };
                AppContext.OBK_UniqueNumber.Add(firstUniqueNumber);
                AppContext.SaveChanges();
                return newFullNumber;
            }
            else
            {
                var newUniNumber = template.Substring(0, template.Length - productNumber.Number.ToString().Length) + productNumber.Number;
                var returnNumber = KZ + "." + regionCode + "." + typeCode + "." + ZZ + "." + newUniNumber;
                return returnNumber;
            }
        }

        #endregion



        #region Заклчюение для серии и партии

        public OBKExpertiseConclusion ExpertiseConclusion(Guid declarationId)
        {
            var ad = AppContext.OBK_AssessmentDeclaration.FirstOrDefault(e => e.Id == declarationId);
            if (ad == null)
                return null;
            var taskMaterails = AppContext.OBK_TaskMaterial.Where(e => e.OBK_Tasks.AssessmentDeclarationId == declarationId).GroupBy(d => d.ProductSeriesId);
            var productSeries = AppContext.OBK_Procunts_Series.Where(e => taskMaterails.Any(s => s.Key == e.Id));
            if (!productSeries.Any()) return null;
            
            var okbConclusion = new OBKExpertiseConclusion();
            var eConclusions = new List<ExpertiseConclusion>();
            foreach (var ps in productSeries)
            {
                var ec = new ExpertiseConclusion
                {
                    ProductId = ps.OBK_RS_Products.Id,
                    ProductSeriesId = ps.Id,
                    ProductNameRu = ps.OBK_RS_Products.NameRu,
                    ProductNameKz = ps.OBK_RS_Products.NameKz,
                    ProductSeries = ps.Series,
                    SeriesParty = ps.SeriesParty + " " + ps.sr_measures.name,
                    ResearchCenterResultName = ResearchCenterResultName(ps.OBK_TaskMaterial.AsQueryable()),
                    ResearchCenterResult = ps.OBK_TaskMaterial.All(x => x.OBK_ResearchCenterResult.All(e => e.ExpertiseResult == true)) ? 1 : 0,//ps.OBK_StageExpDocument.Count(e=>e.ProductSeriesId == ps.Id) > 0 ? 2 : ps.OBK_TaskMaterial.All(x=>x.OBK_ResearchCenterResult.All(e=>e.ExpertiseResult == true)) ? 1 : 0
                    BtnResult = ps.OBK_StageExpDocument.Count(e => e.ProductSeriesId == ps.Id) > 0 ? 2:3
                };
                eConclusions.Add(ec);
            }
            okbConclusion.AssessmentDeclarationId = declarationId;
            okbConclusion.AssessmentDeclarationType = ad?.OBK_Ref_Type.Code;
            okbConclusion.ExpertiseConclusion = eConclusions;
            return okbConclusion;
        }

        private static string ResearchCenterResultName(IQueryable<OBK_TaskMaterial> tms)
        {
            var result = tms.Where(e=>e.StatusId != new SafetyAssessmentRepository().GetStageStatusByCode(OBK_Ref_StageStatus.Completed).Id);
            if (result.Any()) return "Испытания не завершены";

            var tm = tms.FirstOrDefault(e => e.OBK_ResearchCenterResult.Any(x => x.ExpertiseResult == null));
            if (tm != null)
                return "Испытания не завершены";
            var tm1 = tms.FirstOrDefault(e => e.OBK_ResearchCenterResult.Any(x => x.ExpertiseResult == false));
            if (tm1 != null)
                return "Не соответсвует требованиям";
            var tm2 = tms.FirstOrDefault(e => e.OBK_ResearchCenterResult.Any(x => x.ExpertiseResult == true));
            if (tm2 != null)
                return "Соотвествует требованиям";
            return null;
        }

        public SubTaskDetails GetTaskDetails(Guid assessmentDeclarationId, int productSeriesId)
        {
            var statusId = new SafetyAssessmentRepository().GetStageStatusByCode(OBK_Ref_StageStatus.Completed).Id;
            var tes = AppContext.OBK_TaskExecutor.Where(
                e => e.OBK_Tasks.AssessmentDeclarationId == assessmentDeclarationId &&
                     e.OBK_TaskMaterial.ProductSeriesId == productSeriesId); //&& e.OBK_TaskMaterial.StatusId == statusId

            var stds = new SubTaskDetails();
            stds.AssessmentDeclarationId = assessmentDeclarationId;
            stds.ProductSeriesId = productSeriesId;
            var strs = new List<OBKSubTaskResult>();
            foreach (var te in tes)
            {
                var str = new OBKSubTaskResult();
                if (te.OBK_TaskMaterial.StatusId == statusId)
                {
                    str = new OBKSubTaskResult
                    {
                        Id = te.OBK_TaskMaterial.Id,
                        TaskExecutorId = te.Id,
                        ProductNameRu = te.OBK_TaskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameRu,
                        ProductNameKz = te.OBK_TaskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameKz,
                        Regulation = te.OBK_TaskMaterial.Regulation,
                        LaboratoryExpertName = te.Employee?.DisplayName,
                        LaboratoryTypeName = te.OBK_TaskMaterial.OBK_Ref_LaboratoryType?.NameRu,
                        UnitLaboratoryName = te.OBK_TaskMaterial.Unit?.DisplayName,
                        SubTaskNumber = te.OBK_TaskMaterial.SubTaskNumber,
                        SubTaskCreateDate = te.OBK_TaskMaterial.CreatedDate,
                        SubTaskIndicator = te.OBK_TaskMaterial.OBK_ResearchCenterResult
                            .Where(x => x.ExecutorId == te.ExecutorId)
                            .Select(e => new SubTaskIndicator
                            {
                                ResearchCenterId = e.Id,
                                LaboratoryMarkNameRu = e.OBK_Ref_LaboratoryMark.NameRu,
                                LaboratoryMarkNameKz = e.OBK_Ref_LaboratoryMark.NameKz,
                                Claim = e.Claim,
                                FactResult = e.FactResult,
                                Humidity = e.Humidity,
                                LaboratoryRegulationNameRu = e.OBK_Ref_LaboratoryRegulation.NameRu,
                                LaboratoryRegulationNameKz = e.OBK_Ref_LaboratoryRegulation.NameKz,
                                ExpertiseResultName = e.ExpertiseResult == null
                                    ? "Испытания не завершены"
                                    : (bool) e.ExpertiseResult
                                        ? "Соотвествует требованиям"
                                        : "Не соотвествует требованиям",
                                TaskComment = e.OBK_ResearchCenterResultCom.Count > 0
                            })
                            .ToList()
                    };
                }
                else
                {
                    str = new OBKSubTaskResult
                    {
                        Id = te.OBK_TaskMaterial.Id,
                        TaskExecutorId = te.Id,
                        ProductNameRu = te.OBK_TaskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameRu,
                        ProductNameKz = te.OBK_TaskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameKz,
                        Regulation = te.OBK_TaskMaterial.Regulation,
                        LaboratoryExpertName = te.Employee?.DisplayName,
                        LaboratoryTypeName = te.OBK_TaskMaterial.OBK_Ref_LaboratoryType?.NameRu,
                        UnitLaboratoryName = te.OBK_TaskMaterial.Unit?.DisplayName,
                        SubTaskNumber = te.OBK_TaskMaterial.SubTaskNumber,
                        SubTaskCreateDate = te.OBK_TaskMaterial.CreatedDate,
                        SubTaskIndicator = null
                    };
                }
                strs.Add(str);
            }
            stds.SubTaskResult = strs;
            return stds;
        }

        public OBKExpertiseConclusionPositive ExpertiseConclusionPositive(int productSeriesId, Guid adId)
        {
            var ad = AppContext.OBK_AssessmentDeclaration.FirstOrDefault(e => e.Id == adId);
            if (ad == null) return null;
            switch (ad.OBK_Ref_Type.Code)
            {
                case CodeConstManager.OBK_SA_PARTY:
                case CodeConstManager.OBK_SA_SERIAL:
                    return PartyExpertiseConclusionPositive(productSeriesId, ad);               
                case CodeConstManager.OBK_SA_DECLARATION:
                    break;
            }
            return null;
        }

        private OBKExpertiseConclusionPositive PartyExpertiseConclusionPositive(int productSeriesId, OBK_AssessmentDeclaration ad)
        {
            var ps = AppContext.OBK_Procunts_Series.FirstOrDefault(e => e.Id == productSeriesId);

            if (ps == null) return null;
            var ecp = new OBKExpertiseConclusionPositive();
            ecp.AssessmentDeclarationId = ad.Id;
            ecp.ProductSeriesId = productSeriesId;
            ecp.ecEndDate = ps.SeriesEndDate;
            ecp.ecReasonNameRu = ReasonExpertiseConclusionPositive(ps.OBK_TaskMaterial);
            ecp.ecReasonNameKz = ReasonExpertiseConclusionPositive(ps.OBK_TaskMaterial);
            ecp.ecProductNameRu = ps.OBK_RS_Products.NameRu + ", серия " + ps.Series + ", годен до " + ps.SeriesEndDate + ", партия " + ps.SeriesParty + " " + ps.sr_measures.name;
            ecp.ecProductNameKz = ps.OBK_RS_Products.NameKz + ", " + ps.Series + " сериясы, сақтау мерзімі " + ps.SeriesEndDate + " ж., партия " + ps.SeriesParty + " " + ps.sr_measures.name_kz;
            ecp.ecAdditionalInfoRu = "Договора поставки " + ad.InvoiceContractRu + " инвойс " + ad.InvoiceRu + " дата инвойса " + ad.InvoiceDate;
            ecp.ecAdditionalInfoKz = "Жеткізу шарты " + ad.InvoiceContractKz + " инвойс " + ad.InvoiceKz + " инвойс күні " + ad.InvoiceDate;
            ecp.ecNumber = GenerateNumber(ad.Id, productSeriesId);
            return ecp;
        }

        private string ReasonExpertiseConclusionPositive(ICollection<OBK_TaskMaterial> tm)
        {
            if (!tm.Any()) return null;
            var rows = new List<string>();
            foreach (var t in tm)
            {
                var row = "№" + t.SubTaskNumber + ", от " + String.Format("{0:dd.MM.yyyy}", t.CreatedDate) + ", " + t.Unit.DisplayName + "; ";
                rows.Add(row);
            }
            return string.Join(", ", rows);
        }

        public bool SaveExpertiseConclusionPositive(OBKExpertiseConclusionPositive ecp)
        {
            try
            {
                var model =
                    AppContext.OBK_StageExpDocument.FirstOrDefault(
                        e => e.AssessmentDeclarationId == ecp.AssessmentDeclarationId &&
                             e.ProductSeriesId == ecp.ProductSeriesId);
                if (model != null)
                {
                    model.ProductSeriesId = ecp.ProductSeriesId;
                    model.ExpResult = true;
                    model.ExpStartDate = Convert.ToDateTime(ecp.ecStartDate);
                    model.ExpEndDate = Convert.ToDateTime(ecp.ecEndDate);
                    model.ExpReasonNameRu = ecp.ecReasonNameRu;
                    model.ExpReasonNameKz = ecp.ecReasonNameKz;
                    model.ExpProductNameRu = ecp.ecProductNameRu;
                    model.ExpProductNameKz = ecp.ecProductNameKz;
                    model.ExpAddInfoRu = ecp.ecAdditionalInfoRu;
                    model.ExpAddInfoKz = ecp.ecAdditionalInfoKz;
                    model.ExpConclusionNumber = ecp.ecNumber;
                    model.ExpApplication = false;
                    model.ExpApplicationNumber = ecp.ecApplicationNumber;
                    model.ExpBlankNumber = ecp.ecBlankNumber;
                    model.AssessmentDeclarationId = ecp.AssessmentDeclarationId;
                    model.ExecutorId = UserHelper.GetCurrentEmployee().Id;

                    var blankNumber = AppContext.OBK_BlankNumber.FirstOrDefault(o => o.Object_Id == model.Id);
                    blankNumber.Number = int.Parse(ecp.ecBlankNumber);
                    blankNumber.EmployeeId = UserHelper.GetCurrentEmployee().Id;

                    AppContext.SaveChanges();
                    return true;
                }
                else
                {
                    var sed = new OBK_StageExpDocument
                    {
                        Id = Guid.NewGuid(),
                        ProductSeriesId = ecp.ProductSeriesId,
                        ExpResult = true,
                        ExpStartDate = Convert.ToDateTime(ecp.ecStartDate),
                        ExpEndDate = Convert.ToDateTime(ecp.ecEndDate),
                        ExpReasonNameRu = ecp.ecReasonNameRu,
                        ExpReasonNameKz = ecp.ecReasonNameKz,
                        ExpProductNameRu = ecp.ecProductNameRu,
                        ExpProductNameKz = ecp.ecProductNameKz,
                        ExpAddInfoRu = ecp.ecAdditionalInfoRu,
                        ExpAddInfoKz = ecp.ecAdditionalInfoKz,
                        ExpConclusionNumber = ecp.ecNumber,
                        ExpBlankNumber = ecp.ecBlankNumber,
                        ExpApplication = false,
                        AssessmentDeclarationId = ecp.AssessmentDeclarationId,
                        ExecutorId = UserHelper.GetCurrentEmployee().Id
                    };
                    //AppContext.OBK_StageExpDocumentResult.Add(sedr);
                    AppContext.OBK_StageExpDocument.Add(sed);
                    AppContext.SaveChanges();

                    var blankType = AppContext.OBK_BlankType.FirstOrDefault(o => BlankTypes.ZBK.Equals(o.Code));

                    var blankNumber = new OBK_BlankNumber();
                    blankNumber.Id = Guid.NewGuid();
                    blankNumber.Number = int.Parse(ecp.ecBlankNumber);
                    blankNumber.CreateDate = DateTime.Now;
                    blankNumber.Object_Id = sed.Id;
                    blankNumber.BlankTypeId = blankType.Id;
                    blankNumber.EmployeeId = UserHelper.GetCurrentEmployee().Id;
                    blankNumber.Corrupted = false;
                    AppContext.OBK_BlankNumber.Add(blankNumber);
                    AppContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public OBKExpertiseConclusionNegative ExpertiseConclusionNegative(int productSeriesId, Guid adId)
        {
            var ad = AppContext.OBK_AssessmentDeclaration.FirstOrDefault(e => e.Id == adId);
            var productSeries = AppContext.OBK_Procunts_Series.FirstOrDefault(e => e.Id == productSeriesId);
            if (ad == null) return null;
            var ecn = new OBKExpertiseConclusionNegative();
            ecn.AssessmentDeclarationId = adId;
            ecn.ProductSeriesId = productSeriesId;
            ecn.ProductId = productSeries?.OBK_RS_Products.Id;
            ecn.Reasons = new SelectList(new SafetyAssessmentRepository().GetRefReasons("Party", false), "Id", "Name");
            return ecn;
        }

        public bool SaveExpertiseConclusionNegative(OBKExpertiseConclusionNegative ecn)
        {
            try
            {
                var model =
                    AppContext.OBK_StageExpDocument.FirstOrDefault(
                        e => e.AssessmentDeclarationId == ecn.AssessmentDeclarationId &&
                             e.ProductSeriesId == ecn.ProductSeriesId);
                if (model != null)
                {
                    model.ExpResult = false;
                    model.ExpApplication = false;
                    model.ExpReasonNameRu = ecn.ExpReasonNameRu;
                    model.ExpReasonNameKz = ecn.ExpReasonNameKz;
                    model.ProductId = ecn.ProductId;                   
                    model.ProductSeriesId = ecn.ProductSeriesId;
                    model.RefReasonId = ecn.RefReasonId;
                    model.AssessmentDeclarationId = ecn.AssessmentDeclarationId;
                    model.ExecutorId = UserHelper.GetCurrentEmployee().Id;
                    AppContext.SaveChanges();
                    return true;
                }
                OBK_StageExpDocument sed = new OBK_StageExpDocument
                {
                    Id = Guid.NewGuid(),
                    ExpResult = false,
                    ExpApplication = false,
                    ExpReasonNameRu = ecn.ExpReasonNameRu,
                    ExpReasonNameKz = ecn.ExpReasonNameKz,
                    ProductId = ecn.ProductId,
                    ProductSeriesId = ecn.ProductSeriesId,
                    RefReasonId = ecn.RefReasonId,
                    AssessmentDeclarationId = ecn.AssessmentDeclarationId,
                    ExecutorId = UserHelper.GetCurrentEmployee().Id
                };
                AppContext.OBK_StageExpDocument.Add(sed);
                AppContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public OBKExpertiseConclusionPositive ShowExpertiseConclusionPositive(int productSeriesId, Guid adId)
        {
            var ps =
                AppContext.OBK_StageExpDocument.FirstOrDefault(
                    e => e.AssessmentDeclarationId == adId && e.ProductSeriesId == productSeriesId);

            if (ps == null) return null;
            var ecp = new OBKExpertiseConclusionPositive();
            ecp.ToShow = true;
            ecp.AssessmentDeclarationId = adId;
            ecp.ProductSeriesId = productSeriesId;
            ecp.ecEndDate = ps.ExpEndDate?.ToString();
            ecp.ecReasonNameRu = ps.ExpReasonNameRu;
            ecp.ecReasonNameKz = ps.ExpReasonNameKz;
            ecp.ecProductNameRu = ps.ExpProductNameRu;
            ecp.ecProductNameKz = ps.ExpProductNameKz;
            ecp.ecAdditionalInfoRu = ps.ExpAddInfoRu;
            ecp.ecAdditionalInfoKz = ps.ExpAddInfoKz;
            ecp.ecNumber = ps.ExpConclusionNumber;
            ecp.ecApplicationNumber = ps.ExpApplicationNumber;
            ecp.ecBlankNumber = ps.ExpBlankNumber;
            return ecp;
        }

        public OBKExpertiseConclusionNegative ShowExpertiseConclusionNegative(int productSeriesId, Guid adId)
        {
            var ps =
                AppContext.OBK_StageExpDocument.FirstOrDefault(
                    e => e.AssessmentDeclarationId == adId && e.ProductSeriesId == productSeriesId);

            if (ps != null)
            {
                OBKExpertiseConclusionNegative ecn = new OBKExpertiseConclusionNegative();
                ecn.ToShow = true;
                ecn.AssessmentDeclarationId = adId;
                ecn.ProductSeriesId = productSeriesId;
                ecn.ExpReasonNameRu = ps.ExpReasonNameRu;
                ecn.ExpReasonNameKz = ps.ExpReasonNameKz;
                ecn.Reasons = new SelectList(new SafetyAssessmentRepository().GetRefReasons("Party", false), "Id",
                    "Name", ps.RefReasonId);
                return ecn;
            }
            return null;
        }

        public OBKTaskComment GetTaskComments(Guid rcId, Guid teId)
        {
            var comments = AppContext.OBK_ResearchCenterResultCom.Where(e => e.ResearchCenterResultId == rcId);
            var tc = new OBKTaskComment();
            tc.TaskExecutorId = teId;
            tc.ResearchCenterId = rcId;
            if (!comments.Any()) return tc;
            var tComs = new List<TaskComments>();
            foreach (var c in comments)
            {
                var tCom = new TaskComments();
                tCom.AutorName = c.Employee.DisplayName;
                tCom.Note = c.Note;
                tCom.Createdate = c.Createdate;
                tComs.Add(tCom);
            }
            tc.TaskComment = tComs;
            return tc;
        }

        public void SaveTaskComment(Guid rcId, string note)
        {
            var userId = UserHelper.GetCurrentEmployee().Id;
            var rcr = new OBK_ResearchCenterResultCom();
            rcr.Id = Guid.NewGuid();
            rcr.Createdate = DateTime.Now;
            rcr.Note = note;
            rcr.ResearchCenterResultId = rcId;
            rcr.UserId = userId;
            AppContext.OBK_ResearchCenterResultCom.Add(rcr);
            AppContext.SaveChanges();
        }

        public void ReturnToResearchCenter(Guid adId, int psId)
        {
            var tms = AppContext.OBK_TaskMaterial.Where(
                e => e.ProductSeriesId == psId && e.OBK_Tasks.AssessmentDeclarationId == adId &&
                     e.OBK_ResearchCenterResult.Any(x => x.TaskMaterialId == e.Id && x.OBK_ResearchCenterResultCom.Any()));
            var tes = AppContext.OBK_TaskExecutor.Where(
                e => tms.Any(x => x.TaskId == e.TaskId) && e.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_ASSIGNING); //&& e.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_ASSIGNING
            var tebs = AppContext.OBK_TaskExecutor.Where(e => tms.Any(x => x.TaskId == e.TaskId) && e.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_SIGNER);
            var statusId = new SafetyAssessmentRepository().GetStageStatusByCode(OBK_Ref_StageStatus.InReWork).Id;
            foreach (var te in tes)
            {
                foreach (var tm in tms)
                {
                    if (tm.UnitLaboratoryId != GetUnitLaboratory(te.ExecutorId)) continue;
                    te.IsCompleted = false;
                    te.SignedData = null;
                    tm.StatusId = statusId;
                }
            }
            foreach (var teb in tebs)
            {
                teb.IsCompleted = false;
                teb.SignedData = null;
            }
            AppContext.SaveChanges();
        }

        public void ReturnToResearchCenters(List<OBKReturnToResearchCenter> rtrcs)
        {
            foreach (var rtrc in rtrcs)
            {
                var tmes = AppContext.OBK_TaskMaterailExecutor.Where(e => rtrc.TaskmaterailId == e.TaskMaterialId);
                var tms = AppContext.OBK_TaskMaterial.Where(e => rtrc.TaskmaterailId == e.Id);
                //Директор ИЦ и подписывающий
                var tebs = AppContext.OBK_TaskExecutor.Where(e => tms.Any(x => x.TaskId == e.TaskId));
                var labTypeId = AppContext.OBK_ResearchCenterResult.First(e => e.Id == rtrc.ResearchCenterId).OBK_TaskMaterial.LaboratoryTypeId;
                var statusId = new SafetyAssessmentRepository().GetStageStatusByCode(OBK_Ref_StageStatus.InReWork).Id;
                foreach (var tme in tmes)
                {
                    if (rtrc.TaskExecutorId != tme.TaskExecutorId) continue;
                    tme.OBK_TaskExecutor.IsCompleted = false;
                    tme.OBK_TaskExecutor.SignedData = null;
                    tme.OBK_TaskMaterial.StatusId = statusId;
                }
                foreach (var teb in tebs)
                {
                    if ((teb.ExecutorType != OBK_CONTRACT_STAGE_EXECUTOR_TYPE_ASSIGNING || teb.OBK_TaskMaterailExecutor.Any(x=>x.OBK_TaskMaterial.LaboratoryTypeId != labTypeId)) && teb.ExecutorType != OBK_CONTRACT_STAGE_EXECUTOR_TYPE_SIGNER) continue;
                    teb.IsCompleted = false;
                    teb.SignedData = null;
                }
            }
            AppContext.SaveChanges();
        }

        public Guid GetUnitLaboratory(Guid userId)
        {
            var employe = AppContext.Employees.FirstOrDefault(e => e.Id == userId);
            var unit = AppContext.Units.FirstOrDefault(e => e.Id == employe.PositionId);
            if(unit==null)return Guid.Empty;
            return unit.Parent.Id;
        }

        public bool GetValidShowSignBtn(Guid adId)
        {
            var ad = AppContext.OBK_AssessmentDeclaration.FirstOrDefault(e => e.Id == adId);
            if (ad == null) return false;
            foreach (var p in ad.OBK_Contract.OBK_RS_Products)
            {
                foreach (var ps in p.OBK_Procunts_Series)
                {
                    var sed = ps.OBK_StageExpDocument.FirstOrDefault(e => e.ProductSeriesId == ps.Id);
                    if (sed == null)
                        return false;
                }
            }
            return true;
        }

        public object GetExpRejectFormData(Guid id, int productSeriesId, int pid)
        {
            var ad = AppContext.OBK_AssessmentDeclaration.FirstOrDefault(e => e.Id == id);
            if (ad == null) return null;
            var product = AppContext.OBK_RS_Products.FirstOrDefault(e => e.Id == pid);
            var psIds = AppContext.OBK_Procunts_Series.Where(e => e.OBK_RS_Products.Id == pid).Select(x=>x.Id).ToList();
            var rcrs = AppContext.OBK_ResearchCenterResult.Where(e => psIds.Contains(e.OBK_TaskMaterial.ProductSeriesId) && (bool)!e.ExpertiseResult).ToList();
            var uad = AppContext.UnitsAddresses.OrderByDescending(e=>e.CreatedDate).FirstOrDefault(e => e.UnitsId == ad.OBK_Contract.Unit.Id && (bool)!e.IsDeleted);
            var obj = new
            {
                UnitNameRu = ad.OBK_Contract.Unit.Name,
                UnitNameKz = ad.OBK_Contract.Unit.NameKz,
                UnitAddressRu = uad?.AddressNameRu,
                UnitAddressKz = uad?.AddressNameKz,
                DeclarantNameRu = ad.OBK_Contract.OBK_Declarant.NameRu,
                DeclarantNameKz = ad.OBK_Contract.OBK_Declarant.NameKz,
                BossFio = ad.OBK_Contract.OBK_DeclarantContact.BossLastName + " " +
                          ad.OBK_Contract.OBK_DeclarantContact.BossFirstName + " " +
                          ad.OBK_Contract.OBK_DeclarantContact.BossMiddleName,
                DeclarantAddressRu = ad.OBK_Contract.OBK_DeclarantContact.AddressLegalRu,
                DeclarantAddressKz = ad.OBK_Contract.OBK_DeclarantContact.AddressLegalKz,
                //ResultDate = ad.OBK_StageExpDocument.FirstOrDefault(e => e.AssessmentDeclarationId == id && !e.ExpResult)?.ExpStartDate,
                UniqueNumber = ad.Number,
                StartDate = ad.SendDate,
                LabIndex = product?.RegTypeId == 1 ? product.NdName + " " + product.NdNumber + " от " + product.RegDate.ToShortDateString() : "СП Фирмы по показателям токсичность",
                Products = AppContext.OBK_StageExpDocument.Where(e => psIds.Contains((int)e.ProductSeriesId) && !e.ExpResult).Select(sed => new
                {
                    sed.OBK_Procunts_Series.OBK_RS_Products.NameRu,
                    sed.OBK_Procunts_Series.OBK_RS_Products.NameKz,
                    sed.OBK_Procunts_Series.OBK_RS_Products.ProducerNameRu,
                    sed.OBK_Procunts_Series.OBK_RS_Products.ProducerNameKz,
                    sed.OBK_Procunts_Series.OBK_RS_Products.CountryNameRu,
                    sed.OBK_Procunts_Series.OBK_RS_Products.CountryNameKZ,
                    sed.OBK_Procunts_Series.Series,
                    sed.OBK_Procunts_Series.SeriesEndDate,
                    sed.OBK_Procunts_Series.SeriesParty,
                    sed.OBK_Procunts_Series.sr_measures.name,
                    sed.OBK_Procunts_Series.sr_measures.name_kz
                }).ToList(),
                ProtocolNumbers = string.Join(", ", rcrs.Select(e=>e.OBK_TaskMaterial.SubTaskNumber)),
                ProtocolDate = $"{rcrs?.FirstOrDefault()?.OBK_TaskMaterial.CreatedDate:dd.MM.yyyy}"
            };
            return obj;
        }

        #endregion



    }
}

