using Teme.Shared.Data.Context;

namespace Teme.Contract.Data.DTO
{
    public interface IConvertDtoRepo
    {
        DeclarantDto ConvertEntityToDeclarant(Declarant decalarant);
        DeclarantDetailDto ConvertEntityToDeclarantDetail(DeclarantDetail decalarantDetail);
    }
}