using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Repository.DictionaryRep;
using PW.Ncels.Database.Repository.DirectionToPay;
using PW.Ncels.Database.Repository.Expertise;

namespace PW.Prism.Controllers.Expertise
{
    public class MaterialDirectionController : Controller
    {
        // GET: MaterialDirection
        public ActionResult Index()
        {
            var guid = Guid.NewGuid();
            return PartialView(guid);
        }

        /// <summary>
        /// Получить список направлений
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult ReadDirectionList([DataSourceRequest] DataSourceRequest request)
        {
            MaterialDirectionRepository repository = new MaterialDirectionRepository(false);
            // var currentUserId = UserHelper.GetCurrentEmployee().Id;

            var data =
                repository.GetMaterialDirectionsViewAsQuarable(d => d.DeleteDate == null);
            
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SendDirection(Guid directionId)
        {
            MaterialDirectionRepository repository = new MaterialDirectionRepository(false);
            EXP_MaterialDirections m = repository.GetAsQuarable(d => d.Id == directionId).FirstOrDefault();

            if (m != null)
            {
                DictionaryRepository drep = new DictionaryRepository(false);
                var statusId = drep.GetDictionaryElementIdByTypeAndCode(Dictionary.MaterialDirectionStatusDic.DicCode, Dictionary.MaterialDirectionStatusDic.Sended);
                if (statusId.HasValue)
                    m.StatusId = statusId.Value;

                m.SendDate = DateTime.Now;
                m.SendEmployeeId = UserHelper.GetCurrentEmployee().Id;

                repository.Update(m);
                repository.Save();
            }

            return Json(directionId, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AcceptDirection(Guid directionId)
        {
            MaterialDirectionRepository repository = new MaterialDirectionRepository(false);
            EXP_MaterialDirections m = repository.GetAsQuarable(d => d.Id == directionId).FirstOrDefault();

            if (m != null)
            {
                DictionaryRepository drep = new DictionaryRepository(false);
                var statusId = drep.GetDictionaryElementIdByTypeAndCode(Dictionary.MaterialDirectionStatusDic.DicCode, Dictionary.MaterialDirectionStatusDic.Accepted);
                if (statusId.HasValue)
                    m.StatusId = statusId.Value;

                m.ReceiveDate = DateTime.Now;
                m.ExecutorEmployeeId = UserHelper.GetCurrentEmployee().Id;

                repository.Update(m);
                repository.Save();
            }

            return Json(directionId, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RejectDirection(Guid directionId, string comment)
        {
            MaterialDirectionRepository repository = new MaterialDirectionRepository(false);
            EXP_MaterialDirections m = repository.GetAsQuarable(d => d.Id == directionId).FirstOrDefault();

            if (m != null)
            {
                DictionaryRepository drep = new DictionaryRepository(false);
                var statusId = drep.GetDictionaryElementIdByTypeAndCode(Dictionary.MaterialDirectionStatusDic.DicCode, Dictionary.MaterialDirectionStatusDic.Rejected);
                if (statusId.HasValue)
                    m.StatusId = statusId.Value;

                m.Comment = comment;
                m.RejectDate = DateTime.Now;
                m.ExecutorEmployeeId = UserHelper.GetCurrentEmployee().Id;
                
                repository.Update(m);
                repository.Save();
            }

            return Json(directionId, JsonRequestBehavior.AllowGet);
        }
    }
}