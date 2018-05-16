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

namespace PW.Ncels.Database.Repository.OBK
{
    /// <summary>
    /// 
    /// </summary>
    public class SafetyAssessmentRepository : ARepository
    {

        public SafetyAssessmentRepository()
        {
            AppContext = CreateDatabaseContext();
        }

        public SafetyAssessmentRepository(bool isProxy)
        {
            AppContext = CreateDatabaseContext(isProxy);
        }

        public SafetyAssessmentRepository(ncelsEntities context) : base(context)
        {
        }

        /// <summary>
        /// Получение списка контрактов
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public IQueryable<OBK_Contract> GetContractsByStatuses(Guid employeeId, int type)
        {
            //todo надо добавить фильтр для договоро и отображать только подписанные и активные
            return AppContext.OBK_Contract.Where(e => e.EmployeeId == employeeId && e.Type == type);
        }

        /// <summary>
        /// Показать список c проставлением дополнительной информации по владельцу
        /// </summary>
        /// <param name="employeeId">владелец</param>
        /// <returns></returns>
        public IEnumerable<OBK_Contract> GetActiveContractListWithInfo(Guid employeeId, int type)
        {
            var list = GetContractsByStatuses(employeeId, type).OrderBy(e => e.StartDate);

            foreach (var contract in list)
            {
                var buider = new StringBuilder("№" + contract.Number);
                buider.Append("; Дата:" + contract.CreatedDate.ToShortDateString());
                contract.ContractInfo = buider.ToString();
            }
            return list;
        }

        /// <summary>
        /// Показать список 
        /// </summary>
        /// <param name="employeeId">владелец</param>
        /// <returns></returns>
        public IEnumerable<Dictionary> GetProductSamples()
        {
            return AppContext.Dictionaries.Where(o => o.Type == "ProductSample");
        }

        /// <summary>
        /// Показать список 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OBK_Dictionaries> GetInspectionInstalls()
        {
            return AppContext.OBK_Dictionaries.Where(o => o.Type == "InspectionInstalled");
        }

        /// <summary>
        /// Показать список 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OBK_Dictionaries> GetStorageConditions()
        {
            return AppContext.OBK_Dictionaries.Where(o => o.Type == "StorageConditions");
        }
        /// <summary>
        /// Показать список 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OBK_Dictionaries> GetPackageConditions()
        {
            return AppContext.OBK_Dictionaries.Where(o => o.Type == "PackageCondition");
        }
        /// <summary>
        /// Показать список 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OBK_Dictionaries> GetMarkings()
        {
            return AppContext.OBK_Dictionaries.Where(o => o.Type == "Marking");
        }

        /// <summary>
        /// Показать список 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OBK_Applicant> OBKApplicants()
        {
            return AppContext.OBK_Applicant.ToList();
        }

        /// <summary>
        /// Показать список продуктов
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OBK_RS_Products> OBKContractProducts(Guid? contractId, Guid? actReception)
        {
            return AppContext.OBK_RS_Products.Where(o => o.ContractId == contractId && o.ExpertisePlace == 0
            && (o.ActReceptionId == null || o.ActReceptionId == actReception)).ToList();
        }

        /// <summary>
        /// Показать список доступных продуктов
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OBK_RS_Products> OBKContractAvailableProducts(Guid? contractId)
        {
            return AppContext.OBK_RS_Products.Where(o => o.ContractId == contractId && o.ActReceptionId == null && o.ExpertisePlace == 0).ToList();
        }


        /// <summary>
        /// Показать список 
        /// </summary>
        /// <param name="employeeId">владелец</param>
        /// <returns></returns>
        public IEnumerable<sr_measures> GetMeasures()
        {
            return AppContext.sr_measures;
        }

        /// <summary>
        /// Показать список c проставлением дополнительной информации по владельцу
        /// </summary>
        /// <param name="employeeId">владелец</param>
        /// <returns></returns>
        public IQueryable<object> GetProductSeries(Guid? contractId)
        {
            var data = from series in AppContext.OBK_Procunts_Series
                       join product in AppContext.OBK_RS_Products on series.OBK_RS_ProductsId equals product.Id
                       join contract in AppContext.OBK_Contract on product.ContractId equals contract.Id
                       join measure in AppContext.sr_measures on series.SeriesMeasureId equals measure.id
                       where contract.Id == contractId
                       select new
                       {
                           serieId = series.Id,
                           name = product.DrugFormFullName != null ? product.DrugFormFullName : product.NameRu,
                           measure = measure.name,
                           measureId = series.SeriesMeasureId,
                           serie = series.Series,
                           serieParty = series.SeriesParty,
                           seriesStartDate = series.SeriesStartdate,
                           seriesEndDate = series.SeriesEndDate,
                           quantity = series.Quantity,
                           producerName = product.ProducerNameRu
                       };
            return data;
        }

        /// <summary>
        /// Поиск договора по id
        /// </summary>
        public OBK_Contract GetContractById(Guid? id)
        {
            return AppContext.OBK_Contract.FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Справочник Тип заявления ОБК
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OBK_Ref_Type> GetObkRefTypes()
        {
            return AppContext.OBK_Ref_Type.ToList();
        }

        public OBK_Ref_Type GetObkRefTypes(string type)
        {
            return AppContext.OBK_Ref_Type.FirstOrDefault(e => e.Code == type);
        }

        /// <summary>
        /// Список стран
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Dictionary> GetCounties()
        {
            return AppContext.Dictionaries.Where(o => o.Type == "Country").ToList();
        }
        /// <summary>
        /// статус для пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OBK_Ref_Status GetStatus(int id)
        {
            return AppContext.OBK_Ref_Status.FirstOrDefault(e => e.Id == id);
        }
        /// <summary>
        /// этап
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OBK_Ref_Stage GetStage(int id)
        {
            return AppContext.OBK_Ref_Stage.FirstOrDefault(e => e.Id == id);
        }

        public OBK_Ref_StageStatus GetStageStatus(int id)
        {
            return AppContext.OBK_Ref_StageStatus.FirstOrDefault(e => e.Id == id);
        }
        /// <summary>
        /// основание для УОБК
        /// </summary>
        /// <returns></returns>
        public IQueryable<OBK_Ref_Reason> GetRefReasons(string code, bool expertiseResult)
        {
            return AppContext.OBK_Ref_Reason.Where(e => !e.IsDeleted
                                                        && e.Code == code && e.ExpertiseResult == expertiseResult);
        }

        /// <summary>
        /// валюта
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Dictionary> GetObkCurrencies()
        {
            return AppContext.Dictionaries.Where(o => o.Type == "Currency").ToList();
        }

        public OBK_AssessmentDeclaration GetById(string modelId)
        {
            return AppContext.OBK_AssessmentDeclaration.FirstOrDefault(e => e.Id == new Guid(modelId));
        }

        public IEnumerable<OBK_RS_Products> GetRsProductsAndSeries(Guid modelId)
        {
            return AppContext.OBK_RS_Products.Where(e => e.ContractId == modelId)
                .Include(x => x.OBK_Procunts_Series)
                .ToList();
        }

        public OBK_Procunts_Series GetProcuntsSeries(int? id)
        {
            return AppContext.OBK_Procunts_Series.FirstOrDefault(e => e.Id == id);
        }

        public ReesrtObk GetSearchReestr(string regNumber, string tradeName, string manufacturer, string mnn)
        {
            var reestr = AppContext.sr_register.FirstOrDefault(x => x.reg_number == regNumber);
            if (reestr != null)
            {
                return AppContext.ReesrtObks.FirstOrDefault(x => x.register_id == reestr.id);
            }
            return null;
        }

        public IEnumerable<Dictionary> GetOrganizationForm()
        {
            return AppContext.Dictionaries.Where(x => x.Type == "OpfType").ToList();
        }

        public IEnumerable<OBK_Ref_CertificateType> GetCertificateType()
        {
            return AppContext.OBK_Ref_CertificateType.Where(e => !e.IsDeleted).ToList();
        }

        /// <summary>
        /// Поиск органищация по id
        /// </summary>
        public OBK_Declarant GetDeclarantById(Guid? id)
        {
            return AppContext.OBK_Declarant.FirstOrDefault(e => e.Id == id);
        }
        /// <summary>
        /// Поиск контактов органищация по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OBK_DeclarantContact GetDeclarantContactById(Guid? id)
        {
            return AppContext.OBK_DeclarantContact.FirstOrDefault(e => e.Id == id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="declaration"></param>
        /// <returns></returns>
        public OBK_AssessmentDeclaration Update(OBK_AssessmentDeclaration declaration)
        {
            var attachedEntity = AppContext.Set<OBK_AssessmentDeclaration>().Find(declaration.Id);
            AppContext.Entry(attachedEntity).CurrentValues.SetValues(declaration);
            AppContext.Commit(true);
            return declaration;
        }

        public void SaveAssessmentDeclaration(OBK_AssessmentDeclaration declaration)
        {
            AppContext.OBK_AssessmentDeclaration.Add(declaration);
            AppContext.SaveChanges();
        }

        public OBK_AssessmentDeclaration FindDeclarationByContract(Guid contractId)
        {
            var model = AppContext.OBK_AssessmentDeclaration.FirstOrDefault(e => e.ContractId == contractId);
            return model;
        }

        /// <summary>
        /// загрузка списка заявлений
        /// </summary>
        /// <param name="request"></param>
        /// <param name="isRegisterProject"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<object> GetSafetyAssessmentDeclarationList(ModelRequest request, bool isRegisterProject,
            int? type)
        {
            try
            {
                //Database query
                var employeeId = UserHelper.GetCurrentEmployee().Id;
                var org = UserHelper.GetCurrentEmployee();
                var v = type != null
                    ? AppContext.OBK_AssessmentDeclarationView.Where(m => m.OwnerId == employeeId)
                        .OrderByDescending(m => m.SortDate)
                        .AsQueryable()
                    : AppContext.OBK_AssessmentDeclarationView.Where(m => m.OwnerId == employeeId).AsQueryable();
                //search
                if (!string.IsNullOrEmpty(request.SearchValue))
                {
                    v =
                        v.Where(
                            a => a.Number.Contains(request.SearchValue) || a.StausName.Contains(request.SearchValue));
                }

                //sort
                if (!(string.IsNullOrEmpty(request.SortColumn) && string.IsNullOrEmpty(request.SortColumnDir)))
                {
                    v = v.OrderBy(request.SortColumn + " " + request.SortColumnDir);
                }


                int recordsTotal = await v.CountAsync();
                var expertiseViews = v.Skip(request.Skip).Take(request.PageSize);
                return
                    new
                    {
                        draw = request.Draw,
                        recordsFiltered = recordsTotal,
                        recordsTotal = recordsTotal,
                        Data = await expertiseViews.ToListAsync()
                    };
            }

            catch (Exception e)
            {
                return new { IsError = true, Message = e.Message };
            }

        }

        /// <summary>
        /// Cохранение изменений
        /// </summary>
        /// <param name="code"></param>
        /// <param name="modelId">id</param>
        /// <param name="userId">id пользователя</param>
        /// <param name="recordId">id записи</param>
        /// <param name="fieldName">наименование поля</param>
        /// <param name="fieldValue">значение</param>
        /// <param name="fieldDisplay">значение</param>
        /// <returns></returns>
        public SubUpdateField UpdateModel(string code, int typeId, string modelId, string userId, long? recordId,
            string fieldName, string fieldValue, string fieldDisplay, Guid? actId)
        {
            bool isNew = false;
            var model = GetById(modelId);
            if (model == null)
            {
                model = new OBK_AssessmentDeclaration
                {
                    EmployeeId = UserHelper.GetCurrentEmployee().Id,
                    TypeId = GetObkRefTypes(typeId.ToString()).Id,
                    Id = new Guid(modelId),
                    CreatedDate = DateTime.Now,
                    StatusId = CodeConstManager.STATUS_DRAFT_ID,
                    CertificateDate = DateTime.Now,
                    IsDeleted = false,
                    CertificateGMPCheck = GetObkRefTypes(typeId.ToString()).Code == CodeConstManager.OBK_SA_DECLARATION
                };
                isNew = true;
            }

            switch (code)
            {
                case "main":
                    {
                        return UpdateMain(isNew, model, fieldName, fieldValue, userId, fieldDisplay);
                    }
                case "product":
                    {
                        return UpdateProduct(model, recordId, fieldName, fieldValue, userId, fieldDisplay);
                    }
                case "act":
                    {
                        return UpdateAct(model, fieldName, actId, fieldValue, userId, fieldDisplay);

                    }
                case "act-series":
                    {
                        return UpdateActProduct(model, fieldName, recordId, fieldValue, userId, fieldDisplay);
                    }
            }
            return null;
        }

        private SubUpdateField UpdateProduct(OBK_AssessmentDeclaration model, long? recordId, string fieldName,
            string fieldValue, string userId, string fieldDisplay)
        {
            OBK_RS_Products entity = null;
            if (recordId > 0)
            {
                entity = AppContext.Set<OBK_RS_Products>().FirstOrDefault(e => e.Id == recordId);
            }

            var property = entity.GetType().GetProperty(fieldName);
            if (property != null)
            {
                var t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                object safeValue;
                if (string.IsNullOrEmpty(fieldValue))
                {
                    fieldValue = null;
                }
                if (t == typeof(Guid))
                {
                    safeValue = fieldValue == null ? null : Convert.ChangeType(new Guid(fieldValue), t);
                }
                else
                {
                    safeValue = fieldValue == null ? null : Convert.ChangeType(fieldValue, t);

                }
                property.SetValue(entity, safeValue, null);
            }
            if (entity.Id == 0)
            {
                AppContext.OBK_RS_Products.Add(entity);
            }
            AppContext.SaveChanges();

            SaveHistoryField(model.Id, fieldName, fieldValue, new Guid(userId), fieldDisplay);

            var subUpdateField = new SubUpdateField();
            subUpdateField.ModelId = model.ObjectId;
            subUpdateField.RecordId = entity.Id;

            return subUpdateField;
        }

        public SubUpdateField UpdateAct(OBK_AssessmentDeclaration model, string fieldName, Guid? recordId,
           string fieldValue, string userId, string fieldDisplay)
        {
            var entity = AppContext.OBK_ActReception.First(o => o.Id == recordId);
            var property = entity.GetType().GetProperty(fieldName);

            if (property != null)
            {
                var t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                object safeValue;
                if (string.IsNullOrEmpty(fieldValue))
                {
                    fieldValue = null;
                }
                if (t == typeof(Guid))
                {
                    safeValue = fieldValue == null ? null : Convert.ChangeType(new Guid(fieldValue), t);
                }
                else
                {
                    safeValue = fieldValue == null ? null : Convert.ChangeType(fieldValue, t);

                }
                property.SetValue(entity, safeValue, null);
            }

            AppContext.SaveChanges();

            SaveHistoryField(model.Id, fieldName, fieldValue, new Guid(userId), fieldDisplay);

            var subUpdateField = new SubUpdateField();
            subUpdateField.ModelId = model.ObjectId;

            return subUpdateField;
        }

        private SubUpdateField UpdateActProduct(OBK_AssessmentDeclaration model, string fieldName, long? recordId,
           string fieldValue, string userId, string fieldDisplay)
        {
            var entity = AppContext.OBK_Procunts_Series.First(o => o.Id == recordId);
            var property = entity.GetType().GetProperty(fieldName);
            if (property != null)
            {
                var t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                object safeValue;
                if (string.IsNullOrEmpty(fieldValue))
                {
                    fieldValue = null;
                }
                if (t == typeof(Guid))
                {
                    safeValue = fieldValue == null ? null : Convert.ChangeType(new Guid(fieldValue), t);
                }
                else
                {
                    safeValue = fieldValue == null ? null : Convert.ChangeType(fieldValue, t);

                }
                property.SetValue(entity, safeValue, null);
            }

            AppContext.SaveChanges();

            SaveHistoryField(model.Id, fieldName, fieldValue, new Guid(userId), fieldDisplay);

            var subUpdateField = new SubUpdateField();
            subUpdateField.ModelId = model.ObjectId;

            return subUpdateField;
        }

        private SubUpdateField UpdateMain(bool isNew, OBK_AssessmentDeclaration model, string fieldName,
            string fieldValue, string userId, string fieldDisplay)
        {
            var property = model.GetType().GetProperty(fieldName);
            if (property != null)
            {
                var t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                object safeValue;
                if (string.IsNullOrEmpty(fieldValue))
                {
                    fieldValue = null;
                }
                if (t == typeof(Boolean))
                {
                    safeValue = Boolean.Parse(fieldValue);
                }
                if (t == typeof(Guid))
                {
                    safeValue = fieldValue == null ? null : Convert.ChangeType(new Guid(fieldValue), t);
                }
                else
                {
                    safeValue = fieldValue == null ? null : Convert.ChangeType(fieldValue, t);
                }
                property.SetValue(model, safeValue, null);
            }
            if (isNew)
            {
                AppContext.OBK_AssessmentDeclaration.Add(model);
                AppContext.SaveChanges();
            }
            SaveHistoryField(model.Id, fieldName, fieldValue, new Guid(userId), fieldDisplay);
            var subUpdateField = new SubUpdateField();
            subUpdateField.ModelId = model.ObjectId;

            return subUpdateField;
        }


        /// <summary>
        /// Сохранение истории поле
        /// </summary>
        /// <param name="modelId">ид заявление</param>
        /// <param name="fieldName">изменяемое поле</param>
        /// <param name="fieldValue">значение</param>
        /// <param name="userId">автор</param>
        /// <param name="fieldDisplay"></param>
        protected void SaveHistoryField(Guid modelId, string fieldName, string fieldValue, Guid userId,
            string fieldDisplay)
        {
            var history = new OBK_AssessmentDeclarationFieldHistory
            {
                AssessmentDeclarationId = modelId,
                ControlId = fieldName,
                UserId = userId,
                ValueField = fieldValue,
                DisplayField = fieldDisplay,
                CreateDate = DateTime.Now
            };
            AppContext.OBK_AssessmentDeclarationFieldHistory.Add(history);
            AppContext.SaveChanges();
        }

        /// <summary>
        /// удаление записи
        /// </summary>
        /// <param name="id"></param>
        /// <param name="guid"></param>
        public void DeleteReport(string id, Guid guid)
        {
            var model = GetById(id);
            model.IsDeleted = true;
            AppContext.SaveChanges();
        }

        /// <summary>
        /// создание дубликата
        /// </summary>
        /// <param name="id"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        public OBK_AssessmentDeclaration DublicateAssessmentDeclaration(string id, Guid guid)
        {
            var oldModel = GetById(id);
            if (oldModel == null)
            {
                return null;
            }
            var model = new OBK_AssessmentDeclaration
            {
                Id = Guid.NewGuid(),
                EmployeeId = guid,
                StatusId = CodeConstManager.STATUS_DRAFT_ID,
                CreatedDate = DateTime.Now,
                CertificateGMP = oldModel.CertificateGMP,
                CertificateNumber = oldModel.CertificateNumber,
                AssuranceCheck = oldModel.AssuranceCheck,
                OrderCheck = oldModel.OrderCheck,
                StabilityCheck = oldModel.StabilityCheck,
                PaymentCheck = oldModel.PaymentCheck,
                TypeId = oldModel.TypeId,
                ContractId = oldModel.ContractId,
                CertificateDate = oldModel.CertificateDate,
                CertificateGMPCheck = oldModel.CertificateGMPCheck,
                InvoiceRu = oldModel.InvoiceRu,
                InvoiceKz = oldModel.InvoiceKz,
                InvoiceDate = oldModel.InvoiceDate,
                InvoiceContractRu = oldModel.InvoiceContractRu,
                InvoiceContractKz = oldModel.InvoiceContractKz,
                InvoiceAgentLastName = oldModel.InvoiceAgentLastName,
                InvoiceAgentFirstName = oldModel.InvoiceAgentFirstName,
                InvoiceAgentMiddelName = oldModel.InvoiceAgentMiddelName,
                InvoiceAgentPositionName = oldModel.InvoiceAgentPositionName,
                IsDeleted = false,
                DesignDate = oldModel.DesignDate,
                ObkContracts = oldModel.ObkContracts
            };

            AppContext.OBK_AssessmentDeclaration.Add(model);
            AppContext.SaveChanges();
            return model;
        }

        /// <summary>
        /// получение комментариев
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="idControl"></param>
        /// <returns></returns>
        public OBK_AssessmentDeclarationCom GetComments(string modelId, string idControl)
        {
            return
                AppContext.OBK_AssessmentDeclarationCom.FirstOrDefault(
                    e => e.ControlId == idControl && modelId == e.AssessmentDeclarationId.ToString());
        }

        public List<OBK_AssessmentDeclarationFieldHistory> GetFieldHistories(string modelId, string idControl)
        {
            return
                AppContext.OBK_AssessmentDeclarationFieldHistory.Where(
                    e => e.ControlId == idControl && e.AssessmentDeclarationId.ToString() == modelId).ToList();
        }
        /// <summary>
        /// сохранение комметария
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="idControl"></param>
        /// <param name="isError"></param>
        /// <param name="comment"></param>
        /// <param name="fieldValue"></param>
        /// <param name="userId"></param>
        /// <param name="fieldDisplay"></param>
        public void SaveComment(string modelId, string idControl, bool isError, string comment, string fieldValue,
            string userId, string fieldDisplay)
        {
            var entityId = new Guid(modelId);
            var model =
                AppContext.OBK_AssessmentDeclarationCom.FirstOrDefault(
                    e => e.ControlId == idControl && e.AssessmentDeclarationId.Equals(entityId)) ??
                new OBK_AssessmentDeclarationCom
                {
                    DateCreate = DateTime.Now,
                    AssessmentDeclarationId = entityId,
                    ControlId = idControl,
                };

            model.IsError = isError;
            model.OBK_AssessmentDeclarationComRecord.Add(new OBK_AssessmentDeclarationComRecord
            {
                CreateDate = DateTime.Now,
                Note = comment,
                UserId = new Guid(userId),
                OBK_AssessmentDeclarationCom = model,
                ValueField = fieldValue,
                DisplayField = fieldDisplay
            });
            if (model.Id == 0)
            {

                AppContext.OBK_AssessmentDeclarationCom.Add(model);
            }
            AppContext.SaveChanges();
        }
        /// <summary>
        /// получение историии для истории этапов
        /// </summary>
        /// <param name="modelId"></param>
        /// <returns></returns>
        public IQueryable<OBK_DeclarationHistory> GetDeclarationHistory(Guid modelId)
        {
            var result = AppContext.OBK_AssessmentDeclarationHistory.Join(AppContext.OBK_AssessmentStage,
                    history => history.AssessmentDeclarationId, stage => stage.DeclarationId,
                    (history, stage) => new OBK_DeclarationHistory()
                    {
                        AssessmentDeclarationId = history.AssessmentDeclarationId,
                        StageId = stage.StageId,
                        StageStatusId = stage.StageStatusId,
                        StatusId = history.StatusId,
                        StartDate = stage.StartDate,
                        EndDate = stage.EndDate,
                        Note = history.Note
                    })
                .Where(b => b.AssessmentDeclarationId == modelId);
            return result;
        }


        public OBK_AssessmentDeclaration GetPreamble(Guid id)
        {
            var context = CreateDatabaseContext(false);
            var preamble = context.OBK_AssessmentDeclaration
                //.Include(e => e.)
                //.Include(e => e.ObkRsProductses)
                //.Include(e => e.ObkProcuntsSeries)
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == id);
            return preamble;
        }
        /// <summary>
        /// генерация ук
        /// </summary>
        /// <returns></returns>
        public string GetAppNumber()
        {
            int year = DateTime.Now.Year;
            var numer = AppContext.OBK_AssessmentDeclaration.Where(e => e.SendDate.Value.Year == year).Max(e => e.Number);
            if (numer == null)
            {
                return year + "000001";
            }
            long numberConvert;
            if (long.TryParse(numer, out numberConvert))
            {
                var newNumber = numberConvert + 1;
                return newNumber.ToString();
            }
            return null;
        }
        /// <summary>
        /// сохранение истории при отправке в ЦОЗ
        /// </summary>
        /// <param name="history"></param>
        /// <param name="getCurrentUserId"></param>
        public void SaveHisotry(OBK_AssessmentDeclarationHistory history, Guid? getCurrentUserId)
        {
            AppContext.OBK_AssessmentDeclarationHistory.Add(history);
            AppContext.SaveChanges();
        }
        /// <summary>
        /// Сохранение этапов заявления
        /// </summary>
        /// <param name="stage"></param>
        public void SaveStage(OBK_AssessmentStage stage)
        {
            AppContext.OBK_AssessmentStage.AddOrUpdate(stage);
            AppContext.SaveChanges();
        }
        /// <summary>
        /// Отправка этапов ОБК в работу выбранным исполнителям
        /// </summary>
        /// <param name="stageIds"></param>
        /// <param name="executorIds"></param>
        public void SendToWork(Guid[] stageIds, Guid[] executorIds)
        {
            var stages = AppContext.OBK_AssessmentStage.Where(e => stageIds.Contains(e.Id)).ToList();
            var executors = AppContext.Employees.Where(e => executorIds.Contains(e.Id)).ToList();

            foreach (var stage in stages)
            {
                foreach (var executor in executors)
                {
                    var stageExecutor = new OBK_AssessmentStageExecutors
                    {
                        AssessmentStageId = stage.Id,
                        ExecutorId = executor.Id,
                        ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR
                    };
                    stage.StageStatusId = GetStageStatusByCode(OBK_Ref_StageStatus.InWork).Id;
                    AppContext.OBK_AssessmentStageExecutors.Add(stageExecutor);
                    AppContext.SaveChanges();
                }
            }
        }
        public OBK_Ref_StageStatus GetStageStatusByCode(string code)
        {
            return AppContext.OBK_Ref_StageStatus.AsNoTracking().FirstOrDefault(e => e.Code == code);
        }
        /// <summary>
        /// сохранение заявления
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual OBK_AssessmentDeclaration SaveOrUpdate(OBK_AssessmentDeclaration entity, Guid? userId)
        {
            if (entity.Id == Guid.Empty)
            {
                try
                {
                    entity.CreatedDate = DateTime.Now;
                    AppContext.MarkAsAdded(entity);
                    AppContext.Commit(true);
                    return entity;
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }

            //var suspendedStage = AppContext.EXP_ExpertiseStage.FirstOrDefault(x =>
            //    !x.IsHistory
            //    && x.DeclarationId == entity.Id
            //    && x.IsSuspended
            //);
            //if (suspendedStage != null)
            //{
            //    LogHelper.Log.DebugFormat("Найден приостановленный этап {0}", suspendedStage.Id);
            //    if (suspendedStage.SuspendedStartDate.HasValue)
            //    {
            //        suspendedStage.IsSuspended = false;
            //        var suspendedDays = (DateTime.Now - suspendedStage.SuspendedStartDate.Value).TotalDays;
            //        LogHelper.Log.DebugFormat("Всего дней приостановки {0}", suspendedDays);
            //        if (suspendedStage.EndDate.HasValue)
            //        {
            //            suspendedStage.EndDate = suspendedStage.EndDate.Value.AddDays(suspendedDays);
            //        }
            //        else
            //        {
            //            LogHelper.Log.DebugFormat("У этапа {0} не указана дата завершения исполнения EndDate", suspendedStage.Id);
            //        }
            //        AppContext.Commit(true);
            //    }
            //    else
            //    {
            //        LogHelper.Log.DebugFormat("У этапа {0} почему-то не указана дата начала приостановки", suspendedStage.Id);
            //    }
            //}
            //else
            //{
            //    LogHelper.Log.Debug("Заявление не содержит приостановленных этапов");
            //}

            var attachedEntity = AppContext.Set<OBK_AssessmentDeclaration>().Find(entity.Id);
            AppContext.Entry(attachedEntity).CurrentValues.SetValues(entity);
            AppContext.Commit(true);
            //Отправка заявления на этап ЦОЗ
            if (entity.StatusId != CodeConstManager.STATUS_DRAFT_ID)
            {
                string resultDescription;
                var stageRepository = new AssessmentStageRepository();
                if (!stageRepository.HasStage(entity.Id, CodeConstManager.STAGE_OBK_COZ))
                    stageRepository.ToNextStage(entity, null, new[] { CodeConstManager.STAGE_OBK_COZ }, out resultDescription);
            }
            return entity;
        }

        public IQueryable<Dictionary> GetAddRequeiredDocumentCode(IQueryable<Dictionary> dicListQuery, string id)
        {
            var request = GetById(id);
            var contract = GetContractById(request.ContractId);
            if (contract?.OBK_RS_Products.Count >= 1)
            {
                foreach (var product in contract.OBK_RS_Products)
                {
                    foreach (var dList in dicListQuery)
                    {
                        if (product.RegTypeId == 1)
                        {
                            if (dList.Id == new Guid("462110CE-FEFD-451D-9C4C-EE05704CCFCE"))
                            {
                                dList.Code = "1";
                            }
                        }
                        if (product.RegTypeId == 2)
                        {
                            if (dList.Id == new Guid("7ED9DA5A-45EC-4499-B713-2E503189DB0B"))
                            {
                                dList.Code = "1";
                            }
                        }
                    }

                }
            }
            var result = dicListQuery;
            return result;
        }

        #region внутренний портал

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="stage"></param>
        /// <param name="userId"></param>
        /// <param name="customFilter"></param>
        /// <returns></returns>
        public IQueryable<OBK_AssessmentDeclarationRegisterView> SafetyAssessmentRegisterList(string status, int stage, Guid userId, DeclarationRegistryFilter customFilter)
        {
            // добавить этап обк (stage)
            if (stage == 15)
            {
                var commissionExecutor = AppContext.OBK_OP_Commission.Where(e => e.EmployeeId == userId);
                var programExecutor = AppContext.OBK_AssessmentStageExecutors.Where(e => e.ExecutorId == userId);
                var reportOPExecutor = AppContext.OBK_AssessmentReportOPExecutors.Where(e => e.EmployeeId == userId);
                var stageId = AppContext.OBK_AssessmentStage.Where(x => x.StageId == stage);
                var q = AppContext.OBK_AssessmentDeclarationRegisterView
                    .Where(e => stageId.Any(x => x.DeclarationId == e.DeclarationId)
                                && (commissionExecutor.Any(x => x.DeclarationId == e.DeclarationId)
                                || programExecutor.Any(x => x.OBK_AssessmentStage.DeclarationId == e.DeclarationId)
                                || reportOPExecutor.Any(x => x.OBK_AssessmentReportOP.DeclarationId == e.DeclarationId)));
                return q;
            }
            else
            {
                var query =
                AppContext.OBK_AssessmentDeclarationRegisterView
                .Where(e => e.ExecutorId == userId && e.StageCode == stage.ToString());
                return query;
            }

        }

        /// <summary>
        /// сохранение результата экспертизы документов
        /// </summary>
        /// <param name="expDocument"></param>
        public void SaveExpDocument(OBK_StageExpDocument expDocument)
        {
            AppContext.OBK_StageExpDocument.AddOrUpdate(expDocument);
            AppContext.SaveChanges();
            if(!expDocument.ExpResult) return;
            var blank = AppContext.OBK_BlankNumber.FirstOrDefault(o => o.Object_Id == expDocument.Id);
            if (blank == null)
            {
                var blankType = AppContext.OBK_BlankType.FirstOrDefault(o => CodeConstManager.BlankTypes.ZBK.Equals(o.Code));
                blank = new OBK_BlankNumber()
                {
                    Id = Guid.NewGuid(),
                    Object_Id = expDocument.Id,
                    CreateDate = DateTime.Now,
                    BlankTypeId = blankType.Id,
                    Corrupted = false
                };
            }

            blank.Number = int.Parse(expDocument.ExpBlankNumber);
            blank.EmployeeId = UserHelper.GetCurrentEmployee().Id;

            AppContext.OBK_BlankNumber.AddOrUpdate(blank);
            AppContext.SaveChanges();
        }

        public OBK_StageExpDocument GetStageExpDocument(int? prodSerId)
        {
            return AppContext.OBK_StageExpDocument.FirstOrDefault(e => e.ProductSeriesId == prodSerId);
        }

        public OBK_StageExpDocument GetStageExpDocument(Guid assessmentDeclaraiontId)
        {
            return AppContext.OBK_StageExpDocument.FirstOrDefault(e => e.AssessmentDeclarationId == assessmentDeclaraiontId && !e.ExpResult);
        }

        public OBK_StageExpDocumentResult GetStageExpDocumentResult(Guid assessmentDeclarationId)
        {
            return AppContext.OBK_StageExpDocumentResult.FirstOrDefault(e => e.AssessmetDeclarationId == assessmentDeclarationId);
        }

        public OBK_StageExpDocument GetStageExpDocumentByAssessmentDeclarationId(Guid id)
        {
            return AppContext.OBK_StageExpDocument.FirstOrDefault(e => e.AssessmentDeclarationId == id);
        }

        public void SendOutputResult(Guid id, string receiverFio, DateTime receivedDate)
        {
            var stages = AppContext.OBK_AssessmentStage.Where(e => e.OBK_AssessmentDeclaration.Id == id).ToList();
            var declarant = AppContext.OBK_AssessmentDeclaration.FirstOrDefault(e => e.Id == id);
            if (declarant == null)
                return;
            foreach (var stage in stages)
            {
                switch (stage.StageId)
                {
                    case CodeConstManager.STAGE_OBK_COZ:
                        var status = GetStageStatusByCode(OBK_Ref_StageStatus.Completed);
                        stage.StageStatusId = status.Id;
                        stage.ResultId = CodeConstManager.STAGE_OBK_COMPLETED_POSITIVE;
                        break;

                    case CodeConstManager.STAGE_OBK_EXPERTISE_DOC:
                        switch (stage.ResultId)
                        {
                            case CodeConstManager.STAGE_OBK_COMPLETED_POSITIVE:
                                declarant.StatusId = CodeConstManager.STATUS_OBK_CONCLUSION_ISSUE;
                                break;
                            case CodeConstManager.STAGE_OBK_COMPLETED_NEGATIVE:
                                declarant.StatusId = CodeConstManager.STATUS_OBK_REFUSAL_ISSUE;
                                break;
                        }
                        break;
                }
            }
            declarant.ReceiverFIO = receiverFio;
            declarant.ReceivedDate = receivedDate;

            AppContext.SaveChanges();
        }

        #endregion

        #region Архив
        public IQueryable<OBK_ArchiveView> ArchiveList()
        {
            return AppContext.OBK_ArchiveView;
        }

        public object RequestTypeList()
        {
            return AppContext.OBK_Ref_Type.Where(o => o.ViewOption == 1).Select(o => new { Id = o.Id, Name = o.NameRu }).OrderBy(o => o.Name);
        }

        public object CountryList()
        {
            return AppContext.Dictionaries.Where(o => "Country".Equals(o.Type)).Select(o => new { Id = o.Id, Name = o.Name }).OrderBy(o => o.Name);
        }

        public IQueryable<OBK_ZBKArchiveView> ZBKCopyList(Guid? declarationId)
        {
            return AppContext.OBK_ZBKArchiveView.Where(o => o.AssessmentDeclarationId == declarationId);
        }

        public IQueryable<AdditionalContractArchive> AdditionalContractList(Guid? contractId)
        {
            return AppContext.OBK_Contract.Where(o => o.ParentId == contractId).Select(o => new AdditionalContractArchive
            {
                Id = o.Id,
                Number = o.Number,
                StartDate = o.StartDate,
                additionalType = o.Dictionary.Name
            });
        }

        public void DeleteSerialActReception(Guid? actReceptionId)
        {
            var forUpdateAct = AppContext.OBK_RS_Products.Where(o => o.ActReceptionId == actReceptionId).ToList();
            foreach (var temp in forUpdateAct)
            {
                temp.UsedInSerie = null;
                temp.ActReceptionId = null;
            }

            var forDeleteProductsIdes = forUpdateAct.Select(o => o.Id).ToList();
            var forDeleteSeries = AppContext.OBK_Procunts_Series.Where(o => forDeleteProductsIdes.Contains(o.OBK_RS_ProductsId)).ToList();
            AppContext.OBK_Procunts_Series.RemoveRange(forDeleteSeries);
            AppContext.SaveChanges();

            var act = AppContext.OBK_ActReception.FirstOrDefault(o => o.Id == actReceptionId);
            AppContext.OBK_ActReception.Remove(act);
            AppContext.SaveChanges();
        }
        #endregion

        #region Акт отбора
        public void SaveProductSeries(List<SerieProduct> productSeries, Guid actReceptionId)
        {
            //Удаляем
            var forUpdateAct = AppContext.OBK_RS_Products.Where(o => o.ActReceptionId == actReceptionId).ToList();
            foreach(var temp in forUpdateAct)
            {
                temp.UsedInSerie = null;
                temp.ActReceptionId = null;
            }

            var forDeleteProductsIdes = forUpdateAct.Select(o => o.Id).ToList();
            var forDeleteSeries = AppContext.OBK_Procunts_Series.Where(o => forDeleteProductsIdes.Contains(o.OBK_RS_ProductsId)).ToList();
            AppContext.OBK_Procunts_Series.RemoveRange(forDeleteSeries);
            AppContext.SaveChanges();

            //Записываем
            var productIdes = productSeries.Select(o => o.ProductId).ToList();
            var products = AppContext.OBK_RS_Products.Where(o => productIdes.Contains(o.Id)).ToList();

            foreach (var temp in products)
            {
                temp.UsedInSerie = true;
                temp.ActReceptionId = actReceptionId;
            }

            AppContext.SaveChanges();

            foreach (var temp in productSeries)
            {
                OBK_Procunts_Series serie = new OBK_Procunts_Series()
                {
                    Series = temp.serie,
                    SeriesStartdate = temp.startDate,
                    SeriesEndDate = temp.endDate,
                    SeriesParty = temp.serieParty,
                    OBK_RS_ProductsId = temp.ProductId,
                    SeriesMeasureId = temp.MeasureId,
                    Quantity = temp.quantity,
                };

                AppContext.OBK_Procunts_Series.Add(serie);
            }

            AppContext.SaveChanges();
        }

        public void SaveSerialExpertActReception(OBK_ActReception reception, string actDate)
        {
            DateTime? actD = null;
            if (actDate != null || !actDate.Equals(""))
            {
                actD = DateTime.Parse(actDate);
            }
            reception.ActDate = actD;
            var employee = UserHelper.GetCurrentEmployee();
            reception.Worker = employee.FullName;
            reception.WorkerId = employee.Id;
            AppContext.OBK_ActReception.AddOrUpdate(reception);

            AppContext.SaveChanges();
        }

        /// <summary>
        /// Показать список c проставлением дополнительной информации по владельцу
        /// </summary>
        /// <param name="employeeId">владелец</param>
        /// <returns></returns>
        public IQueryable<object> GetSeriaProductSeries(Guid? actReceptionId)
        {
            var data = from series in AppContext.OBK_Procunts_Series
                       join product in AppContext.OBK_RS_Products on series.OBK_RS_ProductsId equals product.Id
                       join contract in AppContext.OBK_Contract on product.ContractId equals contract.Id
                       join measure in AppContext.sr_measures on series.SeriesMeasureId equals measure.id
                       where product.ActReceptionId == actReceptionId
                       select new
                       {
                           serieId = series.Id,
                           name = product.DrugFormFullName != null ? product.DrugFormFullName : product.NameRu,
                           measure = measure.name,
                           measureId = series.SeriesMeasureId,
                           serie = series.Series,
                           serieParty = series.SeriesParty,
                           seriesStartDate = series.SeriesStartdate,
                           seriesEndDate = series.SeriesEndDate,
                           quantity = series.Quantity,
                           producerName = product.ProducerNameRu,
                           productId = product.Id
                       };
            return data;
        }
        #endregion
    }
}
