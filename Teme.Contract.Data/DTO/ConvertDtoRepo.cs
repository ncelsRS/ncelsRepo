using System.Collections.Generic;
using System.Linq;
using Teme.Shared.Data.Context;

namespace Teme.Contract.Data.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class ConvertDtoRepo : IConvertDtoRepo
    {
        public DeclarantDto ConvertEntityToDeclarant(Declarant decalarant)
        {
            return new DeclarantDto()
            {
                Id = decalarant.Id,
                IdNumber = decalarant.IdNumber,
                NameRu = decalarant.NameRu,
                NameKz = decalarant.NameKz,
                NameEn = decalarant.NameEn,
                CountryId = decalarant?.CountryId,
                OrganizationFormId = decalarant?.OrganizationFormId,
                IsResident = decalarant.IsResident,
                DeclarantDetailDto = ConvertEntityToDeclarantDetail(decalarant.DeclarantDetails.OrderByDescending(e => e.DateCreate).FirstOrDefault())
            };
        }

        public DeclarantDetailDto ConvertEntityToDeclarantDetail(DeclarantDetail decalarantDetail)
        {
            return new DeclarantDetailDto()
            {
                Id = decalarantDetail.Id,
                LegalAddress = decalarantDetail.LegalAddress,
                FactAddress = decalarantDetail.FactAddress,
                BossLastName = decalarantDetail.BossLastName,
                BossFirstName = decalarantDetail.BossFirstName,
                BossMiddleName = decalarantDetail.BossMiddleName,
                BossPositionRu = decalarantDetail.BossPositionRu,
                BossPositionKz = decalarantDetail.BossPositionKz,
                BankName = decalarantDetail.BankName,
                BankIik = decalarantDetail.BankIik,
                BankSwift = decalarantDetail.BankSwift,
                BankBin = decalarantDetail.BankBin,
                CurrencyId = decalarantDetail?.CurrencyId,
                Phone = decalarantDetail.Phone,
                Phone2 = decalarantDetail.Phone2,
                Email = decalarantDetail.Email,
                DeclarantDocType = decalarantDetail.DeclarantDocType,
                DeclarantDocWithoutNumber = decalarantDetail.DeclarantDocWithoutNumber,
                DeclarantDocNumber = decalarantDetail.DeclarantDocNumber,
                DeclarantDocStartDate = decalarantDetail.DeclarantDocStartDate,
                DeclarantDocEndDate = decalarantDetail.DeclarantDocEndDate,
                DeclarantPerpetualDoc = decalarantDetail.DeclarantPerpetualDoc
            };
        }

        public ContractDto ConvertEntityToContract(Shared.Data.Context.Contract contract)
        {
            return new ContractDto()
            {
                Id = contract.Id,
                WorkflowId = contract?.WorkflowId,
                ContractType = contract.ContractType,
                ContractForm = contract.ContractForm,
                HolderType = contract.HolderType,
                ChoosePayer = contract.ChoosePayer,
                ContractScope = contract?.ContractScope,
                Number = contract?.Number,
                DeclarantIsManufacture = contract.DeclarantIsManufacture,
                MedicalDeviceNameRu = contract?.MedicalDeviceNameRu,
                MedicalDeviceNameKz = contract?.MedicalDeviceNameKz,
                DeclarantId = contract?.DeclarantId,
                DeclarantDetailId = contract?.DeclarantDetailId,
                ManufacturId = contract?.ManufacturId,
                ManufacturDetailId = contract?.ManufacturDetailId,
                PayerId = contract?.PayerId,
                PayerDetailId = contract?.PayerDetailId,
                CostWorkDto = contract?.CostWorks != null ? ConvertEntityToCostWork(contract.CostWorks) : null
            };
        }

        public IEnumerable<CostWorkDto> ConvertEntityToCostWork(ICollection<CostWork> costs)
        {
            return costs.Select(e => new CostWorkDto{
                Id = e.Id,
                PriceListId = e?.PriceListId,
                ContractId = e.ContractId,
                Count = e.Count,
                IsImport = e.IsImport,
                PriceWithValueAddedTax = e.PriceWithValueAddedTax,
                TotalPriceWithValueAddedTax = e.TotalPriceWithValueAddedTax
            });
        }
    }
}
