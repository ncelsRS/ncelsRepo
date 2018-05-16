using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Repository.Expertise;
using PW.Ncels.Database.Repository.Lims;
using PW.Prism.Controllers.Base;
using PW.Prism.ViewModels.Lims;

namespace PW.Prism.Controllers.Lims
{
    public class PersonalTmcController : LimsBaseController
    {
        // Ректив персонала
        public PartialViewResult PersonalTmcList()
        {
            PersonalTmcViewModel model = new PersonalTmcViewModel()
            {
                Id =  Guid.NewGuid()
            };
            return PartialView(model);
        }


        public JsonResult ReadData([DataSourceRequest] DataSourceRequest request)
        {
            TmcRepository repository = new TmcRepository();
            var qr = repository.LtaGetAsQuarable();
            qr = base.FilterByCurrentUser(qr, repository.GetContext());
            qr = qr.OrderBy(m => m.Name);
            var data = qr;

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult ReadTmcUsedOffList([DataSourceRequest] DataSourceRequest request, Guid tmcId)
        {
            TmcOffRepository repository = new TmcOffRepository();
            var currentEmployeeId = UserHelper.GetCurrentEmployee().Id;
            var data = repository.TuoGetAsQuarable(tuo => tuo.TmcId == tmcId && tuo.CreatedEmployeeId == currentEmployeeId && tuo.DeleteDate == null);
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateTmcUseOff([DataSourceRequest] DataSourceRequest request, TmcUseOffView model)
        {
            string expertiseNumber = null;
            TmcOffRepository repository = new TmcOffRepository();

            if (model.RefExtertiseStatement != null)
            {
                DrugDeclarationRepository ddRepo = new DrugDeclarationRepository();
                var exp = ddRepo.GetById(model.RefExtertiseStatement.Value);

                if (exp != null)
                    expertiseNumber = exp.Number;
            }

            TmcOff tmc = new TmcOff()
            {
                Id = Guid.NewGuid(),
                StateType = model.StateType,
                CreatedDate = DateTime.Now,
                CreatedEmployeeId = UserHelper.GetCurrentEmployee().Id,
                Count = model.Count,
                Note = model.Note,
                TmcOutId = model.TmcOutId,
                TmcId = model.TmcId,
                ExpertiseStatementId = model.RefExtertiseStatement,
                ExpertiseStatementNumber = expertiseNumber
            };

            repository.Insert(tmc);
            repository.Save();

            return Json(new[] { tmc }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateTmcUseOff([DataSourceRequest] DataSourceRequest request, TmcUseOffView model)
        {
            string expertiseNumber = null;
            TmcOffRepository repository = new TmcOffRepository();

            if (model.RefExtertiseStatement != null)
            {
                DrugDeclarationRepository ddRepo = new DrugDeclarationRepository();
                var exp = ddRepo.GetById(model.RefExtertiseStatement.Value);

                if (exp != null)
                    expertiseNumber = exp.Number;
            }
            
            var tmcOff = repository.GetAsQuarable(toff => toff.Id == model.Id).FirstOrDefault();
            if (tmcOff != null)
            {
                tmcOff.Count = model.Count;
                tmcOff.Note = model.Note;
                tmcOff.ExpertiseStatementNumber = expertiseNumber;
                tmcOff.ExpertiseStatementId = model.RefExtertiseStatement;
            }
            
            repository.Update(tmcOff);
            repository.Save();

            return Json(new[] { tmcOff }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyTmcUseOff([DataSourceRequest] DataSourceRequest request, TmcUseOffView model)
        {
            if (model != null)
            {
                TmcOffRepository repository = new TmcOffRepository();
                TmcOff d = repository.GetById(model.Id);
                repository.Delete(d);
                repository.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

    }
}