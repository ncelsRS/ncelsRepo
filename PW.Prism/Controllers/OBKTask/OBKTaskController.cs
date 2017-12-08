using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Repository.OBK;
using PW.Prism.ViewModels.OBK;

namespace PW.Prism.Controllers.OBKTask
{
    /// <summary>
    /// Создание задания
    /// </summary>
    [Authorize]
    public class OBKTaskController : Controller
    {
        private readonly ncelsEntities _db = UserHelper.GetCn();
        public ActionResult Task(Guid id)
        {
            var model = GetAssessmentStage(id);
            var products = _db.OBK_RS_Products.Where(e => e.ContractId == model.OBK_AssessmentDeclaration.ContractId).ToList();
            TaskViewModel task = new TaskViewModel();
            List<ProductViewModel> productViewModel = new List<ProductViewModel>();

            foreach (var product in products)
            {
                foreach (var productSeries in product.OBK_Procunts_Series)
                {
                    var productView = new ProductViewModel
                    {
                        Id = productSeries.Id,
                        ProductNameRu = product.NameRu,
                        ProductSeries = productSeries.Series
                    };
                    productViewModel.Add(productView);
                }
            }
            task.ProductViewModels = productViewModel;
            return PartialView("Index", task);
        }

        protected virtual OBK_AssessmentStage GetAssessmentStage(Guid? id)
        {
            return new AssessmentStageRepository().GetById(id);
        }


    }
}