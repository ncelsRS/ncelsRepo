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
                CountryId = decalarant.CountryId,
                OrganizationFormId = decalarant.OrganizationFormId,
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
                CurrencyId = decalarantDetail.CurrencyId,
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
    }
}
