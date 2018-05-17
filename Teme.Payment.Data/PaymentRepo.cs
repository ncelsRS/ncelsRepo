using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Repos.PaymentRepo;
using Context = Teme.Shared.Data.Context;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Teme.Payment.Data.DTO;

namespace Teme.Payment.Data
{
    public class PaymentRepo : BasePaymentRepo, IPaymentRepo
    {
        public PaymentRepo(Context.TemeContext context) : base(context)
        {
        }
        public async Task CreatePayment(Context.Payment payment)
        {
            Context.Payments.Add(payment);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Автосохранение платежа
        /// </summary>
        /// <param name="data">Type</param>
        /// <param name="fieldName">наименование поля</param>
        /// <param name="value">значение</param>
        /// <param name="id">id платежа</param>
        /// <returns></returns>
        public async Task UpdateContractSql(Type data, string fieldName, object value, int id)
        {
            await Context.Database.ExecuteSqlCommandAsync(string.Format(@"UPDATE {0} SET {1} = '{2}' WHERE Id = {3}", data.Name + "s", fieldName, value, id));
        }

        /// <summary>
        /// Получение списка заявок на платеж
        /// </summary>
        /// <param name="contractId">Id Договора</param>
        /// <returns></returns>
        public async Task<IQueryable<Context.Payment>> GetListPayments(int contractId)
        {
            return await Task.Run(() => Context.Payments.Where(e => !e.isDeleted && e.ContractId == contractId));
        }

        public async Task<PaymentDto> GetPayment(int id)
        {

            return await Context.Payments.Where(e => e.Id == id)
                .Select(e => new PaymentDto
                {
                    Id = e.Id,
                    ApplicationAreaKz = e.ApplicationAreaKz,
                    ApplicationAreaRu = e.ApplicationAreaRu,
                    AppointmentKz = e.AppointmentKz,
                    AppointmentRu = e.AppointmentRu,
                    CardNumber = e.CardNumber,
                    ChangesMade = e.ChangesMade,
                    DegreeRiskClassId = e.DegreeRiskClassId,
                    ContractForm = e.ContractForm,
                    ContractId = e.ContractId,
                    IsBlank = e.IsBlank,
                    IsClosedSystem = e.IsClosedSystem,
                    IsDiagnostics = e.IsDiagnostics,
                    IsMeasures = e.IsMeasures,
                    IsPresenceMedicinalProduct = e.IsPresenceMedicinalProduct,
                    IsStyryl = e.IsStyryl,
                    IsTypeImnMt = e.IsTypeImnMt,
                    NameKz = e.NameKz,
                    NameRu = e.NameRu,
                    NumberModificationImn = e.NumberModificationImn,
                    RationaleManufacturer = e.RationaleManufacturer,
                    RevisionBeforeChanges = e.RevisionBeforeChanges,
                    TradeName = e.TradeName,
                    PaymentEquipmentDtos = e.PaymentEquipments.Select(x => new PaymentEquipmentDto
                    {
                        Id = x.Id,
                        Code = x.Code,
                        CountryId = x.CountryId,
                        EquipmentTypeId = x.EquipmentTypeId,
                        Manufacturer = x.Manufacturer,
                        Model = x.Model,
                        Name = x.Name
                    }),
                    PaymentPackagingDtos = e.PaymentPackaging.Select(x => new PaymentPackagingDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        NumberUnitsInBox = x.NumberUnitsInBox,
                        PackagingtTypeId = x.PackagingtTypeId,
                        ShortDescription = x.ShortDescription,
                        SizeHeight = x.SizeHeight,
                        SizeLength = x.SizeLength,
                        SizeMeasureId = x.SizeMeasureId,
                        SizeWidth = x.SizeWidth,
                        VolumeMeasureId = x.VolumeMeasureId,
                        VolumeValue = x.VolumeValue
                    }),
                    PaymentPlatformDtos = e.PaymentPlatforms.Select(x => new PaymentPlatformDto
                    {
                        Id = x.Id,
                        CountryId = x.CountryId,
                        FactAddress = x.FactAddress,
                        LegalAddress = x.LegalAddress,
                        NameEn = x.NameEn,
                        NameKz = x.NameKz,
                        NameRu = x.NameRu,
                    })

                })
                .FirstOrDefaultAsync();
        }
    }
}
