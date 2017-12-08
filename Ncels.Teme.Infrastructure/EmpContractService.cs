using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ncels.Teme.Contracts;
using Ncels.Teme.Contracts.ViewModels;
using PW.Ncels.Database.DataModel;

namespace Ncels.Teme.Infrastructure
{
    public class EmpContractService : IEmpContractService
    {
        private IUnitOfWork _uow;

        private const string Manufactur = "Производитель";
        private const string Declarant = "Заявитель";
        private const string Payer = "Третье лицо";

        private const string ManufacturCode = "Производитель";
        private const string DeclarantCode = "Заявитель";
        private const string PayerCode = "Третье лицо";

        public EmpContractService()
        {
            // TODO - через DI
            _uow = new UnitOfWork();
        }

        public IQueryable<EmpContractViewModel> GetContracts()
        {
            return _uow.GetQueryable<EMP_Contract>()
                //.Where(x => x.DeclarantId != null && x.DeclarantContactId != null)
                .Where(x => x.ManufacturId != null && x.ManufacturContactId != null)
                .Where(x => x.DeclarantId != null && x.DeclarantContactId != null)
                .Where(x => x.PayerId != null && x.PayerContactId != null)
                .Select(x => new EmpContractViewModel
                {
                    Id = x.Id,
                    Number = x.Number,
                    CreateDate = x.CreatedDate,
                    //StageStatusCode = x.
                });
        }

        public EmpContractDetailsViewModel GetContractDetailsViewModel(Guid contractId)
        {
            var contract = _uow.GetQueryable<EMP_Contract>().FirstOrDefault(x => x.Id == contractId);
            if (contract == null) return new EmpContractDetailsViewModel();

            var manufacturer = GetDeclarant(contract.OBK_DeclarantManufactur, contract.OBK_DeclarantContactManufactur);
            manufacturer.Title = Manufactur;
            manufacturer.Code = ManufacturCode;

            var declarant = GetDeclarant(contract.OBK_Declarant, contract.OBK_DeclarantContact);
            declarant.Title = Declarant;
            declarant.Code = DeclarantCode;

            var payer = GetDeclarant(contract.OBK_DeclarantPayer, contract.OBK_DeclarantContactPayer);
            payer.Code = PayerCode;
            var payers = new List<SelectListItem>
            {
                new SelectListItem {Value = ManufacturCode, Text = Manufactur},
                new SelectListItem {Value = DeclarantCode, Text = Declarant},
                new SelectListItem {Value = PayerCode, Text = Payer}
            };
            var selectPayer = payers.FirstOrDefault(x => x.Value == contract.ChoosePayer);
            if (selectPayer != null) selectPayer.Selected = true;

            return new EmpContractDetailsViewModel
            {
                Id = contract.Id,
                Manufacturer = manufacturer,
                Declarant = declarant,
                Payer = payer,
                Payers = payers,
                ChoosPayer = contract.ChoosePayer,
                MedicalDeviceName = contract.MedicalDeviceName,
                WorkCosts = contract.EMP_CostWorks.Select(x => new EmpContractWorkCostViewModel
                {
                    WorkName = x.EMP_Ref_PriceList.EMP_Ref_ServiceType.NameRu,
                    Price = x.Price ?? 0,
                    Count = x.Count ?? 0
                }).ToList()
            };
        }

        private EmpContractDeclarantViewModel GetDeclarant(OBK_Declarant declarant, OBK_DeclarantContact declarantContact)
        {
            var boolValues = new List<SelectListItem>
            {
                new SelectListItem {Selected = false, Text = "Нет", Value = false.ToString()},
                new SelectListItem {Selected = false, Text = "Да", Value = true.ToString()}
            };
            //var selecteed = boolValues.FirstOrDefault(x=>x.Value==declarantContact.is)

            return new EmpContractDeclarantViewModel
            {
                IsResident = declarant.IsResident,
                NameKz = declarant.NameKz,
                NameRu = declarant.NameRu,
                NameEn = declarant.NameEn,
                Countries = GetDictionaryList("Country", declarant.CountryId),
                Bin = declarant.Bin,
                OrganizationForms = GetDictionaryList("OpfType", declarant.OrganizationFormId),
                NonResidentsNames = GetNonResidentNameList(declarant.CountryId),
                BossLastName = declarantContact.BossLastName,
                BossFirstName = declarantContact.BossFirstName,
                BossMiddleName = declarantContact.BossMiddleName,
                BossPositionRu = declarantContact.BossPosition,
                BossPositionKz = declarantContact.BossPositionKz,
                AddressLegal = declarantContact.AddressLegalRu,
                AddressFact = declarantContact.AddressFact,
                Phone = declarantContact.Phone,
                Email = declarantContact.Email,
                BankName = declarantContact.BankNameRu,
                BankIik = declarantContact.BankIik,
                Currencies = GetDictionaryList("Currency", declarantContact.CurrencyId),
                BankBik = declarantContact.BankBik,
                Iin = declarant.Iin,
                BankAccount = declarantContact.BankAccount,

                //BoolValues = 
            };
        }

        public IEnumerable<SelectListItem> GetSignerList(EMP_Contract contract)
        {
            string[] signerCodes = { "ncels_deputyceo", "ncels_ceo" };
            return _uow.GetQueryable<Employee>().Where(e => signerCodes.Contains(e.Position.Code)).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Position.ShortName + " " + x.ShortName,
                Selected = x.Id == contract.Signer
            });
        }

        private IEnumerable<SelectListItem> GetNonResidentNameList(Guid? countryId)
        {
            return countryId != null
                ? _uow.GetQueryable<OBK_Declarant>()
                    .Where(x => x.CountryId == countryId && x.IsConfirmed && !x.IsResident)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.NameRu,
                        Selected = x.CountryId == countryId
                    }).ToList()
                : new List<SelectListItem> { new SelectListItem { Value = Guid.Empty.ToString(), Text = "Нет данных", Selected = true } };
        }

        private IEnumerable<SelectListItem> GetDictionaryList(string type, Guid? selectedItem)
        {
            return _uow.GetQueryable<Dictionary>().Where(x => x.Type == type)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                    Selected = x.Id == selectedItem
                });
        }
    }
}
