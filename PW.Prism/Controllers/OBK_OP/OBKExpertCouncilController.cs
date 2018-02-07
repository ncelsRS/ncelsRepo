using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Repository.OBK;
using PW.Prism.ViewModels.OBK.ExpertCouncil;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PW.Prism.Controllers.OBK_OP
{
    public class OBKExpertCouncilController : Controller
    {
        private ncelsEntities repo = new ncelsEntities();

        // GET: OBKExpertCouncil
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult DeclarationList()
        {
            return PartialView();
        }

        public ActionResult ListRegister([DataSourceRequest] DataSourceRequest request, DeclarationRegistryFilter customFilter = null)
        {
            var list = repo.OBK_AssessmentDeclaration__OBK_ExpertCouncil
                .Where(x => x.OBK_AssessmentDeclaration.OBK_AssessmentStage.FirstOrDefault(s => s.OBK_Ref_Stage.Code == "15").OBK_Ref_StageStatus.Code == "OPReportOnEC")
                .Select(x => new ECDeclarationListMV
                {
                    Id = x.DeclarationId,
                    ExpertCouncilId = x.ExpertCouncilId,
                    Number = x.OBK_AssessmentDeclaration.Number,
                    FirstSendDate = x.OBK_AssessmentDeclaration.FirstSendDate,
                    DeclarantName = x.OBK_AssessmentDeclaration.OBK_Contract.OBK_Declarant.NameRu,
                    //CountryName = repo.OBK_Dictionaries.FirstOrDefault(d => d.Id == x.OBK_AssessmentDeclaration).Name,
                    ContractNumber = x.OBK_AssessmentDeclaration.OBK_Contract.Number,
                    ContractDate = x.OBK_AssessmentDeclaration.OBK_Contract.StartDate,
                    Result = x.Result,
                    Comment = x.Comment
                });
                //.ToList()
                //.Select(x => new
                //{
                //    x.Id,
                //    x.Number,
                //    x.DeclarantName,
                //    x.Country,
                //    x.ContractNumber,
                //    x.Result,
                //    x.Comment,
                //    FirstSendDate = x.FirstSendDate?.ToString("dd.MM.yyyy"),
                //    ContractDate = x.ContractDate?.ToString("dd.MM.yyyy")
                //});
            return Json(list.ToDataSourceResult(request));
        }

        [HttpGet]
        public ActionResult GetLeftPane()
        {
            var model = repo.OBK_ExpertCouncil
                .Where(c => c.Date.Year == DateTime.Now.Year)
                .Take(12).ToList();
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult GetECType()
        {
            try
            {
                var res = new[] { "Экспертный совет по ЛС", "Экспертный совет по ИМН" };
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (ArgumentException ex)
            {
                Response.StatusCode = 400;
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SaveExpertCouncil(ExpertCouncilMV council)
        {
            var entity = new OBK_ExpertCouncil
            {
                Name = council.Name,
                Date = council.Date
            };
            repo.OBK_ExpertCouncil.Add(entity);
            repo.SaveChanges();
            return Json(new { isSuccess = true });
        }

        public ActionResult ListFutureCouncils()
        {
            try
            {
                var date = DateTime.Now;
                date.AddDays(1);
                var res = repo.OBK_ExpertCouncil
                    .Where(x => x.Date > date)
                    .ToList()
                    .Select(x => new
                    {
                        Value = x.Id,
                        Text = $"{x.Date.ToString("dd.MM.yyyy")} {x.Name}"
                    })
                    .AsEnumerable();
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (ArgumentException ex)
            {
                Response.StatusCode = 400;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}