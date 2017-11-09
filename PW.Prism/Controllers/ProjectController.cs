using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Enums.PriceProject;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;

namespace PW.Prism.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private ncelsEntities db = UserHelper.GetCn();
        // GET: Project

        #region views
        public ActionResult Price()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }
		public ActionResult RefArchive() {
			Guid guid = Guid.NewGuid();
			return PartialView(guid);
		}
		public ActionResult PriceLs()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }
        public ActionResult PriceImn()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }
        public ActionResult RePriceLs()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }
        public ActionResult RePriceImn()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }
        public ActionResult RegisterLs()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }
        public ActionResult Register()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }
        public ActionResult ReRegisterLs()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }
        public ActionResult ChRegisterLs()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }

		public ActionResult ListEd() {
			Guid guid = Guid.NewGuid();
			return PartialView(guid);
		}

		public ActionResult ListAlo() {
			Guid guid = Guid.NewGuid();
			return PartialView(guid);
		}

		public ActionResult ListKnf() {
			Guid guid = Guid.NewGuid();
			return PartialView(guid);
		}

        public ActionResult PriceProjectHistory(){
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }

		public ActionResult RequestOrders() {
			Guid guid = Guid.NewGuid();
			return PartialView(guid);
		}

        public ActionResult PriceProjectHistoryList(string id){
            if (!string.IsNullOrEmpty(id)) {
                var param = id.Split('|');
                if (param.Length == 2) {
                    var type = int.Parse(param[0]);
                    var year = int.Parse(param[1]);
                    ViewBag.Type = type;
                    ViewBag.Year = year;
                    return PartialView(Guid.NewGuid());
                }
            }
            return null;
        }

        public object RequestOrderTypesList(string id){
            if (string.IsNullOrEmpty(id)) {
                var types = EnumHelper.GetDisplayNameEnumList<RequestOrderType>();
                var list = types.Select(x => new ProjectRequestsTreeItemModel
                {
                    id = x.Key.ToString(),
                    ListType = x.Key,
                    Type = 1,
                    Name = x.Value,
                    ParentId = null,
                    hasChildren = true,
                    Year = 0
                });
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            var param = id.Split('|');
            if (param.Length == 1) {
                var listType = int.Parse(param[0]);
                var y = new List<int>();
                var startYear = 2015;
                var endYear = 2017;
                while (startYear <= endYear) {
                    y.Add(startYear);
                    startYear++;
                }
                var list = y.Select(x => new ProjectRequestsTreeItemModel {
                    id = listType.ToString() + "|" + x.ToString(),
                    ListType = int.Parse(param[0]),
                    Type = 2,
                    Name = x.ToString(),
                    ParentId = null,
                    hasChildren = false,
                    Year = x
                });
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            throw new Exception("Некорректное значение параметра");
        }

        public object RequestOrderList(string id){
            if (string.IsNullOrEmpty(id)) {
                var types = EnumHelper.GetDisplayNameEnumList<RequestOrderType>();
                var list = types.Select(x => new ProjectRequestsTreeItemModel
                {
                    id = x.Key.ToString(),
                    ListType = x.Key,
                    Type = 1,
                    Name = x.Value,
                    ParentId = null,
                    hasChildren = true,
                    Year = 0
                });
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            var param = id.Split('|');
            if (param.Length == 1) {
                var listType = int.Parse(param[0]);
                var y = new List<int>();
                var startYear = 2017;
                while (startYear <= (DateTime.Now.Year + 1)) {
                    y.Add(startYear);
                    startYear++;
                }
                var list = y.Select(x => new ProjectRequestsTreeItemModel {
                    id = listType.ToString() + "|" + x.ToString(),
                    ListType = int.Parse(param[0]),
                    Type = 2,
                    Name = x.ToString(),
                    ParentId = null,
                    hasChildren =
                        db.RequestOrders.Any(o => !o.IsDeleted && o.OrderType == listType && o.OrderYear == x),
                    Year = x
                });
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            if (param.Length == 2) {
                var listType = int.Parse(param[0]);
                var year = int.Parse(param[1]);
                var rows = db.RequestOrders.Where(o => !o.IsDeleted && o.OrderType == listType && o.OrderYear == year).ToList();
                var list = rows.Select(x => new ProjectRequestsTreeItemModel{
                    id = listType.ToString() + "|" + year.ToString() + "|" + x.Id.ToString(),
                    ListType = x.OrderType,
                    Type = 3,
                    Name = x.OrderNumber,
                    ParentId = null,
                    hasChildren = false,
                    Year = x.OrderYear
                });
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            throw new Exception("Некорректное значение параметра");
        }

        public object GetRequestOrderNumber(string id) {
            if (!string.IsNullOrEmpty(id)) {
                var param = id.Split('|');
                if (param.Length == 3) {
                    var guid = new Guid(param[2]);
                    var row = db.RequestOrders.FirstOrDefault(o => o.Id == guid);
                    if (row != null) {
                        return Json(row.OrderNumber, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return null;
        }

        public ActionResult CheckDeleteRequestOrder(string id) {
            if (!string.IsNullOrEmpty(id)) {
                var param = id.Split('|');
                if (param.Length == 3) {
                    var guid = new Guid(param[2]);
                    var hasRequests = db.RequestLists.Any(o => o.RequestOrderId == guid);
                    if (!hasRequests) {
                        return Content(bool.TrueString);
                    }
                }
            }
            return Content(bool.FalseString);
        }

        public ActionResult RequestsList(string id, string parentId, Guid? modelId, string uid){
            if (!string.IsNullOrEmpty(id)) {
                var param = id.Split('|');
                if (param.Length == 3) {
                    var gid = new Guid(param[2]);
                    var ro = db.RequestOrders.FirstOrDefault(x => x.Id == gid);
                    if (ro != null) {
                        return PartialView(ro);
                    }
                    
                }else if (param.Length == 2) {
                    var type = int.Parse(param[0]);
                    var year = int.Parse(param[1]);
                    return PartialView(new RequestOrder {
                        OrderType = type,
                        OrderYear = year
                    });
                }
            }
            return null;
        }

        public ActionResult KnfRead([DataSourceRequest] DataSourceRequest request) {
			var data = db.Ref_Knfs.Where(x=>x.Type == 0).ToList().OrderBy(o => o.Name);
			return Json(data.ToDataSourceResult(request));
		}

		public ActionResult EdRead([DataSourceRequest] DataSourceRequest request) {
			var data = db.Ref_Knfs.Where(x => x.Type == 1).ToList().OrderBy(o => o.Name);
			return Json(data.ToDataSourceResult(request));
		}

		public ActionResult AloRead([DataSourceRequest] DataSourceRequest request) {
			var data = db.Ref_Knfs.Where(x => x.Type == 2).ToList().OrderBy(o => o.Name);
			return Json(data.ToDataSourceResult(request));
		}

		public ActionResult OthRead([DataSourceRequest] DataSourceRequest request) {
			var data = db.Ref_Knfs.Where(x => x.Type == 3).ToList().OrderBy(o => o.Name);
			return Json(data.ToDataSourceResult(request));
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult KnfCreate([DataSourceRequest] DataSourceRequest request, Ref_Knfs item) {
			if (item != null) {
				item.Id = Guid.NewGuid();
				item.Type = 0;
				db.Ref_Knfs.Add(item);
				db.SaveChanges();
			}

			return Json(new[] { item }.ToDataSourceResult(request, ModelState));
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult EdCreate([DataSourceRequest] DataSourceRequest request, Ref_Knfs item) {
			if (item != null) {
				item.Id = Guid.NewGuid();
				item.Type = 1;
				db.Ref_Knfs.Add(item);
				db.SaveChanges();
			}

			return Json(new[] { item }.ToDataSourceResult(request, ModelState));
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult AloCreate([DataSourceRequest] DataSourceRequest request, Ref_Knfs item) {
			if (item != null) {
				item.Id = Guid.NewGuid();
				item.Type = 2;
				db.Ref_Knfs.Add(item);
				db.SaveChanges();
			}

			return Json(new[] { item }.ToDataSourceResult(request, ModelState));
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult OthCreate([DataSourceRequest] DataSourceRequest request, Ref_Knfs item) {
			if (item != null) {
				item.Id = Guid.NewGuid();
				item.Type = 3;
				db.Ref_Knfs.Add(item);
				db.SaveChanges();
			}

			return Json(new[] { item }.ToDataSourceResult(request, ModelState));
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult KnfUpdate([DataSourceRequest] DataSourceRequest request, Ref_Knfs item) {
			if (item != null && ModelState.IsValid) {
				Ref_Knfs newItem = db.Ref_Knfs.First(o => o.Id == item.Id);
				newItem.Name = item.Name;
				newItem.Code = item.Code;
				newItem.Cost = item.Cost;
				newItem.GroupFarm = item.GroupFarm;
				newItem.Form = item.Form;
				newItem.Number = item.Number;
				newItem.Characteristic = item.Characteristic;
				newItem.CostTn = item.CostTn;
				newItem.DateExpiry = item.DateExpiry;
				newItem.Dosage = item.Dosage;
				newItem.Measure = item.Measure;
				newItem.State = item.State;
				newItem.Tn = item.Tn;
				db.SaveChanges();
			}

			return Json(new[] { item }.ToDataSourceResult(request, ModelState));
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult KnfDestroy([DataSourceRequest] DataSourceRequest request, Ref_Knfs item) {
			if (item != null) {
				Ref_Knfs newItem = db.Ref_Knfs.First(o => o.Id == item.Id);
				db.Ref_Knfs.Remove(newItem);
				db.SaveChanges();
			}

			return Json(new[] { item }.ToDataSourceResult(request, ModelState));
		}

        [HttpPost]
        public ActionResult RequestOrderAddUpdate(string dataId, string orderName){
            if (!string.IsNullOrEmpty(dataId)) {
                var param = dataId.Split('|');
                if (param.Length == 2) {
                    var type = int.Parse(param[0]);
                    var year = int.Parse(param[1]);
                    var requestOrder = new RequestOrder {
                        Id = Guid.NewGuid(),
                        OrderYear = year,
                        OrderType = type,
                        CreatedDate = DateTime.Now,
                        OrderNumber = orderName,
                        IsDeleted = false
                    };
                    db.RequestOrders.Add(requestOrder);
                    db.SaveChanges();
                    return Content(bool.TrueString);
                }
                if (param.Length == 3) {
                    var gid = new Guid(param[2]);
                    var ro = db.RequestOrders.FirstOrDefault(x => x.Id == gid);
                    if (ro != null) {
                        ro.OrderNumber = orderName;
                        db.Entry(ro).State = EntityState.Modified;
                        db.SaveChanges();
                        return Content(bool.TrueString);
                    }
                    
                }
            }
            return Content(bool.FalseString);
        }

        [HttpPost]
        public ActionResult RequestOrderDelete(string dataId){
            if (!string.IsNullOrEmpty(dataId)) {
                var param = dataId.Split('|');
                if (param.Length == 3) {
                    var gid = new Guid(param[2]);
                    var ro = db.RequestOrders.FirstOrDefault(x => x.Id == gid);
                    if (ro != null) {
                        db.RequestOrders.Remove(ro);
                        db.SaveChanges();
                        return Content(bool.TrueString);
                    }
                    
                }
            }
            return Content(bool.FalseString);
        }

        public ActionResult ListOth() {
			Guid guid = Guid.NewGuid();
			return PartialView(guid);
		}

		public ActionResult RegisterArchive()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }

        public ActionResult ListPrice([DataSourceRequest] DataSourceRequest request,int ? type) {
            if (type != null) {
				var prices = db.PriceProjectJournals
				.Join(db.Documents, p => p.Id, d => d.Id, (p, d) => new { p, d })
				.Where(m => m.p.Type == type && m.p.Status > 0 && !m.d.IsArchive).Select(m => m.p).AsQueryable();
				DataSourceResult result = prices.ToDataSourceResult(request);
                return Json(result);
            }
            else {
				var prices = db.PriceProjectJournals
				.Join(db.Documents, p => p.Id, d => d.Id, (p, d) => new { p, d })
				.Where(m => m.p.Status > 0 && !m.d.IsArchive).Select(m => m.p).AsQueryable();
				DataSourceResult result = prices.ToDataSourceResult(request);
                return Json(result);
            }
        }

		public ActionResult RefArchiveList([DataSourceRequest] DataSourceRequest request, int? type) {
			if (type != null) {
				var prices = db.PriceProjectJournals
				.Join(db.Documents, p => p.Id, d => d.Id, (p, d) => new { p, d })
				.Where(m => m.p.Type == type && m.p.Status > 0 && m.d.IsArchive).Select(m => m.p).AsQueryable();
				DataSourceResult result = prices.ToDataSourceResult(request);
				return Json(result);
			}
			else {
				var prices = db.PriceProjectJournals
				.Join(db.Documents, p => p.Id, d => d.Id, (p, d) => new { p, d })
				.Where(m => m.p.Status > 0 && m.d.IsArchive).Select(m => m.p).AsQueryable();
				DataSourceResult result = prices.ToDataSourceResult(request);
				return Json(result);
			}
		}

		public ActionResult ListRegister([DataSourceRequest] DataSourceRequest request,int ? type)
        {
            if (type != null)
            {
                var prices = db.RegisterProjectJournals
                   .Join(db.Documents, p => p.Id, d => d.Id, (p, d) => new { p, d })
                    .Where(m => m.p.Type == type && m.p.Status > 0 && !m.d.IsArchive).Select(m => m.p).AsQueryable();
                DataSourceResult result = prices.ToDataSourceResult(request);
                return Json(result);
            }
            else
            {
                var prices = db.RegisterProjectJournals
                   .Join(db.Documents, p => p.Id, d => d.Id, (p, d) => new { p, d })
                    .Where(m => m.p.Status > 0 && !m.d.IsArchive).Select(m=>m.p).AsQueryable();
                DataSourceResult result = prices.ToDataSourceResult(request);
                return Json(result);
            }
        }

        public ActionResult ListRegisterArchive([DataSourceRequest] DataSourceRequest request)
        {
                var prices = db.RegisterProjectJournals
                   .Join(db.Documents, p => p.Id, d => d.Id, (p, d) => new { p, d })
                    .Where(m => m.p.Status > 0 && m.d.IsArchive).Select(m=>m.p).AsQueryable();
                DataSourceResult result = prices.ToDataSourceResult(request);
                return Json(result);
        }

		public ActionResult PrtPrjsList([DataSourceRequest] DataSourceRequest request, Guid id) {
				var projects = db.PrtPrjsViews.Where(m => m.ProtocolId == id).ToList();
				DataSourceResult result = projects.ToDataSourceResult(request);
				return Json(result);
		}

		public ActionResult PrjPrtsList([DataSourceRequest] DataSourceRequest request, Guid id) {
			var projects = db.PrtPrjsView2.Where(m => m.ProjectId == id).AsQueryable();
			DataSourceResult result = projects.ToDataSourceResult(request);
			return Json(result);
		}

		#endregion

        [HttpPost]
        public ActionResult SetPayed(Guid id) {
            RegisterProject rp = db.RegisterProjects.FirstOrDefault(x => x.Id == id);
            if (rp != null) {
                rp.IsPayed = true;
                db.SaveChanges();
            }
            return Json(rp, JsonRequestBehavior.AllowGet);
        }


	    public ActionResult ListRequest(int type, Guid id) {
		    ViewBag.Type = type;
			return PartialView("ListRequest2", id);
		}

        public ActionResult ListRequestOrder(int type, Guid id){
            ViewBag.Type = type;
            return PartialView(id);
        }

        public ActionResult ReadRegisterOrderer([DataSourceRequest] DataSourceRequest request)
        {
            var registers = db.RegisterOrdererViews.AsQueryable();
            DataSourceResult result = registers.ToDataSourceResult(request);
            return Json(result);
        }

		public ActionResult ReadRegisterOrderer2([DataSourceRequest] DataSourceRequest request) {
			var registers = db.RegisterOrderer2Views.AsNoTracking().AsQueryable();
			DataSourceResult result = registers.ToDataSourceResult(request);
			return Json(result);
		}

		public ActionResult ReadNewRegisterOrders([DataSourceRequest] DataSourceRequest request, Guid requestOrderId) {
		    var list = db.RequestLists.Where(x => x.RequestOrderId == requestOrderId);
            var listReestrId = list.Select(x=> x.ReestrId);
            var listDfId = list.Select(x => x.RegisterDfId);
            var registers = db.RegisterOrderer2Views.AsNoTracking().AsQueryable();
		    registers = registers.Where(x => !(listReestrId.Contains(x.regId) && listDfId.Contains(x.dfId)));
			DataSourceResult result = registers.ToDataSourceResult(request);
			return Json(result);
		}

		[HttpPost]
        public ActionResult AddListRequest(int reestrId, int type )
        {
            var model = db.RegisterOrdererViews.First(m=>m.ReestrId==reestrId);
            var request = new RequestList() {
                Id = Guid.NewGuid(),
                Type = type,
                Concentration = model.Concentration,
                MnnName = model.MnnName,
                RegNumber = model.RegNumber,
                AtxCode = model.AtxCode,
                DrugForm = model.DrugForm,
                TradeName = model.TradeName,
                Manufacturer = model.Manufacturer,
                Characteristic = model.Characteristic,
                Country = model.Country,
                Dosage = model.Dosage,
                Measure = model.Measure,
                ReestrId = model.ReestrId,
                RegDate = model.RegDate,
                RegDateExpire = model.RegDateExpire,
				substance_count = model.substance_count?.ToString() ?? "0",
				unit_count = model.unit_count?.ToString() ?? "0",
				dosage_comment = model.dosage_comment
				

            };
            db.RequestLists.Add(request);
            db.SaveChanges();
            return Json(request);
        }

		[HttpPost]
		public ActionResult AddListRequest2(int reestrId, int type, string regNumber, string volume) {
		    RegisterOrderer2Views model = null;
		    if (string.IsNullOrEmpty(volume)) {
                model = db.RegisterOrderer2Views.First(m => m.reg_number == regNumber && m.volume == null);
            }
		    else {
                model = db.RegisterOrderer2Views.First(m => m.reg_number == regNumber && m.volume == volume);
            }
            
			//var model = db.RegisterOrderer2Views.First(m => m.IntId == reestrId);
			var request = new RequestList() {
				Id = Guid.NewGuid(),
				Type = type,
				Concentration = model.concentration,
				MnnName = model.C_int_name,
				RegNumber = model.reg_number,
				AtxCode = model.C_atc_code,
				DrugForm = model.C_dosage_form_name,
				TradeName = model.name,
				Manufacturer = model.C_producer_name,
				Country = model.C_country_name,
				Dosage = model.dosage_value?.ToString() ?? "0",
				ReestrId = model.regId,
				substance_count = model.substance,
                //unit_count = model.unit_count?.ToString() ?? "0", //unit_count исключен
                dosage_comment = model.dosage_comment,
				volume = model.volume,
                RegisterDfId = model.dfId
			};
			db.RequestLists.Add(request);
			db.SaveChanges();
			return Json(request);
		}

        [HttpPost]
        public ActionResult AddListRequestOrder(int reestrId, int type, Guid id, int regId, int? dfId) {
            var hasRow = db.RequestLists.Any(x =>
                x.RequestOrderId == id
                && x.ReestrId == regId
                && x.RegisterDfId == dfId);
            if(hasRow)
                return Content(bool.FalseString);

            RegisterOrderer2Views model = null;
            if (dfId.HasValue) {
                model = db.RegisterOrderer2Views.First(m => m.IntId == reestrId && m.dfId == dfId);
            }
            else {
                model = db.RegisterOrderer2Views.First(m => m.IntId == reestrId);
                
            }

            //var model = db.RegisterOrderer2Views.First(m => m.IntId == reestrId);
            var request = new RequestList{
                Id = Guid.NewGuid(),
                RequestOrderId = id,
                Type = type,
                Concentration = model.concentration,
                MnnName = model.C_int_name,
                RegNumber = model.reg_number,
                AtxCode = model.C_atc_code,
                DrugForm = model.C_dosage_form_name,
                TradeName = model.name,
                Manufacturer = model.C_producer_name,
                Country = model.C_country_name,
                Dosage = model.dosage_value?.ToString() ?? "0",
                ReestrId = model.regId,
                substance_count = model.substance,
                //unit_count = model.unit_count?.ToString() ?? "0", //unit_count исключен
                dosage_comment = model.dosage_comment,
                volume = model.volume,
                RegisterDfId = model.dfId,
                box_count = model.box_count
            };
            db.RequestLists.Add(request);
            db.SaveChanges();
            return Content(bool.TrueString);
        }

        

        public ActionResult ListRequestRead([DataSourceRequest] DataSourceRequest request,int type)
        {
            var data = db.RequestListViews.Where(x => x.Type == type).ToList().OrderBy(o => o.MnnName);
            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult RequestOrderListRead([DataSourceRequest] DataSourceRequest request, int type, int year, Guid? orderId){
            if (orderId.HasValue && orderId != new Guid()) {
                var data =
                    db.RequestOrderListViews.Where(x => x.OrderId == orderId.Value).AsNoTracking().ToList().OrderBy(o => o.MnnName);
                return Json(data.ToDataSourceResult(request));
            }
            else {
                var data = db.RequestOrderListViews.Where(x => x.OrderType == type && x.OrderYear == year).AsNoTracking().ToList().OrderBy(o => o.MnnName);
                return Json(data.ToDataSourceResult(request));
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RequestOrderListUpdate([DataSourceRequest] DataSourceRequest request, RequestOrderListView item){
            if (item != null && ModelState.IsValid){
                RequestList newItem = db.RequestLists.First(o => o.Id == item.Id);
   
                newItem.Number = item.Number;
                newItem.Applicant = item.Applicant;
                newItem.Mark = item.Mark;
                newItem.LimitPriceMnn = item.LimitPriceMnn;
                newItem.LimitPriceTn = item.LimitPriceTn;
                db.SaveChanges();
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RequestOrderListDestroy([DataSourceRequest] DataSourceRequest request, RequestOrderListView item){
            if (item != null){
                RequestList newItem = db.RequestLists.FirstOrDefault(o => o.Id == item.Id);
                if (newItem != null) {
                    db.RequestLists.Remove(newItem);
                    db.SaveChanges();
                }
            }

            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }
        

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ListRequestUpdate([DataSourceRequest] DataSourceRequest request, RequestListView item)
        {
            if (item != null && ModelState.IsValid)
            {
                RequestList newItem = db.RequestLists.First(o => o.Id == item.Id);
                newItem.LimitPriceMnn = item.LimitPriceMnn;
                newItem.LimitPriceTn = item.LimitPriceTn;
                db.SaveChanges();
            }

            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ListRequestDestroy([DataSourceRequest] DataSourceRequest request, RequestListView item)
        {
            if (item != null)
            {
                RequestList newItem = db.RequestLists.First(o => o.Id == item.Id);
                db.RequestLists.Remove(newItem);
                db.SaveChanges();
            }

            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }

		public ActionResult PpHistoryDetails(Guid? id){
			if (id == null){
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
            PriceProjectArchiveView project = db.PriceProjectArchiveViews.FirstOrDefault(x=>x.Id==id);
			if (project == null){
				return HttpNotFound();
			}
		    ViewBag.UiId = Guid.NewGuid().ToString();

            return PartialView(project);
		}

    }
}