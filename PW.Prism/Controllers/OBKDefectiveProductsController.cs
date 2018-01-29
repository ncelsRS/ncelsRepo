using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Repository.OBK;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace PW.Prism.Controllers
{
    [Authorize]
    public class OBKDefectiveProductsController : Controller
    {
        private OBKDefectiveProductsRepository repository = new OBKDefectiveProductsRepository();

        public ActionResult Index()
        {
            Guid guid = Guid.NewGuid();
            ViewBag.DictionaryList = DictionaryHelper.GetList();
            return PartialView(guid);
        }

        public ActionResult ListNotSended([DataSourceRequest] DataSourceRequest request)
        {
            return Json(repository.ListNotSended().ToDataSourceResult(request));
        }

        public ActionResult ListSended([DataSourceRequest] DataSourceRequest request)
        {
            return Json(repository.ListSended().ToDataSourceResult(request));
        }

        //TODO Отправка письма ЭДО
        public ActionResult SendEDO(List<Guid> ides)
        {
            if (ides == null)
            {
                return Json(new { success = false });
            }

            var result = repository.SendEDO(ides);

            return Json(new { success = result });
        }

        public ActionResult SaveLetterDetails(string number, DateTime date, List<Guid> ides)
        {
            repository.SaveLetterDetails(number, date, ides);
            return Json(new { success = true });
        }

        public FileStreamResult ShowPdfLetter(string ides)
        {
            List<Guid> list = new List<Guid>();
            if (ides != null && !ides.Equals(""))
            {
                var arr = ides.Split(',');
                foreach (var temp in arr)
                {
                    list.Add(Guid.Parse(temp));
                }
            }

            var letterList = repository.LetterList(list);

            StiReport report = new StiReport();
            report.Load(Server.MapPath("../Reports/Mrts/OBK/OBKDefectiveProducts.mrt"));


            report.RegBusinessObject("ZBKList", letterList.ToList());
            report.RegBusinessObject("Organ", new { name = "РГП на ПХВ «Национальный центр экспертизы лекарственных средств, изделий медицинского назначения и медицинской техники» МЗ РК" });
            report.Compile();
            report.Render(false);
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Pdf, stream);
            stream.Position = 0;
            return new FileStreamResult(stream, "application/pdf");
        }

    }
}

