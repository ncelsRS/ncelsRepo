using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Constants
{
    public static class CodeConstManager
    {
        /// <summary>
        /// Статус черновик
        /// </summary>
        public const int STATUS_DRAFT_ID = 1;

        /// <summary>
        /// Статус отправил
        /// </summary>
        public const int STATUS_SEND_ID = 2;

        /// <summary>
        /// Передана на экспертизу
        /// </summary>
        public const int STATUS_EXP_SEND_ID = 3;

        /// <summary>
        /// Возврат
        /// </summary>
        public const int STATUS_REJECT_ID = 4;

        /// <summary>
        /// Повторно
        /// </summary>
        public const int STATUS_REPEAT_ID = 5;

        /// <summary>
        /// Возвращено с экспертизы
        /// </summary>
        public const int STATUS_EXP_REJECT_ID =6;


        /// <summary>
        /// Заключения для согласования
        /// </summary>
        public const int STATUS_EXP_SEND_INSTRUCTION_ID = 8;
        /// <summary>
        /// Выполняется процедура отказа
        /// </summary>
        public const int STATUS_EXP_ON_REFUSING_ID = 9;
        /// <summary>
        /// Отказанно
        /// </summary>
        public const int STATUS_EXP_REFUSED_ID = 10;

        /// <summary>
        /// Регистрация
        /// </summary>
        public const int DRUG_REGISTER_ID = 1;

        /// <summary>
        /// Копия
        /// </summary>
        public const int DRUG_COPY_ID = 13;

        public const string MY_TASK_CODE = "MY";


        /// <summary>
        ///Заключения приняты
        /// </summary>
        public const int STATUS_EXP_ACCEPT_CONCLUSION_ID = 11;

        /// <summary>
        ///Заключения приняты
        /// </summary>
        public const int STATUS_EXP_FOR_REVISION_CONCLUSION_ID = 12;

        #region код таблиц заявлении

        /// <summary>
        ///  Торговое название на экспорт (для отечественных производителей)
        /// </summary>
        public const string EXP_DrugExportTradeCode = "exportTrade";

        /// <summary>
        ///   Список патентной защиты
        /// </summary>
        public const string EXP_DrugPatentCode = "patent";

        /// <summary>
        ///  Лекарственное средство
        /// </summary>
        public const string EXP_DrugTypeCode = "drugType";

        /// <summary>
        ///  Лекарственное средство
        /// </summary>
        public const string EXP_DrugWrappingCode = "wrapping";

        /// <summary>
        ///  Состав
        /// </summary>
        public const string EXP_DrugSubstanceCode = "substance";

        /// <summary>
        ///   Наличие охранного документа на изобретение или полезную модель
        /// </summary>
        public const string EXP_DrugProtectionDoc = "protectionDoc";

        /// <summary>
        ///   Регистрация в стране-производителе и других странах
        /// </summary>
        public const string EXP_DrugOtherCountry = "otherCountry";

        /// <summary>
        ///  Состав
        /// </summary>
        public const string EXP_DrugOrganizationsCode = "org";

        /// <summary>
        ///  Цены
        /// </summary>
        public const string EXP_DrugPrice = "drugPrice";


        /// <summary>
        ///  Дозировка
        /// </summary>
        public const string EXP_DrugDosageCode = "dosage";

        /// <summary>
        ///  Первичка
        /// </summary>
        public const string EXP_DrugPrimaryKindCode = "primaryKind";

        /// <summary>
        ///  Результаты экспертизы
        /// </summary>
        public const string EXP_DrugAppDosageResultCode = "dosageResult";

        /// <summary>
        ///  Замечания и уточнения
        /// </summary>
        public const string EXP_DrugAppDosageRemarkCode = "dosageRemark";

        /// <summary>
        ///  Тип изменение
        /// </summary>
        public const string EXP_DrugChangeTypeCode = "changeType";

        /// <summary>
        ///  НТД
        /// </summary>
        public const string EXP_DrugPrimaryNTDCode = "ntd";

             /// <summary>
        ///  ОТД
        /// </summary>
        public const string EXP_DrugPrimaryOTDCode = "otd";


        /// <summary>
        ///  Замечания первичка
        /// </summary>
        public const string EXP_ExpertiseStageRemarkCode = "remark";

        /// <summary>
        ///  Производитель
        /// </summary>
        public const string EXP_DrugSubstanceManufactureCode = "smanufacture";

        #endregion

        #region коды из общего справочника

        /// <summary>
        ///   Тип производителя
        /// </summary>
        public const string DIC_ORG_MANUFACTURE_TYPE = "OrgManufactureType";
        /// <summary>
        /// Страна
        /// </summary>
        public const string DIC_COUNTRY_TYPE = "Country";

        /// <summary>
        /// Страна
        /// </summary>
        public const string DIC_OPF_TYPE = "OpfType";

        public static readonly string[] LIST_COUNTY_CODE_FOR_PRICE = { "BY", "HU", "LV", "AT", "RU", "TR", "UA" };
     
        #endregion

        #region Виды организации

        /// <summary>
        ///   Заявитель или представительство
        /// </summary>
        public const string ORG_APPLICANT_ID = "d82f2b62-ef6e-49e5-b04a-000000008184";
        
        /// <summary>
        /// Производитель
        /// </summary>
        public const string ORG_MANUFACTURE_ID = "d82f2b62-ef6e-49e5-b04a-000000008180";


        /// <summary>
        /// Держатель регистрационного удостоверения
        /// </summary>
        public const string ORG_HOLDER_ID = "d82f2b62-ef6e-49e5-b04a-000000008182";

        #endregion


        public const string ATTACH_DRUG_FILE_CODE = "DrugDeclaration";
        public const string ATTACH_REMARK_FILE_CODE = "REMARK";


        #region Вид файлов
        public const string FILE_INSTRUCTION_CODE = "4"; //Инструкция
        public const string FILE_MAKET_CODE = "3"; //Макет
        public const string FILE_AND_CODE = "5"; //АНД
        #endregion

        #region Вид переписки
        public const int CORESPONDENCE_KIND_ELECTRONIC = 1;
        #endregion

        #region Тип переписки
        /// <summary>
        /// Входящее
        /// </summary>
        public const int CORESPONDENCE_TYPE_INBOX = 1;

        /// <summary>
        /// Исходящее
        /// </summary>
        public const int CORESPONDENCE_TYPE_OUTBOX = 2;
        #endregion


        #region Вид этапа

        /// <summary>
        /// ЦОЗ
        /// </summary>
        public const int STAGE_COZ = 7;
        /// <summary>
        /// Первичная экспертиза
        /// </summary>
        public const int STAGE_PRIMARY = 1;

        /// <summary>
        /// Фармацевтическая экспертиза
        /// </summary>
        public const int STAGE_PHARMACEUTICAL = 2;

        /// <summary>
        /// Фармакологическая экспертиза
        /// </summary>
        public const int STAGE_PHARMACOLOGICAL = 3;

        /// <summary>
        /// Аналитическая экспертиза
        /// </summary>
        public const int STAGE_ANALITIC = 4;

        /// <summary>
        /// Перевод
        /// </summary>
        public const int STAGE_TRANSLATE = 5;

        /// <summary>
        /// Экспертный совет
        /// </summary>
        public const int STAGE_COUNCIL = 6;

        /// <summary>
        /// Заключение о безопастности
        /// </summary>
        public const int STAGE_SAFETYREPORT = 8;
        #endregion

        /// <summary>
        ///  Черновик
        /// </summary>
        public const string STATUS_DRAFT = "DRAFT";

        /// <summary>
        ///  Отравлено
        /// </summary>
        public const string STATUS_SEND = "SEND";
        /// <summary>
        /// Письмо на согласование
        /// </summary>
        public const string STATUS_ONAGREEMENT = "LETTER_AGREEMENT";
        /// <summary>
        /// Письмо согласованно
        /// </summary>
        public const string STATUS_AGREED = "LETTER_AGREED";
        /// <summary>
        /// Письмо отклонено
        /// </summary>
        public const string STATUS_REJECTED = "LETTER_REJECTED";
        /// <summary>
        /// Письмо на подписание
        /// </summary>
        public const string STATUS_ONSIGNING = "LETTER_ONSIGNING";
        /// <summary>
        /// Письмо подписанно
        /// </summary>
        public const string STATUS_SIGNED = "LETTER_SIGNED";


        #region Статус файла

        /// <summary>
        /// Черновик
        /// </summary>
        public const string STATUS_FILE_CODE_DRAFT = "DRAFT";

        /// <summary>
        /// На согласовании
        /// </summary>
        public const string STATUS_FILE_CODE_ON_AGREEMENT = "ON_AGREEMENT";

        /// <summary>
        /// Согласован
        /// </summary>
        public const string STATUS_FILE_CODE_AGREED = "AGREED";

        /// <summary>
        /// Отказ
        /// </summary>
        public const string STATUS_FILE_CODE_REFUSED = "REFUSED";


        /// <summary>
        /// Отправлен
        /// </summary>
        public const string STATUS_FILE_CODE_SENDED = "SENDED";

        /// <summary>
        /// Принят
        /// </summary>
        public const string STATUS_FILE_CODE_ACCEPTED = "ACCEPTED";

        /// <summary>
        /// На доработку
        /// </summary>
        public const string STATUS_FILE_CODE_FOR_REVISION = "FOR_REVISION";

        #endregion
    }
}
