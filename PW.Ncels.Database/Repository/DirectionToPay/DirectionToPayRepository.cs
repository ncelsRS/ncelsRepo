using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Interfaces;

namespace PW.Ncels.Database.Repository.DirectionToPay
{
    public class DirectionToPayRepository : ARepositoryGeneric<EXP_DirectionToPays>
    {
        public DirectionToPayRepository(bool isProxy = true):base(isProxy)
        { }
        
        public IQueryable<EXP_DirectionToPaysView> GetDirectionToPaysViews(Expression<Func<EXP_DirectionToPaysView, bool>> filter = null)
        {
            if (filter != null) return AppContext.EXP_DirectionToPaysView.Where(filter);
            return AppContext.EXP_DirectionToPaysView.AsQueryable();
        }

        public IQueryable<EXP_DrugDeclarationDirectionToPayView> GetDrugDeclarationViews(Expression<Func<EXP_DrugDeclarationDirectionToPayView, bool>> filter = null)
        {
            if (filter != null) return AppContext.EXP_DrugDeclarationDirectionToPayView.Where(filter);
            return AppContext.EXP_DrugDeclarationDirectionToPayView.AsQueryable();
        }

        public IQueryable<EXP_DrugDeclaration> GetDrugDeclarations(Expression<Func<EXP_DrugDeclaration, bool>> filter = null)
        {
            if (filter != null) return AppContext.EXP_DrugDeclaration.Where(filter);
            return AppContext.EXP_DrugDeclaration.AsQueryable();
        }

        public IQueryable<EXP_PriceListDirectionToPayView> GetPriceListViews(Expression<Func<EXP_PriceListDirectionToPayView, bool>> filter = null)
        {
            if (filter != null) return AppContext.EXP_PriceListDirectionToPayView.Where(filter);
            return AppContext.EXP_PriceListDirectionToPayView.AsQueryable();
        }

        public IQueryable<EXP_PriceList> GetPriceList(Expression<Func<EXP_PriceList, bool>> filter = null)
        {
            if (filter != null) return AppContext.EXP_PriceList.Where(filter);
            return AppContext.EXP_PriceList.AsQueryable();
        }

        public EXP_DrugDeclaration GetDrugDeclarationById(Guid modelId)
        {
            return AppContext.EXP_DrugDeclaration.FirstOrDefault(e => e.Id == modelId);
        }

        public Organization GetPayerByDirectionId(Guid directionId)
        {
            Organization payer = null;
            var contractId = AppContext.EXP_DrugDeclarationDirectionToPayView.Where(d => d.DirectionToPayId == directionId)
                .Select(d => d.ContractId).FirstOrDefault();
            if (contractId != null)
            {

                payer = GetPayerByContractId(contractId.Value);


                /*
                var payerAnon = AppContext.Contracts.Where(c => c.Id == contractId.Value)
                    .Select(c => c.PayerOrganization)
                    .Join(AppContext.Dictionaries, o => o.BankCurencyDicId, d => d.Id,
                        (o, d) => new
                        {
                            Org = o,
                            CurrencyName = d.Name,
                        })
                    .Join(AppContext.Dictionaries, at => at.Org.CountryDicId, d => d.Id,
                        (o, d) => new
                        {
                            Org = o,
                            ContryName = d.Name
                        }).FirstOrDefault();
                if (payerAnon != null)
                {
                    payer = payerAnon.Org.Org;
                    payer.BankCurencyName = payerAnon.Org.CurrencyName;
                    payer.CountryName = payerAnon.ContryName;
                }
                */
            }


            return payer;
        }

        public Organization GetPayerByContractId(Guid contractId)
        {
            Organization payer = null;

            var payerAnon = AppContext.Contracts.Where(c => c.Id == contractId)
                .Select(c => c.PayerOrganization).FirstOrDefault();
//                .Join(AppContext.Dictionaries.DefaultIfEmpty(), o => o.BankCurencyDicId, d => d.Id,
//                    (o, d) => new
//                    {
//                        Org = o,
//                        CurrencyName = d.Name,
//                    })
//                .Join(AppContext.Dictionaries, at => at.Org.CountryDicId, d => d.Id,
//                    (o, d) => new
//                    {
//                        Org = o,
//                        ContryName = d.Name
//                    }).FirstOrDefault();
            if (payerAnon != null)
            {
                payer = payerAnon;
                if (payerAnon.BankCurencyDicId != null)
                {
                    payer.BankCurencyName = AppContext.Dictionaries.Where(d => d.Id == payerAnon.BankCurencyDicId)
                        .Select(d => d.Name)
                        .FirstOrDefault();
                }
                if (payerAnon.CountryDicId != null)
                {
                    payer.CountryName = AppContext.Dictionaries.Where(d => d.Id == payerAnon.CountryDicId)
                        .Select(d => d.Name)
                        .FirstOrDefault();
                }
            }

            return payer;
        }

        public Dictionary GetStatusByCode(string code)
        {
            var directionToPay =
                AppContext.Dictionaries.FirstOrDefault(d => d.Type == Dictionary.ExpDirectionToPayStatus.DicCode && d.Code == code);
            return directionToPay;
        }

        public IQueryable<Dictionary> GetDirectionToPayStatuses()
        {
            return AppContext.Dictionaries.Where(
                d => d.Type == Dictionary.ExpDirectionToPayStatus.DicCode && d.ExpireDate == null);
        }

        public IQueryable<Dictionary> GetOrgManufactureTypes()
        {
            return AppContext.Dictionaries.Where(
                d => d.Type == Dictionary.OrgManufactureType.DicCode && d.ExpireDate == null);
        }

        public IQueryable<Employee> GetEmployList()
        {
            return AppContext.Employees.AsQueryable();
        }

        public EXP_PriceListDrugTypeMapping GetPriceListMappingByDrugFormCode(string drugFormCode)
        {
            return AppContext.EXP_PriceListDrugTypeMapping.FirstOrDefault(p => p.DrugTypeCode == drugFormCode);
        }

        public string GetCountryCode(Guid countryId)
        {
            return AppContext.Dictionaries.Where(d => d.Id == countryId).Select(d => d.Code).FirstOrDefault();
        }


        public void RunSqlScript(string sqlCmd)
        {
            AppContext.Database.ExecuteSqlCommand(sqlCmd);
        }

        #region DirectionPriceList

        public IQueryable<EXP_DirectionToPays_PriceList> GetDirectionPriceList(Expression<Func<EXP_DirectionToPays_PriceList, bool>> filter = null)
        {
            return AppContext.EXP_DirectionToPays_PriceList.Where(filter);
        }

        public void InsertDirectionPriceList(EXP_DirectionToPays_PriceList model)
        {
            AppContext.EXP_DirectionToPays_PriceList.Add(model);
        }

        public void UpdateDirectionPriceList(EXP_DirectionToPays_PriceList model)
        {
            AppContext.EXP_DirectionToPays_PriceList.Attach(model);
            AppContext.Entry(model).State = EntityState.Modified;
        }

        public void DeleteDirectionPriceList(Guid directionToPayId, Guid priceListId)
        {
            EXP_DirectionToPays_PriceList entityToDelete = AppContext.EXP_DirectionToPays_PriceList
                .FirstOrDefault(dp => dp.DirectionToPayId == directionToPayId && dp.PriceListId == priceListId);
            DeleteDirectionPriceList(entityToDelete);
        }

        public void DeleteDirectionPriceList(EXP_DirectionToPays_PriceList model)
        {
            AppContext.Entry(model).State = EntityState.Modified;

            if (AppContext.Entry(model).State == EntityState.Detached)
            {
                AppContext.EXP_DirectionToPays_PriceList.Attach(model);
            }

            var entToDelete = model as ISoftDeleteEntity;
            if (entToDelete != null)
            {
                entToDelete.DeleteDate = DateTime.Now;
                UpdateDirectionPriceList(model);
            }
            else
            {
                AppContext.EXP_DirectionToPays_PriceList.Remove(model);
            }
        }

        #endregion
    }
}