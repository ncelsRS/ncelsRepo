﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Teme.Admin.Data.Model;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Context.References;

namespace Teme.Admin.Data.Repository
{
    public class ReferenceRepo : IReferenceRepo
    {
        private readonly TemeContext _context;

        public ReferenceRepo(TemeContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Условия хранения(справочник)
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Ref_StorageCondition>> StorageConditionAsync() => await _context.Ref_StorageConditions.Where(e => !e.IsDeleted).ToListAsync();
        
        /// <summary>
        /// Классификатор областей медицинского применения медицинских изделий(справочник)
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Ref_ClassifierMedicalArea>> ClassifierMedicalAreaAsync() => await _context.Ref_ClassifierMedicalAreas.Where(e => !e.IsDeleted).ToListAsync();

        /// <summary>
        /// Код Номенклатуры медицинских изделий Республики Казахстан 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IQueryable<Ref_NomenclatureCodeMedProduct>> NomenclatureCodeMedProductAsync(string name, string culture)
        {
            Expression<Func<Ref_NomenclatureCodeMedProduct, bool>> nomeclature;
            if(name != null)
            {
                switch (culture)
                {
                    case "ru":
                        nomeclature = x => x.NameRu.Contains(name) && !x.IsDeleted;
                        break;
                    case "kz":
                        nomeclature = x => x.NameKz.Contains(name) && !x.IsDeleted;
                        break;
                    default:
                        nomeclature = x => x.Id == 0;
                        break;
                }
            }
            else
            {
                nomeclature = x => !x.IsDeleted;
            }
            return await Task.Run(() => _context.Ref_NomenclatureCodeMedProducts.AsQueryable().Where(nomeclature).Take(10));
        }

        /// <summary>
        /// Класс в зависимости от степени потенциального риска применения(справочник)
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Ref_DegreeRiskClass>> DegreeRiskClassAsync() => await _context.Ref_DegreeRiskClasses.Where(e => !e.IsDeleted).ToArrayAsync();

        /// <summary>
        /// Организационная форма(справочник)
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Ref_OrganizationForm>> OrganizationFormAsync() => await _context.Ref_OrganizationForms.Where(e => !e.IsDeleted && e.IsConfirmed).ToArrayAsync();

        /// <summary>
        /// Добавить новую Организационную форума(справочник)
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveOrganizationFormAsync(string nameRu, string nameKz, bool isConfirmed)
        {
            var orgForm = new Ref_OrganizationForm()
            {
                NameRu = nameRu,
                NameKz = nameKz,
                IsConfirmed = isConfirmed
            };
            _context.Ref_OrganizationForms.Add(orgForm);
            await _context.SaveChangesAsync();
            return orgForm.Id;
        }

        /// <summary>
        /// Банки(справочник)
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Ref_Bank>> BanksAsync() => await _context.Ref_Banks.Where(e => !e.IsDeleted && e.IsConfirmed).ToArrayAsync();

        /// <summary>
        /// Добавить новый Банк(справочник)
        /// </summary>
        /// <param name="nameRu"></param>
        /// <param name="nameKz"></param>
        /// <param name="isConfirmed"></param>
        /// <returns></returns>
        public async Task<int> SaveBankAsync(string nameRu, string nameKz, bool isConfirmed)
        {
            var bank = new Ref_Bank()
            {
                NameRu = nameRu,
                NameKz = nameKz,
                IsConfirmed = isConfirmed
            };
            _context.Ref_Banks.Add(bank);
            await _context.SaveChangesAsync();
            return bank.Id;
        }

        /// <summary>
        /// Валюта
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Ref_Currency>> CurrencyAsync() => await _context.Ref_Currencies.Where(e => !e.IsDeleted).ToListAsync();

        /// <summary>
        /// Страны
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Ref_Country>> CountryAsync() => await _context.Ref_Countries.Where(e => !e.IsDeleted).ToListAsync();

        /// <summary>
        /// Тип заявки для калькулятора
        /// </summary>
        /// <param name="contractScope">scope договора</param>
        /// <returns></returns>
        public async Task<IQueryable<Ref_ApplicationType>> CalculatorApplicationType(string contractScope, string contractForm)
            => await Task.Run(() => _context.Ref_ApplicationTypes.AsQueryable().Where(e => !e.IsDeleted && e.ContractForm.Contains(contractForm) && e.Code.Contains(contractScope)));

        /// <summary>
        /// Тип услуги для калькулятора
        /// </summary>
        /// <returns></returns>
        public async Task<IQueryable<Ref_ServiceType>> CalculatorServiceType(int applicationTypeId) 
            => await Task.Run(() => _context.Ref_ServiceTypes.Include(e => e.Children).AsQueryable().Where(x => !x.IsDeleted && x.ApplicationTypeId == applicationTypeId));

        /// <summary>
        /// Налог на добавленную стоимость 
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public async Task<Ref_ValueAddedTax> GetValueAddedTax(int year) => await _context.Ref_ValueAddedTaxes.FirstOrDefaultAsync(e => !e.IsDeleted && e.Year == year);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isImport"></param>
        /// <returns></returns>
        public async Task<Ref_PriceList> GetPriceList(int id, bool isImport) => await _context.Ref_PriceLists.FirstOrDefaultAsync(e => !e.IsDeleted && e.ServiceTypeId == id && e.IsImport == isImport);
    }
}