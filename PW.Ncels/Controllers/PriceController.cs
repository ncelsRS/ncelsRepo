using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Enums;
using PW.Ncels.Database.Enums.PriceProject;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Notifications;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Database.Repository.Price;
using PW.Ncels.Models;

namespace PW.Ncels.Controllers
{
    [Authorize()]
    public class PriceController : ACommonController
    {
        private ncelsEntities db = UserHelper.GetCn();

        public ActionResult PriceSave(Price model)
        {
            if (model != null)
            {
                model.IsIncluded = true;
            }
            var project = db.Prices.Any(o => o.Id == model.Id);
            if (project)
            {
                db.Entry(model).State = EntityState.Modified;
            }
            else
            {
                model.Id = Guid.NewGuid();
                db.Prices.Add(model);
            }
            db.SaveChanges();
            return Json("Ок", JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteNotCompletenessPrices(RequestModel model){
            if (model != null){
                var prices = db.Prices.Where(o => 
                    o.PriceProjectId == model.Project.Id 
                    && o.MtPartsId == null
                    && (o.Type == (int) PriceType.ImnCurrentPrice
                        || o.Type == (int)PriceType.ReImnCurrentPrice));
                db.Prices.RemoveRange(prices);

                db.SaveChanges();
            }
            return Json("Ок", JsonRequestBehavior.AllowGet);
        }

        public ActionResult PriceDelete(Price model)
        {
            var project = db.Prices.First(o => o.Id == model.Id);
            db.Prices.Remove(project);
            db.SaveChanges();
            return Json("Ок", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPrice(ModelRequest request, Guid id)
        {
            var items = db.PricesViews.Where(o => o.PriceProjectId == id && o.Type == 1);
            var count = items.Count();

            var data = new
            {
                draw = request.Draw,
                recordsFiltered = count,
                recordsTotal = count,
                Data = items.ToList()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Вместо тысячи GetPriceType
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <param name="type">type</param>
        /// <returns>Json данные о ценнах</returns>
        public ActionResult GetPriceType(ModelRequest request, Guid id, int type)
        {
            var items = db.PricesViews.Where(o => o.PriceProjectId == id && o.Type == type);
            var count = items.Count();

            var data = new
            {
                draw = request.Draw,
                recordsFiltered = count,
                recordsTotal = count,
                Data = items.ToList()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Для таблицы сведения о стране в ЛС 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns>Json</returns>
	    public ActionResult GetPriceCountry(ModelRequest request, Guid id)
        {
            var items = db.PricesViews.Where(o => o.PriceProjectId == id && o.Type == (int)PriceType.LsPrice);
          

            var itemsVm = items.Select(i => new
            {
                Id = i.Id,
                PriceProjectId = i.PriceProjectId,
                CountryId = i.CountryId,
                Type = i.Type,
                CountryName = i.CountryName,
                ManufacturerPrice = i.ManufacturerPrice,
                ManufacturerPriceNote = i.ManufacturerPriceNote,
                ManufacturerPriceCurrencyDicId = i.ManufacturerPriceCurrencyDicId,
                ManufacturerPriceWithLink = i.ManufacturerPrice.ToString() + " " + i.ManufacturerPriceCurrencyName + " " + i.ManufacturerPriceNote,
                LimitPrice = i.LimitPrice,
                LimitPriceCurrencyDicId = i.LimitPriceCurrencyDicId,
                LimitPriceNote = i.LimitPriceNote,
                LimitPriceWithLink = i.LimitPrice.ToString() + " " + i.LimitPriceCurrencyName + " " + i.LimitPriceNote,
                AvgOptPrice = i.AvgOptPrice,
                AvgOptPriceCurrencyDicId = i.AvgOptPriceCurrencyDicId,
                AvgOptPriceNote = i.AvgOptPriceNote,
                AvgOptPriceWithLink = i.AvgOptPrice.ToString() + " " + i.AvgOptPriceCurrencyName + " " + i.AvgOptPriceNote,
                AvgRozPrice = i.AvgRozPrice,
                AvgRozPriceCurrencyDicId = i.AvgRozPriceCurrencyDicId,
                AvgRozPriceNote = i.AvgRozPriceNote,
                AvgRozPriceWithLink = i.AvgRozPrice.ToString() + " " + i.AvgRozPriceCurrencyName + " " + i.AvgOptPriceNote,
                IsIncluded = i.IsIncluded,
                i.IsAvgOptPrice,
                i.IsAvgRozPrice,
                i.IsLimitPrice,
                i.IsManufacturerPrice,
                i.IsUnitPrice
            });
            var countyDics = new ReadOnlyDictionaryRepository().GetDictionaries(CodeConstManager.DIC_COUNTRY_TYPE);
            var list = countyDics.Where(e => CodeConstManager.LIST_COUNTY_CODE_FOR_PRICE.Contains(e.Code));
            var prices = new ArrayList();

            var ids = new List<Guid>();
            foreach (var country in list)
            {
                var exist = itemsVm.FirstOrDefault(e => e.CountryId == country.Id);
                if (exist != null)
                {
                    prices.Add(exist);
                    ids.Add(exist.Id);
                }
                else
                {
                    var price = new 
                    {
                        Id = Guid.NewGuid(),
                        CountryName = country.Name,
                        Type = (int)PriceType.LsPrice,
                        PriceProjectId = id,
                        CountryId = country.Id,
                        ManufacturerPriceNote = "",
                        ManufacturerPriceWithLink ="",
                        LimitPriceNote = "",
                        LimitPriceWithLink = "",
                        AvgOptPriceNote = "",
                        AvgOptPriceWithLink = "",
                        AvgRozPriceNote = "",
                        AvgRozPriceWithLink = "",
                        IsIncluded = false

                        /*   ManufacturerPrice = 0,
                           ManufacturerPriceNote = "",
                           ManufacturerPriceCurrencyDicId = Guid.Empty,
                           ManufacturerPriceWithLink = "",
                           LimitPrice = 0,
                           LimitPriceCurrencyDicId = Guid.Empty,
                           LimitPriceNote = "",
                           LimitPriceWithLink = "",
                           AvgOptPrice =0,
                           AvgOptPriceCurrencyDicId = Guid.Empty,
                           AvgOptPriceNote = "",
                           AvgOptPriceWithLink = "",
                           AvgRozPrice = 0,
                           AvgRozPriceCurrencyDicId = i.AvgRozPriceCurrencyDicId,
                           AvgRozPriceNote = i.AvgRozPriceNote,
                           AvgRozPriceWithLink = i.AvgRozPrice.ToString() + " " + i.AvgRozPriceCurrencyName + " " + i.AvgOptPriceNote*/

                    };
                    prices.Add(price);

                }

            }
            foreach (var item in itemsVm.Where(e=>!ids.Contains(e.Id)))
            {
                prices.Add(item);
            }
            var count = prices.Count;
            var data = new
            {
                draw = request.Draw,
                recordsFiltered = count,
                recordsTotal = count,
                Data = prices
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPriceType3(ModelRequest request, Guid id)
        {
            var items = db.PricesViews.Where(o => o.PriceProjectId == id && o.Type == 3);
            var count = items.Count();

            var data = new
            {
                draw = request.Draw,
                recordsFiltered = count,
                recordsTotal = count,
                Data = items.ToList()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPriceType4(ModelRequest request, Guid id)
        {
            var items = db.PricesViews.Where(o => o.PriceProjectId == id && o.Type == 4).ToList();
            var count = items.Count();

            var sumItem = new PricesView
            {
                PartsName = "ВСЕГО",
                ManufacturerPrice = items.Sum(x => x.ManufacturerPrice),
                CipPrice = items.Sum(x => x.CipPrice),
                RefPrice = items.Sum(x => x.RefPrice),
                UnitPrice = items.Sum(x => x.UnitPrice),
                RefPriceTypeName = "",
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111")
            };

            items.Add(sumItem);

            var data = new
            {
                draw = request.Draw,
                recordsFiltered = count,
                recordsTotal = count,
                Data = items
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPriceType5(ModelRequest request, Guid id)
        {
            var items = db.PricesViews.Where(o => o.PriceProjectId == id && o.Type == 5);
            var count = items.Count();

            var data = new
            {
                draw = request.Draw,
                recordsFiltered = count,
                recordsTotal = count,
                Data = items.ToList()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPriceType7(ModelRequest request, Guid id)
        {
            var items = db.PricesViews.Where(o => o.PriceProjectId == id && o.Type == 7);
            var count = items.Count();

            var data = new
            {
                draw = request.Draw,
                recordsFiltered = count,
                recordsTotal = count,
                Data = items.ToList()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Для таблицы сведения о ценах в других странах в заявление о внесении изменений в лс
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
	    public ActionResult GetPriceCountryRepriceLs(ModelRequest request, Guid id)
        {
            var items = db.PricesViews.Where(o => o.PriceProjectId == id && o.Type == (int)PriceType.ReLsPrice);
            var count = items.Count();

            var itemsVm = items.Select(i => new
            {
                Id = i.Id,
                PriceProjectId = i.PriceProjectId,
                CountryId = i.CountryId,
                Type = i.Type,
                CountryName = i.CountryName,
                ManufacturerPrice = i.ManufacturerPrice,
                ManufacturerPriceNote = i.ManufacturerPriceNote,
                ManufacturerPriceCurrencyDicId = i.ManufacturerPriceCurrencyDicId,
                ManufacturerPriceWithLink = i.ManufacturerPrice.ToString() + " " + i.ManufacturerPriceCurrencyName + " " + i.ManufacturerPriceNote,
                LimitPrice = i.LimitPrice,
                LimitPriceCurrencyDicId = i.LimitPriceCurrencyDicId,
                LimitPriceNote = i.LimitPriceNote,
                LimitPriceWithLink = i.LimitPrice.ToString() + " " + i.LimitPriceCurrencyName + " " + i.LimitPriceNote,
                AvgOptPrice = i.AvgOptPrice,
                AvgOptPriceCurrencyDicId = i.AvgOptPriceCurrencyDicId,
                AvgOptPriceNote = i.AvgOptPriceNote,
                AvgOptPriceWithLink = i.AvgOptPrice.ToString() + " " + i.AvgOptPriceCurrencyName + " " + i.AvgOptPriceNote,
                AvgRozPrice = i.AvgRozPrice,
                AvgRozPriceCurrencyDicId = i.AvgRozPriceCurrencyDicId,
                AvgRozPriceNote = i.AvgRozPriceNote,
                AvgRozPriceWithLink = i.AvgRozPrice.ToString() + " " + i.AvgRozPriceCurrencyName + " " + i.AvgOptPriceNote
            });

            var data = new
            {
                draw = request.Draw,
                recordsFiltered = count,
                recordsTotal = count,
                Data = itemsVm.ToList()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPriceType6(ModelRequest request, Guid id)
        {
            var items = db.PricesViews.Where(o => o.PriceProjectId == id && o.Type == 6);
            var count = items.Count();

            var data = new
            {
                draw = request.Draw,
                recordsFiltered = count,
                recordsTotal = count,
                Data = items.ToList()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPriceType2(ModelRequest request, Guid id)
        {
            var items = db.PricesViews.Where(o => o.PriceProjectId == id && o.Type == 2 && o.MtPartsId != null).ToList();
            var count = items.Count();

            var sumItem = new PricesView
            {
                PartsName = "ВСЕГО",
                ManufacturerPrice = items.Sum(x => x.ManufacturerPrice),
                CipPrice = items.Sum(x => x.CipPrice),
                RefPrice = items.Sum(x => x.RefPrice),
                UnitPrice = items.Sum(x => x.UnitPrice),
                RefPriceTypeName = "",
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111")
            };

            items.Add(sumItem);

            var data = new
            {
                draw = request.Draw,
                recordsFiltered = count,
                recordsTotal = count,
                Data = items.ToList()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SignOperation(string id)
        {
            var repository = new PriceProjectRepository();
            var project = repository.GetPreamble(new Guid(id));
            var isSuccess = true;
            var preambleXml = string.Empty;
            try
            {
                preambleXml = SerializeHelper.SerializeDataContract(project);
                preambleXml = preambleXml.Replace("utf-16", "utf-8");
            }
            catch (Exception e)
            {
                isSuccess = false;
            }

            return Json(new
            {
                IsSuccess = isSuccess,
                preambleXml
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SignForm(string preambleId, string xmlAuditForm)
        {
            SendPrice(new Guid(preambleId), true, xmlAuditForm);
            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SendProjectPrice(RequestModel request)
        {
            SendPrice(request.Project.Id, false, null);
            return Json("Ок");
        }


        private void SendPrice(Guid id, bool isSgined, string xmlAuditForm)
        {
            Document baseDocument = db.Documents.FirstOrDefault(m => m.Id == id);
            if (baseDocument == null)
            {
                var user = UserHelper.GetCurrentEmployee();
                Document document = new Document()
                {
                    Id = id,
                    DocumentType = 0,
                    ProjectType = 0,
                    ExecutionDate = DateTime.Now.AddDays(30),
                    DocumentDate = DateTime.Now,
                    AttachPath = FileHelper.GetObjectPathRoot(),
                    CorrespondentsInfo = UserHelper.GetCurrentEmployee().DisplayName,
                    CorrespondentsId = user.Id.ToString(),
                    CorrespondentsValue = user.DisplayName,
                    IsTradeSecret = false
                };
                var project = db.PriceProjects.First(o => o.Id == id);

                project.Status = (int)PriceProjectStatus.OnRegistration;
                project.IsSended = true;
                project.IsSigned = isSgined;
                db.Entry(project).State = EntityState.Modified;
                db.Documents.Add(document);
                if (project.Type == 2)
                {
                    var prices = db.Prices.Where(e => e.PriceProjectId == project.PriceProjectId);
                    foreach (var price in prices)
                    {
                        var newPrice = new Price()
                        {
                            Type = price.Type,
                            AvgObkCost = price.AvgObkCost,
                            AvgOptCost = price.AvgOptCost,
                            AvgOptPrice = price.AvgOptPrice,
                            AvgOptPriceCurrencyDicId = price.AvgOptPriceCurrencyDicId,
                            AvgOptPriceNote = price.AvgOptPriceNote,
                            AvgRozPrice = price.AvgRozPrice,
                            AvgRozPriceCurrencyDicId = price.AvgRozPriceCurrencyDicId,
                            AvgRozPriceNote = price.AvgRozPriceNote,
                            AvgRznCost = price.AvgRznCost,
                            BritishCost = price.BritishCost,
                            BritishPrice = price.BritishPrice,
                            CalcDateEnd = price.CalcDateEnd,
                            CalcDateStart = price.CalcDateStart,
                            CipPrice = price.CipPrice,
                            CipPriceCurrencyDicId = price.CipPriceCurrencyDicId,
                            CountryId = price.CountryId,
                            CreatedDate = price.CreatedDate,
                            Description = price.Description,
                            LimitCost = price.LimitCost,
                            LimitPrice = price.LimitPrice,
                            LimitPriceCurrencyDicId = price.LimitPriceCurrencyDicId,
                            LimitPriceNote = price.LimitPriceNote,
                            ManufacturerPrice = price.ManufacturerPrice,
                            ManufacturerPriceCurrencyDicId = price.ManufacturerPriceCurrencyDicId,
                            ManufacturerPriceNote = price.ManufacturerPriceNote,
                            MarkupCost = price.MarkupCost,
                            MarkupCostOpt = price.MarkupCostOpt,
                            MinimalCost = price.MinimalCost,
                            MtPartsId = price.MtPartsId,
                            Name = price.Name,
                            OriginalCost = price.OriginalCost,
                            OwnerPrice = price.OwnerPrice,
                            OwnerPriceCurrencyDicId = price.OwnerPriceCurrencyDicId,
                            PriceProjectId = project.Id,
                            RefPrice = price.RefPrice,
                            RefPriceCurrencyDicId = price.RefPriceCurrencyDicId,
                            RefPriceTypeDicId = price.RefPriceTypeDicId,
                            RequestDate = price.RequestDate,
                            UnitPrice = price.UnitPrice,
                            UnitPriceCurrencyDicId = price.UnitPriceCurrencyDicId,
                            ZakupCost = price.ZakupCost,
                            Id = Guid.NewGuid()
                        };
                        db.Prices.Add(newPrice);
                    }
                }
                db.SaveChanges();
                SendNewAppNotification(id);
            }
            else
            {
                baseDocument.IsTradeSecret = false;
                Document baseDocument2 =
                    db.Documents.Where(m => m.DocumentType == 1)
                        .OrderByDescending(m => m.CreatedDate)
                        .FirstOrDefault(m => m.AnswersId == id.ToString());


                var project = db.PriceProjects.FirstOrDefault(o => o.Id == baseDocument.Id);
                if (project != null)
                {
                    project.Status = (int)PriceProjectStatus.Registered;
                    db.Entry(project).State = EntityState.Modified;
                    project.IsSended = true;
                    project.IsSigned = isSgined;
                    db.SaveChanges();
                }

                if (baseDocument2 != null && !string.IsNullOrEmpty(baseDocument2.RegistratorId))
                {

                    if (baseDocument2.CountDay > 0)
                    {
                        baseDocument2.CompareConterDate = DateTime.Now.AddDays(baseDocument2.CountDay.Value);
                    }
                    baseDocument2.CountDay = null;

                    var registartor = db.Employees.FirstOrDefault(x => x.Id == new Guid(baseDocument2.RegistratorId));
                    if (registartor != null)
                    {
                        new NotificationManager().SendNotification(
                            string.Format("Заявление №{0} исправлено заявителем",
                                string.IsNullOrEmpty(baseDocument.OutgoingNumber)
                                    ? baseDocument.Number
                                    : baseDocument.OutgoingNumber),
                            ObjectType.Letter, baseDocument2.Id, registartor.Id);
                    }
                }

                if (baseDocument2 != null && baseDocument2.MainTaskId != null)
                {

                    Database.DataModel.Task task = db.Tasks.FirstOrDefault(m => m.Id == baseDocument2.MainTaskId);
                    if (task != null)
                    {
                        Activity activity = new Activity
                        {
                            Id = Guid.NewGuid(),
                            ParentTask = task.Id,
                            DocumentId = task.DocumentId,
                            AuthorId = UserHelper.GetCurrentEmployee().Id.ToString(),
                            AuthorValue = UserHelper.GetCurrentEmployee().DisplayName,
                            ExecutorsId = task.ExecutorId,
                            ExecutorsValue = task.ExecutorValue,
                            Type = 2,
                            IsParrent = false,
                            CreatedDate = DateTime.Now,
                            ParentId = task.ActivityId,
                            ExecutionDate = task.Document.ExecutionDate,
                            Text = "Замечания исправлены",
                            IsNotActive = false,
                            TypeEx = task.Stage,
                        };
                        db.Activities.Add(activity);
                        db.SaveChanges();
                    }
                }

            }
            var note = "";
            if (isSgined)
            {
                note = "Заявление отправлено. Дата отправки:" + DateTime.Now;
            }
            else
            {
                note = "Заявление отправлено без подписи. Дата отправки:" + DateTime.Now;
            }
            var history = new PriceProjectsHistory()
            {
                DateCreate = DateTime.Now,
                PriceProjectId = id,
                XmlSign = xmlAuditForm,
                StatusId = (int)PriceProjectStatus.Registered,
                UserId = UserHelper.GetCurrentEmployee().Id,
                Note = note
            };
            db.PriceProjectsHistories.Add(history);
            db.SaveChanges();

            if (!isSgined)
            {
                new NotificationManager().SendNotification(
                    "Предупреждение! Отправленно без подписи ЭЦП, необходимо распечатать заявления и принести в НЦЭЛС.",
                    ObjectType.PriceProject, id, UserHelper.GetCurrentEmployee().Id);
            }
        }
        private void SendNewAppNotification(Guid appId)
        {
            var executors = db.EmployeePermissionRoles.Join(db.PermissionRoleKeys, x => x.PermissionRoleId,
                    x => x.PermissionRoleId,
                    (epr, prk) => new
                    {
                        epr.EmployeeId,
                        prk.PermissionKey,
                        prk.PermissionValue
                    }).Where(o => o.PermissionKey == "IsPriceProjectProcCenterVisibility" && o.PermissionValue.ToLower() == "true").Select(e => e.EmployeeId)
                .ToList();
            var nm = new NotificationManager();
            foreach (var executor in executors)
            {
                nm.SendNotification("Поступила новая заявка на регистрацию", ObjectType.PriceProject, appId, executor);
            }
        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new
                {
                    isSuccess = false
                });
            }
            var model = db.PriceProjects.FirstOrDefault(e => e.Id == new Guid(id));
            if (model != null)
            {
                model.IsArchive = true;
                db.SaveChanges();
            }
            return Json(new
            {
                isSuccess = true
            });
        }

        [HttpPost]
        public virtual ActionResult SetIsIncluded(Guid priceId, bool isCheck, Guid projectId, Guid countryId)
        {
            var price = db.Prices.FirstOrDefault(e => e.Id == priceId);
            if (price != null)
            {
                price.IsIncluded = isCheck;
               
            }
            else
            {
                price = new Price
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    CountryId = countryId,
                    PriceProjectId = projectId,
                    Type = 1,
                    IsIncluded = isCheck,
                };
                db.Prices.Add(price);
            }
            db.SaveChanges();
            return Json(new { Success = true });
        }

    }
}