using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ncels.Helpers;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Enums;
using PW.Ncels.Database.Enums.PriceProject;
using PW.Ncels.Database.Models.Expertise;
using PW.Ncels.Database.Models.Price;
using PW.Ncels.Database.Repository.Expertise;

namespace PW.Ncels.Database.Repository.Price{

    public class PriceProjectRepository : ARepository{

        public PriceProject GetPreamble(Guid id)
        {
            var context = CreateDatabaseContext(false);
            var preamble = context.PriceProjects
                //                .Include(e => e.EXP_DrugWrapping).
                .AsNoTracking()
               .FirstOrDefault(e => e.Id == id);
            return preamble;
        }

        //Получение комментариев по контролу
        public PriceProjectCom GetComments(string modelId, string idControl){
            return AppContext.PriceProjectComs.FirstOrDefault(e => 
                e.ControlId == idControl 
                && modelId == e.PriceProjectId.ToString());
        }

        public List<PriceProjectFieldHistory> GetFieldHistories(string modelId, string idControl)
        {
            return
                AppContext.PriceProjectFieldHistories.Where(
                    e => e.ControlId == idControl && e.PriceProjectId.ToString() == modelId).ToList();
        }

        public void SaveComment(string modelId, string idControl, bool isError, string comment, string fieldValue, string userId, string fieldDisplay){
            try {
                var entityId = new Guid(modelId);
                var model =
                    AppContext.PriceProjectComs.FirstOrDefault(
                        e => e.ControlId == idControl && e.PriceProjectId.Equals(entityId)) ??
                    new PriceProjectCom
                    {
                        DateCreate = DateTime.Now,
                        PriceProjectId = entityId,
                        ControlId = idControl,
                    };

                model.IsError = isError;
                model.PriceProjectComRecords.Add(new PriceProjectComRecord
                {
                    CreateDate = DateTime.Now,
                    Note = comment,
                    UserId = new Guid(userId),
                    PriceProjectCom = model,
                    ValueField = fieldValue,
                    DisplayField = fieldDisplay
                });
                if (model.Id == 0)
                {
                    AppContext.PriceProjectComs.Add(model);
                }
                AppContext.SaveChanges();
            }
            catch (Exception ex) {
                LogHelper.Log.Error("Ошибка сохранения комментария",ex);
                throw;
            }
        }

        public PriceProject GetById(string modelId){
            return AppContext.PriceProjects.FirstOrDefault(e => e.Id == new Guid(modelId));
        }

        public void UpdateModel(string code, string modelId, string userId, long? recordId, string fieldName, string fieldValue, string fieldDisplay){
            PriceProject model = GetById(modelId);
            if (model == null){
                return;
            }

            switch (code){
                case "main":{
                    UpdateMain(model, fieldName, fieldValue, userId, fieldDisplay);
                    break;
                }
            }
            //return null;
        }

        private void UpdateMain(PriceProject model, string fieldName, string fieldValue, string userId, string fieldDisplay){

            var property = model.GetType().GetProperty(fieldName);
            if (property != null){
                var t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                object safeValue;
                if (string.IsNullOrEmpty(fieldValue)){
                    fieldValue = null;
                }
                if (t == typeof(Guid)){
                    safeValue = fieldValue == null ? null : Convert.ChangeType(new Guid(fieldValue), t);
                }
                else{
                    safeValue = fieldValue == null ? null : Convert.ChangeType(fieldValue, t);
                }
                property.SetValue(model, safeValue, null);
            }
            SaveHistoryField(model.Id, fieldName, fieldValue, new Guid(userId), fieldDisplay);
            //var subUpdateField = new SubUpdateField();
            //subUpdateField.ModelId = model.ObjectId;
            //return subUpdateField;
        }

        /// <summary>
        /// Сохранение истории поле
        /// </summary>
        /// <param name="modelId">ид заявление</param>
        /// <param name="fieldName">изменяемое поле</param>
        /// <param name="fieldValue">значение</param>
        /// <param name="userId">автор</param>
        /// <param name="fieldDisplay"></param>
        protected void SaveHistoryField(Guid modelId, string fieldName, string fieldValue, Guid userId, string fieldDisplay){
            var history = new PriceProjectFieldHistory{
                PriceProjectId = modelId,
                ControlId = fieldName,
                UserId = userId,
                ValueField = fieldValue,
                DisplayField = fieldDisplay,
                CreateDate = DateTime.Now
            };
            AppContext.PriceProjectFieldHistories.Add(history);
            AppContext.SaveChanges();
        }


        public IQueryable<PP_ProtocolListView> GetProtocols(Guid userId, int? status) {
            var list = AppContext.PP_ProtocolListView.Where(x => x.UserId == userId || x.ChiefId == userId).AsNoTracking();
            if (status.HasValue) {
                return list.Where(m => m.Status == status.Value).AsNoTracking();
            }

            return list;
        }

        public IEnumerable<Employee> GetLeadership() {
            try {
                var lUnit = AppContext.Units.FirstOrDefault(x => x.Code.Equals("01"));
                if (lUnit == null)
                    return new List<Employee>();
                var list = AppContext.Units.Where(x => x.ParentId == lUnit.Id && x.EmployeeId != null).ToList();
                var employeeList = list.Select(x => x.Employee).ToList();
                return employeeList;
            }
            catch (Exception ex) {
                LogHelper.Log.Error(ex);
                return new List<Employee>();
            }
        }

        public IEnumerable<Employee> GetComissionMembers(bool includeLeadership = true){
            try{
                var cmacUnit = AppContext.Units.FirstOrDefault(x => x.Code.Equals("ncels_cmac"));
                if (cmacUnit == null)
                    return new List<Employee>();
                var list = AppContext.Units.Where(x => x.ParentId == cmacUnit.Id && x.EmployeeId != null).ToList();
                var employeeList = list.Select(x => x.Employee).ToList();
                if (includeLeadership) {
                    var l = GetLeadership().ToList();
                    employeeList.AddRange(l);
                }
                    
                return employeeList;
            }
            catch (Exception ex){
                LogHelper.Log.Error(ex);
                return new List<Employee>();
            }
        }

        public IEnumerable<ProtocolRequester> GetRequesters(){
            try{
                var list = AppContext.PriceProjects.Join(AppContext.Employees, p => p.OwnerId, e => e.Id, (p, e) => new { p, e })
                    .Where(x => x.p.OwnerId != new Guid("00000000-0000-0000-0000-000000000000")
                        && x.p.Status == (int)PriceProjectStatus.Conversation)
                    .Select(x => new {
                        x.e.Id,
                        x.e.DisplayName
                    }).Distinct().ToList();

                if (list.Count > 0) {
                    return list.Select(x => new ProtocolRequester {
                        Id = x.Id,
                        DisplayName = x.DisplayName
                    });
                }

                return new List<ProtocolRequester>();
            }
            catch (Exception ex){
                LogHelper.Log.Error(ex);
                return new List<ProtocolRequester>();
            }
        }

        public Employee GetEmployeeById(Guid requesterId){
            try {
                return AppContext.Employees.FirstOrDefault(x => x.Id == requesterId);
            }
            catch (Exception ex){
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public String GetBossFio(Guid ownerId){
            try{
                var list = AppContext.PriceProjects
                    .Join(AppContext.Organizations, p => p.ProxyOrganizationId, o => o.Id, (p, o) => new { p, o })
                    .Where(x => x.p.OwnerId == ownerId
                        && x.o.BossFio != null
                        && x.p.Status == (int)PriceProjectStatus.Conversation)
                    .OrderByDescending(x=>x.p.CreatedDate)
                    .Select(x => x.o.BossFio)
                    .Take(1)
                    .ToList();
                if (list.Count > 0)
                    return list[0];

                return null;
            }
            catch (Exception ex){
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public IEnumerable<ProtocolPricesModel> GetPricesByRequester(Guid ownerId, bool isImn){
            try{
                var items = AppContext.PriceProjects
                    .Join(AppContext.Organizations, p => p.ProxyOrganizationId, o => o.Id, (p, o) => new { p, o })
                    .Join(AppContext.Prices, x => x.p.Id, p => p.PriceProjectId, (po, p) => new { po, p })
                    .Where(x=>x.po.p.OwnerId == ownerId
                        && x.po.p.Status == (int)PriceProjectStatus.Conversation
                        && x.p.Type < 99
                        && !x.po.p.IsArchive
                        && (
                            (isImn && (x.po.p.Type == (int)PriceProjectType.PriceImn || x.po.p.Type == (int)PriceProjectType.RePriceImn))
                            ||
                            (!isImn && (x.po.p.Type == (int)PriceProjectType.PriceLs || x.po.p.Type == (int)PriceProjectType.RePriceLs))
                        )
                        && x.po.p.NameRu != null)
                    .OrderByDescending(x=>x.po.p.CreatedDate);

                var list = new List<ProtocolPricesModel>();
                foreach (var item in items) {
                    var product = item.po.p.NameRu+", ";
                    if (!string.IsNullOrEmpty(item.po.p.FormNameRu)) {
                        product += item.po.p.FormNameRu + ", ";
                    }
                    if (!string.IsNullOrEmpty(item.po.p.Dosage)){
                        product += item.po.p.Dosage + ", ";
                    }
                    if (!string.IsNullOrEmpty(item.po.p.CountPackage)){
                        product += item.po.p.CountPackage + ", ";
                    }
                    if (!string.IsNullOrEmpty(item.po.p.Concentration)){
                        product += item.po.p.Concentration + ", ";
                    }
                    if (!string.IsNullOrEmpty(item.po.p.Volume)){
                        product += item.po.p.Volume + ", ";
                    }
                    product = product.Remove(product.Length - 2);

                    list.Add(new ProtocolPricesModel {
                        PpId = item.po.p.Id,
                        ProductName = product,
                        UnitPrice = item.p.UnitPrice,
                        BossFio = item.po.o.BossFio,
                        PpType = item.po.p.Type
                    });
                }

                return list;
            }
            catch (Exception ex){
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public bool DeleteProtocol(Guid protocolId) {
            try {
                var protocol = AppContext.PP_Protocols.FirstOrDefault(x => x.Id == protocolId);
                if (protocol != null) {
                    protocol.IsDeleted = true;
                    AppContext.SaveChanges();
                    return true;
                }
                LogHelper.Log.ErrorFormat("Не удалось найти протокол по идентификатору {0}", protocolId);
                return false;
            }
            catch (Exception ex) {
                LogHelper.Log.Error("Не удалось удалить протокол", ex);
                return false;
            }
        }

        public bool CompleteProtocol(Guid protocolId) {
            try {
                var protocol = AppContext.PP_Protocols.FirstOrDefault(x => x.Id == protocolId);
                if (protocol != null) {
                    protocol.Status = (int)ProtocolStatus.Finished;
                    AppContext.SaveChanges();
                    return true;
                }
                LogHelper.Log.ErrorFormat("Не удалось найти протокол по идентификатору {0}", protocolId);
                return false;
            }
            catch (Exception ex) {
                LogHelper.Log.Error("Не удалось Завершить протокол", ex);
                return false;
            }
        }

        public string GenerateProtocolNumber() {
            try {
                var ui = new UniqueIdentificator {
                    CreatedDate = DateTime.Now,
                    Type = "ProtocolNumber"
                };
                AppContext.UniqueIdentificators.Add(ui);
                AppContext.SaveChanges();
                return ui.Id.ToString();
            }
            catch (Exception ex) {
                LogHelper.Log.Error("Не удалось сгенерировать номер протокола", ex);
                throw;
            }
        }

        public DataModel.Price GetPriceById(Guid priceId) {
            try{
                return AppContext.Prices.FirstOrDefault(x => x.Id == priceId);
            }
            catch (Exception ex){
                LogHelper.Log.Error(ex);
                throw;
            }
        }

    }
}
