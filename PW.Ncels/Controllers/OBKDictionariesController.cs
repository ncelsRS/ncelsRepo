using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PW.Ncels.Controllers
{
    public class OBKDictionariesController : ACommonController
    {
        private ncelsEntities db = UserHelper.GetCn();

        [Authorize()]
        [HttpGet]
        public ActionResult GetObkContractTypes()
        {
            var contractTypes = db.OBK_Ref_Type.Where(x => x.ViewOption == CodeConstManager.OBK_VIEW_OPTION_SHOW_ON_CREATE).OrderBy(x => x.Id).Select(o => new { o.Id, Name = o.NameRu, o.Code, o.NameKz });
            return Json(contractTypes.ToList(), JsonRequestBehavior.AllowGet);
        }

        [Authorize()]
        [HttpGet]
        public ActionResult GetObkOrganizations()
        {
            //var obkOrganizations = db.OBK_Organization.Select(o => new { o.Id, Name = o.NameRu, o.NameKz });
            //return Json(obkOrganizations, JsonRequestBehavior.AllowGet);

            var obkDeclarants = db.OBK_Declarant.Where(o => o.IsConfirmed == true).Select(o => new { o.Id, Name = o.NameRu, o.NameKz }).ToList();
            var noData = new { Id = Guid.Empty, Name = "Нет данных", NameKz = "Нет данных" };
            var list = new[] { noData }.ToList().Concat(obkDeclarants);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [Authorize()]
        [HttpGet]
        public ActionResult GetOBKContractDocumentTypeDictionary()
        {
            var values = db.OBK_Ref_ContractDocumentType.Select(x => new { Id = x.Id, Name = x.NameRu, NameKz = x.NameKz, x.NameGenitiveRu, x.NameGenitiveKz });
            return Json(values, JsonRequestBehavior.AllowGet);
        }

        [Authorize()]
        [HttpGet]
        public ActionResult GetMeasureDictionary()
        {
            var srMeasures = db.sr_measures.Where(x => x.block_sign == true).Select(x => new { Id = x.id, Name = x.short_name, NameKz = x.short_name_kz });
            return Json(srMeasures, JsonRequestBehavior.AllowGet);
        }

        [Authorize()]
        [HttpGet]
        //public ActionResult GetServiceNames(int type)
        //{
        //    var names = db.OBK_Ref_PriceList.Where(x => x.TypeId == type).Select(x => new { x.Id, Name = x.NameRu });
        //    return Json(names, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetServiceNames(int type, int? productType, int? degreeRiskId)
        {
            Guid productTypeGuid = Guid.Empty;

            switch (productType)
            {
                case 1:
                    productTypeGuid = new Guid("9106d5e8-35dc-4178-8882-b30166de4c81");
                    break;
                case 2:
                    productTypeGuid = new Guid("9106d5e8-35dc-4178-8882-b30166de4c82");
                    break;
            }

            Guid degreeRiskGuid = Guid.Empty;

            switch (degreeRiskId)
            {
                // Класс 1
                case 1:
                    degreeRiskGuid = new Guid("c02f40cc-757c-42ba-a400-3f7937cf8600");
                    break;
                // Класс 2А
                case 2:
                    degreeRiskGuid = new Guid("c02f40cc-757c-42ba-a400-3f7937cf8601");
                    break;
                // Класс 2Б
                case 3:
                    degreeRiskGuid = new Guid("c02f40cc-757c-42ba-a400-3f7937cf8602");
                    break;
                // Класс 3
                case 4:
                    degreeRiskGuid = new Guid("c02f40cc-757c-42ba-a400-3f7937cf8603");
                    break;
            }

            var names = db.OBK_Ref_PriceList.Where(x =>
            x.TypeId == type &&
            (x.ServiceTypeId == productTypeGuid || productTypeGuid == Guid.Empty) &&
            (x.DegreeRiskId == degreeRiskGuid || degreeRiskGuid == Guid.Empty || x.DegreeRiskId == null)
            ).Select(x => new { x.Id, Name = x.NameRu });
            return Json(names, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetServiceNamesServiceTypeDocument(int type)
        {
            // Документ/Экспертиза
            Guid serviceType = new Guid("9106d5e8-35dc-4178-8882-b30166de4c80");

            var names = db.OBK_Ref_PriceList.Where(x =>
            x.TypeId == type &&
            (x.ServiceTypeId == serviceType)
            ).Select(x => new { x.Id, Name = x.NameRu });
            return Json(names, JsonRequestBehavior.AllowGet);
        }

        public double? GetTax()
        {
            var tax = db.OBK_Ref_ValueAddedTax.Where(x => x.Year == DateTime.Now.Year).FirstOrDefault();
            if (tax != null)
            {
                return tax.Value;
            }
            return null;
        }
    }
}