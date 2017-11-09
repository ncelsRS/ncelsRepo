using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.DataModel
{
    public partial class Dictionary
    {
        /// <summary>
        /// Справочник типов доп. соглашений
        /// </summary>
        public static class DicContractAddition
        {
            /// <summary>
            /// Код справочника
            /// </summary>
            public const string DicCode = "ContractAddition";

            /// <summary>
            /// Соглашение об изменение банковских реквизитов
            /// </summary>
            public const string BankChangedCode = "1";

            /// <summary>
            /// Соглашение об изменение юридического адреса
            /// </summary>
            public const string LegalAddresChangeCode = "2";

            /// <summary>
            /// Соглашение о продлении срока действия договора
            /// </summary>
            public const string ContractPeriodChangedCode = "3";

            /// <summary>
            /// Соглашение о смене руководителя
            /// </summary>
            public const string BossChangedCode = "4";
        }

        /// <summary>
        /// Вид документа
        /// </summary>
        public static class DicDoverenostType
        {
            /// <summary>
            /// Код справочника
            /// </summary>
            public const string DicCode = "DoverennostType";

            /// <summary>
            /// Доверенность от производителя
            /// </summary>
            public const string ProducerProxyCode = "1";

            /// <summary>
            /// Устав
            /// </summary>
            public const string StatuteCode = "2";

            /// <summary>
            /// Доверенность от держателя рег. удостоверения
            /// </summary>
            public const string HolderProxyCode = "3";
        }

        /// <summary>
        /// Справочник типов доп. соглашений
        /// </summary>
        public static class DicContractAdditionAttachType
        {
            /// <summary>
            /// Код справочника
            /// </summary>
            public const string DicCode = "sysAttachContractAdditionDict";

            /// <summary>
            /// Дополнительное соглашение
            /// </summary>
            public const string ContractAdditionCode = "0";

            /// <summary>
            /// Уведомление банка о изменение (письмо)
            /// </summary>
            public const string BankDocCode = "1";

            /// <summary>
            /// Справка с портала e.gov
            /// </summary>
            public const string EgovDocCode = "2";

            /// <summary>
            /// Доверенность от производителя с новым сроком
            /// </summary>
            public const string ProducerProxyCode = "3";

            /// <summary>
            /// Соглашение о смене руководителя
            /// </summary>
            public const string HolderProxyCode = "4";

            /// <summary>
            /// Подтверждающий документ о смене первого руководителя
            /// </summary>
            public const string NewBossCode = "5";
        }

        /// <summary>
        /// Справочник статусов договоров
        /// </summary>
        public static class ContractStatus
        {
            /// <summary>
            /// Код справочника
            /// </summary>
            public const string DicCode = "ContractStatus";
        }

        /// <summary>
        /// Тип держателя договора
        /// </summary>
        public static class ContractHolderType
        {
            public const string DicCode = "ContractHolderType";

            /// <summary>
            /// Заявитель
            /// </summary>
            public const string Applicant = "applicant";

            /// <summary>
            /// Держатель рег. удостоверения
            /// </summary>
            public const string Holder = "holder";

            /// <summary>
            /// Производитель
            /// </summary>
            public const string Producer = "producer";
        }

        /// <summary>
        /// Тип документа надо которым выполняется процесс согласования
        /// </summary>
        public static class ExpAgreedDocType
        {
            /// <summary>
            /// Код справочника
            /// </summary>
            public const string DicCode = "ExpAgreedDocType";

            /// <summary>
            /// Договор
            /// </summary>
            public const string Contract = "1";

            /// <summary>
            /// Направление на оплату
            /// </summary>
            public const string DirectionToPay = "2";

            /// <summary>
            /// Письмо
            /// </summary>
            public const string Letter = "3";

            /// <summary>
            /// Заключение
            /// </summary>
            public const string Conclusion = "4";

            /// <summary>
            /// Итоговый документ
            /// </summary>
            public const string EXP_DrugFinalDocument = "5";

            /// <summary>
            /// файл с перевода
            /// </summary>
            public const string TranslateFile = "6";

            /// <summary>
            /// Макет
            /// </summary>
            public const string MaketFile = "7";

            /// <summary>
            /// Акт выполненых работ
            /// </summary>
            public const string CertificateOfCompletion = "8";

        }

        /// <summary>
        /// Тип задания
        /// </summary>
        public static class ExpTaskType
        {
            /// <summary>
            /// Код справочника
            /// </summary>
            public const string DicCode = "ExpTaskType";

            /// <summary>
            /// Согласование
            /// </summary>
            public const string Agreement = "1";

            /// <summary>
            /// Подписание
            /// </summary>
            public const string Signing = "2";
        }

        /// <summary>
        /// Статус задания
        /// </summary>
        public static class ExpTaskStatus
        {
            /// <summary>
            /// Код справочника
            /// </summary>
            public const string DicCode = "ExpTaskStatus";

            /// <summary>
            /// Выполнена
            /// </summary>
            public const string Executed = "1";

            /// <summary>
            /// Отклонена
            /// </summary>
            public const string Rejected = "2";

            /// <summary>
            /// В работе
            /// </summary>
            public const string InWork = "3";

            /// <summary>
            /// В очереди
            /// </summary>
            public const string InQueue = "4";

            /// <summary>
            /// Аннулирована
            /// </summary>
            public const string Repealed = "5";
        }

        /// <summary>
        /// Статус процесса согласования
        /// </summary>
        public static class ExpActivityStatus
        {
            /// <summary>
            /// Код справочника
            /// </summary>
            public const string DicCode = "ExpActivityStatus";

            /// <summary>
            /// На согласовании
            /// </summary>
            public const string OnAgreement = "1";

            /// <summary>
            /// На подписании
            /// </summary>
            public const string OnSigning = "2";

            /// <summary>
            /// Аннулирована
            /// </summary>
            public const string Repealed = "3";

            /// <summary>
            /// Завершена
            /// </summary>
            public const string Executed = "4";

            /// <summary>
            /// Отклонена
            /// </summary>
            public const string Rejected = "5";
        }

        /// <summary>
        /// Типы заготовленных процессов согласования для типов документов
        /// </summary>
        public static class ExpActivityType
        {
            public const string DicCode = "ExpActivityType";

            /// <summary>
            /// Согласование договора руководителем ЦОЗ
            /// </summary>
            public const string ContractWithProcCenterHead = "1";

            /// <summary>
            /// Согласование договора руководителем ЦОЗ
            /// </summary>
            public const string ContractSigningByApplicantAndCeo = "2";

            /// <summary>
            /// Подписание договора без заявителя
            /// </summary>
            public const string ContractSigningByCeo = "3";

            /// <summary>
            /// Согласование направления на оплату
            /// </summary>
            public const string DirectionToPayAgrement = "4";

            /// <summary>
            /// Согласование итогового документа
            /// </summary>
            public const string FinalDocAgreement = "5";

            /// <summary>
            /// Согласование исходящего письма с экспертизы
            /// </summary>
            public const string ExpertiseLetterAgreement = "6";

            /// <summary>
            /// Подписание исходящего письма с экспертизы
            /// </summary>
            public const string ExpertiseLetterSigning = "7";

            /// <summary>
            ///  Согласование документа на перевод
            /// </summary>
            public const string TranslateDocAgreement = "8";

            /// <summary>
            ///  Согласование макета
            /// </summary>
            public const string MaketDocAgreement = "9";

            /// <summary>
            ///  Согласование акта выполненых работ
            /// </summary>
            public const string CertificateOfComplitionAgreement = "10";
        }

        /// <summary>
        /// Статусы направлений на оплату
        /// </summary>
        public static class ExpDirectionToPayStatus
        {
            public const string DicCode = "ExpDirectionToPayStatus";

            /// <summary>
            /// Сформирован
            /// </summary>
            public const string Created = "0";

            /// <summary>
            /// на согласовании
            /// </summary>
            public const string OnAgreement = "1";

            /// <summary>
            /// Согласован
            /// </summary>
            public const string Agreed = "2";

            /// <summary>
            /// На исправлении
            /// </summary>
            public const string OnСorrection = "3";

            /// <summary>
            /// Аннулирован
            /// </summary>
            public const string Canceled = "4";

            /// <summary>
            /// Отправлен на оплату
            /// </summary>
            public const string SendedTo1C = "5";

            /// <summary>
            /// Оплачен
            /// </summary>
            public const string Paid = "6";

            /// <summary>
            /// Срок оплаты истек
            /// </summary>
            public const string PaymentExpired = "7";

            /// <summary>
            /// Не оплачен
            /// </summary>
            public const string NotPaid = "8";
        }

        /// <summary>
        /// Типы вложений к договорам
        /// </summary>
        public static class SysAttachContract
        {
            public const string DicCode = "sysAttachContractDict";

            /// <summary>
            /// Договор
            /// </summary>
            public const string Contract = "0";
        }

        /// <summary>
        /// Типы вложений к доп. соглашениям
        /// </summary>
        public static class SysAttachContractAddition
        {
            public const string DicCode = "sysAttachContractAdditionDict";

            /// <summary>
            /// Договор
            /// </summary>
            public const string ContractAddition = "0";
        }

        public static class MeasureType
        {
            public const string DicCode = "MeasureType";
        }

        /// <summary>
        /// Типы Материалов РД
        /// </summary>
        public static class MaterialRdType
        {
            public const string DicCode = "MaterialRdType";

            /// <summary>
            /// Образец ЛС
            /// </summary>
            public const string MpSample = "mpsample";

            /// <summary>
            /// Расходный материал
            /// </summary>
            public const string Consumables = "consumables";

            /// <summary>
            /// Стандартный образец
            /// </summary>
            public const string Standartsample = "standartsample";

            /// <summary>
            /// Специфический реагент
            /// </summary>
            public const string Specificreagent = "specificreagent";
        }

        /// <summary>
        /// Внешнее состояние и Заключение о соотвествие/не соотвествии
        /// </summary>
        public static class MaterialRdExternalState
        {
            public const string DicCode = "MaterialRdExternalState";

            /// <summary>
            /// Соответствует
            /// </summary>
            public const string Match = "match";

            /// <summary>
            /// Подписан
            /// </summary>
            public const string NotMatch = "notmatch";
        }

        /// <summary>
        /// статус Материала РД
        /// </summary>
        public static class MaterialRdStatus
        {
            public const string DicCode = "MaterialRdStatus";

            /// <summary>
            /// Зарегестрирован
            /// </summary>
            public const string Registered = "registered";

            /// <summary>
            /// Принят
            /// </summary>
            public const string Accepted = "accepted";
        }

        /// <summary>
        /// статус Материала РД
        /// </summary>
        public static class StorageCondition
        {
            public const string DicCode = "StorageCondition";

            /// <summary>
            /// Комната
            /// </summary>
            public const string Room = "room";

            /// <summary>
            /// Холодильник
            /// </summary>
            public const string Fridge = "fridge";

            /// <summary>
            /// Морозильник
            /// </summary>
            public const string Freezer = "freezer";
        }


        /// <summary>
        /// Статусы документов экспертизы
        /// </summary>
        public static class EXP_DocumentStatus
        {
            public const string DicCode = "EXP_DocumentStatus";

            /// <summary>
            /// Согласован
            /// </summary>
            public const string StatusApproved = "approved";

            /// <summary>
            /// Подписан
            /// </summary>
            public const string StatusSigned = "signed";

            /// <summary>
            /// Отклонен
            /// </summary>
            public const string StatusRejected = "rejected";

            /// <summary>
            /// На согласовании
            /// </summary>
            public const string StatusOnAgreement = "onAgreement";

            /// <summary>
            /// На утверждении
            /// </summary>
            public const string StatusOnSigning = "onSigning";

        }


        /// <summary>
        /// Справочник стран
        /// </summary>
        public static class CountryDic
        {
            public const string DicCode = "Country";
        }

        /// <summary>
        /// Статус направления материалов
        /// </summary>
        public static class MaterialDirectionStatusDic
        {
            public const string DicCode = "MaterialDirectionStatus";

            /// <summary>
            /// Создан
            /// </summary>
            public const string Created = "created";

            /// <summary>
            /// Отправлен
            /// </summary>
            public const string Sended = "sended";

            /// <summary>
            /// Принят
            /// </summary>
            public const string Accepted = "accepted";

            /// <summary>
            /// Отклонен
            /// </summary>
            public const string Rejected = "rejected";
        }

        /// <summary>
        /// Тип ТМЦ 
        /// </summary>
        public static class TmcType
        {
            public const string DicCode = "TmcType";

        }

        /// <summary>
        /// Статус направления материалов
        /// </summary>
        public static class CertificateOfCompletionStatusDic
        {
            public const string DicCode = "CertificateOfCompletionStatus";

            /// <summary>
            /// Создан
            /// </summary>
            public const string New = "new";

            /// <summary>
            /// Отправлен
            /// </summary>
            public const string OnAgreement = "onagreement";

            /// <summary>
            /// Принят
            /// </summary>
            //public const string Accepted = "accepted";
            /// <summary>
            /// Отклонен
            /// </summary>
            public const string Rejected = "rejected";

            /// <summary>
            /// Отправлено заявителю
            /// </summary>
            public const string Sended = "sended";
        }

        public static class OrgManufactureType
        {
            public const string DicCode = "OrgManufactureType";

            /// <summary>
            /// Производитель
            /// </summary>
            public const string Manufacturer = "1";

            /// <summary>
            /// Держатель лицензии
            /// </summary>
            public const string LicenseHolder = "2";

            /// <summary>
            /// Держатель регистрационного удостоверения
            /// </summary>
            public const string HolderOfRegistrationCertificate = "3";

            /// <summary>
            /// Предприятие-упаковщик
            /// </summary>
            public const string PackingCompany = "4";

            /// <summary>
            /// Заявитель или представительство
            /// </summary>
            public const string ApplicantOrRepresentativeOffice = "5";

            /// <summary>
            /// Уполномоченное лицо по осуществлению фармаконадзора в РК
            /// </summary>
            public const string AuthorizedPerson = "6";

            /// <summary>
            /// Выпускающий контроль
            /// </summary>
            public const string IssuingControl = "7";
        }

        /// <summary>
        /// Причины списания
        /// </summary>
        public static class DicWriteOffReasons
        {
            /// <summary>
            /// Код справочника
            /// </summary>
            public const string DicCode = "WriteOffReasons";

            /// <summary>
            /// Использование
            /// </summary>
            public const string Usage = "1";

            /// <summary>
            /// Истечение срока годности
            /// </summary>
            public const string UtilizingDateExpiration = "2";

            /// <summary>
            /// Порча
            /// </summary>
            public const string Damage = "3";

            /// <summary>
            /// Бой
            /// </summary>
            public const string Fight = "4";

            /// <summary>
            /// Погрешность в весе, объеме
            /// </summary>
            public const string WeightVolumePrecision = "5";

            /// <summary>
            /// Летучесть
            /// </summary>
            public const string Volatility = "6";

            /// <summary>
            /// Естественные убытки
            /// </summary>
            public const string NaturalLosses = "7";

        }
    }
}