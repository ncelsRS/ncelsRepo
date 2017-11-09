using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models.OBK;
using PW.Ncels.Database.Notifications;

namespace PW.Ncels.Database.Repository.OBK
{
    public class OBKPaymentRepository : ARepository
    {
        public OBKPaymentRepository()
        {
            AppContext = CreateDatabaseContext();
        }

        public OBKPaymentRepository(bool isProxy)
        {
            AppContext = CreateDatabaseContext(isProxy);
        }

        public OBKPaymentRepository(ncelsEntities context) : base(context) { }


        public void SavePayments(Guid contractId)
        {
            var contract = AppContext.OBK_Contract.FirstOrDefault(x => x.Id == contractId);
            var payment = AppContext.OBK_DirectionToPayments.FirstOrDefault(e => e.ContractId == contractId);
            var pay = new OBK_DirectionToPayments();
            if (contract != null)
            {
                if (payment == null)
                {
                    pay.Id = Guid.NewGuid();
                    pay.CreateDate = DateTime.Now;
                    pay.ContractId = contractId;
                    pay.CreateEmployeeId = UserHelper.GetCurrentEmployee().Id;
                    pay.CreateEmployeeValue = UserHelper.GetCurrentEmployee().DisplayName;
                    pay.DirectionDate = DateTime.Now;
                    pay.Number = contract.Number;
                    pay.PayerId = contract.OBK_Declarant.Id;
                    pay.PayerValue = contract.OBK_Declarant.NameRu;
                    pay.IsDeleted = false;
                    pay.TotalPrice = GetTotalPriceCount(contractId);
                    pay.StatusId = GetPaymentStatus(OBK_Ref_PaymentStatus.OnFormation).Id;
                    pay.OBK_DirectionSignData = new OBK_DirectionSignData
                    {
                        DirectionToPaymentId = pay.Id,
                        ChiefAccountantId = null,//Guid.Parse("E1EE3658-0C35-41EB-99FD-FDDC4D07CEC4"),
                        ExecutorId = null//Guid.Parse("55377FAC-A5F0-4093-BBB6-18BD28E53BE1")
                    };
                }
                else
                {
                    pay.Id = payment.Id;
                    //pay.CreateDate = DateTime.Now;
                    pay.ContractId = payment.ContractId;
                    pay.CreateEmployeeId = UserHelper.GetCurrentEmployee().Id;
                    pay.CreateEmployeeValue = UserHelper.GetCurrentEmployee().DisplayName;
                    pay.DirectionDate = DateTime.Now;
                    pay.Number = payment.Number;
                    pay.PayerId = payment.OBK_Declarant.Id;
                    pay.PayerValue = payment.OBK_Declarant.NameRu;
                    pay.IsDeleted = false;
                    pay.TotalPrice = GetTotalPriceCount(contractId);
                    pay.StatusId = payment.StatusId;
                    pay.OBK_DirectionSignData = payment.OBK_DirectionSignData;
                }
                AppContext.OBK_DirectionToPayments.Add(pay);
                AppContext.SaveChanges();
            }
        }
        /// <summary>
        /// расчет стоимости с НДС
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public decimal GetTotalPriceCount(Guid contractId)
        {
            var priceCounts = AppContext.OBK_ContractPrice.Where(e => e.ContractId == contractId).ToList();
            var tPrice = priceCounts.Sum(e => Math.Round(Convert.ToDecimal(TaxHelper.GetCalculationTax(e.OBK_Ref_PriceList.Price) * e.Count), 2));
            return tPrice;
        }

        public IQueryable<OBK_DirectionToPaymentsView> GetDirectionToPaymentsList(Guid organizationId)
        {
            var data = AppContext.OBK_DirectionToPaymentsView
                .Where(e => e.IsDeleted == false && e.ExpertOrganization == organizationId);
            return data;
        }

        public OBK_Contract GetContract(Guid id)
        {
            return AppContext.OBK_Contract.FirstOrDefault(e => e.Id == id);
        }

        public OBK_Ref_PaymentStatus GetPaymentStatus(string code)
        {
            return AppContext.OBK_Ref_PaymentStatus.First(e => e.Code == code);
        }

        public OBK_DirectionToPayments GetDirectionToPayments(Guid contractId)
        {
            return AppContext.OBK_DirectionToPayments.FirstOrDefault(e => e.ContractId == contractId);
        }

        public Guid GetContractIdGuid(Guid id)
        {
            var declaration = AppContext.OBK_AssessmentDeclaration.FirstOrDefault(e => e.Id == id);
            return declaration.Id;
        }

        public OBK_DirectionSignData GetDirectionSignData(Guid paymentId)
        {
            return AppContext.OBK_DirectionSignData.FirstOrDefault(e => e.DirectionToPaymentId == paymentId);
        }

        /// <summary>
        /// Отправка руководителю для подписания счета на оплату
        /// </summary>
        /// <param name="chiefId"></param>
        /// <param name="id"></param>
        public void SendToChief(Guid chiefId, Guid id)
        {
            var directSignData = AppContext.OBK_DirectionSignData.FirstOrDefault(e => e.DirectionToPaymentId == id);
            if(directSignData!=null)
                directSignData.ChiefAccountantId = chiefId;
            AppContext.SaveChanges();
            new NotificationManager().SendNotificationFromCompany(
                string.Format("К Вам на подписание поступил счет на оплату №{0}", directSignData?.OBK_DirectionToPayments.InvoiceNumber1C),
                ObjectType.Unknown, directSignData.DirectionToPaymentId.ToString(), (Guid)directSignData.ChiefAccountantId);
        }

        public void SendInvoiceToDeclarant(Guid id)
        {
            var directionPay = AppContext.OBK_DirectionToPayments.First(e => e.Id == id);
            directionPay.StatusId = GetPaymentStatus(OBK_Ref_PaymentStatus.SendToPayment).Id;
            directionPay.OBK_Contract.Status = CodeConstManager.STATUS_OBK_EXPECTED_PAYMENT;
            AppContext.SaveChanges();
            //отправка уведоления
            new NotificationManager().SendNotificationFromCompany(
                string.Format("По Вашему договору №{0} поступил счет на оплату", directionPay.OBK_Contract.Number),
                ObjectType.ObkContract, directionPay.OBK_Contract.Id.ToString(), (Guid) directionPay.OBK_Contract.EmployeeId);
        }
        /// <summary>
        /// НДС для формирования печатной формы
        /// </summary>
        /// <param name="nds"></param>
        /// <returns></returns>
        public decimal GetTotalPriceNDS(decimal nds)
        {
            return Math.Round(TaxHelper.GetTaxWithPrice(nds), 2);
        }

        public Employee GetEmpoloyee(Guid userId)
        {
            return AppContext.Employees.FirstOrDefault(e=>e.Id == userId);
        }

        public Dictionary GetDictionary(Guid? id)
        {
            if (id == null)
                return null;
            return AppContext.Dictionaries.FirstOrDefault(e => e.Id == id);
        }

        public string GetSignData(Guid id)
        {
            var payment = AppContext.OBK_DirectionToPayments.FirstOrDefault(e => e.Id == id);
            var contract = AppContext.OBK_Contract.FirstOrDefault(e => e.Id == payment.ContractId);
            var unitsBank = AppContext.UnitsBanks.FirstOrDefault(e => e.UnitsId == contract.ExpertOrganization);

            //if (payment?.OBK_DirectionSignData.ExecutorSign != null)
            //{
            //    var xmlData = SerializeHelper.SerializeDataContract(payment.OBK_DirectionSignData.ExecutorSign);
            //    return xmlData.Replace("utf-16", "utf-8");
            //}
            //else
            //{
            //}
            List<ContractPriceSignData> contractPriceSign = AppContext.OBK_ContractPrice
                .Where(e => e.ContractId == contract.Id)
                .Select(e => new ContractPriceSignData
                {
                    ContractPriceName = e.OBK_RS_Products.NameRu,
                    ContractPriceDicName = e.OBK_Ref_PriceList.NameRu,
                    ContractPrice = e.OBK_Ref_PriceList.Price * TaxHelper.GetNdsRef() + e.OBK_Ref_PriceList.Price,
                    ContractPriceCount = e.Count,
                    ContractPriceTotal = (e.OBK_Ref_PriceList.Price * TaxHelper.GetNdsRef() + e.OBK_Ref_PriceList.Price) * e.Count
                })
                .ToList();
            var result = new OBKPaymentSignData
            {
                Id = id,
                ContractId = payment?.ContractId,
                ContactNumber = contract?.Number,
                //ContactStartDate = contract?.StartDate,
                //ContactTypeName = contract?.OBK_Ref_Type.NameRu,
                //UnitsName = contract?.Unit.Name,
                //UnitsAddress = contract?.Unit.LegalAddress,
                //UnitsPhone = contract?.Unit.Phone,
                //UnitsBin = contract?.Unit.Bin,
                //UnitsIIk = unitsBank?.IIK,
                //UnitsKbe = unitsBank?.KBE,
                //UnitsBankName = unitsBank?.BankNameRu,
                //UnitsBankSwift = unitsBank?.SWIFT,
                //UnitsBankCode = unitsBank?.Code,
                //InvoiceNuber1C = payment?.InvoiceNumber1C,
                //InvoiceDate1C = payment?.InvoiceDatetime1C,
                //DeclarantBin = contract?.OBK_Declarant.Bin,
                //DeclarantOrgName = GetDictionary(contract?.OBK_Declarant.OrganizationFormId).Name,
                //DeclarantName = contract?.OBK_Declarant.NameRu,
                //DeclarantCountryName = GetDictionary(contract?.OBK_DeclarantContact.CurrencyId).Name,
                //DeclarantAddressLegal = contract?.OBK_DeclarantContact.AddressLegalRu,
                //ContractPriceNds = GetTotalPriceCount(payment.ContractId),
                //ContractPriceTotalText =
                //    RuDateAndMoneyConverter.CurrencyToTxtTenge(
                //        Convert.ToDouble(GetTotalPriceCount(payment.ContractId)), false),
                //ChiefAccountant = null,//GetEmpoloyee(Guid.Parse("E1EE3658-0C35-41EB-99FD-FDDC4D07CEC4"))?.ShortName,
                //Executor = null,//GetEmpoloyee(Guid.Parse("55377FAC-A5F0-4093-BBB6-18BD28E53BE1"))?.ShortName,
                //ContractPriceSignDatas = contractPriceSign
            };
            var xmlData = SerializeHelper.SerializeDataContract(result);
            return xmlData.Replace("utf-16", "utf-8");
        }

        public string SaveSignPay(Guid paymentId, string signedData)
        {
            var signData =
                AppContext.OBK_DirectionSignData.FirstOrDefault(e => e.DirectionToPaymentId == paymentId);
            var msg = "";
            if (signData != null)
            {
                if (signData.ExecutorId == null)
                {
                    signData.ExecutorSign = signedData;
                    signData.ExecutorSignDate = DateTime.Now;
                    signData.ExecutorId = UserHelper.GetCurrentEmployee().Id;
                    AppContext.SaveChanges();
                    msg = "Счет на оплату подписан";
                }
                if (signData.ChiefAccountantId == UserHelper.GetCurrentEmployee().Id)
                {
                    signData.ChiefAccountantSign = signedData;
                    signData.ChiefAccountantSignDate = DateTime.Now;
                    AppContext.SaveChanges();
                    msg = "Счет на оплату подписан";
                }
                if (signData.ChiefAccountantSign != null && signData.ExecutorSign != null)
                {
                    SendInvoiceToDeclarant(paymentId);
                    msg = "Счет на оплату подписан и отправлен заявителю";
                }
            }
            return msg;
        }


        #region Акт выполненых работ

        public void SaveCertificateOfCompletion(Guid id)
        {
            var declaration = AppContext.OBK_AssessmentDeclaration.FirstOrDefault(e => e.Id == id);
            var directToPay = AppContext.OBK_DirectionToPayments.FirstOrDefault(e => e.ContractId == declaration.ContractId);
            var contractPrice = AppContext.OBK_ContractPrice.Where(e => e.ContractId == declaration.ContractId);
            double taxValue = TaxHelper.GetNdsRef();
            var totalPrice = Convert.ToDecimal(contractPrice.Sum(e => (e.OBK_Ref_PriceList.Price * taxValue + e.OBK_Ref_PriceList.Price) * e.Count));

            if (declaration != null)
            {
                OBK_CertificateOfCompletion act = new OBK_CertificateOfCompletion
                {
                    Id = Guid.NewGuid(),
                    Number = declaration.Number,
                    ContractId = (Guid)declaration.ContractId,
                    AssessmentDeclarationId = id,
                    InvoiceNumber1C = directToPay?.InvoiceNumber1C,
                    InvoiceDatetime1C = directToPay?.InvoiceDatetime1C,
                    TotalPrice = totalPrice,
                    CreateDate = DateTime.Now,
                    SendDate = DateTime.Now,
                    ActNumber1C = null,
                    ActDate1C = null,
                    ActReturnedBack = false,
                    SendNotification = false
                };
                AppContext.OBK_CertificateOfCompletion.Add(act);
                AppContext.SaveChanges();
            }
        }

        #endregion


        #region оплата Job
        #region Счет на оплату

        public List<OBK_DirectionToPayments> GetDeclarantPaid()
        {
            return AppContext.OBK_DirectionToPayments.Where(e => e.IsPaid && !e.IsNotFullPaid && (e.SendNotification == null || e.SendNotification == "sendNotFullPaid"))
                .ToList();
        }

        public List<OBK_DirectionToPayments> GetDeclarantNotFullPaid()
        {
            return AppContext.OBK_DirectionToPayments.Where(e => e.IsPaid && e.IsNotFullPaid && e.SendNotification == null)
                .ToList();
        }

        public List<OBK_DirectionToPayments> GetPaidExpired()
        {
            var result = AppContext.OBK_DirectionToPayments
                .Where(e => (bool) !e.IsDeleted &&
                            e.OBK_Contract.Status != CodeConstManager.STATUS_OBK_PAYMENT_EXPIRED &&
                            e.OBK_Contract.Status == CodeConstManager.STATUS_OBK_EXPECTED_PAYMENT &&
                            e.OBK_Contract.Status == CodeConstManager.STATUS_OBK_NOT_FULL_PAYMENT)
                .ToList();
            List<OBK_DirectionToPayments> dPayments = new List<OBK_DirectionToPayments>();
            foreach (var r in result)
            {
                if (GetWordDate(r.DirectionDate) < DateTime.Now)
                    dPayments.Add(r);
            }
            return dPayments;
        }

        public DateTime GetWordDate(DateTime sendDate)
        {
            var factDate = sendDate.AddDays(5);
            if (factDate.DayOfWeek == DayOfWeek.Saturday)
                factDate.AddDays(2);
            if (factDate.DayOfWeek == DayOfWeek.Sunday)
                factDate.AddDays(1);
            return factDate;
        }

        public void UpdateStatus(OBK_DirectionToPayments dicPayments)
        {
            dicPayments.OBK_Contract.Status = CodeConstManager.STATUS_OBK_PAYMENT_EXPIRED;
            AppContext.SaveChanges();
        }

        public void UpdateNotificationToPayment(OBK_DirectionToPayments dicPayments, bool payStatus)
        {
            dicPayments.SendNotification = payStatus ? "send" : "sendNotFullPaid";
            dicPayments.OBK_Contract.Status = payStatus ? CodeConstManager.STATUS_OBK_ACTIVE : CodeConstManager.STATUS_OBK_NOT_FULL_PAYMENT;
            AppContext.SaveChanges();
        }

        #endregion
        #region акт выполненных работ

        public List<OBK_CertificateOfCompletion> GetCertificateOfCompletions()
        {
            return AppContext.OBK_CertificateOfCompletion
                .Where(e => e.ActNumber1C != null && e.SendNotification != true && e.ActReturnedBack != true)
                .ToList();
        }

        public void UpdateNotificationToAct(OBK_CertificateOfCompletion act)
        {
            AppContext.SaveChangesAsync();
        }

        #endregion
        #endregion
    }
}
