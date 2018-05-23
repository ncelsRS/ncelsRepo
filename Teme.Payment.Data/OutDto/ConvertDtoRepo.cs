using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Payment.Data.OutDto
{
    public class ConvertDtoRepo : IConvertDtoRepo
    {
        public PaymentDto ConvertEntityToPayment(Shared.Data.Context.Payment payment)
        {
            return new PaymentDto()
            {
                Id = payment.Id,

                //WorkflowId = contract?.WorkflowId,
                //ContractType = contract.ContractType,
                //ContractForm = contract.ContractForm,
                //HolderType = contract.HolderType,
                //ChoosePayer = contract.ChoosePayer,
                //ContractScope = contract?.ContractScope,
                //Number = contract?.Number,
                //DeclarantIsManufacture = contract.DeclarantIsManufacture,
                //MedicalDeviceNameRu = contract?.MedicalDeviceNameRu,
                //MedicalDeviceNameKz = contract?.MedicalDeviceNameKz,
                //DeclarantId = contract?.DeclarantId,
                //DeclarantDetailId = contract?.DeclarantDetailId,
                //ManufacturId = contract?.ManufacturId,
                //ManufacturDetailId = contract?.ManufacturDetailId,
                //PayerId = contract?.PayerId,
                //PayerDetailId = contract?.PayerDetailId,
            };
        }
    }
}
