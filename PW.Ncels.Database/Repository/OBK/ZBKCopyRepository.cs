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
                        ZBKCopyId = stage.Id,
                        ExecutorId = executor.Id,
                        ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR
                    };
                    stage.StageStatusId = GetStageStatusByCode(OBK_Ref_StageStatus.InWork).Id;
                    AppContext.OBK_ZBKCopyStageExecutors.AddOrUpdate(stageExecutor);
                    AppContext.SaveChanges();
                }
            }
        }

        public OBK_Ref_StageStatus GetStageStatusByCode(string code)
        {
            return AppContext.OBK_Ref_StageStatus.AsNoTracking().FirstOrDefault(e => e.Code == code);
        }

        public List<ZBKViewModel> ListZBKCopies(int type, int stage)
        {
            //var list = AppContext.OBK_ZBKCopyStage.Where(o => o.StageId == stage && o.StageStatusId == type).ToList();
            var list = AppContext.OBK_ZBKCopyStage.ToList();
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
                OBK_ZBKCopyStage ZBKCopyStage = AppContext.OBK_ZBKCopyStage.FirstOrDefault(o => o.OBK_ZBKCopyId == zbkCopy.Id);

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
                model.StageId = ZBKCopyStage.Id;
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

        public IQueryable<object> ZBKCopies(Guid? declarationNumber, int? declarationType, string decisionNumber, DateTime? decisionDate)
        {
            var userId = UserHelper.GetCurrentEmployee().Id;
            var data = from StageExpDoc in AppContext.OBK_StageExpDocument
                       join adec in AppContext.OBK_AssessmentDeclaration
                            on StageExpDoc.AssessmentDeclarationId equals adec.Id into AssessmentDeclaraion
                       from AssessDec in AssessmentDeclaraion.DefaultIfEmpty()
                       join zbkC in AppContext.OBK_ZBKCopy
                            on StageExpDoc.Id equals zbkC.OBK_StageExpDocumentId into OBK_ZBKCopy
                       from zbkCopy in OBK_ZBKCopy.DefaultIfEmpty()
                           //where AssessDec.EmployeeId == userId
                       select new
                       {
                           stageExpDocId = StageExpDoc.Id,
                           expConclusionNumber = StageExpDoc.ExpConclusionNumber,
                           expBlankNumber = StageExpDoc.ExpBlankNumber,
                           expStartDate = StageExpDoc.ExpStartDate,
                           status = StageExpDoc.ExpEndDate >= StageExpDoc.ExpStartDate ? "Действующий" : "Срок действия истек",
                           assessDecId = AssessDec.Id,
                           assessDecType = AssessDec.TypeId,
                           employeeId = AssessDec.EmployeeId,
                           zbkCopyId = (Guid?)zbkCopy.Id
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
                           //where AssessDec.EmployeeId == userId
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
            return AppContext.OBK_Ref_Type;
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

        public bool Update(Guid Id, int quantity)
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

            return model;
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
            return model;
        }
    }
}
