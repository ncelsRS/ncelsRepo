using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
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







    }
}
