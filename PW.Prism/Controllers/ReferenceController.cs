using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using Antlr.Runtime;
using Aspose.Pdf;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Database.Repository.Expertise;
using Document = PW.Ncels.Database.DataModel.Document;

namespace PW.Prism.Controllers
{
    [Authorize]
    public class ReferenceController : Controller
    {
        private ncelsEntities db = UserHelper.GetCn();

        public ActionResult ContactAsync()
        {
            return PartialView();
        }

        [HttpPost]
        // GET: /Contact/
        public ActionResult ContactAsyncData([DataSourceRequest] DataSourceRequest request, string FullName)
        {

            Guid organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
            var data = db.Employees.Where(x => x.OrganizationId == organizationId).OrderBy(o => o.FullName).ToList()
                .Select(o => new EmployeeModel
                {
                    FullName = o.FullName,
                    Phone = o.Phone,
                    Email = o.Email
                });

            return Json(data.ToDataSourceResult(request));
        }

        // GET: /Reference/
        public ActionResult Index()
        {
            Guid guid = Guid.NewGuid();
            ViewBag.DictionaryList = DictionaryHelper.GetList();
            return PartialView(guid);
        }

        public ActionResult Nomenclature()
        {
            Guid guid = Guid.NewGuid();
            ViewBag.NomenclatureList = DictionaryHelper.GetNomenclatureList();
            return PartialView(guid);
        }

        public ActionResult Correspondent()
        {
            Guid guid = Guid.NewGuid();
            ViewBag.CorTypeList = DictionaryHelper.GetCorRefTypeList();
            return PartialView(guid);
        }

        public ActionResult ListOrganization()
        {
            var data = db.Units.Where(x => x.PositionState != 5 && x.Type == 0)
                .OrderBy(o => o.Rank);
            return Json(data.Select(o => new { o.Id, Name = o.DisplayName }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ArchivView()
        {
            Guid guid = Guid.NewGuid();
            ViewBag.NomenclatureList = DictionaryHelper.GetNomenclatureList();
            return PartialView(guid);
        }

        public ActionResult ReadArchivDocuments([DataSourceRequest] DataSourceRequest request)
        {
            Guid id = UserHelper.GetCurrentEmployee().OrganizationId;
            var data = db.ArchivViews.Where(o => o.OrganizationId == id);
            return Json(data.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ArchivUpdate([DataSourceRequest] DataSourceRequest request, ArchivView dictionary)
        {
            if (dictionary != null && ModelState.IsValid)
            {
                Document d = db.Documents.First(o => o.Id == dictionary.Id);
                d.Akt = dictionary.Akt;
                d.Book = dictionary.Book;
                d.Deed = dictionary.Deed;
                d.Note = dictionary.Note;

                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult ListDepartment(Guid? id)
        {

            var data = db.Units.Where(x => x.ParentId == id).Where(o => o.Type == 1)
                .OrderBy(o => o.Name);
            var data1 = db.Units.Where(x => x.Id == id)
                .OrderBy(o => o.Name);
            var result = data.Union(data1);
            return Json(result.Select(o => new { o.Id, Name = o.DisplayName }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListDepartmentsForReport()
        {
            var parentCode = OrganizationConsts.ReasearchCenterCode;
            var currentUserDepartmentId = UserHelper.GetDepartment().Id;
            var codes = new List<string>();
            for (int i = 4; i <= 8; i++)
                codes.Add(parentCode + i);

            var parentDepartment = db.Units.FirstOrDefault(u => u.Code == parentCode);
            IQueryable<Unit> data = null;
            if (parentDepartment != null)
            {
                var qr = db.Units.Where(x => x.ParentId == parentDepartment.Id)
                    .Where(o => o.Type == 1);
                qr = qr.Where(u => codes.Contains(u.Code));
                // filter by current user
                qr = qr.Where(d => d.Id == currentUserDepartmentId);
                data = qr.OrderBy(o => o.Name);
            }

            return Json(data?.Select(o => new { o.Id, Name = o.DisplayName }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListBranch(Guid? id)
        {
            try
            {
                var data = db.Units.Where(x => x.ParentId == id).Where(o => o.Type == 1);
                var data1 = db.Units.Where(x => x.Id == id).OrderBy(o => o.Name);
                var secs =
                    db.Units.Where(x => x.ParentId.HasValue && data.Select(d => d.Id).Contains(x.ParentId.Value))
                        .Where(o => o.Type == 1);
                var result = data.Union(data1).Union(secs).OrderBy(x => x.DisplayName);
                return Json(result.Select(o => new { o.Id, Name = o.DisplayName }), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                List<Unit> list = new List<Unit>();
                return Json(list.Select(o => new { o.Id, Name = o.Name }), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult StageSettingsList([DataSourceRequest] DataSourceRequest request, string type)
        {
            var data = db.Settings.Where(o => o.Type == "Stage");
            return Json(data.ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StageSettingsUpdate([DataSourceRequest] DataSourceRequest request, Setting dictionary)
        {
            if (dictionary != null && ModelState.IsValid)
            {
                var dic = db.Settings.First(o => o.Id == dictionary.Id);
                dic.Value = dictionary.Value;
                dic.UserId = dictionary.UserId;
                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult ListPosition(Guid? id)
        {
            //Guid guid = UserHelper.GetCurrentEmployee().OrganizationId;
            var data = db.Units.Where(x => x.ParentId == id && x.PositionState == 2 && x.Type == 2)
                .OrderBy(o => o.Name);

            return Json(data.Select(o => new { o.Id, Name = o.Name }), JsonRequestBehavior.AllowGet);

        }

        public ActionResult ReadRegisterProjectStatus()
        {
            var data = db.Dictionaries.Where(x => x.Type == "RegisterProjectStatus")
                .Select(o => new { o.Id, o.Code, o.Name })
                .OrderBy(o => o.Name);
            return Json(data.ToList().Select(m => new { Id = Convert.ToInt32(m.Code), m.Name }),
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadPriceProjectStatus()
        {
            var data = db.Dictionaries.Where(x => x.Type == "PriceProjectStatus")
                .Select(o => new { o.Id, o.Code, o.Name })
                .OrderBy(o => o.Name);
            return Json(data.ToList().Select(m => new { Id = Convert.ToInt32(m.Code), m.Name }),
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadRemarkTypes()
        {
            var data = db.RemarkTypes.Select(x => new {x.Id, x.Name})
                .OrderBy(x => x.Name);

            return Json(data.ToList().Select(m => new { Id = m.Id, m.Name}), JsonRequestBehavior.AllowGet);
        }

        public ActionResult List(string text)
        {
            if (text == "Nomenclature")
            {
                Guid orgId = UserHelper.GetCurrentEmployee().OrganizationId;
                var data =
                    db.Dictionaries.Where(
                        o => o.Type == text && o.OrganizationId == orgId && o.Year == DateTime.Now.Year.ToString())
                        .OrderBy(o => o.DepartmentsValue)
                        .ThenBy(o => o.Code)
                        .ToList();
                if (!EmployePermissionHelper.IsAllNomenclature)
                {
                    IList<string> departaments = new List<string>();

                    Unit unit = UserHelper.GetDepartment();

                    // вверх
                    while (unit != null && unit.Type == 1)
                    {
                        departaments.Add(unit.Id.ToString().ToLower());
                        unit = unit.Parent;
                    }

                    unit = UserHelper.GetDepartment();
                    // вниз
                    GetBranch(unit, departaments);

                    data =
                        data.Where(o => o.Year == DateTime.Now.Year.ToString() && departaments.Contains(o.DepartmentsId))
                            .ToList();
                }
                return
                    Json(
                        data.Select(
                            o => new { o.Id, Name = o.Code + " " + LocalizationHelper.GetString(o.Name, o.NameKz) }),
                        JsonRequestBehavior.AllowGet);
            }
            var dataDic = db.Dictionaries.Where(o => o.Type == text).OrderBy(o => o.Name).ToList();
            return Json(dataDic.Select(o => new { o.Id, Name = LocalizationHelper.GetString(o.Name, o.NameKz) }),
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListOfUnitsExceptCode(string text, Guid Id)
        {
            var dataDic = db.Dictionaries.Where(o => o.Type == text && o.Id != Id).OrderBy(o => o.Name).ToList();
            return Json(dataDic.Select(o => new { o.Id, Name = LocalizationHelper.GetString(o.Name, o.NameKz) }),
                JsonRequestBehavior.AllowGet);
        }

        private IList<string> GetBranch(Unit unit, IList<string> departaments)
        {
            if (unit != null && unit.Type == 1)
            {
                departaments.Add(unit.Id.ToString().ToLower());
                List<Unit> units = db.Units.Where(x => x.ParentId == unit.Id && x.Type == 1).ToList();
                foreach (Unit unit1 in units)
                    GetBranch(unit1, departaments);
            }
            return departaments;
        }

        public ActionResult ListNomenclature(string text)
        {
            if (text == "Nomenclature")
            {
                Guid orgId = UserHelper.GetCurrentEmployee().OrganizationId;
                var data =
                    db.Dictionaries.Where(o => o.Type == text && o.OrganizationId == orgId)
                        .OrderBy(o => o.DisplayName)
                        .ToList();
                if (EmployePermissionHelper.IsAllNomenclature)
                {
                    data = data.Where(o => o.Year == DateTime.Now.Year.ToString()).ToList();
                }
                else
                {
                    var departament = UserHelper.GetDepartment();
                    if (departament != null && departament.Parent != null)
                    {
                        data = data.Where(o => o.Year == DateTime.Now.Year.ToString() &&
                                               (o.DepartmentsId == departament.Id.ToString() ||
                                                o.DepartmentsId == departament.Parent.Id.ToString())).ToList();
                    }
                    if (departament != null && departament.Type == 0)
                    {
                        data = data.Where(o => o.Year == DateTime.Now.Year.ToString()).ToList();
                    }
                    if (departament != null && departament.Type != 0 && departament.Parent == null)
                    {
                        data = data.Where(o => o.Year == DateTime.Now.Year.ToString() &&
                                               o.DepartmentsId == departament.Id.ToString()).ToList();
                    }
                }
                return Json(data.Select(o => new { o.Id, Name = o.DisplayName }), JsonRequestBehavior.AllowGet);
            }
            var dataDic = db.Dictionaries.Where(o => o.Type == text).OrderBy(o => o.DisplayName).ToList();
            return Json(dataDic.Select(o => new { o.Id, Name = o.Name }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListAutoComplete(string text)
        {

            var data = db.Dictionaries.Where(o => o.Type == text).OrderBy(o => o.DisplayName).ToList();

            var result = data.Select(o => o.DisplayName).ToArray();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AllNomenclatureList([DataSourceRequest] DataSourceRequest request, string year)
        {
            Guid id = UserHelper.GetCurrentEmployee().OrganizationId;
            var data = db.Dictionaries
                .Where(o => o.Type == "Nomenclature" && o.Year == year && o.OrganizationId == id)
                .OrderBy(o => o.DisplayName).ToList()
                .Select(o => new NomenclatureModel
                {
                    Id = o.Id,
                    Name = o.Name,
                    Code = o.Code,
                    NameKz = o.NameKz,
                    Type = o.Type,
                    DepartmentsId = DictionaryHelper.GetItems(o.DepartmentsId, o.DepartmentsValue)
                });
            return Json(data.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NomenclatureCreate([DataSourceRequest] DataSourceRequest request,
            NomenclatureModel dictionary, string year)
        {
            if (dictionary != null && ModelState.IsValid)
            {

                Dictionary d = new Dictionary()
                {
                    Id = Guid.NewGuid(),
                    Type = "Nomenclature",
                    Name = dictionary.Name,
                    NameKz = dictionary.NameKz,
                    Code = dictionary.Code,
                    Note = dictionary.Note,
                    DisplayName = dictionary.Code + " - " + dictionary.Name,
                    DepartmentsId = DictionaryHelper.GetItemsId(dictionary.DepartmentsId),
                    DepartmentsValue = DictionaryHelper.GetItemsName(dictionary.DepartmentsId),
                    OrganizationId = UserHelper.GetCurrentEmployee().OrganizationId,
                    Year = year
                };
                db.Dictionaries.Add(d);
                db.SaveChanges();
                dictionary.Id = d.Id;
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NomenclatureUpdate([DataSourceRequest] DataSourceRequest request,
            NomenclatureModel dictionary, string year)
        {
            if (dictionary != null && ModelState.IsValid)
            {
                Dictionary d = db.Dictionaries.First(o => o.Id == dictionary.Id);
                d.Name = dictionary.Name;
                d.NameKz = dictionary.NameKz;
                d.Code = dictionary.Code;
                d.Note = dictionary.Note;
                d.DisplayName = dictionary.Code + " - " + dictionary.Name;
                d.DepartmentsId = DictionaryHelper.GetItemsId(dictionary.DepartmentsId);
                d.DepartmentsValue = DictionaryHelper.GetItemsName(dictionary.DepartmentsId);
                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NomenclatureDestroy([DataSourceRequest] DataSourceRequest request,
            NomenclatureModel dictionary, string year)
        {
            if (dictionary != null)
            {
                Dictionary d = db.Dictionaries.First(o => o.Id == dictionary.Id);
                db.Dictionaries.Remove(d);
                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        #region RefAnalyseIndicator

        public ActionResult RefAnalyseIndicator(){
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }

        public ActionResult RefAnalyseIndicatorList([DataSourceRequest] DataSourceRequest request){
            var data = db.EXP_DIC_AnalyseIndicator
                .Where(o => !o.IsDeleted)
                .OrderBy(o => o.NameRu).ToList();
            return Json(data.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RefAnalyseIndicatorCreate([DataSourceRequest] DataSourceRequest request, EXP_DIC_AnalyseIndicator dictionary) {
            var maxId = db.EXP_DIC_AnalyseIndicator.AsNoTracking().Max(x => x.Id);
            if (dictionary != null && ModelState.IsValid){
                EXP_DIC_AnalyseIndicator d = new EXP_DIC_AnalyseIndicator{
                    Id = maxId + 1,
                    NameRu = dictionary.NameRu,
                    NameKz = dictionary.NameKz,
                    Code = dictionary.Code,
                    DateCreate = DateTime.Now,
                    DateEdit = DateTime.Now,
                    IsDeleted = false
                };
                db.EXP_DIC_AnalyseIndicator.Add(d);
                db.SaveChanges();
                dictionary.Id = d.Id;
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RefAnalyseIndicatorUpdate([DataSourceRequest] DataSourceRequest request, EXP_DIC_AnalyseIndicator dictionary){
            if (dictionary != null && ModelState.IsValid){
                EXP_DIC_AnalyseIndicator d = db.EXP_DIC_AnalyseIndicator.First(o => o.Id == dictionary.Id);
                d.NameRu = dictionary.NameRu;
                d.NameKz = dictionary.NameKz;
                d.Code = dictionary.Code;
                db.SaveChanges();
            }
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RefAnalyseIndicatorDestroy([DataSourceRequest] DataSourceRequest request, EXP_DIC_AnalyseIndicator dictionary){
            if (dictionary != null){
                EXP_DIC_AnalyseIndicator d = db.EXP_DIC_AnalyseIndicator.First(o => o.Id == dictionary.Id);
                d.IsDeleted = true;
                db.SaveChanges();
            }
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region RefChangeType

        public ActionResult RefChangeType(){
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }

        public ActionResult RefChangeTypeList([DataSourceRequest] DataSourceRequest request){
            var data = db.EXP_DIC_ChangeType
                .Where(o => !o.IsDeleted)
                .OrderBy(o => o.ChangeName).ToList();
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RefChangeTypeCreate([DataSourceRequest] DataSourceRequest request, EXP_DIC_ChangeType dictionary) {
            var maxId = db.EXP_DIC_ChangeType.AsNoTracking().Max(x => x.Id);
            if (dictionary != null && ModelState.IsValid){
                EXP_DIC_ChangeType d = new EXP_DIC_ChangeType{
                    Id = maxId + 1,
                    ChangeName = dictionary.ChangeName,
                    ChangeType = dictionary.ChangeType,
                    Code = dictionary.Code,
                    DateCreate = DateTime.Now,
                    DateEdit = DateTime.Now,
                    IsDeleted = false
                };
                db.EXP_DIC_ChangeType.Add(d);
                db.SaveChanges();
                dictionary.Id = d.Id;
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RefChangeTypeUpdate([DataSourceRequest] DataSourceRequest request, EXP_DIC_ChangeType dictionary){
            if (dictionary != null && ModelState.IsValid){
                EXP_DIC_ChangeType d = db.EXP_DIC_ChangeType.First(o => o.Id == dictionary.Id);
                d.ChangeName = dictionary.ChangeName;
                d.ChangeType = dictionary.ChangeType;
                d.Code = dictionary.Code;
                db.SaveChanges();
            }
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RefChangeTypeDestroy([DataSourceRequest] DataSourceRequest request, EXP_DIC_ChangeType dictionary){
            if (dictionary != null){
                EXP_DIC_ChangeType d = db.EXP_DIC_ChangeType.First(o => o.Id == dictionary.Id);
                d.IsDeleted = true;
                db.SaveChanges();
            }
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region RefNormDocFarm

        public ActionResult RefNormDocFarm(){
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }

        public ActionResult RefNormDocFarmList([DataSourceRequest] DataSourceRequest request){
            var data = db.EXP_DIC_NormDocFarm
                .Where(o => !o.IsDeleted)
                .OrderBy(o => o.NameRu).ToList();
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RefNormDocFarmCreate([DataSourceRequest] DataSourceRequest request, EXP_DIC_NormDocFarm dictionary) {
            var maxId = db.EXP_DIC_NormDocFarm.AsNoTracking().Max(x => x.Id);
            if (dictionary != null && ModelState.IsValid){
                EXP_DIC_NormDocFarm d = new EXP_DIC_NormDocFarm{
                    Id = maxId + 1,
                    NameRu = dictionary.NameRu,
                    NameKz = dictionary.NameKz,
                    Code = dictionary.Code,
                    DateCreate = DateTime.Now,
                    DateEdit = DateTime.Now,
                    IsDeleted = false
                };
                db.EXP_DIC_NormDocFarm.Add(d);
                db.SaveChanges();
                dictionary.Id = d.Id;
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RefNormDocFarmUpdate([DataSourceRequest] DataSourceRequest request, EXP_DIC_NormDocFarm dictionary){
            if (dictionary != null && ModelState.IsValid){
                EXP_DIC_NormDocFarm d = db.EXP_DIC_NormDocFarm.First(o => o.Id == dictionary.Id);
                d.NameRu = dictionary.NameRu;
                d.NameKz = dictionary.NameKz;
                d.Code = dictionary.Code;
                db.SaveChanges();
            }
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RefNormDocFarmDestroy([DataSourceRequest] DataSourceRequest request, EXP_DIC_NormDocFarm dictionary){
            if (dictionary != null){
                EXP_DIC_NormDocFarm d = db.EXP_DIC_NormDocFarm.First(o => o.Id == dictionary.Id);
                d.IsDeleted = true;
                db.SaveChanges();
            }
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region RefPrimaryOTD

        public ActionResult RefPrimaryOTD(){
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }

        public object ListPrimaryOTD(int? id){
		    if (!id.HasValue) {
		        var r = db.EXP_DIC_PrimaryOTD.Where(x => !x.IsDeleted && x.ParentId == null);
		        var list = r.Select(x => new RefTreeItemModel {
		            id = x.Id,
		            Code = x.Code,
		            NameRu = x.NameRu,
		            NameKz = x.NameKz,
		            ParentId = null,
		            hasChildren = x.EXP_DIC_PrimaryOTD1.Any(c => !c.IsDeleted)
		        });
		        return Json(list, JsonRequestBehavior.AllowGet);
		    }
		    else {
                var r = db.EXP_DIC_PrimaryOTD.Where(x => !x.IsDeleted && x.ParentId == id);
                var list = r.Select(x => new RefTreeItemModel{
                    id = x.Id,
                    Code = x.Code,
                    NameRu = x.NameRu,
                    NameKz = x.NameKz,
                    ParentId = null,
                    hasChildren = x.EXP_DIC_PrimaryOTD1.Any(c => !c.IsDeleted)
                });
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }

		public ActionResult RefPrimaryOtdEdit(int? id, int? parentId, Guid? modelId, string uid){
			ViewBag.ModelId = modelId;
			ViewBag.ModeUid = uid;
			List<int> items = new List<int>();
		    if (id.HasValue) {
                items.Add(id.Value);
            }
		    if (parentId.HasValue) {
		        items.Add(parentId.Value);
                ViewBag.IsDeleted = false;
            }
		    else {
		        ViewBag.IsDeleted = true;
		    }

		    return PartialView(items.ToArray());
        }

        [HttpPost]
        public ActionResult RefPrimaryOtdUpdate(EXP_DIC_PrimaryOTD request){
            if (request != null){
                EXP_DIC_PrimaryOTD record = db.EXP_DIC_PrimaryOTD.FirstOrDefault(x=>x.Id == request.Id);
                if (record != null) {
                    record.Code = request.Code;
                    record.NameRu = request.NameRu;
                    record.NameKz = request.NameKz;
                    record.DateEdit = DateTime.Now;
                    db.SaveChanges();
                    return Content(bool.TrueString);
                }
                else {
                    request.DateEdit = DateTime.Now;
                    db.EXP_DIC_PrimaryOTD.Add(request);
                    db.SaveChanges();
                    return Content(bool.TrueString);
                }
            }
            return Content(bool.FalseString);
        }

        [HttpPost]
        public ActionResult RefPrimaryOtdDelete(int rowId){
            if (rowId > 0){
                EXP_DIC_PrimaryOTD record = db.EXP_DIC_PrimaryOTD.FirstOrDefault(x=>x.Id == rowId);
                if (record != null) {
                    record.IsDeleted = true;
                    db.SaveChanges();
                    return Content(bool.TrueString);
                }
            }
            return Content(bool.FalseString);
        }

        public ActionResult RefPrimaryOtdRead(int id, int? parentId) {
		    if (id == 0) {
		        var maxId = db.EXP_DIC_PrimaryOTD.AsNoTracking().Max(x => x.Id);
		        var newRecord = new EXP_DIC_PrimaryOTD {
		            Id = maxId + 1,
		            ParentId = null,
		            DateCreate = DateTime.Now,
		            ParentFullName = "-",
		            WindowTitle = "Добавление новой записи"
		        };
                return Content(JsonConvert.SerializeObject(newRecord, Formatting.Indented, new JsonSerializerSettings{
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    DateFormatString = "dd.MM.yyyy"
                }));
            }

		    EXP_DIC_PrimaryOTD record = db.EXP_DIC_PrimaryOTD.FirstOrDefault(x=>x.Id == id);
            if (record == null || parentId.HasValue) {
                EXP_DIC_PrimaryOTD parentRecord = db.EXP_DIC_PrimaryOTD.FirstOrDefault(x => x.Id == parentId.Value);
                var maxId = db.EXP_DIC_PrimaryOTD.AsNoTracking().Max(x => x.Id);
		        record = new EXP_DIC_PrimaryOTD {
		            Id = maxId + 1,
		            ParentId = parentId,
		            DateCreate = DateTime.Now,
                    ParentFullName = parentRecord.FullName,
                    WindowTitle = "Добавление новой записи"
                };
		    }
		    else {
                record.WindowTitle = "Редактирование";
                record.ParentFullName = record.EXP_DIC_PrimaryOTD2 != null ? record.EXP_DIC_PrimaryOTD2.FullName : "-";
            }
            return Content(JsonConvert.SerializeObject(record, Formatting.Indented, new JsonSerializerSettings() {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateFormatString = "dd.MM.yyyy"
			}));
		}

        #endregion

        public ActionResult AllList([DataSourceRequest] DataSourceRequest request, string type)
        {
            var data =
                db.Dictionaries.Where(o => o.Type == type).OrderBy(o => o.DisplayName).Select(o => new ReferenceModel
                {
                    Id = o.Id,
                    Name = o.Name,
                    Code = o.Code,
                    NameKz = o.NameKz,
                    Type = o.Type
                });
            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult AllSettingsList([DataSourceRequest] DataSourceRequest request, string type)
        {
            var data = db.Settings;
            return Json(data.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SettingsUpdate([DataSourceRequest] DataSourceRequest request, Setting dictionary)
        {
            if (dictionary != null && ModelState.IsValid)
            {
                var dic = db.Settings.First(o => o.Id == dictionary.Id);
                dic.Value = dictionary.Value;
                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult AllListUnits([DataSourceRequest] DataSourceRequest request)
        {
            Guid organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
            var data = db.Units
                .Where(o => o.Id != organizationId && (o.Type == 0 || o.Type == -2))
                .OrderBy(o => o.DisplayName)
                .Include(o => o.Parent).ToList().Select(o => new UnitModel(o)).ToList();
            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult AllRefListUnits([DataSourceRequest] DataSourceRequest request, string type)
        {
            Guid organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
            if (string.IsNullOrEmpty(type))
            {
                var data = db.Units
                    .Where(o => (o.Type == 0 || o.Type == -2)).
                    OrderBy(o => o.DisplayName)
                    .Include(o => o.Parent).ToList().Select(o => new UnitModel(o));
                return Json(data.ToDataSourceResult(request));
            }
            var data1 = db.Units
                .Where(o => (o.Type == 0 || o.Type == -2)).Where(o => o.UnitTypeDictionaryId == type).
                OrderBy(o => o.DisplayName)
                .Include(o => o.Parent).ToList().Select(o => new UnitModel(o));
            return Json(data1.ToDataSourceResult(request));
        }

        public ActionResult AllMultiListUnits()
        {
            Guid organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
            var data = db.Units
                .Where(o => (o.Type == 0 || o.Type == -2))
                .OrderBy(o => o.Name)
                .Include(o => o.Parent).ToList();
            return Json(data.Select(o => new { o.Id, Name = LocalizationHelper.GetString(o.Name, o.NameKz) }),
                JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UnitCreate([DataSourceRequest] DataSourceRequest request, UnitModel dictionary)
        {
            if (dictionary != null && ModelState.IsValid)
            {
                Dictionary dic =
                    db.Dictionaries.First(
                        o => o.Type == "DepartmentTypeDictionary" && o.Id == new Guid(dictionary.Category));
                Unit baseUnit = db.Units.Find(dictionary.Id);
                if (baseUnit == null)
                {
                    baseUnit = new Unit()
                    {
                        Id = Guid.NewGuid(),
                        Type = -2,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,

                        Name = dictionary.Name,
                        NameKz = dictionary.NameKz,
                        Email = dictionary.Email,
                        UnitTypeDictionaryId = dic != null ? dic.Id.ToString() : string.Empty,
                        UnitTypeDictionaryValue = dic != null ? dic.Name : string.Empty
                    };
                    db.Units.Add(baseUnit);
                }
                //baseUnit = dictionary.GetUnit(baseUnit);

                db.SaveChanges();
                dictionary.Id = baseUnit.Id;
                dictionary.NameFull = LocalizationHelper.GetString(baseUnit.Name, baseUnit.NameKz);
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UnitRefCreate([DataSourceRequest] DataSourceRequest request, UnitModel dictionary)
        {
            if (dictionary != null && ModelState.IsValid)
            {
                Guid idDic = new Guid(DictionaryHelper.GetItemsId(dictionary.UnitTypeDictionaryId));
                Dictionary dic =
                    db.Dictionaries.First(o => o.Type == "DepartmentTypeDictionary" && o.Id == idDic);
                Unit baseUnit = db.Units.Find(dictionary.Id);
                if (baseUnit == null)
                {
                    baseUnit = new Unit()
                    {
                        Id = Guid.NewGuid(),
                        Type = -2,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        Name = dictionary.Name,
                        NameKz = dictionary.NameKz,
                        Email = dictionary.Email,
                        UnitTypeDictionaryId = dic != null ? dic.Id.ToString() : string.Empty,
                        UnitTypeDictionaryValue = dic != null ? dic.Name : string.Empty
                    };
                    db.Units.Add(baseUnit);
                }
                //baseUnit = dictionary.GetUnit(baseUnit);
                dictionary.Category = dic != null ? dic.Id.ToString() : string.Empty;
                db.SaveChanges();
                dictionary.Id = baseUnit.Id;
                dictionary.NameFull = LocalizationHelper.GetString(baseUnit.Name, baseUnit.NameKz);
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UnitUpdate([DataSourceRequest] DataSourceRequest request, UnitModel dictionary)
        {
            if (dictionary != null && ModelState.IsValid)
            {
                Unit baseUnit = db.Units.Find(dictionary.Id);
                if (baseUnit == null)
                {
                    baseUnit = new Unit()
                    {
                        Id = dictionary.Id,
                        Type = -2,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };
                    db.Units.Add(baseUnit);
                }
                Dictionary dic =
                    db.Dictionaries.First(
                        o => o.Type == "DepartmentTypeDictionary" && o.Id == new Guid(dictionary.Category));
                baseUnit.Name = dictionary.Name;
                baseUnit.NameKz = dictionary.NameKz;
                baseUnit.Email = dictionary.Email;
                baseUnit.UnitTypeDictionaryId = dic != null ? dic.Id.ToString() : string.Empty;
                baseUnit.UnitTypeDictionaryValue = dic != null ? dic.Name : string.Empty;
                //baseUnit = dictionary.GetUnit(baseUnit);

                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UnitRefUpdate([DataSourceRequest] DataSourceRequest request, UnitModel dictionary)
        {
            if (dictionary != null && ModelState.IsValid)
            {
                Unit baseUnit = db.Units.Find(dictionary.Id);
                if (baseUnit == null)
                {
                    baseUnit = new Unit()
                    {
                        Id = dictionary.Id,
                        Type = -2,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };
                    db.Units.Add(baseUnit);
                }
                Guid idDic = new Guid(DictionaryHelper.GetItemsId(dictionary.UnitTypeDictionaryId));
                Dictionary dic =
                    db.Dictionaries.First(o => o.Type == "DepartmentTypeDictionary" && o.Id == idDic);
                baseUnit.Name = dictionary.Name;
                baseUnit.NameKz = dictionary.NameKz;
                baseUnit.Email = dictionary.Email;
                baseUnit.UnitTypeDictionaryId = dic != null ? dic.Id.ToString() : string.Empty;
                baseUnit.UnitTypeDictionaryValue = dic != null ? dic.Name : string.Empty;
                dictionary.Category = dic != null ? dic.Id.ToString() : string.Empty;
                //baseUnit = dictionary.GetUnit(baseUnit);

                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UnitDestroy([DataSourceRequest] DataSourceRequest request, UnitModel dictionary)
        {
            if (dictionary != null)
            {
                Unit d = db.Units.First(o => o.Id == dictionary.Id);
                db.Units.Remove(d);
                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DicCreate([DataSourceRequest] DataSourceRequest request, ReferenceModel dictionary,
            string type)
        {
            if (dictionary != null && ModelState.IsValid)
            {

                Dictionary d = new Dictionary()
                {
                    Id = Guid.NewGuid(),
                    Type = type,
                    Name = dictionary.Name,
                    NameKz = dictionary.NameKz,
                    Code = dictionary.Code,
                    Note = dictionary.Note
                };
                db.Dictionaries.Add(d);
                db.SaveChanges();
                dictionary.Id = d.Id;
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DicUpdate([DataSourceRequest] DataSourceRequest request, ReferenceModel dictionary,
            string type)
        {
            if (dictionary != null && ModelState.IsValid)
            {
                Dictionary d = db.Dictionaries.First(o => o.Id == dictionary.Id);
                d.Name = dictionary.Name;
                d.NameKz = dictionary.NameKz;
                d.Code = dictionary.Code;
                d.Note = dictionary.Note;

                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DicDestroy([DataSourceRequest] DataSourceRequest request, ReferenceModel dictionary,
            string type)
        {
            if (dictionary != null)
            {
                Dictionary d = db.Dictionaries.First(o => o.Id == dictionary.Id);
                db.Dictionaries.Remove(d);
                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult ListEmploye()
        {
            Guid organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
            if (EmployePermissionHelper.IsAllEmploye)
            {
                var data = db.Units.Where(
                    x => x.Type == 2 && x.PositionState == 1 && x.Employee != null)
                    .Select(o => o.Employee)
                    .OrderBy(o => o.DisplayName);
                return Json(data.Select(o => new { o.Id, Name = o.DisplayName }), JsonRequestBehavior.AllowGet);
            }
            List<Employee> employees = new List<Employee>();
            employees = employees.Union(GetSubEmployees(UserHelper.GetCurrentEmployee())).ToList();
            employees = employees.Union(GetDeputySubEmployees()).ToList();
            employees = employees.Union(GetAssistantsSubEmployees()).ToList();
            employees.Remove(UserHelper.GetCurrentEmployee());
            return Json(employees.Select(o => new { o.Id, Name = o.DisplayName }).OrderBy(o => o.Name),
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRegion()
        {
            // Guid organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
            var data = db.Dictionaries.Where(o => o.Type.Equals("RegionDictionary")).ToList();
            return Json(data.Select(o => new { o.Id, Name = o.Name }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AllListEmploye()
        {
            Guid organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
            //var data = db.Units.Where(
            //	x => x.Type == 2 && x.PositionState == 1 && x.Employee != null && x.Employee.OrganizationId == organizationId)
            //	.Select(o => o.Employee)
            //	.OrderBy(o => o.DisplayName);
            var data = db.Units.Where(
                x => x.Type == 2 && x.PositionState == 1 && x.Employee != null)
                .Select(o => o.Employee)
                .OrderBy(o => o.DisplayName);
            return Json(data.Select(o => new { o.Id, Name = o.DisplayName }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPrices(Guid id)
        {
            var data = db.PriceListExpertises.ToList();
            var org = db.Organizations.FirstOrDefault(m => m.ObjectId == id);
            var rp = db.RegisterProjects.FirstOrDefault(m => m.Id == id);
            if (org != null && org.CountryDicId != null)
            {
                var country = db.Dictionaries.FirstOrDefault(m => m.Id == org.CountryDicId.Value).Code;
                if (country != null && rp != null)
                {
                    if (country == "KZ")
                    {
                        if (rp.Type == 0)
                        {
                            return
                                Json(
                                    data.Select(
                                        o =>
                                            new
                                            {
                                                o.Id,
                                                Name = string.Format("{0} {1}", o.Name, o.PriceRegisterKzNds),
                                                Price = o.PriceRegisterKzNds
                                            }), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return
                                Json(
                                    data.Select(
                                        o =>
                                            new
                                            {
                                                o.Id,
                                                Name = string.Format("{0} {1}", o.Name, o.PriceReRegisterKzNds),
                                                Price = o.PriceReRegisterKzNds
                                            }), JsonRequestBehavior.AllowGet);

                        }
                    }
                    else
                    {
                        if (rp.Type == 0)
                        {
                            return
                                Json(
                                    data.Select(
                                        o =>
                                            new
                                            {
                                                o.Id,
                                                Name = string.Format("{0} {1}", o.Name, o.PriceRegisterForeignNds),
                                                Price = o.PriceRegisterForeignNds
                                            }), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return
                                Json(
                                    data.Select(
                                        o =>
                                            new
                                            {
                                                o.Id,
                                                Name = string.Format("{0} {1}", o.Name, o.PriceReRegisterForeignNds),
                                                Price = o.PriceReRegisterForeignNds
                                            }), JsonRequestBehavior.AllowGet);

                        }
                    }
                }
            }

            return
                Json(
                    data.Select(
                        o =>
                            new
                            {
                                o.Id,
                                Name = string.Format("{0} {1}", o.Name, o.PriceRegisterKzNds),
                                Price = o.PriceRegisterKzNds
                            }), JsonRequestBehavior.AllowGet);


        }

        public ActionResult EmployeeNotGuide()
        {
            var data = db.Units.Join(db.Employees, u => u.Id, e => e.PositionId, (u, e) => new { u, e }).Where(
                x => x.u.Type == 2 && x.u.PositionState == 1 && x.u.Employee != null && !x.e.IsGuide)
                .Select(o => o.u.Employee)
                .OrderBy(o => o.DisplayName);
            return Json(data.Select(o => new { o.Id, Name = o.DisplayName }), JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<Employee> GetSubEmployees(Employee employee)
        {
            Guid organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
            List<Employee> employees = new List<Employee>();
            Unit unit = employee.Position.Parent;
            employees.AddRange(
                db.Units.Where(
                    x =>
                        x.ParentId == unit.Id && x.Type == 2 && x.PositionState == 1 && x.Employee != null &&
                        x.Employee.OrganizationId == organizationId).Select(o => o.Employee));
            IList<string> managersId =
                db.Units.Where(
                    x => x.CuratorId == employee.Id.ToString() && x.ManagerId != null && x.ManagerId != string.Empty)
                    .Select(x => x.ManagerId)
                    .ToList();
            if (managersId.Count > 0)
                employees.AddRange(
                    db.Units.Where(
                        x =>
                            x.Type == 2 && x.PositionState == 1 && x.Employee != null &&
                            x.Employee.OrganizationId == organizationId)
                        .Where(x => managersId.Contains(x.Employee.Id.ToString()))
                        .Select(o => o.Employee));

            IList<Guid> subDepartmentsId =
                db.Units.Where(x => x.ParentId == unit.Id && x.Type == 1).Select(o => o.Id).ToList();
            if (subDepartmentsId.Count > 0)
                employees.AddRange(
                    db.Units.Where(
                        x =>
                            x.Type == 2 && x.PositionState == 1 && x.Employee != null &&
                            x.Employee.OrganizationId == organizationId)
                        .Where(x => x.ParentId != null && subDepartmentsId.Contains(x.ParentId.Value))
                        .Select(o => o.Employee));
            foreach (var item in subDepartmentsId)
            {
                IList<Guid> subSectorsId =
                    db.Units.Where(x => x.ParentId == item && x.Type == 1).Select(o => o.Id).ToList();
                if (subSectorsId.Count > 0)
                    employees.AddRange(
                        db.Units.Where(
                            x =>
                                x.Type == 2 && x.PositionState == 1 && x.Employee != null &&
                                x.Employee.OrganizationId == organizationId)
                            .Where(x => x.ParentId != null && subSectorsId.Contains(x.ParentId.Value))
                            .Select(o => o.Employee));
            }
            return employees;
        }

        private IEnumerable<Employee> GetDeputySubEmployees()
        {
            List<Employee> employees = new List<Employee>();
            string currentEmployeeId = UserHelper.GetCurrentEmployee().Id.ToString();
            foreach (
                Employee employee in
                    db.Employees.Where(
                        x =>
                            x.DeputyId != null && x.DeputyId.Contains(currentEmployeeId) && x.Position != null &&
                            x.Position.PositionState == 1))
                employees = employees.Union(GetSubEmployees(employee)).ToList();
            return employees;
        }

        private IEnumerable<Employee> GetAssistantsSubEmployees()
        {
            List<Employee> employees = new List<Employee>();
            string currentEmployeeId = UserHelper.GetCurrentEmployee().Id.ToString();
            foreach (
                Employee employee in
                    db.Employees.Where(
                        x =>
                            x.AssistantsId != null && x.AssistantsId.Contains(currentEmployeeId) && x.Position != null &&
                            x.Position.PositionState == 1))
                employees = employees.Union(GetSubEmployees(employee)).ToList();
            return employees;
        }

        public ActionResult ListEmployeAll(string possitionCode = null)
        {
            IQueryable<Employee> query;
            if (!string.IsNullOrEmpty(possitionCode))
            {
                var positionId = DictionaryHelper.GetDicIdByCode("PositionTypeDictionary", possitionCode).ToString();
                query = db.Units.Where(x => x.Type == 2 && x.Employee != null && x.UnitTypeDictionaryId == positionId)
                    .Select(o => o.Employee)
                    .OrderBy(o => o.DisplayName);
            }
            else
                query = db.Units.Where(x => x.Type == 2 && x.Employee != null)
                    .Select(o => o.Employee)
                    .OrderBy(o => o.DisplayName);
            return Json(query.Select(o => new { o.Id, Name = o.DisplayName }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListEmployeIc()
        {
            // TODO need to change
            var idIc = Guid.Parse("C0AD22A0-9912-47C3-AADB-8DAD27ACB141");
            var data = db.Units.Where(x => x.Type == 2 && x.Employee != null && x.Parent != null
                                           && x.Parent.ParentId.HasValue && x.Parent.ParentId.Value == idIc)
                .Select(o => o.Employee)
                .OrderBy(o => o.DisplayName);
            return Json(data.Select(o => new { o.Id, Name = o.DisplayName }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListEmployeeHeader()
        {
            // TODO need to change
            // var idIc = Guid.Parse("C0AD22A0-9912-47C3-AADB-8DAD27ACB141");
            string HeadCode = OrganizationConsts.HeadCode;
            var data = db.Units.Where(x => x.Type == 2 && x.Employee != null && x.Parent != null
                                           && x.Parent.Code == HeadCode)
                .Select(o => o.Employee)
                .OrderBy(o => o.DisplayName);
            return Json(data.Select(o => new { o.Id, Name = o.DisplayName }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListEmployeeHeaderAndFinanceDepHead()
        {
            // TODO need to change
            // var idIc = Guid.Parse("C0AD22A0-9912-47C3-AADB-8DAD27ACB141");
            string HeadCode = OrganizationConsts.HeadCode;
            var data = db.Units.Where(x => x.Type == 2 && x.Employee != null && x.Parent != null
                                           && x.Parent.Code == HeadCode)
                .Select(o => o.Employee)
                .OrderBy(o => o.DisplayName);
            var dlist = data.Select(o => new { o.Id, Name = o.DisplayName }).ToList();

            var financeDep = db.Units.FirstOrDefault(f => f.Code == OrganizationConsts.FinanceDepartmentCode);
            if (financeDep != null
                && !string.IsNullOrEmpty(financeDep.BossId)
                && !string.IsNullOrEmpty(financeDep.BossValue))
            {
                dlist.Add(new { Id = Guid.Parse(financeDep.BossId), Name = financeDep.BossValue });
            }

            return Json(dlist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListEmployeeOfDepartmentByDepartmentCode(string code)
        {
            var data = db.Units.Where(x => x.Type == 2
                                           && x.Employee != null
                                           && x.Parent != null
                                           && x.Parent.Code == code)
                .Select(o => o.Employee)
                .OrderBy(o => o.DisplayName);
            return Json(data.Select(o => new { o.Id, Name = o.DisplayName }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListEmployeeOfDepartmentByDepartmentId(Guid depId)
        {
            var data = db.Units.Where(x => x.Type == 2
                                           && x.Employee != null
                                           && x.Id != null
                                           && x.Parent != null
                                           && x.ParentId == depId
                                           || x.Parent.ParentId == depId)
                .Select(o => o.Employee)
                .Where(o => o.Id != null)
                .OrderBy(o => o.DisplayName);

            var list = data.Select(o => new { o.Id, Name = o.DisplayName }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //
        //	public ActionResult ListUnit() {
        //            var data = db.Units.Where(x => x.Type == 1).OrderBy(o => o.Code);
        //            return Json(data.Select(o => new { o.Id, Name = o.DisplayName }), JsonRequestBehavior.AllowGet);
        //        }
        //
        //        public ActionResult ListCategory()
        //        {
        //            var items = db.Dictionaries.Where(o => o.Type == "DepartmentTypeDictionary").ToList();
        //            return Json(items.Select(o => new { o.Id, Name = LocalizationHelper.GetString(o.Name, o.NameKz) }), JsonRequestBehavior.AllowGet);
        //        }
        //        public ActionResult HolidayTypeDictionaryList()
        //        {
        //            var data = db.Dictionaries.Where(x => x.Type == "HolidayTypeDictionary").OrderBy(o => o.Name);
        //            return Json(data.Select(o => new { o.Id, Name = o.DisplayName }), JsonRequestBehavior.AllowGet);
        //        }
        //        public ActionResult ListDocumentOut(string text, string data)
        //        {
        //            var result = string.IsNullOrEmpty(text) ? db.Documents.Where(o => o.DocumentType == 1).OrderBy(o => o.DisplayName).Take(100).ToList() : db.Documents.Where(o => o.DocumentType == 1 && o.DisplayName.Contains(text)).OrderBy(o => o.DisplayName).Take(100).ToList();
        //            var res = result.Select(o => new Item() { Id = o.Id.ToString(), Name = o.DisplayName });
        //            if (data != null)
        //            {
        //                var js = new JavaScriptSerializer();
        //                var myobj = js.Deserialize<List<Item>>(data);
        //                res = res.Union(myobj);
        //            }
        //            return Json(res, JsonRequestBehavior.AllowGet);
        //        }
        public ActionResult ListUnit()
        {
            var data = db.Units.Where(x => x.Type == 1).OrderBy(o => o.Code);
            return Json(data.Select(o => new { o.Id, Name = o.DisplayName }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListCategory()
        {
            var items = db.Dictionaries.Where(o => o.Type == "DepartmentTypeDictionary").ToList();
            return Json(items.Select(o => new { o.Id, Name = LocalizationHelper.GetString(o.Name, o.NameKz) }),
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult HolidayTypeDictionaryList()
        {
            var data = db.Dictionaries.Where(x => x.Type == "HolidayTypeDictionary").OrderBy(o => o.Name);
            return Json(data.Select(o => new { o.Id, Name = o.DisplayName }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListDocumentOut(string text, string data)
        {
            var result = string.IsNullOrEmpty(text)
                ? db.Documents.Where(o => o.DocumentType == 1).OrderBy(o => o.DisplayName).Take(100).ToList()
                : db.Documents.Where(o => o.DocumentType == 1 && o.DisplayName.Contains(text))
                    .OrderBy(o => o.DisplayName)
                    .Take(100)
                    .ToList();
            var res = result.Select(o => new Item() { Id = o.Id.ToString(), Name = o.DisplayName });
            if (data != null)
            {
                var js = new JavaScriptSerializer();
                var myobj = js.Deserialize<List<Item>>(data);
                res = res.Union(myobj);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListDocumentDestination(string text, string data)
        {
            if (data != null)
            {
                var js = new JavaScriptSerializer();
                var myobj = js.Deserialize<List<Item>>(data);
                Guid id;
                if (Guid.TryParse(myobj[0].Id, out id))
                {
                    var result = db.Documents.Where(o => o.Id == id).ToList();
                    var res = result.Select(o => new Item { Id = o.Id.ToString(), Name = o.DisplayName });
                    res = res.Union(myobj);
                    return Json(res, JsonRequestBehavior.AllowGet);
                }
            }
            return null;
        }

        public ActionResult AllListEmployeForAdministrativeDocument()
        {
            IOrderedQueryable<Employee> data;
            Guid organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
            if (organizationId == Guid.Parse("8F0B91F3-AF29-4D3C-96D6-019CBBDFC8BE"))
                data =
                    db.Units.Where(x => x.Type == 2 && x.PositionState == 1 && x.Employee != null)
                        .Select(o => o.Employee)
                        .OrderBy(o => o.DisplayName);
            else
                data =
                    db.Units.Where(
                        x =>
                            x.Type == 2 && x.PositionState == 1 && x.Employee != null &&
                            x.Employee.OrganizationId == organizationId)
                        .Select(o => o.Employee)
                        .OrderBy(o => o.DisplayName);
            return Json(data.Select(o => new { o.Id, Name = o.DisplayName }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListDocumentInById(string id, string propertyName)
        {
            List<Item> items = new List<Item>();
            if (!string.IsNullOrEmpty(id))
            {
                var key = new Guid(id);
                var document = db.Documents.Find(key);
                switch (propertyName)
                {
                    case "AutoAnswersId":

                        items = DictionaryHelper.GetItems(document.AutoAnswersId, document.AutoAnswersValue);
                        break;
                    default:
                        break;
                }
            }
            //var result = string.IsNullOrEmpty(text) ? db.Documents.Where(o => o.DocumentType == 0).OrderBy(o => o.DisplayName).Take(100).ToList() : db.Documents.Where(o => o.DocumentType == 0).Where(o => o.DisplayName.Contains(text)).OrderBy(o => o.DisplayName).Take(100).ToList();
            //var res = result.Select(o => new Item() { Id = o.Id.ToString(), Name = o.DisplayName });
            //if (data != null)
            //{
            //	var js = new JavaScriptSerializer();
            //	var myobj = js.Deserialize<List<Item>>(data);
            //	res = res.Union(myobj);
            //}
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListDocumentIn(string text, string data)
        {

            var result = string.IsNullOrEmpty(text)
                ? db.Documents.Where(o => o.DocumentType == 0).OrderBy(o => o.DisplayName).Take(100).ToList()
                : db.Documents.Where(o => o.DocumentType == 0)
                    .Where(o => o.DisplayName.Contains(text))
                    .OrderBy(o => o.DisplayName)
                    .Take(100)
                    .ToList();
            var res = result.Select(o => new Item() { Id = o.Id.ToString(), Name = o.DisplayName });
            if (data != null)
            {
                var js = new JavaScriptSerializer();
                var myobj = js.Deserialize<List<Item>>(data);
                res = res.Union(myobj);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListDocumentInAndCit(string text, string data)
        {

            var result = string.IsNullOrEmpty(text)
                ? db.Documents.Where(o => o.DocumentType == 0 || o.DocumentType == 2)
                    .OrderBy(o => o.DisplayName)
                    .Take(100)
                    .ToList()
                : db.Documents.Where(o => o.DocumentType == 0 || o.DocumentType == 2)
                    .Where(o => o.DisplayName.Contains(text))
                    .OrderBy(o => o.DisplayName)
                    .Take(100)
                    .ToList();
            var res = result.Select(o => new Item() { Id = o.Id.ToString(), Name = o.DisplayName });
            if (data != null)
            {
                var js = new JavaScriptSerializer();
                var myobj = js.Deserialize<List<Item>>(data);
                res = res.Union(myobj);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListDocumentAdm(string text, string data)
        {

            var result = string.IsNullOrEmpty(text)
                ? db.Documents.Where(o => o.DocumentType == 3).OrderBy(o => o.DisplayName).Take(100).ToList()
                : db.Documents.Where(o => o.DocumentType == 3)
                    .Where(o => o.DisplayName.Contains(text))
                    .OrderBy(o => o.DisplayName)
                    .Take(100)
                    .ToList();
            var res = result.Select(o => new Item() { Id = o.Id.ToString(), Name = o.DisplayName });
            if (data != null)
            {
                var js = new JavaScriptSerializer();
                var myobj = js.Deserialize<List<Item>>(data);
                res = res.Union(myobj);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListDocumentAdmHolidays(string text, string data)
        {
            var result = string.IsNullOrEmpty(text)
                ? db.Documents.Where(
                    o =>
                        o.DocumentType == 3 &&
                        o.AdministrativeTypeDictionaryId.ToUpper() == "91E8A59D-497C-470F-ACB4-71BD13E49823")
                    .OrderBy(o => o.DisplayName)
                    .Take(100)
                    .ToList()
                : db.Documents.Where(o => o.DocumentType == 3)
                    .Where(o => o.DisplayName.Contains(text))
                    .OrderBy(o => o.DisplayName)
                    .Take(100)
                    .ToList();
            var res = result.Select(o => new Item() { Id = o.Id.ToString(), Name = o.DisplayName });
            if (data != null)
            {
                var js = new JavaScriptSerializer();
                var myobj = js.Deserialize<List<Item>>(data);
                res = res.Union(myobj);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListDocumentPrj(string text, string data)
        {

            var result = string.IsNullOrEmpty(text)
                ? db.Documents.Where(o => o.DocumentType == 4).OrderBy(o => o.DisplayName).Take(100).ToList()
                : db.Documents.Where(o => o.DocumentType == 4)
                    .Where(o => o.DisplayName.Contains(text))
                    .OrderBy(o => o.DisplayName)
                    .Take(100)
                    .ToList();
            var res = result.Select(o => new Item() { Id = o.Id.ToString(), Name = o.DisplayName });
            if (data != null)
            {
                var js = new JavaScriptSerializer();
                var myobj = js.Deserialize<List<Item>>(data);
                res = res.Union(myobj);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListServer([DataSourceRequest] DataSourceRequest request, string text)
        {
            var data = db.Dictionaries.Where(o => o.Type == text).ToList();
            return Json(data.Select(o => new { o.Id, o.Name }).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult OpenDocument(Guid id)
        {
            Document document = db.Documents.Find(id);

            switch (document.DocumentType)
            {
                case 0:
                    return RedirectToAction("Card", "IncomingDoc", new { id = id });
                case 1:
                    if (document.OutgoingType == 0)
                        return RedirectToAction("CardInit", "OutgoingDoc", new { id = id });
                    return RedirectToAction("Card", "OutgoingDoc", new { id = id });
                case 2:
                    return RedirectToAction("Card", "CitizenDoc", new { id = id });
                case 3:
                    return RedirectToAction("Card", "AdministrativeDoc", new { id = id });
                case 4:
                    return RedirectToAction("CardCor", "ProjectDoc", new { id = id });
                case 5:
                    if (document.ProjectType == 2)
                        return RedirectToAction("Card", "CorrespondenceDoc", new { id = id });
                    if (document.ProjectType == 5)
                        return RedirectToAction("Cardinit", "CorrespondenceDoc", new { id = id });
                    return PartialView();
            }
            return PartialView();
        }

        public ActionResult CorrespondentList(Guid id, string type)
        {
            ViewBag.Units =
                db.Dictionaries.Where(o => o.Type == "DepartmentTypeDictionary")
                    .OrderBy(o => o.Name)
                    .Select(o => new { o.Id, o.Name })
                    .ToList();
            return PartialView(new string[] { id.ToString(), type });
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult OutgoingNumbers(string correspondent)
        {
            List<object> dataResult = new List<object>();
            if (!string.IsNullOrEmpty(correspondent))
            {
                var data = db.Documents.Where(x => x.DocumentType == 0 && x.CorrespondentsValue == correspondent)
                    .GroupBy(x => x.OutgoingNumber + "#" + x.AutoOutgoingDate)
                    .OrderBy(x => x.Key);
                foreach (IGrouping<string, Document> item in data)
                {
                    string[] res = item.Key.Split('#');
                    if (!string.IsNullOrEmpty(res[0]))
                        dataResult.Add(new { Id = Guid.NewGuid(), Name = res[0], Date = res[1] });
                }
            }
            return Json(dataResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OutgoingNumbersAll(List<Item> correspondent, string text)
        {
            //string cor = DictionaryHelper.GetItemsId(correspondent);
            Guid orgId = UserHelper.GetCurrentEmployee().OrganizationId;
            List<object> dataResult = new List<object>();
            var data = db.Documents.Where(x => x.DocumentType == 0 && x.StateType > 0 && x.OrganizationId == orgId)
                .Where(o => o.OutgoingNumber.Contains(text))
                .GroupBy(x => x.OutgoingNumber + "#" + x.AutoOutgoingDate)
                .OrderBy(x => x.Key);
            foreach (IGrouping<string, Document> item in data)
            {
                string[] res = item.Key.Split('#');
                if (!string.IsNullOrEmpty(res[0]))
                    dataResult.Add(res[0] + " от " + res[1]);
            }
            return Json(dataResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListProjectForProtocols()
        {
            var projects = db.RegisterProjectJournals.Where(m => m.Status > 0).ToList();
            return
                Json(
                    projects.Select(
                        o =>
                            new
                            {
                                o.Id,
                                Name =
                                    string.Format("{0} от {1} Заявитель: {2} - {3}", o.Number ?? "Б/Н",
                                        o.CreatedDate.ToString("dd.MM.yyyy"), o.ApplicantName ?? "Не указан", o.NameRu)
                            }),
                    JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListProtocolForProjects()
        {
            var projects = db.Documents.Where(m => m.DocumentType == 4 && m.ProjectType > 9 && m.StateType > 0).ToList();
            return
                Json(
                    projects.Select(
                        o =>
                            new
                            {
                                o.Id,
                                Name =
                                    string.Format("{0} от {1}", o.Number ?? "Б/Н",
                                        o.CreatedDate.Value.ToString("dd.MM.yyyy"))
                            }), JsonRequestBehavior.AllowGet);
        }


        public ActionResult ListOfExretiseStatementOnAnaliticStage()
        {
            IQueryable<Task> q = db.Tasks //.Include("Document.Template")
                .Where(o => !o.Document.IsDeleted && !o.IsActive)
                .Where(o => o.Stage == 4);
            var analiticStages = q.Select(o => new
            {
                Id = o.DocumentId,
                Name = o.DocumentValue
            }).ToList();

            return Json(analiticStages, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOrganizations()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Organizations.AsNoTracking().Where(o => o.OriginalOrgId == null), JsonRequestBehavior.AllowGet);
        }        
    }
}
