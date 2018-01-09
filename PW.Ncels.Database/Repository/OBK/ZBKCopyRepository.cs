using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Ncels.Helpers;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Models.Expertise;
using PW.Ncels.Database.Models.OBK;
using PW.Ncels.Database.Repository.Expertise;
using System.Data.Objects.SqlClient;
using System.Web.Script.Serialization;
using PW.Ncels.Database.Notifications;

namespace PW.Ncels.Database.Repository.OBK
{
    public class ZBKCopyRepository : ARepository
    {

        /// <summary>
        /// Отправка этапов ОБК в работу выбранным исполнителям
        /// </summary>
        /// <param name="stageIds"></param>
        /// <param name="executorIds"></param>
        public void SendToWork(Guid[] stageIds, Guid[] executorIds)
        {
            var stages = AppContext.OBK_ZBKCopyStage.Where(e => stageIds.Contains(e.Id)).ToList();
            var executors = AppContext.Employees.Where(e => executorIds.Contains(e.Id)).ToList();

            foreach (var stage in stages)
            {
                //ZBKCopyStageExeutor
                foreach (var executor in executors)
                {
                    var stageExecutor = new OBK_ZBKCopyStageExecutors
                    {
                        ZBKCopyStageId = stage.Id,
                        ExecutorId = executor.Id,
                        ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR
                    };

                    stage.StageStatusId = GetStageStatusByCode(OBK_Ref_StageStatus.InWork).Id;
                    if (stage.StageId == CodeConstManager.STAGE_OBK_COZ)
                    {
                        var copy = AppContext.OBK_ZBKCopy.FirstOrDefault(o => o.Id == stage.OBK_ZBKCopyId);
                        var expDocument = AppContext.OBK_StageExpDocument.FirstOrDefault(o => o.Id == copy.OBK_StageExpDocumentId);
                        var text = "Поступил новый запрос на оформление копий ЗБК по заключению №<" + expDocument.ExpConclusionNumber + ">";
                        var notification = new NotificationManager().SendNotification(text, ObjectType.OBK_ZBKCopy, copy.Id, executor.Id);
                    }

                    if(stage.StageId == CodeConstManager.STAGE_OBK_EXPERTISE_DOC)
                    {
                        var copy = AppContext.OBK_ZBKCopy.FirstOrDefault(o => o.Id == stage.OBK_ZBKCopyId);
                        var expDocument = AppContext.OBK_StageExpDocument.FirstOrDefault(o => o.Id == copy.OBK_StageExpDocumentId);
                        var declaration = AppContext.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == expDocument.AssessmentDeclarationId);
                        var contract = AppContext.OBK_Contract.FirstOrDefault(o => o.Id == declaration.ContractId);
                        var declarant = AppContext.OBK_Declarant.FirstOrDefault(o => o.Id == contract.DeclarantId);
                        var text = "Поступил новый запрос на оформление копий ЗБК от <" + declarant.NameRu + ">. №<" + expDocument.ExpConclusionNumber + ">";

                        var notification = new NotificationManager().SendNotification(text, ObjectType.OBK_ZBKCopy, copy.Id, executor.Id);
                    }

                    AppContext.OBK_ZBKCopyStageExecutors.AddOrUpdate(stageExecutor);
                    AppContext.SaveChanges();
                }
            }
        }

        public object GetZBKCopyCost(Guid id)
        {
            var copy = AppContext.OBK_ZBKCopy.FirstOrDefault(o => o.Id == id);
            var refType = AppContext.OBK_Ref_Type.FirstOrDefault(o => o.Code == "5");
            var priceList = AppContext.OBK_Ref_PriceList.FirstOrDefault(o => o.TypeId == refType.Id);
            var unit = AppContext.Dictionaries.FirstOrDefault(o => o.Id == priceList.UnitId);
            var payment = AppContext.OBK_DirectionToPayments.FirstOrDefault(o => o.ZBKCopy_id == id);

            var result = new
            {
                copyQuantity = copy.CopyQuantity,
                name = priceList.NameRu,
                price = priceList.Price,
                unit = unit.Name,
                totalWithNds = GetTotalPriceCount(copy),
                totalWithoutNds = GetTotalPriceWithoutNds(copy),
                invoice1c = payment.InvoiceNumber1C,
                InvoiceDatetime1C = payment.InvoiceDatetime1C == null ? "" : ((DateTime)payment.InvoiceDatetime1C).ToString("dd.MM.yyyy"),
                expApplication = copy.ExpApplication,
            };
            return result;
        }

        public bool SendToOBK(Guid id)
        {
            var stage = AppContext.OBK_ZBKCopyStage.FirstOrDefault(o => o.OBK_ZBKCopyId == id);
            return true;
        }

        public OBK_ZBKCopyBlank InitBlankNumber(Guid zbkCopyId)
        {
            return AppContext.OBK_ZBKCopyBlank.FirstOrDefault(o => o.ZBKCopyId == zbkCopyId);
        }

        public OBK_ZBKCopyBlank SaveStartBlankNumber(string startNumber, Guid zbkCopyId, bool expApplication)
        {
            var blank = AppContext.OBK_ZBKCopyBlank.FirstOrDefault(o => o.ZBKCopyId == zbkCopyId);
            if (blank == null)
            {
                blank = new OBK_ZBKCopyBlank()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = DateTime.Now,
                    ZBKCopyId = zbkCopyId,
                };
            }

            blank.EmployeeId = UserHelper.GetCurrentEmployee().Id;
            blank.StartNumber = startNumber;
            int? startNum = int.Parse(startNumber);

            var copy = AppContext.OBK_ZBKCopy.FirstOrDefault(o => o.Id == zbkCopyId);

            int? endPrimeNumber = startNum + copy.CopyQuantity - 1;
            blank.EndPrimeNumber = endPrimeNumber.ToString();

            if (expApplication == false)
            {
                blank.StartApplicationNumber = (endPrimeNumber + 1).ToString();
                blank.EndApplicationNumber = (endPrimeNumber + copy.CopyQuantity).ToString();
            }

            AppContext.OBK_ZBKCopyBlank.AddOrUpdate(blank);
            AppContext.SaveChanges();

            return blank;
        }

        public bool SaveReceiver(string receiver, DateTime? receiveDate, Guid zbkCopyId, bool zbkCopiesReady)
        {
            var copy = AppContext.OBK_ZBKCopy.FirstOrDefault(o => o.Id == zbkCopyId);
            if (copy == null)
            {
                return false;
            }
            copy.ExtraditeDate = receiveDate;
            copy.ReceiverFIO = receiver;
            copy.zbkCopiesReady = zbkCopiesReady;

            var finalStatus = AppContext.OBK_Ref_Status.FirstOrDefault(o => CodeConstManager.STATUS_OBK_CONCLUSION_ISSUE.ToString().Equals(o.Code));
            copy.StatusId = finalStatus.Id;

            var stage = AppContext.OBK_ZBKCopyStage.FirstOrDefault(o => o.OBK_ZBKCopyId == zbkCopyId && o.StageId == CodeConstManager.STAGE_OBK_COZ);
            var stageStatus = AppContext.OBK_Ref_StageStatus.FirstOrDefault(o => OBK_Ref_StageStatus.Completed.Equals(o.Code));
            stage.StageStatusId = stageStatus.Id;

            AppContext.SaveChanges();

            return true;
        }

        public List<ZBKTransferRegister> ZBKTransferRegister()
        {
            List<OBK_ZBKCopy> list = AppContext.OBK_ZBKCopy.Where(o => o.ExtraditeDate != null).ToList();
            List<ZBKTransferRegister> registerList = new List<ZBKTransferRegister>();
            int i = 1;
            foreach (var temp in list)
            {

                ZBKTransferRegister model = new ZBKTransferRegister();
                var stageExpDocument = AppContext.OBK_StageExpDocument.FirstOrDefault(o => o.Id == temp.OBK_StageExpDocumentId);
                var declaration = AppContext.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == stageExpDocument.AssessmentDeclarationId);
                var contract = AppContext.OBK_Contract.FirstOrDefault(o => o.Id == declaration.ContractId);
                var declarantContact = AppContext.OBK_DeclarantContact.FirstOrDefault(o => o.Id == contract.DeclarantContactId);
                var declarant = AppContext.OBK_Declarant.FirstOrDefault(o => o.Id == contract.DeclarantId);
                var organization = AppContext.Dictionaries.FirstOrDefault(o => o.Id == declarant.OrganizationFormId);
                var refType = AppContext.OBK_Ref_Type.FirstOrDefault(o => o.Id == declaration.TypeId);
                
                model.Declarer = declarantContact.BossLastName + " " + declarantContact.BossFirstName
                    + " " + declarantContact.BossMiddleName;
                model.ConclusionNumber = stageExpDocument.ExpConclusionNumber;
                model.SendDate = temp.SendDate;
                model.DrugFormFullName = stageExpDocument.ExpProductNameRu;
                model.RequestType = "Копия";
                model.ExtraditeDate = temp.ExtraditeDate;
                model.ReceiverFIO = temp.ReceiverFIO;
                model.Order = i;
                registerList.Add(model);
                i++;
            }

            return registerList;
        }

        public bool SendToPayment(Guid id)
        {
            var zbkCopy = AppContext.OBK_ZBKCopy.Include(o => o.OBK_StageExpDocument).FirstOrDefault(x => x.Id == id);
            var declaration = AppContext.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == zbkCopy.OBK_StageExpDocument.AssessmentDeclarationId);
            var contract = AppContext.OBK_Contract.FirstOrDefault(o => o.Id == declaration.ContractId);
            var payment = AppContext.OBK_DirectionToPayments.FirstOrDefault(e => e.ZBKCopy_id == id);
            var stage = AppContext.OBK_ZBKCopyStage.FirstOrDefault(o => o.OBK_ZBKCopyId == id);

            if (stage.SendToAccountant != true)
            {
                stage.SendToAccountant = true;
            }

            var pay = new OBK_DirectionToPayments();
            if (zbkCopy != null)
            {
                if (payment == null)
                {
                    pay.Id = Guid.NewGuid();
                    pay.CreateDate = DateTime.Now;
                    pay.ZBKCopy_id = id;
                    pay.ContractId = contract.Id;
                    pay.CreateEmployeeId = UserHelper.GetCurrentEmployee().Id;
                    pay.CreateEmployeeValue = UserHelper.GetCurrentEmployee().DisplayName;
                    pay.DirectionDate = DateTime.Now;
                    pay.Number = contract.Number;
                    pay.PayerId = contract.OBK_Declarant.Id;
                    pay.PayerValue = contract.OBK_Declarant.NameRu;
                    pay.IsDeleted = false;
                    pay.TotalPrice = zbkCopy.ExpApplication == false ? GetTotalPriceCount(zbkCopy) * 2 : GetTotalPriceCount(zbkCopy);
                    pay.StatusId = GetPaymentStatus(OBK_Ref_PaymentStatus.ReqSign).Id;

                    //заглушка
                    Random r = new Random();
                    pay.InvoiceNumber1C = "ALM00012091" + r.Next(100, 999);
                    pay.InvoiceDatetime1C = DateTime.Now;
                    pay.OBK_DirectionSignData = new OBK_DirectionSignData
                    {
                        DirectionToPaymentId = pay.Id,
                        ChiefAccountantId = null,//Guid.Parse("E1EE3658-0C35-41EB-99FD-FDDC4D07CEC4"),
                        ExecutorId = null//Guid.Parse("55377FAC-A5F0-4093-BBB6-18BD28E53BE1")
                    };

                    AppContext.OBK_DirectionToPayments.Add(pay);
                    AppContext.SaveChanges();
                }
            }

            return true;
        }

        /// <summary>
        /// расчет стоимости с НДС
        public decimal GetTotalPriceCount(OBK_ZBKCopy copy)
        {
            var refType = AppContext.OBK_Ref_Type.FirstOrDefault(o => o.Code == "5");
            var ref_PriceList = AppContext.OBK_Ref_PriceList.FirstOrDefault(o => o.TypeId == refType.Id);
            if (copy.ExpApplication == false)
            {
                return Math.Round(Convert.ToDecimal(TaxHelper.GetCalculationTax(ref_PriceList.Price) * copy.CopyQuantity * 2), 2);
            }
            return Math.Round(Convert.ToDecimal(TaxHelper.GetCalculationTax(ref_PriceList.Price) * copy.CopyQuantity), 2);
        }

        /// <summary>
        /// расчет стоимости без НДС
        public decimal GetTotalPriceWithoutNds(OBK_ZBKCopy copy)
        {
            var refType = AppContext.OBK_Ref_Type.FirstOrDefault(o => o.Code == "5");
            var ref_PriceList = AppContext.OBK_Ref_PriceList.FirstOrDefault(o => o.TypeId == refType.Id);

            return Math.Round(Convert.ToDecimal(ref_PriceList.Price * copy.CopyQuantity), 2);
        }

        /// <summary>
        /// расчет 1-копий с НДС
        public decimal GetZbkCopyNds(Guid copyId)
        {
            var refType = AppContext.OBK_Ref_Type.FirstOrDefault(o => o.Code == "5");
            var ref_PriceList = AppContext.OBK_Ref_PriceList.FirstOrDefault(o => o.TypeId == refType.Id);
            return Math.Round(Convert.ToDecimal(TaxHelper.GetCalculationTax(ref_PriceList.Price)), 2);
        }

        public OBK_Ref_PaymentStatus GetPaymentStatus(string code)
        {
            return AppContext.OBK_Ref_PaymentStatus.First(e => e.Code == code);
        }

        public OBK_Ref_StageStatus GetStageStatusByCode(string code)
        {
            return AppContext.OBK_Ref_StageStatus.AsNoTracking().FirstOrDefault(e => e.Code == code);
        }

        public bool SaveOriginals(Guid zbkCopyId)
        {
            var copy = AppContext.OBK_ZBKCopy.FirstOrDefault(o => o.Id == zbkCopyId);
            if (copy == null)
            {
                return false;
            }
            copy.OriginalsGiven = true;

            var stage = AppContext.OBK_ZBKCopyStage.FirstOrDefault(o => o.OBK_ZBKCopyId == zbkCopyId);
            DublicateStageToOBK(stage);

            var expDocument = AppContext.OBK_StageExpDocument.FirstOrDefault(o => o.Id == copy.OBK_StageExpDocumentId);
            var declaration = AppContext.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == expDocument.AssessmentDeclarationId);
            var contract = AppContext.OBK_Contract.FirstOrDefault(o => o.Id == declaration.ContractId);
            var declarant = AppContext.OBK_Declarant.FirstOrDefault(o => o.Id == contract.DeclarantId);
            var text = "Поступил новый запрос на оформление копий ЗБК от <" + declarant.NameRu + ">. №<" + expDocument.ExpConclusionNumber + ">";
            var stageObk = AppContext.OBK_AssessmentStage.FirstOrDefault(o => o.StageId == CodeConstManager.STAGE_OBK_EXPERTISE_DOC && o.DeclarationId == declaration.Id);
            var stageExecutorCoz = AppContext.OBK_AssessmentStageExecutors.FirstOrDefault(o => o.AssessmentStageId == stageObk.Id
            && o.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_ASSIGNING);

            var notification = new NotificationManager().SendNotification(text, ObjectType.OBK_ZBKCopy, copy.Id, stageExecutorCoz.ExecutorId);

            AppContext.SaveChanges();

            return true;
        }

        public bool ConfirmPaperCopy(Guid zbkCopyId)
        {
            var copy = AppContext.OBK_ZBKCopy.FirstOrDefault(o => o.Id == zbkCopyId);
            if (copy == null)
            {
                return false;
            }
            copy.zbkCopiesReady = true;

            var notification = new NotificationManager().SendNotificationFromCompany(
                         "Ваш запрос на копии ЗБК готов. Просим вас забрать копии ЗБК.",
                         ObjectType.OBK_ZBKCopy, copy.Id.ToString(), (Guid)copy.EmployeeId);

            AppContext.SaveChanges();

            return true;
        }

        public void DublicateStageToOBK(OBK_ZBKCopyStage cozStage)
        {
            var stage = AppContext.OBK_Ref_Stage.FirstOrDefault(o => CodeConstManager.STAGE_OBK_EXPERTISE_DOC.ToString().Equals(o.Code));
            var stageStatus = AppContext.OBK_Ref_StageStatus.FirstOrDefault(o => EXP_DIC_StageStatus.New.ToString().Equals(o.Code));

            OBK_ZBKCopyStage obk = new OBK_ZBKCopyStage()
            {
                Id = Guid.NewGuid(),
                StartDate = DateTime.Now,
                OBK_ZBKCopyId = cozStage.OBK_ZBKCopyId,
                SendToAccountant = cozStage.SendToAccountant,
                StageId = stage.Id,
                StageStatusId = stageStatus.Id
            };

            AppContext.OBK_ZBKCopyStage.Add(obk);
        }

        public List<ZBKViewModel> ListZBKCopies(int stage)
        {
            var list = AppContext.OBK_ZBKCopyStage.Where(o => o.StageId == stage).ToList();
            //var list = AppContext.OBK_ZBKCopyStage.ToList();
            List<ZBKViewModel> models = new List<ZBKViewModel>();

            foreach (var temp in list)
            {
                ZBKViewModel model = new ZBKViewModel();

                var copy = AppContext.OBK_ZBKCopy.FirstOrDefault(o => o.Id == temp.OBK_ZBKCopyId);

                var stageExpDocument = AppContext.OBK_StageExpDocument.FirstOrDefault(o => o.Id == copy.OBK_StageExpDocumentId);
                var declaration = AppContext.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == stageExpDocument.AssessmentDeclarationId);
                var contract = AppContext.OBK_Contract.FirstOrDefault(o => o.Id == declaration.ContractId);
                var declarantContact = AppContext.OBK_DeclarantContact.FirstOrDefault(o => o.Id == contract.DeclarantContactId);
                var declarant = AppContext.OBK_Declarant.FirstOrDefault(o => o.Id == contract.DeclarantId);
                var organization = AppContext.Dictionaries.FirstOrDefault(o => o.Id == declarant.OrganizationFormId);
                var refType = AppContext.OBK_Ref_Type.FirstOrDefault(o => o.Id == declaration.TypeId);
                var status = AppContext.OBK_Ref_StageStatus.FirstOrDefault(o => o.Id == temp.StageStatusId);

                OBK_ZBKCopy zbkCopy = AppContext.OBK_ZBKCopy.FirstOrDefault(o => o.Id == temp.OBK_ZBKCopyId);

                model.Declarer = declarantContact.BossLastName + " " + declarantContact.BossFirstName
                    + " " + declarantContact.BossMiddleName;
                model.ConclusionNumber = stageExpDocument.ExpConclusionNumber;
                model.ContractNumber = contract.Number;
                model.DeclarationNumber = declaration.Number;
                model.StartDate = stageExpDocument.ExpStartDate;
                model.ExpireDate = stageExpDocument.ExpEndDate;
                model.OrganizationName = organization.Name;
                model.CopyQuantity = zbkCopy.CopyQuantity;
                model.DeclarationType = refType.NameRu;
                model.ContractId = contract.Id;
                model.AttachPath = zbkCopy.AttachPath;
                model.Id = zbkCopy.Id;
                model.StageId = temp.Id;
                model.ExpApplication = stageExpDocument.ExpApplication;
                model.StageStatusCode = status.Code;

                models.Add(model);
            }

            return models;
        }

        public string GetSignData(Guid id)
        {
            var zbkCopy = AppContext.OBK_ZBKCopy.FirstOrDefault(o => o.Id == id);

            if (zbkCopy == null)
                return null;

            OBK_ZBKCopy zc = new OBK_ZBKCopy
            {
                Id = zbkCopy.Id,
                OBK_StageExpDocumentId = zbkCopy.Id
            };
            var xmlData = SerializeHelper.SerializeDataContract(zc);
            return xmlData.Replace("utf-16", "utf-8");
        }

        public string GetStageSign(Guid zbkCopyStageId)
        {
            var stage = AppContext.OBK_ZBKCopyStage.FirstOrDefault(o => o.Id == zbkCopyStageId);

            if (stage == null)
                return null;

            OBK_ZBKCopyStage zc = new OBK_ZBKCopyStage
            {
                Id = stage.Id,
                OBK_ZBKCopyId = stage.OBK_ZBKCopyId
            };
            var xmlData = SerializeHelper.SerializeDataContract(zc);
            return xmlData.Replace("utf-16", "utf-8");
        }

        public string SaveSignZBKCopy(Guid id, string signedData)
        {
            OBK_ZBKCopySignData signData = new OBK_ZBKCopySignData();
            signData.Id = Guid.NewGuid();
            signData.OBK_ZBKCopyId = id;
            signData.SignDateTime = DateTime.Now;
            signData.SignerId = UserHelper.GetCurrentEmployee().Id;
            signData.SignXmlData = signedData;

            AppContext.OBK_ZBKCopySignData.Add(signData);
            AppContext.SaveChanges();

            return "Успешно подписан!";
        }

        public string SaveSignedZbkCopyStage(Guid id, string signedData)
        {
            OBK_ZBKCopyStageSignData signData = new OBK_ZBKCopyStageSignData();
            signData.Id = Guid.NewGuid();
            signData.StageId = id;
            signData.SignDateTime = DateTime.Now;
            signData.SignerId = UserHelper.GetCurrentEmployee().Id;
            signData.SignXmlData = signedData;

            AppContext.OBK_ZBKCopyStageSignData.Add(signData);

            var stage = AppContext.OBK_ZBKCopyStage.FirstOrDefault(o => o.Id == id);
            var copy = AppContext.OBK_ZBKCopy.FirstOrDefault(o => o.Id == stage.OBK_ZBKCopyId);
            var copyStatus = AppContext.OBK_Ref_Status.FirstOrDefault(o => CodeConstManager.STATUS_OBK_SIGN_ACT.ToString().Equals(o.Code));
            copy.StatusId = copyStatus.Id;

            var stageStatus = AppContext.OBK_Ref_StageStatus.FirstOrDefault(o => OBK_Ref_StageStatus.Completed.ToString().Equals(o.Code));
            stage.StageStatusId = stageStatus.Id;

            var stageCoz = AppContext.OBK_ZBKCopyStage.FirstOrDefault(o => o.OBK_ZBKCopyId == copy.Id && o.StageId == CodeConstManager.STAGE_OBK_COZ);
            var stageStatusCoz = AppContext.OBK_Ref_StageStatus.FirstOrDefault(o => OBK_Ref_StageStatus.RequiresIssuingZBKCopy.ToString().Equals(o.Code));
            stageCoz.StageStatusId = stageStatusCoz.Id;

            var stageExecutors = AppContext.OBK_ZBKCopyStageExecutors.FirstOrDefault(o => o.ZBKCopyStageId == stage.Id);
            var executor = AppContext.Employees.FirstOrDefault(o => o.Id == stageExecutors.ExecutorId);

            var notification = new NotificationManager().SendNotificationFromCompany(
                           "Уведомление о поступлении акта выполненных работ",
                           ObjectType.OBK_ZBKCopy, copy.Id.ToString(), (Guid)copy.EmployeeId);

            AppContext.SaveChanges();

            return "Успешно подписан!";
        }



        public IQueryable<object> ZBKCopies(Guid? declarationNumber, int? declarationType, string decisionNumber, DateTime? decisionDate)
        {
            var userId = UserHelper.GetCurrentEmployee().Id;
            var statusId = AppContext.OBK_Ref_Status.FirstOrDefault(o => CodeConstManager.STATUS_OBK_CONCLUSION_ISSUE.ToString().Equals(o.Code)).Id;

            var data = from StageExpDoc in AppContext.OBK_StageExpDocument
                       join adec in AppContext.OBK_AssessmentDeclaration
                            on StageExpDoc.AssessmentDeclarationId equals adec.Id into AssessmentDeclaraion
                       from AssessDec in AssessmentDeclaraion.DefaultIfEmpty()
                       where AssessDec.EmployeeId == userId && AssessDec.StatusId == statusId
                       && StageExpDoc.ExpConclusionNumber != null
                       select new
                       {
                           stageExpDocId = StageExpDoc.Id,
                           expConclusionNumber = StageExpDoc.ExpConclusionNumber,
                           expBlankNumber = StageExpDoc.ExpBlankNumber,
                           expStartDate = StageExpDoc.ExpStartDate,
                           status = StageExpDoc.ExpEndDate > StageExpDoc.ExpStartDate ? "Действующий" : "Срок действия истек",
                           assessDecId = AssessDec.Id,
                           assessDecType = AssessDec.TypeId,
                           employeeId = AssessDec.EmployeeId
                       };

            var res = data.ToList();

            if (decisionNumber != null && !decisionNumber.Equals(""))
            {
                res = res.Where(o => decisionNumber.Equals(o.expConclusionNumber)).ToList();
            }

            if (decisionDate != null)
            {
                res = res.Where(o => decisionDate == o.expStartDate).ToList();
            }

            if (declarationNumber != null && !declarationNumber.Equals(""))
            {
                res = res.Where(o => declarationNumber == o.assessDecId).ToList();
            }

            if (declarationType != null)
            {
                res = res.Where(o => o.assessDecType == declarationType).ToList();
            }

            return res.AsQueryable();
        }

        public IQueryable<object> ZBKCopiesCreated()
        {
            var userId = UserHelper.GetCurrentEmployee().Id;
            var data = from zbkCopy in AppContext.OBK_ZBKCopy
                       join StageExpDoc in AppContext.OBK_StageExpDocument
                            on zbkCopy.OBK_StageExpDocumentId equals StageExpDoc.Id
                       join adec in AppContext.OBK_AssessmentDeclaration
                            on StageExpDoc.AssessmentDeclarationId equals adec.Id into AssessmentDeclaraion
                       from AssessDec in AssessmentDeclaraion.DefaultIfEmpty()
                       where AssessDec.EmployeeId == userId
                       select new
                       {
                           stageExpDocId = StageExpDoc.Id,
                           expConclusionNumber = StageExpDoc.ExpConclusionNumber,
                           expStartDate = StageExpDoc.ExpStartDate,
                           status = StageExpDoc.ExpEndDate >= StageExpDoc.ExpStartDate ? "Действующий" : "Срок действия истек",
                           assessDecId = AssessDec.Id,
                           assessDecType = AssessDec.TypeId,
                           employeeId = AssessDec.EmployeeId,
                           zbkCopyId = (Guid?)zbkCopy.Id,
                           copyQuantity = zbkCopy.CopyQuantity,
                           sendDate = zbkCopy.SendDate
                       };

            var res = data.ToList();

            return res.AsQueryable();
        }

        public IEnumerable<OBK_AssessmentDeclaration> AssessmentDeclarationNumbers()
        {
            var userId = UserHelper.GetCurrentEmployee().Id;
            return AppContext.OBK_AssessmentDeclaration.Where(o => o.EmployeeId == userId);
        }

        public IEnumerable<OBK_Ref_Type> OBK_Ref_Type()
        {
            return AppContext.OBK_Ref_Type.Where(o => o.ViewOption == 1);
        }

        public IQueryable<object> Products(Guid contractId)
        {
            var result = from series in AppContext.OBK_Procunts_Series
                         join product in AppContext.OBK_RS_Products on series.OBK_RS_ProductsId equals product.Id
                         join contract in AppContext.OBK_Contract on product.ContractId equals contract.Id
                         join measure in AppContext.sr_measures on series.SeriesMeasureId equals measure.id
                         where contract.Id == contractId
                         select new
                         {
                             fullName = product.DrugFormFullName,
                             name = product.NameRu,
                             serieParty = series.SeriesParty,
                             country = product.CountryNameRu,
                             producerName = product.ProducerNameRu
                         };

            return result;
        }

        public bool Update(Guid Id, int quantity, DateTime letterDate, string letterNumber)
        {
            var copy = AppContext.OBK_ZBKCopy.FirstOrDefault(o => o.Id == Id);

            if (copy == null)
            {
                return false;
            }

            copy.CopyQuantity = quantity;
            copy.SendDate = DateTime.Now;
            copy.EmployeeId = UserHelper.GetCurrentEmployee().Id;
            copy.StatusId = CodeConstManager.STATUS_OBK_SEND_ID;
            copy.LetterdDate = letterDate;
            copy.LetterNumber = letterNumber;

            OBK_ZBKCopyStage stage = AppContext.OBK_ZBKCopyStage.FirstOrDefault(o => o.OBK_ZBKCopyId == Id);
            if (stage == null)
            {
                var ref_stage = AppContext.OBK_Ref_Stage.FirstOrDefault(o => o.Code.Equals(CodeConstManager.STAGE_OBK_COZ.ToString()));
                var stageStatus = AppContext.OBK_Ref_StageStatus.FirstOrDefault(o => o.Code.Equals(OBK_Ref_StageStatus.New));

                stage = new OBK_ZBKCopyStage();
                stage.Id = Guid.NewGuid();
                stage.OBK_ZBKCopyId = Id;
                stage.StartDate = DateTime.Now;
                stage.StageId = ref_stage.Id;
                stage.StageStatusId = stageStatus.Id;
            }
            else
            {
                var stageStatus = AppContext.OBK_Ref_StageStatus.FirstOrDefault(o => o.Code.Equals(OBK_Ref_StageStatus.InWork));
                stage.StageStatusId = stageStatus.Id;
            }

            var expDocument = AppContext.OBK_StageExpDocument.FirstOrDefault(o => o.Id == copy.OBK_StageExpDocumentId);
            var text = "Поступил новый запрос на оформление копий ЗБК по заключению №<" + expDocument.ExpConclusionNumber + ">";
            var declaration = AppContext.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == expDocument.AssessmentDeclarationId);
            var stageCoz = AppContext.OBK_AssessmentStage.FirstOrDefault(o => o.StageId == CodeConstManager.STAGE_OBK_COZ && o.DeclarationId == declaration.Id);
            var stageExecutorCoz = AppContext.OBK_AssessmentStageExecutors.FirstOrDefault(o => o.AssessmentStageId == stageCoz.Id
            && o.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_ASSIGNING);

            var notification = new NotificationManager().SendNotification(text, ObjectType.OBK_ZBKCopy, copy.Id, stageExecutorCoz.ExecutorId);

            AppContext.OBK_ZBKCopyStage.AddOrUpdate(stage);
            AppContext.SaveChanges();

            return true;
        }

        public ZBKViewModel GetZBKViewModel(Guid? stageId)
        {
            var stage = AppContext.OBK_ZBKCopyStage.FirstOrDefault(o => o.Id == stageId);
            var zbkCopy = AppContext.OBK_ZBKCopy.FirstOrDefault(o => o.Id == stage.OBK_ZBKCopyId);

            ZBKViewModel model = new ZBKViewModel();

            var stageExpDocument = AppContext.OBK_StageExpDocument.FirstOrDefault(o => o.Id == zbkCopy.OBK_StageExpDocumentId);
            var declaration = AppContext.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == stageExpDocument.AssessmentDeclarationId);
            var contract = AppContext.OBK_Contract.FirstOrDefault(o => o.Id == declaration.ContractId);
            var declarantContact = AppContext.OBK_DeclarantContact.FirstOrDefault(o => o.Id == contract.DeclarantContactId);
            var declarant = AppContext.OBK_Declarant.FirstOrDefault(o => o.Id == contract.DeclarantId);
            var organization = AppContext.Dictionaries.FirstOrDefault(o => o.Id == declarant.OrganizationFormId);
            var refType = AppContext.OBK_Ref_Type.FirstOrDefault(o => o.Id == declaration.TypeId);
            var stageStatusCode = AppContext.OBK_Ref_StageStatus.FirstOrDefault(o => o.Id == stage.StageStatusId);

            model.Declarer = declarantContact.BossLastName + " " + declarantContact.BossFirstName
                + " " + declarantContact.BossMiddleName;
            model.ConclusionNumber = stageExpDocument.ExpConclusionNumber;
            model.ContractNumber = contract.Number;
            model.DeclarationNumber = declaration.Number;
            model.StartDate = stageExpDocument.ExpStartDate;
            model.ExpireDate = stageExpDocument.ExpEndDate;
            model.OrganizationName = organization.Name;
            model.CopyQuantity = zbkCopy.CopyQuantity;
            model.DeclarationType = refType.NameRu;
            model.ContractId = contract.Id;
            model.AttachPath = zbkCopy.AttachPath;
            model.Id = zbkCopy.Id;
            model.ExpApplication = stageExpDocument.ExpApplication;
            model.Notes = zbkCopy.Notes;
            model.StageStatusCode = stageStatusCode.Code;
            model.OriginalsGiven = zbkCopy.OriginalsGiven;
            model.StageId = stage.Id;
            model.ExtraditeDate = zbkCopy.ExtraditeDate;
            model.zbkCopiesReady = zbkCopy.zbkCopiesReady;
            model.SendToAccountant = stage.SendToAccountant;
            model.LetterDate = zbkCopy.LetterdDate;
            model.LetterNumber = zbkCopy.LetterNumber;
            model.Nds = TaxHelper.GetNdsRef() + 1;

            return model;
        }

        public ZBKViewModel CreateCopy(Guid stageExpDocId, Guid? ZBKCopyId)
        {

            ZBKViewModel model = new ZBKViewModel();

            var stageExpDocument = AppContext.OBK_StageExpDocument.FirstOrDefault(o => o.Id == stageExpDocId);
            var declaration = AppContext.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == stageExpDocument.AssessmentDeclarationId);
            var contract = AppContext.OBK_Contract.FirstOrDefault(o => o.Id == declaration.ContractId);
            var declarantContact = AppContext.OBK_DeclarantContact.FirstOrDefault(o => o.Id == contract.DeclarantContactId);
            var declarant = AppContext.OBK_Declarant.FirstOrDefault(o => o.Id == contract.DeclarantId);
            var organization = AppContext.Dictionaries.FirstOrDefault(o => o.Id == declarant.OrganizationFormId);
            var refType = AppContext.OBK_Ref_Type.FirstOrDefault(o => o.Id == declaration.TypeId);

            OBK_ZBKCopy zbkCopy = AppContext.OBK_ZBKCopy.FirstOrDefault(o => o.Id == ZBKCopyId);
            var status = AppContext.OBK_Ref_Status.FirstOrDefault(o => o.Code.Equals(CodeConstManager.STATUS_OBK_DRAFT_ID.ToString()));
            if (zbkCopy == null)
            {
                zbkCopy = new OBK_ZBKCopy();
                zbkCopy.Id = Guid.NewGuid();
                zbkCopy.AttachPath = FileHelper.GetObjectPathRoot();
                zbkCopy.OBK_StageExpDocumentId = stageExpDocId;
                zbkCopy.ExpApplication = stageExpDocument.ExpApplication;
                AppContext.OBK_ZBKCopy.Add(zbkCopy);
                AppContext.SaveChanges();
                zbkCopy.StatusId = status.Id;
            }

            model.Declarer = declarantContact.BossLastName + " " + declarantContact.BossFirstName
                + " " + declarantContact.BossMiddleName;
            model.ConclusionNumber = stageExpDocument.ExpConclusionNumber;
            model.ContractNumber = contract.Number;
            model.DeclarationNumber = declaration.Number;
            model.StartDate = stageExpDocument.ExpStartDate;
            model.ExpireDate = stageExpDocument.ExpEndDate;
            model.OrganizationName = organization.Name;
            model.CopyQuantity = zbkCopy.CopyQuantity;
            model.DeclarationType = refType.NameRu;
            model.ContractId = contract.Id;
            model.AttachPath = zbkCopy.AttachPath;
            model.Id = zbkCopy.Id;
            model.ExpApplication = stageExpDocument.ExpApplication;
            model.CopyQuantity = zbkCopy.CopyQuantity;
            model.Nds = TaxHelper.GetNdsRef() + 1;

            return model;
        }

        public bool ReplaceBlank(string blankForReplace, string newBlank, Guid zbkCopyId)
        {
            OBK_ZBKCopyCorruptedBlank blank = new OBK_ZBKCopyCorruptedBlank()
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                EmployeeId = UserHelper.GetCurrentEmployee().Id,
                CorruptedBlankNumber = blankForReplace,
                NewBlankNumber = newBlank,
                ZBKCopyId = zbkCopyId
            };

            AppContext.OBK_ZBKCopyCorruptedBlank.Add(blank);
            AppContext.SaveChanges();

            return true;
        }

        public List<OBKReplacedBlanks> GetReplacedBlanks(Guid zbkCopyId)
        {
            var list = AppContext.OBK_ZBKCopyCorruptedBlank.Where(o => o.ZBKCopyId == zbkCopyId).ToList();
            List<OBKReplacedBlanks> replacedBlanks = new List<OBKReplacedBlanks>();

            foreach (var temp in list)
            {
                replacedBlanks.Add(new OBKReplacedBlanks()
                {
                    CorruptedBlankNumber = temp.CorruptedBlankNumber,
                    NewBlankNumber = temp.NewBlankNumber
                });
            }

            return replacedBlanks;
        }

        public Guid? ContractId(Guid ZBKCopyId)
        {
            var copy = AppContext.OBK_ZBKCopy.FirstOrDefault(o => o.Id == ZBKCopyId);
            var expDoc = AppContext.OBK_StageExpDocument.FirstOrDefault(o => o.Id == copy.OBK_StageExpDocumentId);
            var declalration = AppContext.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == expDoc.AssessmentDeclarationId);

            return declalration.ContractId;
        }

        public ZBKViewModel EditCopy(Guid ZBKCopyId)
        {
            ZBKViewModel model = new ZBKViewModel();
            OBK_ZBKCopy zbkCopy = AppContext.OBK_ZBKCopy.FirstOrDefault(o => o.Id == ZBKCopyId);

            var stageExpDocument = AppContext.OBK_StageExpDocument.FirstOrDefault(o => o.Id == zbkCopy.OBK_StageExpDocumentId);
            var declaration = AppContext.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == stageExpDocument.AssessmentDeclarationId);
            var contract = AppContext.OBK_Contract.FirstOrDefault(o => o.Id == declaration.ContractId);
            var declarantContact = AppContext.OBK_DeclarantContact.FirstOrDefault(o => o.Id == contract.DeclarantContactId);
            var declarant = AppContext.OBK_Declarant.FirstOrDefault(o => o.Id == contract.DeclarantId);
            var organization = AppContext.Dictionaries.FirstOrDefault(o => o.Id == declarant.OrganizationFormId);
            var refType = AppContext.OBK_Ref_Type.FirstOrDefault(o => o.Id == declaration.TypeId);
            var directionPayment = AppContext.OBK_DirectionToPayments.FirstOrDefault(o => o.ZBKCopy_id == ZBKCopyId);
            if (zbkCopy.StatusId != null)
            {
                var refStatus = AppContext.OBK_Ref_Status.FirstOrDefault(o => o.Id == zbkCopy.StatusId);
                if (refStatus != null)
                {
                    model.refStatus = int.Parse(refStatus.Code);
                }
            }

            model.Declarer = declarantContact.BossLastName + " " + declarantContact.BossFirstName
                + " " + declarantContact.BossMiddleName;
            model.ConclusionNumber = stageExpDocument.ExpConclusionNumber;
            model.ContractNumber = contract.Number;
            model.DeclarationNumber = declaration.Number;
            model.StartDate = stageExpDocument.ExpStartDate;
            model.ExpireDate = stageExpDocument.ExpEndDate;
            model.OrganizationName = organization.Name;
            model.CopyQuantity = zbkCopy.CopyQuantity;
            model.DeclarationType = refType.NameRu;
            model.ContractId = contract.Id;
            model.AttachPath = zbkCopy.AttachPath;
            model.Id = zbkCopy.Id;
            model.ExpApplication = stageExpDocument.ExpApplication;
            model.CopyQuantity = zbkCopy.CopyQuantity;
            model.Notes = zbkCopy.Notes;
            model.Nds = TaxHelper.GetNdsRef() + 1;
            model.LetterNumber = zbkCopy.LetterNumber;
            model.LetterDate = zbkCopy.LetterdDate;
            if (directionPayment != null)
            {
                if (directionPayment.ZBKCopy_id != null && directionPayment.InvoiceNumber1C != null)
                {
                    model.PaymentInvoice = true;
                }
            }

            return model;
        }
    }
}
