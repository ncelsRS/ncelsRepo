using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.Ajax.Utilities;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Enums;
using PW.Ncels.Database.Helpers;
using PW.Prism.ViewModels.Commissions;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;

namespace PW.Prism.Controllers
{
    [Authorize]
    public class CommissionController : Controller
    {
        private ncelsEntities db = UserHelper.GetCn();

        public ActionResult CommissionList()
        {
            var model = new CommissionListModel();
            model.TypeList = db.CommissionTypes.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            model.TypeList.Insert(0, new SelectListItem { Text = "Все", Value = "" });
            return PartialView("CommissionList", model);
        }

        public ActionResult CommissionRead([DataSourceRequest] DataSourceRequest request)
        {
            var data = db.CommissionsViews.Select(x => new CommissionListItemModel
            {
                Id = x.Id,
                Number = x.FullNumber,
                KindShortName = x.KindShortName,
                Date = x.Date,
                TypeId = x.TypeId,
                TypeName = x.TypeName,
                IsComplete = x.IsComplete,
                Comment = x.Comment,
            });
            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult Commission(int? id)
        {
            var model = new CommissionModel();
            if (id != null && id != -1)
            {
                var dbCommission = db.Commissions.SingleOrDefault(x => x.Id == id);
                if (dbCommission == null)
                {
                    return Json(new { success = false, message = "Повестка не найдена" });
                }
                model.Id = dbCommission.Id;
                model.Number = dbCommission.FullNumber;
                model.Comment = dbCommission.Comment;
                model.IsComplete = dbCommission.IsComplete;
                model.Date = dbCommission.Date;
                model.Kind = dbCommission.KindId;
                model.Type = dbCommission.TypeId;
                model.Questions = db.CommissionQuestions.Where(x => x.CommissionId == dbCommission.Id).Include(x=>x.CommissionQuestionType).ToList();
            }
            model.TypeList = db.CommissionTypes.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            model.KindList = db.CommissionKinds.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            model.CommissionUnitTypes = db.CommissionUnitTypes.ToList();
            model.Units = GetEmployees(id);
            return PartialView("CommissionTab", model);
        }

        public ActionResult GetCommissionNumber(int type)
        {
            int number;
            var fullNumber = GetCommissionNumber(type, out number);
            return Json(new { success = true, number = fullNumber });
        }

        private string GetCommissionNumber(int type, out int number)
        {
            var dbType = db.CommissionTypes.Single(x => x.Id == type);
            number = db.Commissions.Where(x => x.TypeId == type).Select(x => x.Number).DefaultIfEmpty(0).Max();
            number++;
            var fullNumber = number + "-" + dbType.Name;//!!!!!!!!!!!!!!!shortName;
            fullNumber = fullNumber.Replace("Экспертный совет", "ЭС");
            fullNumber = fullNumber.Replace("Фармакологическая комиссия", "ФМК");
            fullNumber = fullNumber.Replace("Фармацевтическая комиссия", "ФЦК");
            return fullNumber;
        }

        public ActionResult SaveCommission(SaveCommissionModel data)
        {
            var isCreate = data.Id == null;
            var dbTypes = db.CommissionUnitTypes.ToList();
            if(data.Units == null)
            {
                data.Units = new List<SaveCommissionModel.Unit>();
            }
            foreach (var type in data.Units.GroupBy(x => x.Type))
            {
                var dbType = dbTypes.Single(x => x.Id == type.Key);
                if (dbType.MaxCount != null)
                {
                    if (dbType.MaxCount < type.Count())
                    {
                        return Json(new { success = false, message = "Участников '" + dbType.Name + "' должно быть не более " + dbType.MaxCount });
                    }
                }
            }
            var badUnit = data.Units.GroupBy(x => x.Id).FirstOrDefault(x => x.Count() > 1);
            if(badUnit != null)
            {
                return Json(new { success = false, message = "Участникам запрещено занимать сразу 2 позиции" });
            }

            Commission dbCommission;
            if (data.Id != null)
            {
                dbCommission = db.Commissions.SingleOrDefault(x => x.Id == data.Id);
                if (dbCommission == null)
                {
                    return Json(new { success = false, message = "Повестка не найдена" });
                }
                if (data.Date.Date != dbCommission.Date && data.Date < DateTime.Now.Date)
                {
                    return Json(new { success = false, message = "Дату проведения не допустима" });
                }
            }
            else
            {
                if (data.Date < DateTime.Now.Date)
                {
                    return Json(new { success = false, message = "Дату проведения не допустима" });
                }
                dbCommission = new Commission();
                db.Commissions.Add(dbCommission);
                int number;
                var fullNumber = GetCommissionNumber(data.Type.Value, out number);
                dbCommission.Number = number;
                dbCommission.FullNumber = fullNumber;
                dbCommission.TypeId = data.Type.Value;
            }
            var prevDate = dbCommission.Date;
            dbCommission.Date = data.Date;
            dbCommission.KindId = data.Kind;
            dbCommission.Comment = data.Comment;
            // dbCommission.IsComplete = data.Complete ?? false;
            if (isCreate)
            {
                ActionLogger.WriteInt(db, "Повестка '" + dbCommission.FullNumber + "': Создана");
            }
            else
            {
                ActionLogger.WriteInt(db, "Повестка '" + dbCommission.FullNumber + "': Отредактирована");
            }
            db.SaveChanges();

            var dbUnits = db.CommissionUnits.Where(x => x.CommissionId == dbCommission.Id).ToList();
            var existsUnits = data.Units.Select(x => x.Id).ToList();
            var removedUnits = dbUnits.Where(x => !existsUnits.Contains(x.EmployeeId)).ToList();
            var changedUnitCount = 0;
            var addUnits = new List<CommissionUnit>();
            foreach (var unit in data.Units)
            {
                var dbUnit = dbUnits.SingleOrDefault(x => x.EmployeeId == unit.Id);
                if(dbUnit == null)
                {
                    dbUnit = new CommissionUnit();
                    dbUnit.CommissionId = dbCommission.Id;
                    dbUnit.EmployeeId = unit.Id;
                    dbUnit.UnitTypeId = unit.Type;
                    addUnits.Add(dbUnit);
                }
                else
                {
                    if (dbUnit.UnitTypeId != unit.Type)
                    {
                        dbUnit.UnitTypeId = unit.Type;
                        changedUnitCount++;
                    }
                }
            }
            if (removedUnits.Count > 0 || addUnits.Count > 0 || changedUnitCount > 0)
            {
                var additionalText = "";
                additionalText += addUnits.Count == 0 ? "" : "Добавлено " + addUnits.Count + ";";
                additionalText += changedUnitCount == 0 ? "" : "Изменено " + changedUnitCount + ";";
                additionalText += removedUnits.Count == 0 ? "" : "Удалено " + removedUnits.Count+";";
                db.CommissionUnits.RemoveRange(removedUnits);
                db.CommissionUnits.AddRange(addUnits);
                ActionLogger.WriteInt(db, "Повестка '" + dbCommission.FullNumber + "': Отредактированы участники", additionalText);
                db.SaveChanges();
            }
            if(isCreate)
            {
                var dbDosages = GetCommissionAvailableDosages(dbCommission.Id).ToList();
                foreach (var dbDosage in dbDosages)
                {
                    var dbComQuestion = new CommissionDrugDosage();
                    dbComQuestion.CommissionId = dbCommission.Id;
                    dbComQuestion.DrugDosageId = dbDosage.DosageId;
                    dbComQuestion.StageId = dbDosage.DosageStageId;
                    db.CommissionDrugDosages.Add(dbComQuestion);
                    CheckNeedCommissionAfterAdd(dbDosage.DosageId, dbDosage.DosageStageId, dbCommission.TypeId, dbCommission.Id);
                }
                if(dbDosages.Count > 0)
                {
                    var dbQuestion = new CommissionQuestion();
                    dbQuestion.CommissionId = dbCommission.Id;
                    dbQuestion.TypeId = 1;
                    dbQuestion.Number = 1;
                    db.CommissionQuestions.Add(dbQuestion);
                    var additionalText = dbDosages.Count + "шт.";
                    ActionLogger.WriteInt(db, "Повестка '" + dbCommission.FullNumber + "': Автоматически добавлены заявки", additionalText);
                }
                db.SaveChanges();
            }
            else
            {
                if(prevDate.Date != dbCommission.Date.Date)
                {
                    CommissionHelper.SendChangeDateNotifications(dbCommission.Id, prevDate);
                }
            }
            return Json(new { success = true, id = dbCommission.Id });
        }

        public ActionResult RefreshCommissionDosages(int commissionId)
        {
            var msg = "";
            var dbCommission = db.Commissions.Single(x => x.Id == commissionId);
            var dbDosages = GetCommissionAvailableDosages(dbCommission.Id).ToList();
            foreach (var dbDosage in dbDosages)
            {
                var dbComQuestion = new CommissionDrugDosage();
                dbComQuestion.CommissionId = dbCommission.Id;
                dbComQuestion.DrugDosageId = dbDosage.DosageId;
                dbComQuestion.StageId = dbDosage.DosageStageId;
                db.CommissionDrugDosages.Add(dbComQuestion);
                CheckNeedCommissionAfterAdd(dbDosage.DosageId, dbDosage.DosageStageId, dbCommission.TypeId, dbCommission.Id);
            }
            if (dbDosages.Count > 0)
            {
                msg += dbCommission.FullNumber + " добавлено " + dbDosages.Count + "заявок\r\n";
            }
            db.SaveChanges();
            if (String.IsNullOrEmpty(msg))
            {
                msg = "Заявки для добавления отсутствуют";
            }
            return Json(new { success = true, message = msg });
        }

        public ActionResult RefreshCommissionDosagesGlobal()
        {
            var msg = RefreshCommissionDosagesGlobalMethod();
            return Json(new { success = true, message = msg });
        }

        public string RefreshCommissionDosagesGlobalMethod()
        {
            var msg = "";
            var commissionTypes = db.CommissionTypes.ToList();
            foreach (var commissionType in commissionTypes)
            {
                var dbCommission = db.Commissions.Where(x => x.TypeId == commissionType.Id && x.IsComplete == false).OrderBy(x => x.Date).FirstOrDefault();
                if (dbCommission != null)
                {
                    var dbDosages = GetCommissionAvailableDosages(dbCommission.Id).ToList();
                    foreach (var dbDosage in dbDosages)
                    {
                        var dbComQuestion = new CommissionDrugDosage();
                        dbComQuestion.CommissionId = dbCommission.Id;
                        dbComQuestion.DrugDosageId = dbDosage.DosageId;
                        dbComQuestion.StageId = dbDosage.DosageStageId;
                        db.CommissionDrugDosages.Add(dbComQuestion);
                        CheckNeedCommissionAfterAdd(dbDosage.DosageId, dbDosage.DosageStageId, dbCommission.TypeId, dbCommission.Id);
                    }
                    if (dbDosages.Count > 0)
                    {
                        msg += dbCommission.FullNumber + " добавлено " + dbDosages.Count + "заявок\r\n";
                    }
                }
            }
            db.SaveChanges();
            if (String.IsNullOrEmpty(msg))
            {
                msg = "Заявки для добавления отсутствуют";
            }
            return msg;
        }

        public List<CommissionEmployeeDepartment> GetEmployees(int? commissionId)
        {
            //метод конечно не царский, но времени оч мало :(
            var myOrgId = UserHelper.GetCurrentEmployee().OrganizationId;
            var dbEmployees = db.Employees.Where(x => x.OrganizationId == myOrgId).ToList();
            var positionIds = dbEmployees
                .Join(db.Units.Where(x => x.Type == (int)UnitType.Post),
                x => x.PositionId,
                x => x.Id,
                (e, u) => new { EmployeeId = e.Id, DepartmentId = u.ParentId }).ToDictionary(x=>x.EmployeeId, x=>x.DepartmentId);
            var dbDepIds = positionIds.Select(x => x.Value).Distinct().ToList();
            var dbDepList = new List<Unit>();
            while (true) 
            {
                var dbDeps = db.Units.Where(x => dbDepIds.Contains(x.Id)).ToList();
                dbDepList.AddRange(dbDeps);
                var parentDepIds = dbDeps.Where(x => x.ParentId != myOrgId).Select(x => x.ParentId).ToList();
                if (parentDepIds.Count == 0)
                {
                    break;
                }
                dbDepIds = parentDepIds;
            }
            dbDepList = dbDepList.DistinctBy(x=>x.Id).OrderBy(x => x.Name).ToList();
            dbEmployees = dbEmployees.OrderBy(x => x.LastName).ThenBy(x=>x.FirstName).ThenBy(x=>x.MiddleName).ToList();
            List<CommissionUnit> dbUnits = null;
            if (commissionId != null)
            {
                dbUnits = db.CommissionUnits.Where(x => x.CommissionId == commissionId).ToList();
            }
            var mDeps = GetDeps(dbDepList, dbEmployees, positionIds, dbUnits, myOrgId);
            return mDeps;
        }

        private List<CommissionEmployeeDepartment> GetDeps(List<Unit> dbDepList, List<Employee> dbEmployees, Dictionary<Guid, Guid?> positionIds, List<CommissionUnit> dbUnits, Guid rootId)
        {
            List<CommissionEmployeeDepartment> mDeps = new List<CommissionEmployeeDepartment>();
            var deps = dbDepList.Where(x => x.ParentId == rootId).ToList();
            foreach (var unit in deps)
            {
                var dep1 = new CommissionEmployeeDepartment();
                dep1.Id = unit.Id;
                dep1.Name = unit.Name;
                dep1.Employees = new List<CommissionEmployee>();
                var emplIds = positionIds.Where(x => x.Value == unit.Id).Select(x => x.Key).ToList();
                var dbDepEmployees = dbEmployees.Where(x => emplIds.Contains(x.Id)).ToList();
                foreach (var dbDepEmployee in dbDepEmployees)
                {
                    var emp1 = new CommissionEmployee();
                    emp1.Id = dbDepEmployee.Id;
                    emp1.Name = dbDepEmployee.LastName + " " + dbDepEmployee.FirstName + " " + dbDepEmployee.MiddleName;
                    if(dbUnits != null)
                    {
                        var dbCommissionUnit = dbUnits.SingleOrDefault(x => x.EmployeeId == dbDepEmployee.Id);
                        if(dbCommissionUnit != null)
                        {
                            emp1.Type = dbCommissionUnit.UnitTypeId;
                        }
                    }
                    dep1.Employees.Add(emp1);
                }
                dep1.Departments = GetDeps(dbDepList, dbEmployees, positionIds, dbUnits, dep1.Id);
                mDeps.Add(dep1);
            }
            return mDeps;
        }

        public ActionResult DeleteCommission(int id)
        {
            var dbItem = db.Commissions.SingleOrDefault(x => x.Id == id);
            if(dbItem == null)
            {
                return Json(new { success = false, message = "Заседание не найдено" });
            }
            //if(db.CommissionQuestions.Any(x=>x.CommissionId == id))
            //{
            //return Json(new { success = false, message = "Перед удалением, удалите вопросы повестки" });
            //}
            db.CommissionDrugDosages.RemoveRange(db.CommissionDrugDosages.Where(x => x.CommissionId == id));
            db.CommissionQuestions.RemoveRange(db.CommissionQuestions.Where(x => x.CommissionId == id));
            db.Commissions.Remove(dbItem);
            db.SaveChanges();
            return Json(new { success = true });
        }

        public ActionResult CompleteCommission(int id)
        {
            var dbItem = db.Commissions.SingleOrDefault(x => x.Id == id);
            if (dbItem == null)
            {
                return Json(new { success = false, message = "Заседание не найдено" });
            }
            var dbDrugDeclarations = db.CommissionDrugDosages.Where(x => x.CommissionId == id && x.ConclusionTypeId == null)
                .Join(db.EXP_DrugDosage, x => x.DrugDosageId, x => x.Id, (x, y) => new { CD = x, DD = y })
                .ToList();
            if(dbDrugDeclarations.Count > 0)
            {
                var badDdMessage = "Необходимо внести решения по следующим заявкам: ";
                foreach (var dd in dbDrugDeclarations)
                {
                    badDdMessage += " заявка №" + dd.DD.RegNumber + ",";
                }
                badDdMessage = badDdMessage.Substring(0, badDdMessage.Length - 1);
                return Json(new { success = false, message = badDdMessage });
            }
            dbItem.IsComplete = true;
            db.SaveChanges();
            return Json(new { success = true });
        }

        public ActionResult CommissionDrugDelaretionRead([DataSourceRequest] DataSourceRequest request, int commissionId)
        {
            var dbCommissionDrugDosages = db.CommissionDrugDosages.AsQueryable();
            var emp = UserHelper.GetCurrentEmployee();
            var dbComUnit = db.CommissionUnits.SingleOrDefault(x => x.CommissionId == commissionId && x.EmployeeId == emp.Id);
            if(dbComUnit == null)
            {
                return Json(Enumerable.Empty<CommissionDrugDosage>().AsQueryable().ToDataSourceResult(request));
            }
            if(dbComUnit.UnitTypeId == (int)PW.Ncels.Database.Enums.CommissionUnitType.Dust) //todo не уверен что правильно трактовал задачу
            {
                var dbStageIds = db.EXP_ExpertiseStage.Where(x => x.Executors.Any(x2 => x2.Id == emp.Id)).Select(x=>x.Id);
                dbCommissionDrugDosages = dbCommissionDrugDosages.Where(x => dbStageIds.Contains(x.StageId));
            }
            //todo Помимо добавленных полей на Рассмотрения заявок, должны отображаться следующие поля:
            //1)	Решение Эксперта;
            //2)	Торговое наименование препарата – автоматическое заполнение;
            //3)	v Производитель, страна – автоматическое заполнение;
            //4)	v Лекарственная форма – автоматическое заполнение;
            //5)	v ATX– автоматическое заполнение;
            //6)	v непатентованное название (МНН) – автоматическое заполнение;
            //7)	v Срок регистрации – автоматическое заполнение;
            //8)	v Срок годности – автоматическое заполнение;
            //9)	v Форма отпуска – автоматическое заполнение;
            //10)	Подразделение – автоматическое заполнение;
            //11)	Эксперт – автоматическое заполнение;
            //12)	Дата заключения – автоматическое заполнение;
            //13)	Тип НД – автоматическое заполнение;
            //14)	Признак Повтор
            var data = dbCommissionDrugDosages.Include(x => x.CommissionConclusionType)
                .Where(x => x.CommissionId == commissionId)
                .Join(db.Exp_DrugDosageStageView, 
                    x => new { A = x.DrugDosageId, B =  x.StageId, C = x.CommissionId }, 
                    x => new { A = x.DosageId, B = x.DosageStageId, C = (int)x.CommissionId },
                    (dd, cd) => new { dd, cd })
                .Join(db.EXP_DrugDeclaration, x => x.cd.DeclarationId, x => x.Id, //todo это можно уже не джоинить, но всёжи
                (d, dec) => new CommissionDrugDeclarationListItemModel
                {
                    Id = d.dd.Id,
                    DrugDosageId = d.cd.DosageId,
                    DeclarationNumber = d.cd.DeclarationNumber,
                    Number = d.cd.DosageRegNumber,
                    Name = d.cd.DeclarationNameRu,
                    Date = d.cd.DeclarationCreatedDate, // взял любую дату, не времени искать)
                    DosageStageId = d.cd.DosageStageId,
                    StageName = d.cd.StageNameRu,
                    ConclusionId = d.dd.CommissionConclusionType.Id,
                    ConclusionName = d.dd.CommissionConclusionType.Name,
                    ConclusionComment = d.dd.ConclusionComment,
                    ProducerNameRu = d.cd.ProducerNameRu,
                    ProducerCountryName = d.cd.ProducerCountryName,
                    ProducerDocDate = d.cd.ProducerDocDate,
                    ProducerDocExpiryDate = d.cd.ProducerDocExpiryDate,
                    DosageFormName = d.cd.DosageFormName,
                    DeclarationAtxName = d.cd.DeclarationAtxName,
                    DeclarationMnnName = d.cd.DeclarationMnnNameRu,
                    DosageSaleTypeName = d.cd.DosageSaleTypeName,
                    NtdNameRu = d.cd.NtdNameRu,
                    IsRepeat = d.cd.PrevCommissionCount > 0,
                    ResultNameRu = d.cd.ResultNameRu,
                    ResultDate = d.cd.ResultDate,
                    ResultCreatorShortName = d.cd.ResultCreatorShortName,
                });


            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult CommissionDrugDelaretionForAddRead([DataSourceRequest] DataSourceRequest request, int commissionId)
        {

            var emp = UserHelper.GetCurrentEmployee();
            var dbComUnit = db.CommissionUnits.SingleOrDefault(x => x.CommissionId == commissionId && x.EmployeeId == emp.Id);
            if (dbComUnit == null)
            {
                return Json(Enumerable.Empty<CommissionDrugDosage>().AsQueryable().ToDataSourceResult(request));
            }

            var dbDosages = GetCommissionAvailableDosages(commissionId);
            if (dbComUnit.UnitTypeId == (int)PW.Ncels.Database.Enums.CommissionUnitType.Dust) //todo не уверен что правильно трактовал задачу
            {
                var dbStageIds = db.EXP_ExpertiseStage.Where(x => x.Executors.Any(x2 => x2.Id == emp.Id)).Select(x => x.Id);
                dbDosages = dbDosages.Where(x => dbStageIds.Contains(x.DosageStageId));
            }
            var data = dbDosages.Select(x=>
                     new CommissionDrugDeclarationListItemModel
                     {
                         DosageStageId = x.DosageStageId,
                         DrugDosageId = x.DosageId,
                         DeclarationNumber = x.DeclarationNumber,
                         Number = x.DosageRegNumber,
                         Name = x.DeclarationNameRu,
                         Date = x.DeclarationCreatedDate, // взял любую дату, не времени искать)
                         StageName = x.StageNameRu,
                     });
            return Json(data.ToDataSourceResult(request));
        }

        private IQueryable<Exp_DrugDosageStageForAddView> GetCommissionAvailableDosages(int commissionId)
        {
            //todo замутил ту ещё хрень :(
            var dbCommission = db.Commissions.Single(x => x.Id == commissionId);
            var approvedStatusCode = "approved";
            var dbDosages = db.Exp_DrugDosageStageForAddView.AsQueryable();
            var sd = EXP_DIC_StageResult.DoesNotMatch;
            var srr = EXP_DIC_StageResult.RemoveFromRegistration;
            var sm = EXP_DIC_StageResult.Matches;
            var sr = EXP_DIC_StageResult.Recommended;
            var sdr = EXP_DIC_StageResult.DoesNotRecommended;
            var sw = EXP_DIC_StageResult.NeedMoreWork;
            var sc = EXP_DIC_StageResult.NeedExpertCouncilConclusion;


            var ct2 = (int)PW.Ncels.Database.Enums.CommissionType.PharmaceuticalCommission;
            var ct3 = (int)PW.Ncels.Database.Enums.CommissionType.PharmacologicalCommission;
            var ccne = (int)PW.Ncels.Database.Enums.CommissionConclusionType.NeedExpertCouncilConclusion;
            var ccnr = (int)PW.Ncels.Database.Enums.CommissionConclusionType.DoNoRecommend;
            var ccnw = (int)PW.Ncels.Database.Enums.CommissionConclusionType.NeedMoreWork;
            var cckh = (int)PW.Ncels.Database.Enums.CommissionConclusionType.KilledHimself;

            if (dbCommission.TypeId == (int)PW.Ncels.Database.Enums.CommissionType.ExpertCouncil)
            {
                //Первичная экспертиза - не соответствует,снят с регистрации заявителем, в срок приостановки заявления заявителем
                //Аналитическая экспертиза - не соответствует, в срок приостановки заявления заявителем
                var t = (int)PW.Ncels.Database.Enums.CommissionType.ExpertCouncil;
              //  var t2 = (int)PW.Ncels.Database.Enums.CommissionType.ExpertCouncil;
                //var ccnwId = db.CommissionConclusionTypes.Single(x => x.CommissionType == t2 && x.Code == ccnw).Id;
                var comIds = db.Commissions.Where(x => x.TypeId == t).Select(x => x.Id);
                var ddsIds = db.EXP_ExpertiseStageDosage.Join(
                      db.CommissionDrugDosages.Where(x => comIds.Contains(x.CommissionId)),
                      x => new { S = x.StageId, D = x.DosageId },
                      x => new { S = x.StageId, D = x.DrugDosageId },
                      (s, d) => new { s, d })
                   //     .Where(x => x.d.ConclusionTypeId.Value != ccnwId || x.d.CommissionId == dbCommission.Id)
                      .Select(x => x.s.Id);

                var ddsIds2 = db.EXP_ExpertiseStageDosage.Join(
                     db.CommissionDrugDosageNeedCommissions.Where(x => x.IsNeedEs),
                        x => new { S = x.StageId, D = x.DosageId },
                        x => new { S = x.StageId, D = x.DrugDosageId },
                        (s, d) => new { s, d })
                        .Select(x => x.s.Id);

                dbDosages = dbDosages.Where(x =>
                (x.StageId == EXP_DIC_StageInt.PrimaryExp && x.FinalDocStatusCode == approvedStatusCode && (!ddsIds.Contains(x.Id) || ddsIds2.Contains(x.Id)) && (x.ResultId == sd || x.ResultId == srr))
                || (x.StageId == EXP_DIC_StageInt.AnalyticalExp && x.FinalDocStatusCode == approvedStatusCode && (!ddsIds.Contains(x.Id) || ddsIds2.Contains(x.Id)) && (x.ResultId == sd || x.ResultId == srr))

                //Решение ФМК/ФМЦ комиссии отправляется на Экспертный Совет
                || (x.StageId == EXP_DIC_StageInt.PharmaceuticalExp //&& x.CommissionTypeId == ct2 
                                                                    // && (!ddsIds.Contains(x.Id) || ddsIds2.Contains(x.Id))
                        && ddsIds2.Contains(x.Id)
                    //&& (x.CommissionConclusionTypeCode == ccne || x.CommissionConclusionTypeCode == ccnr || x.CommissionConclusionTypeCode == cckh)//временно отменено, берём все
                    )
                || (x.StageId == EXP_DIC_StageInt.PharmacologicalExp //&& x.CommissionTypeId == ct3 
                                                                     // && (!ddsIds.Contains(x.Id) || ddsIds2.Contains(x.Id))
                        && ddsIds2.Contains(x.Id)

                    // && (x.CommissionConclusionTypeCode == ccne || x.CommissionConclusionTypeCode == ccnr || x.CommissionConclusionTypeCode == cckh) //временно отменено, берём все
                    )
                    );
            }
            else if (dbCommission.TypeId == (int)PW.Ncels.Database.Enums.CommissionType.PharmacologicalCommission)
            {
                var t = (int)PW.Ncels.Database.Enums.CommissionType.PharmacologicalCommission;
                //var t2 = (int)PW.Ncels.Database.Enums.CommissionType.ExpertCouncil;
               // var ccnwId = db.CommissionConclusionTypes.Single(x => x.CommissionType == t2 && x.Code == ccnw).Id;
                var comIds = db.Commissions.Where(x => x.TypeId == t).Select(x => x.Id);
                var ddsIds = db.EXP_ExpertiseStageDosage.Join(
               db.CommissionDrugDosages.Where(x => comIds.Contains(x.CommissionId)),
                       x => new { S = x.StageId, D = x.DosageId },
                       x => new { S = x.StageId, D = x.DrugDosageId },
                       (s, d) => new { s, d })
                  //      .Where(x => x.d.ConclusionTypeId.Value != ccnwId || x.d.CommissionId == dbCommission.Id)
                       .Select(x => x.s.Id);
                var ddsIds2 = db.EXP_ExpertiseStageDosage.Join(
                     db.CommissionDrugDosageNeedCommissions.Where(x=>x.IsNeedFmk),
                        x => new { S = x.StageId, D = x.DosageId },
                        x => new { S = x.StageId, D = x.DrugDosageId },
                        (s, d) => new { s, d })
                        .Select(x => x.s.Id);

                //Фармакалогическая экспертиза - рекомендовать, не рекомендовать, рассмотреть документы повторно, снят с регистрации заявителем
                dbDosages = dbDosages.Where(x => (!ddsIds.Contains(x.Id) || ddsIds2.Contains(x.Id)) && x.StageId == EXP_DIC_StageInt.PharmacologicalExp
                // all statuses && (x.StageId == sd || x.StageId == srr || x.StageId == sm)
                );
            }
            else if (dbCommission.TypeId == (int)PW.Ncels.Database.Enums.CommissionType.PharmaceuticalCommission)
            {
                var t = (int)PW.Ncels.Database.Enums.CommissionType.PharmaceuticalCommission;
                //var t2 = (int)PW.Ncels.Database.Enums.CommissionType.ExpertCouncil;
                //var ccnwId = db.CommissionConclusionTypes.Single(x => x.CommissionType == t2 && x.Code == ccnw).Id;
                var comIds = db.Commissions.Where(x => x.TypeId == t).Select(x => x.Id);
                var ddsIds = db.EXP_ExpertiseStageDosage.Join(
                db.CommissionDrugDosages.Where(x => comIds.Contains(x.CommissionId)),
                        x => new { S = x.StageId, D = x.DosageId },
                        x => new { S = x.StageId, D = x.DrugDosageId },
                        (s, d) => new { s, d })
                        //.Where(x=> x.d.ConclusionTypeId.Value != ccnwId || x.d.CommissionId == dbCommission.Id)
                        .Select(x => x.s.Id);
                var ddsIds2  = db.EXP_ExpertiseStageDosage.Join(
                     db.CommissionDrugDosageNeedCommissions.Where(x => x.IsNeedFmc),
                        x => new { S = x.StageId, D = x.DosageId },
                        x => new { S = x.StageId, D = x.DrugDosageId },
                        (s, d) => new { s, d })
                        .Select(x => x.s.Id);

                //Фармацевтическая экспертиза - рекомендовать, не рекомендовать, рассмотреть документы повторно, снят с регистрации заявителем
                dbDosages = dbDosages.Where(x => (!ddsIds.Contains(x.Id) || ddsIds2.Contains(x.Id)) && x.StageId == EXP_DIC_StageInt.PharmaceuticalExp
                // all statuses && (x.StageId == sd || x.StageId == srr || x.StageId == sm)
                );
            }
            else
            {
                throw new Exception("Такого не должно произойти");
            }
            return dbDosages;
        }

        public ActionResult AddCommissionDrugDeclaration(int commissionId, long dosageId, Guid dosageStageId)
        {
            var dbCommission = db.Commissions.Single(x => x.Id == commissionId);
            var isExists = db.CommissionDrugDosages.Any(x => x.DrugDosageId == dosageId && x.StageId == dosageStageId && x.CommissionId == commissionId);
            if(isExists)
            {
                return Json(new { success = false, message = "Эта сущность уже включена в повестку" });
            }
            var dbComQuestion = new CommissionDrugDosage();
            dbComQuestion.CommissionId = commissionId;
            dbComQuestion.DrugDosageId = dosageId;
            dbComQuestion.StageId = dosageStageId;
            db.CommissionDrugDosages.Add(dbComQuestion);

            CheckNeedCommissionAfterAdd(dosageId, dosageStageId, dbCommission.TypeId, dbCommission.Id);

            ActionLogger.WriteInt(db, "Повестка '" + dbCommission.FullNumber + "': Добавлена заявка");
            db.SaveChanges();
            return Json(new { success = true, id = dbComQuestion.Id });
        }

        private void CheckNeedCommissionAfterAdd(long dosageId, Guid dosageStageId, int commissionTypeId, int commissionId)
        {
            var ncDosage = db.CommissionDrugDosageNeedCommissions.FirstOrDefault(x => x.DrugDosageId == dosageId && x.StageId == dosageStageId);
            if (ncDosage != null)
            {
                if (commissionTypeId == (int)PW.Ncels.Database.Enums.CommissionType.PharmaceuticalCommission)
                {
                    ncDosage.IsNeedFmc = false;
                }
                if (commissionTypeId == (int)PW.Ncels.Database.Enums.CommissionType.PharmacologicalCommission)
                {
                    ncDosage.IsNeedFmk = false;
                }
                if (commissionTypeId == (int)PW.Ncels.Database.Enums.CommissionType.ExpertCouncil)
                {
                    ncDosage.IsNeedEs = false;
                }
            }
            var dbDosage = db.EXP_ExpertiseStageDosage.Single(x => x.StageId == dosageStageId && x.DosageId == dosageId);
            var lastResult = dbDosage.EXP_ExpertiseStageDosageResult.OrderBy(x => x.Id).LastOrDefault();
            if(lastResult != null)
            {
                var dbComDosage = db.CommissionDrugDosages.Local.Single(x => x.CommissionId == commissionId && x.DrugDosageId == dosageId && x.StageId == dosageStageId);
                dbComDosage.ExpertResultId = lastResult.Id;
            }
        }

        public ActionResult RemoveCommissionDrugDeclaration(int id)
        {
            var dbComQuestion = db.CommissionDrugDosages.Single(x => x.Id == id);
            var dbCommission = db.Commissions.Single(x => x.Id == dbComQuestion.CommissionId);
            db.CommissionDrugDosages.Remove(dbComQuestion);
            ActionLogger.WriteInt(db, "Повестка '" + dbCommission.FullNumber + "': Удалена заявка");
            db.SaveChanges();
            return Json(new { success = true});
        }

        public ActionResult GetCreateCommissionQuestionView(int commissionId)
        {
            var dbQuestions = db.CommissionQuestions.Where(x => x.CommissionId == commissionId).ToList();
            var nextNumber = dbQuestions.Select(x => x.Number).DefaultIfEmpty(0).Max() + 1;
            var existsCreateTypes = dbQuestions.Select(x => x.TypeId).ToList();
            var dbTypes = db.CommissionQuestionTypes.Where(x => !existsCreateTypes.Contains(x.Id)).ToList();
            var model = new CreateCommissionQuestionModel();
            model.TypeList = dbTypes.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            model.NextNumber = nextNumber;
            model.CommissionId = commissionId;
            return PartialView("CreateCommissionQuestion", model);
        }

        public ActionResult AddCommissionQuestion(CreateCommissionQuestionModel data)
        {
            var dbCommission = db.Commissions.SingleOrDefault(x => x.Id == data.CommissionId);
            if (dbCommission == null)
            {
                return Json(new { success = false, message = "Повестка не найдена" });
            }
            var dbQuestions = db.CommissionQuestions.Where(x => x.CommissionId == data.CommissionId).ToList();
            if (dbQuestions.Any(x=>x.TypeId == data.Type))
            {
                return Json(new { success = false, message = "Вопрос этого типа уже существует" });
            }
            var nextNumber = dbQuestions.Select(x => x.Number).DefaultIfEmpty(0).Max() + 1;
            var dbQuestion = new CommissionQuestion();
            dbQuestion.CommissionId = data.CommissionId;
            dbQuestion.TypeId = data.Type;
            dbQuestion.Number = nextNumber;
            dbQuestion.Comment = data.Comment;
            db.CommissionQuestions.Add(dbQuestion);
            ActionLogger.WriteInt(db, "Повестка '" + dbCommission.FullNumber + "': Добавлен вопрос");
            db.SaveChanges();

            var model = new CommissionModel();
            model.Id = dbCommission.Id;
            model.Questions = db.CommissionQuestions.Where(x => x.CommissionId == data.CommissionId).Include(x => x.CommissionQuestionType).ToList();
            return PartialView("CommissionQuestionsMain", model);
        }

        public ActionResult SaveCommissionQuestionComment(string comment, int commissionId, int typeId)
        {
            var dbQuestion = db.CommissionQuestions.SingleOrDefault(x => x.CommissionId == commissionId && x.TypeId == typeId);
            if (dbQuestion == null)
            {
                return Json(new { success = false, message = "Вопрос повестки не найден" });
            }
            var dbCommission = db.Commissions.Single(x => x.Id == commissionId);
            dbQuestion.Comment = comment;
            ActionLogger.WriteInt(db, "Повестка '" + dbCommission.FullNumber + "': Отредактирован комментарий вопроса");
            db.SaveChanges();
            return Json(new { success = true});
        }

        public ActionResult GetConclusionCommissionDrugDeclarationView(int id)
        {
            try
            {
                var dbCommissionDrugDeclaration = db.CommissionDrugDosages.SingleOrDefault(x => x.Id == id);
                if (dbCommissionDrugDeclaration == null)
                {
                    return Json(new { success = false, message = "Сущность не найдена" });
                }
                var dbCommission = db.Commissions.Single(x => x.Id == dbCommissionDrugDeclaration.CommissionId);
                var dbDrugDeclaration = db.EXP_DrugDosage.Single(x => x.Id == dbCommissionDrugDeclaration.DrugDosageId);
                var model = new ConclusionCommissionDrugDeclarationModel();
                model.Id = id;
                model.CommissionId = dbCommission.Id;
                model.Name = dbDrugDeclaration.EXP_DrugDeclaration.NameRu;
                model.Number = dbDrugDeclaration.RegNumber;
                model.ConclusionList = db.CommissionConclusionTypes.Where(x=>x.CommissionType == dbCommission.TypeId)
                    .Select(x => new SelectListItem { Value = x.Code.ToString(), Text = x.Name }).ToList();
                return PartialView("ConclusionCommissionDrugDeclaration", model);
            }
            catch (Exception ex) //todo можно удалить
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public ActionResult CreateConclusionCommissionDrugDeclaration(ConclusionCommissionDrugDeclarationModel data)
        {
            var employeeId = UserHelper.GetCurrentEmployee().Id;
            var dbCommissionDrugDeclaration = db.CommissionDrugDosages.SingleOrDefault(x => x.Id == data.Id);
            if (dbCommissionDrugDeclaration == null)
            {
                return Json(new { success = false, message = "Эта сущность не входит в экспертный совет" });
            }
            var dbCommission = db.Commissions.Single(x => x.Id == dbCommissionDrugDeclaration.CommissionId);
            var dbDosage = db.EXP_ExpertiseStageDosage.Single(x => x.DosageId == dbCommissionDrugDeclaration.DrugDosageId && x.StageId == dbCommissionDrugDeclaration.StageId);
            var stageTypeId = db.EXP_ExpertiseStage.Single(x => x.Id == dbDosage.StageId).StageId;
            var concl = db.CommissionConclusionTypes.SingleOrDefault(x => x.CommissionType == dbCommission.TypeId && x.Code == data.Type);
            if(concl == null)
            {
                return Json(new { success = false, message = "Недопустимое решение" });
            }
            dbCommissionDrugDeclaration.ConclusionTypeId = concl.Id;
            dbCommissionDrugDeclaration.ConclusionComment = data.Comment;
            
            var resultChange = false; //todo поидее во все места должен зайти, но мало ли :)
            if (dbCommission.TypeId == (int)PW.Ncels.Database.Enums.CommissionType.ExpertCouncil)
            {
                if (concl.Code == (int)PW.Ncels.Database.Enums.CommissionConclusionType.Recommend)
                {
                    if (stageTypeId == EXP_DIC_StageInt.PharmaceuticalExp
                        || stageTypeId == EXP_DIC_StageInt.PharmacologicalExp)
                    {
                        dbDosage.ResultId = EXP_DIC_StageResult.Recommended;
                        resultChange = true;
                    }
                    else
                    {
                        dbDosage.ResultId = EXP_DIC_StageResult.Matches;
                        resultChange = true;
                    }
                }
                if (concl.Code == (int)PW.Ncels.Database.Enums.CommissionConclusionType.NeedMoreWork)
                {
                    dbDosage.ResultId = EXP_DIC_StageResult.NeedMoreWork;
                        resultChange = true;
                }
                if (concl.Code == (int)PW.Ncels.Database.Enums.CommissionConclusionType.DoNoRecommend)
                {
                    if(stageTypeId == EXP_DIC_StageInt.PharmaceuticalExp
                        || stageTypeId == EXP_DIC_StageInt.PharmacologicalExp)
                    {
                        dbDosage.ResultId = EXP_DIC_StageResult.DoesNotRecommended;
                        resultChange = true;
                    }
                    else
                    {
                        dbDosage.ResultId = EXP_DIC_StageResult.DoesNotMatch;
                        resultChange = true;
                    }
                }
                if (concl.Code == (int)PW.Ncels.Database.Enums.CommissionConclusionType.KilledHimself)
                {
                    dbDosage.ResultId = EXP_DIC_StageResult.RemoveFromRegistration;
                        resultChange = true;
                }
            }
            if (dbCommission.TypeId == (int)PW.Ncels.Database.Enums.CommissionType.PharmaceuticalCommission
                || dbCommission.TypeId == (int)PW.Ncels.Database.Enums.CommissionType.PharmacologicalCommission)
            {

                if (concl.Code == (int)PW.Ncels.Database.Enums.CommissionConclusionType.Recommend)
                {
                    dbDosage.ResultId = EXP_DIC_StageResult.Recommended;
                        resultChange = true;
                }
                if (concl.Code == (int)PW.Ncels.Database.Enums.CommissionConclusionType.NeedMoreWork)
                {
                    dbDosage.ResultId = EXP_DIC_StageResult.NeedMoreWork;
                        resultChange = true;
                }
                if (concl.Code == (int)PW.Ncels.Database.Enums.CommissionConclusionType.DoNoRecommend)
                {
                    dbDosage.ResultId = EXP_DIC_StageResult.DoesNotRecommended;
                        resultChange = true;
                }
                if (concl.Code == (int)PW.Ncels.Database.Enums.CommissionConclusionType.KilledHimself)
                {
                    dbDosage.ResultId = EXP_DIC_StageResult.RemoveFromRegistration;
                        resultChange = true;
                }
                if (concl.Code == (int)PW.Ncels.Database.Enums.CommissionConclusionType.NeedExpertCouncilConclusion)
                {
                    dbDosage.ResultId = EXP_DIC_StageResult.NeedExpertCouncilConclusion;
                        resultChange = true;
                }
            }
            if (resultChange)
            {
                var r = new EXP_ExpertiseStageDosageResult();
                r.ResultDate = DateTime.Now;
                r.ResultId = dbDosage.ResultId;
                r.ResultCreatorId = employeeId;
                r.StageDosageId = dbDosage.Id;
                r.CommissionId = dbCommission.Id;
                db.EXP_ExpertiseStageDosageResult.Add(r);
            }

            var ncDosage = db.CommissionDrugDosageNeedCommissions.FirstOrDefault(x => x.DrugDosageId == dbDosage.DosageId && x.StageId == dbCommissionDrugDeclaration.StageId);
            if (ncDosage == null)
            {
                ncDosage = new CommissionDrugDosageNeedCommission();
                ncDosage.DrugDosageId = dbDosage.DosageId;
                ncDosage.StageId = dbCommissionDrugDeclaration.StageId;
                db.CommissionDrugDosageNeedCommissions.Add(ncDosage);
            }
            if (dbCommission.TypeId == (int)PW.Ncels.Database.Enums.CommissionType.ExpertCouncil)
            {
                if (concl.Code == (int)PW.Ncels.Database.Enums.CommissionConclusionType.NeedMoreWork)
                {
                    if (dbDosage.EXP_ExpertiseStage.StageId == EXP_DIC_StageInt.PharmacologicalExp)
                    {
                        ncDosage.IsNeedFmk = true;
                    }
                    else if (dbDosage.EXP_ExpertiseStage.StageId == EXP_DIC_StageInt.PharmaceuticalExp)
                    {
                        ncDosage.IsNeedFmc = true;
                    }
                    else
                    {
                        ncDosage.IsNeedEs = true;
                    }
                }
            }
            if (dbCommission.TypeId == (int)PW.Ncels.Database.Enums.CommissionType.PharmaceuticalCommission)
            {
                if (concl.Code == (int)PW.Ncels.Database.Enums.CommissionConclusionType.NeedMoreWork)
                {
                    ncDosage.IsNeedFmc = true;
                    ncDosage.IsNeedEs = false;
                }
                if (concl.Code == (int)PW.Ncels.Database.Enums.CommissionConclusionType.DoNoRecommend
                  || concl.Code == (int)PW.Ncels.Database.Enums.CommissionConclusionType.NeedExpertCouncilConclusion
                  || concl.Code == (int)PW.Ncels.Database.Enums.CommissionConclusionType.KilledHimself)
                {
                    ncDosage.IsNeedEs = true;
                    ncDosage.IsNeedFmc = false;
                }
                if (concl.Code == (int)PW.Ncels.Database.Enums.CommissionConclusionType.Recommend)
                {
                    ncDosage.IsNeedFmk = false;
                    ncDosage.IsNeedEs = false;
                }
            }
            if (dbCommission.TypeId == (int)PW.Ncels.Database.Enums.CommissionType.PharmacologicalCommission)
            {
                if (concl.Code == (int)PW.Ncels.Database.Enums.CommissionConclusionType.NeedMoreWork)
                {
                    ncDosage.IsNeedFmk = true;
                    ncDosage.IsNeedEs = false;
                }
                if (concl.Code == (int)PW.Ncels.Database.Enums.CommissionConclusionType.DoNoRecommend
                  || concl.Code == (int)PW.Ncels.Database.Enums.CommissionConclusionType.NeedExpertCouncilConclusion
                  || concl.Code == (int)PW.Ncels.Database.Enums.CommissionConclusionType.KilledHimself)
                {
                    ncDosage.IsNeedEs = true;
                    ncDosage.IsNeedFmk = false;
                }
                if (concl.Code == (int)PW.Ncels.Database.Enums.CommissionConclusionType.Recommend)
                {
                    ncDosage.IsNeedFmk = false;
                    ncDosage.IsNeedEs = false;
                }
            }

            ActionLogger.WriteInt(db, employeeId, "Повестка '"+ dbCommission.FullNumber+"': Добавлено заключение по заявке");
            db.SaveChanges();
            return Json(new { success = true, message = "Заключение поставлено" });
        }

        public ActionResult ExportFile2(int id, int type)
        {
            var dbCommission = db.Commissions.Single(x => x.Id == id);
            StiReport report = new StiReport();
            string name;
            if (type == 1)
            {
                name = "Повестка " + dbCommission.FullNumber + ".doc";
                if (dbCommission.TypeId == (int)(int)PW.Ncels.Database.Enums.CommissionType.ExpertCouncil)
                {
                    report.Load(Server.MapPath("../Reports/Commission/povestkaES.mrt"));
                }
                else if (dbCommission.TypeId == (int)(int)PW.Ncels.Database.Enums.CommissionType.PharmaceuticalCommission)
                {
                    report.Load(Server.MapPath("../Reports/Commission/povestkaFMC.mrt"));
                }
                else if (dbCommission.TypeId == (int)(int)PW.Ncels.Database.Enums.CommissionType.PharmacologicalCommission)
                {
                    report.Load(Server.MapPath("../Reports/Commission/povestkaFMK.mrt"));
                }
                else
                {
                    throw new Exception("only  commission type == (1|2|3)");
                }
            }
            else if (type == 2)
            {
                name = "Протокол " + dbCommission.FullNumber + ".doc";
                if (dbCommission.TypeId == (int)(int)PW.Ncels.Database.Enums.CommissionType.ExpertCouncil)
                {
                    report.Load(Server.MapPath("../Reports/Commission/protokolES.mrt"));
                }
                else if (dbCommission.TypeId == (int)(int)PW.Ncels.Database.Enums.CommissionType.PharmaceuticalCommission)
                {
                    report.Load(Server.MapPath("../Reports/Commission/protokolFMC.mrt"));
                }
                else if (dbCommission.TypeId == (int)(int)PW.Ncels.Database.Enums.CommissionType.PharmacologicalCommission)
                {
                    report.Load(Server.MapPath("../Reports/Commission/protokolFMK.mrt"));
                }
                else
                {
                    throw new Exception("only  commission type == (1|2|3)");
                }
            }
            else
            {
                throw new Exception("only type == (1|2)");
            }
            foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
            {
                data.ConnectionString = UserHelper.GetCnString();
            }
            if (report.Dictionary.Variables.Contains("CommissionId"))
            {
                report.Dictionary.Variables["CommissionId"].ValueObject = id;
            }
            if (report.Dictionary.Variables.Contains("OrganizationId"))
            {
                report.Dictionary.Variables["OrganizationId"].ValueObject = UserHelper.GetCurrentEmployee().OrganizationId;
            }
            report.Render(false);
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Word2007, stream);
            stream.Position = 0;
            return File(stream, "application/word", name);
        }
    }
}