using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Prism.Controllers.Base;

namespace PW.Prism.Controllers
{
    public class MyTmcController : BaseController
    {
        private ncelsEntities db = new ncelsEntities();

        // GET: MyTmc
        public ActionResult RequestList()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }

        public ActionResult StorageList()
        {
            Guid guid = Guid.NewGuid();
            ViewBag.TmcTypes =
                db.Dictionaries.Where(o => o.Type == Dictionary.TmcType.DicCode && o.ExpireDate == null)
                    .ToList().OrderBy(o => o.Name)
                    .Select(o => new Item() { Id = o.Id.ToString(), Name = o.Name }).ToList();

            return PartialView(guid);
        }

        public ActionResult AddOutTmc(Guid id, int count, Guid? measureTypeConvertDicId = null) {
            TmcOutCount toc = new TmcOutCount()
            {
                Id = id,
                MeasureTypeConvertDicId = measureTypeConvertDicId,
                Count = count
            };
            return PartialView(toc);
        }
        public ActionResult ListOut() {
            Guid empId = UserHelper.GetCurrentEmployee().Id;
   
            return Json(db.TmcOutViews.Where(o=>o.StateType == 0 && o.CreatedEmployeeId == empId).Select(o => new { o.Id, Name = o.Note }).OrderBy(o => o.Name), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetListTmcs(string text = null)
        {
            var qr = db.TmcViews.AsQueryable();
            if (!string.IsNullOrEmpty(text))
                qr = qr.Where(t => t.Name.Contains(text));

            var tmcList = qr.Select(t => new {t.Id, t.Name, t.MeasureTypeConvertDicId, t.CountActual})
                .OrderBy(t => t.Name);
            return Json(tmcList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ConfirmAddOutTmc(TmcOutCount outCount, Guid tmcId) {

            outCount.Id = Guid.NewGuid();
            db.TmcOutCounts.Add(outCount);
            //var item = db.TmcOutCounts.First(o => o.Id == tmcId);
            //item.StateType = 1;
             db.SaveChanges();
            return Json(tmcId, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MyTmcList()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }
        
        #region Request
        public ActionResult ReadRequest([DataSourceRequest] DataSourceRequest request)
        {
            var qr = db.TmcOutViews.AsQueryable();
            qr = base.FilterByCurrentUser(qr, db);
            qr = qr.OrderByDescending(m => m.CreatedDate);
            var data = qr.Select(o => new TmcOutViewModel()
            {
                Id = o.Id,
                CreatedDate = o.CreatedDate,
                CreatedEmployeeId = o.CreatedEmployeeId,
                Note = o.Note,
                OwnerEmployeeId = o.OwnerEmployeeId,
                OwnerEmployeeValue = o.OwnerEmployeeValue,
                StateType = o.StateType,
                Rack = o.Rack,
                Safe = o.Safe,
                OutTypeDicId = o.OutTypeDicId,
                OutTypeDicValue = o.OutTypeDicValue,
                StateTypeValue = o.StateTypeValue,
                StorageDicId = o.StorageDicId,
                StorageDicValue = o.StorageDicValue,
                TmcOutId = o.Id
            }).ToList();


            foreach (var d in data)
            {
                var tmcComment = db.TmcApplicationComments.Where(c => c.ApplicationId == d.Id)
                        .OrderByDescending(c => c.CreateDate)
                        .FirstOrDefault();
                if (tmcComment != null)
                    d.Comment = tmcComment.Comment;
            }

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateRequest([DataSourceRequest] DataSourceRequest request, TmcOutViewModel dictionary)
        {
            TmcOut tmcOut = new TmcOut()
            {
                Id = Guid.NewGuid(),
                StateType = dictionary.StateType,
                Note = dictionary.Note,
                OutTypeDicId = dictionary.OutTypeDicId,
                CreatedDate = DateTime.Now,
                CreatedEmployeeId = UserHelper.GetCurrentEmployee().Id,
                StorageDicId = dictionary.StorageDicId,
                OwnerEmployeeId = dictionary.OwnerEmployeeId,
                Safe = dictionary.Safe,
                Rack = dictionary.Rack,
            };

            db.TmcOuts.Add(tmcOut);
            db.SaveChanges();
            dictionary.Id = tmcOut.Id;

            var newDictionary = db.TmcOutViews.First(o => o.Id == tmcOut.Id);
            dictionary.OwnerEmployeeValue = newDictionary.OwnerEmployeeValue;
            dictionary.StateTypeValue = newDictionary.StateTypeValue;
            dictionary.OutTypeDicValue = newDictionary.OutTypeDicValue;
            dictionary.TmcOutId = tmcOut.Id;
            

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateRequest([DataSourceRequest] DataSourceRequest request, TmcOutViewModel dictionary)
        {
            TmcOut d = db.TmcOuts.First(o => o.Id == dictionary.Id);
            d.StateType = dictionary.StateType;
            d.Note = dictionary.Note;
            d.OutTypeDicId = dictionary.OutTypeDicId;
            d.StorageDicId = dictionary.StorageDicId;
            d.OwnerEmployeeId = dictionary.OwnerEmployeeId;
            d.Rack = dictionary.Rack;
            d.Safe = dictionary.Safe;
            db.SaveChanges();

            var newDictionary = db.TmcOutViews.First(o => o.Id == d.Id);
            dictionary.OwnerEmployeeValue = newDictionary.OwnerEmployeeValue;
            dictionary.StateTypeValue = newDictionary.StateTypeValue;
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyRequest([DataSourceRequest] DataSourceRequest request, TmcOutView dictionary)
        {
            if (dictionary != null)
            {
                TmcOut d = db.TmcOuts.First(o => o.Id == dictionary.Id);
                db.TmcOuts.Remove(d);
                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region RequestChildrens
        public ActionResult ReadRequestChildrens([DataSourceRequest] DataSourceRequest request, Guid tmcOutId)
        {
            var data = db.TmcOutCountViews.OrderByDescending(m => m.Name).Where(m => m.TmcOutId == tmcOutId).Select(o => new
            {
                o.Id,
                o.Name,
                o.Count,
                o.Note,
                o.CountFact,
                o.OwnerEmployeeValue,
                o.StateType,
                o.Rack,
                o.Safe,
                o.MeasureTypeConvertDicId,
                o.MeasureTypeConvertDicValue,
                o.StateTypeValue,
                o.TmcId,
                o.TmcOutId,
                o.StorageDicValue,
                o.ApplicationStateType,
                TmcCount = o.CountActual
            });
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateRequestChildrens([DataSourceRequest] DataSourceRequest request, TmcOutCountViewModel dictionary)
        {
            TmcOutCount tmc = new TmcOutCount()
            {
                Id = Guid.NewGuid(),
                StateType = dictionary.StateType,
                Note = dictionary.Note,
                CountFact = dictionary.CountFact,
                Count = dictionary.Count,
                TmcOutId = Guid.Parse(dictionary.TmcOutIdString)
            };

            if (dictionary.TmcId != null)
                tmc.TmcId = dictionary.TmcId.Value;

            db.TmcOutCounts.Add(tmc);
            db.SaveChanges();
            dictionary.Id = tmc.Id;


            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateRequestChildrens([DataSourceRequest] DataSourceRequest request, TmcOutCountViewModel dictionary)
        {

            TmcOutCount d = db.TmcOutCounts.First(o => o.Id == dictionary.Id);
            d.StateType = dictionary.StateType;
            d.Note = dictionary.Note;
            d.Count = dictionary.Count;
            d.CountFact = dictionary.CountFact;

            if (dictionary.TmcId != null)
                d.TmcId = dictionary.TmcId.Value;
            
            db.SaveChanges();


            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyRequestChildrens([DataSourceRequest] DataSourceRequest request, TmcOutCountViewModel dictionary)
        {
            if (dictionary != null)
            {
                TmcOutCount d = db.TmcOutCounts.First(o => o.Id == dictionary.Id);
                db.TmcOutCounts.Remove(d);
                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        public ActionResult ReadMyTmcList([DataSourceRequest] DataSourceRequest request)
        {
            var usr = UserHelper.GetCurrentEmployee();
            var data = db.TmcOutCountViews.Where(o=>o.CreatedEmployeeId == usr.Id && o.CountActual != 0 && o.CountFact != 0)
                .OrderByDescending(m => m.Name).Select(o => new
            {
                o.Id,
                o.Name,
                o.Count,
                o.CountActual,
                o.CountFact,
                o.OwnerEmployeeValue,
                o.StateType,
                o.Rack,
                o.Safe,
                o.MeasureTypeConvertDicId,
                o.MeasureTypeConvertDicValue,
                o.StateTypeValue,
                o.CountConvert,
                o.Manufacturer,
                o.CreatedDate,
                o.CreatedEmployeeId,
                o.ExpiryDate,
                o.ManufactureDate,
                o.MeasureTypeDicId,
                o.MeasureTypeDicValue,
                o.OwnerEmployeeId,
                o.Number,
                o.TmcTypeDicId,
                o.StorageDicValue,
                o.TmcTypeDicValue,
                o.StorageDicId,
                o.Serial,
                o.PackageDicId,
                o.PackageDicValue,
                o.Code,
                o.TmcCount,
                o.TmcCountFact,
              //  TmcId = o.Id,
                TmcOutId = o.Id
            });
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #region TmcStorage 
        public ActionResult ReadTmcList([DataSourceRequest] DataSourceRequest request)
        {
            var data = db.TmcViews.Where(o => o.StateType == 1).OrderByDescending(m => m.Name).Select(o => new
            {
                o.Id,
                o.Name,
                o.Count,
                o.CountActual,
                o.CountFact,
                o.OwnerEmployeeValue,
                o.StateType,
                o.Rack,
                o.Safe,
                o.MeasureTypeConvertDicId,
                o.MeasureTypeConvertDicValue,
                o.StateTypeValue,
                o.CountConvert,
                o.Manufacturer,
                o.CreatedDate,
                o.CreatedEmployeeId,
                o.ExpiryDate,
                o.ManufactureDate,
                o.MeasureTypeDicId,
                o.MeasureTypeDicValue,
                o.OwnerEmployeeId,
                o.Number,
                o.TmcTypeDicId,
                o.StorageDicValue,
                o.TmcTypeDicValue,
                o.StorageDicId,
                o.Serial,
                o.PackageDicId,
                o.PackageDicValue,
                o.Code,
                TmcId = o.Id,
                TmcOutId = o.Id
            });
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region TmcListChildren
        public ActionResult ReadTmcListChildrens([DataSourceRequest] DataSourceRequest request, Guid tmcOutId)
        {
            var data = db.TmcOffViews.Join(db.TmcOutCountViews, tov => tov.TmcOutId, tocv => tocv.Id, (tov, tocv) => new { TmcOffView = tov, TmcOutCountView = tocv })
                .OrderByDescending(m => m.TmcOffView.CreatedDate).Where(m => m.TmcOffView.TmcOutId == tmcOutId && m.TmcOutCountView.Id == tmcOutId).Select(o => new TmcOffViewModel()
            {
                Id = o.TmcOffView.Id,
                    CreatedDate = o.TmcOffView.CreatedDate,
                    Count = o.TmcOffView.Count,
                    Note = o.TmcOffView.Note,
                    CreatedEmployeeId = o.TmcOffView.CreatedEmployeeId,
                    CreatedEmployeeValue = o.TmcOffView.CreatedEmployeeValue,
                    StateType = o.TmcOffView.StateType,
                    StateTypeValue = o.TmcOffView.StateTypeValue,
                    TmcOutId = o.TmcOffView.TmcOutId,
                    TmcName = o.TmcOutCountView.Name,
                    TmcCount = o.TmcOutCountView.CountActual,
                    ExtertiseStatementNumber = o.TmcOffView.ExpertiseStatementNumber
                });
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateTmcListChildrens([DataSourceRequest] DataSourceRequest request, TmcOffViewModel dictionary)
        {
            string expertiseNumber = null;
            if (dictionary.RefExtertiseStatement != null)
            {
                expertiseNumber = db.Documents.Where(d => d.Id == dictionary.RefExtertiseStatement.Value)
                    .Select(d => d.Number)
                    .FirstOrDefault();
            }
            
            TmcOff tmc = new TmcOff()
            {
                Id = Guid.NewGuid(),
                StateType = dictionary.StateType,
                CreatedDate = DateTime.Now,
                CreatedEmployeeId = UserHelper.GetCurrentEmployee().Id,
                Count = dictionary.Count,
                Note = dictionary.Note,
                TmcOutId = Guid.Parse(dictionary.TmcOutIdString),
                ExpertiseStatementId = dictionary.RefExtertiseStatement,
                ExpertiseStatementNumber = expertiseNumber
            };

            db.TmcOffs.Add(tmc);
            db.SaveChanges();

            dictionary.Id = tmc.Id;
            var item = db.TmcOffViews.First(o => o.Id == tmc.Id);
            dictionary.CreatedEmployeeValue = item.CreatedEmployeeValue;
            dictionary.StateTypeValue = "На списании";

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateTmcListChildrens([DataSourceRequest] DataSourceRequest request, TmcOffViewModel dictionary)
        {
            EXP_DrugDeclaration drugDeclaration = null;
            if (dictionary.RefExtertiseStatement != null)
            {
                drugDeclaration = db.EXP_DrugDeclaration
                    .FirstOrDefault(d => d.Id == dictionary.RefExtertiseStatement.Value);
            }

            TmcOff tmcOff = db.TmcOffs.First(o => o.Id == dictionary.Id);
            tmcOff.StateType = dictionary.StateType;
            tmcOff.CreatedDate = dictionary.CreatedDate;
            tmcOff.Count = dictionary.Count;
            tmcOff.Note = dictionary.Note;
            tmcOff.StateType = dictionary.StateType;
            if (drugDeclaration != null)
            {
                tmcOff.ExpertiseStatementId = dictionary.RefExtertiseStatement;
                tmcOff.ExpertiseStatementNumber = drugDeclaration.Number;
                tmcOff.ExpertiseStatementTypeStr = drugDeclaration.EXP_DIC_Type.NameRu;
            }
            
            db.SaveChanges();

            var item = db.TmcOffViews.First(o => o.Id == tmcOff.Id);
            dictionary.CreatedEmployeeValue = item.CreatedEmployeeValue;
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyTmcListChildrens([DataSourceRequest] DataSourceRequest request, TmcOffView dictionary)
        {
            if (dictionary != null)
            {
                TmcOff d = db.TmcOffs.First(o => o.Id == dictionary.Id);
                db.TmcOffs.Remove(d);
                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        #endregion TmcListChildrens
    }
}