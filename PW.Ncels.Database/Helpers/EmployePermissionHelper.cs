using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Helpers
{
    public class EmployePermissionHelper
    {
        public class EmployeePermission
        {
            public Guid EmployeeId { get; set; }
            public string PermissionKey { get; set; }
            public string PermissionValue { get; set; }
            //public int Id { get; set; }
            public string GroupName { get; set; }
        }

        public enum PermissionValueTypes
        {
            Classic = 1,
            Tree = 2,
        }
        private static List<string> _actualPermissionKeys;

        private static ncelsEntities db = UserHelper.GetCn();
        public static void Init()
        {
            _actualPermissionKeys = new List<string>();

            AddPermission("IsMenuOfficeVisibility", @"Модуль 'Канцелярия'", "Вкладка 'Канцелярия' предназначена для просмотра списка документов всех типов, а так же для создания новых документов и их регистрации.", "Доступ в главном меню канцелярия");
            AddPermission("IsMenuInDocVisibility", @"Модуль 'Канцелярия'", "Просмотр списка документов и свойств выбранного документа, работа с документами", "Доступ в главном меню канцелярия");
            AddPermission("IsMenuOutDocVisibility", @"Модуль 'Канцелярия'", "Просмотр списка документов и свойств выбранного документа, работа с документами", "Доступ в главном меню канцелярия");
            AddPermission("IsMenuInitiativeOutDocVisibility", @"Модуль 'Канцелярия'", "Просмотр списка документов и свойств выбранного документа, работа с документами", "Доступ в главном меню канцелярия");
            AddPermission("IsMenuCitizenDocVisibility", @"Модуль 'Канцелярия'", "Просмотр списка документов и свойств выбранного документа, работа с документами", "Доступ в главном меню канцелярия");
            AddPermission("IsMenuAdminDocVisibility", @"Модуль 'Канцелярия'", "Просмотр списка документов и свойств выбранного документа, работа с документами", "Доступ в главном меню канцелярия");
            AddPermission("IsMenuAdminMainDocVisibility", @"Модуль 'Канцелярия'", "Просмотр списка приказов по основной деятельности", "Доступ в главном меню канцелярия");
            AddPermission("IsMenuProtocolDocVisibility", @"Модуль 'Канцелярия'", "Просмотр списка документов и свойств выбранного документа, работа с документами", "Доступ в главном меню канцелярия");
            AddPermission("IsMenuInnerDocVisibility", @"Модуль 'Канцелярия'", "Просмотр списка документов и свойств выбранного документа, работа с документами", "Доступ в главном меню канцелярия");
            AddPermission("IsMenuReportDocVisibility", @"Модуль 'Канцелярия'", "Просмотр списка документов и свойств выбранного документа, работа с документами", "Доступ в главном меню канцелярия");
            AddPermission("IsMenuInDocRegionsVisibility", @"Модуль 'Канцелярия'", "Просмотр списка входящих документов в филиалах", "Доступ в главном меню канцелярия");
            AddPermission("IsMenuContractVisibility", @"Модуль 'Канцелярия'", "Просмотр списка трудовых договоров", "Доступ в главном меню канцелярия");
            AddPermission("IsMenuArchiv", @"Модуль 'Канцелярия'", "Просмотр списка документов", "Доступ к архиву");
            AddPermission("IsMenuInnerDepartmentPermission", @"Модуль 'Канцелярия'", "Просмотр списка служебные записки департамента", "Доступ в главном меню канцелярия");

            AddPermission("IsMenuTaskVisibility", @"Модуль 'Задания'", "Вкладка 'Мой Задания' предназначена для просмотра списка заданий.", "Доступ в главном меню задания");
            AddPermission("IsMenuTaskListVisibility", @"Модуль 'Задания'", "Просмотр новых заданий, принятых в работу заданий и перепорученных заданий.", "Доступ в главном меню задания");
            AddPermission("IsMenuTaskExcludeVisibility", @"Модуль 'Задания'", "Просмотр заданий исполненных.", "Доступ в главном меню задания");
            AddPermission("IsMenuTaskRejectVisibility", @"Модуль 'Задания'", "Просмотр заданий отказанных.", "Доступ в главном меню задания");
            AddPermission("IsMenuTaskNewVisibility", @"Модуль 'Задания'", "Просмотр новых заданий.", "Доступ в главном меню задания");


            AddPermission("IsMenuMyDocVisibility", @"Модуль 'Мои документы'", "Вкладка 'Мои документы' предназначена для просмотра списка Мои документы.", "Доступ в главном меню Мои документы");
            AddPermission("IsMenuMyDocProjectVisibility", @"Модуль 'Мои документы'", "Просмотр списка проектов и свойств выбранного проекта, работа с листом согласования.", "Доступ в главном меню Мои документы");
            AddPermission("IsMenuMyDocInnerVisibility", @"Модуль 'Мои документы'", "Просмотр списка документов и свойств выбранного документа, работа с документами", "Доступ в главном меню Мои документы");
            AddPermission("IsMenuMyDocReportVisibility", @"Модуль 'Мои документы'", "Просмотр отчета для начальников отдела", "Доступ в главном меню Мои документы");


            AddPermission("IsMenuDicVisibility", @"Модуль 'Справочники'", "Вкладка 'Справочники' предназначена для просмотра списка справочников всех типов", "Доступ в главном меню на справочники");
            AddPermission("IsMenuDicAllVisibility", @"Модуль 'Справочники'", "Просмотр списка справочников, изменение, добавление", "Доступ в главном меню на справочники");
            AddPermission("IsMenuDicNomenVisibility", @"Модуль 'Справочники'", "Просмотр списка номеклатуры, изменение, добавление", "Доступ в главном меню на справочники");

            AddPermission("IsMenuUnitVisibility", @"Модуль 'Кадровый учет'", "Вкладка 'Организационная структура' предназначена для просмотра дерева огранизационной структуры", "Доступ в главном меню кадровый учет");
            AddPermission("IsMenuUnitListVisibility", @"Модуль 'Кадровый учет'", "Просмотр списка огранизационной структуры", "Доступ в главном меню кадровый учет");
            AddPermission("IsMenuPermissionRoleListVisibility", @"Модуль 'Кадровый учет'", "Просмотр списка ролей прав доступа", "Доступ в главном меню кадровый учет");
            AddPermission("IsMenuActionLogsListVisibility", @"Модуль 'Кадровый учет'", "Просмотр логов действий пользователей", "Доступ в главном меню кадровый учет");

            AddPermission("IsMenuVisitVisibility", @"Модуль 'Приём'", "Вкладка 'Приём' предназначена для просмотра настроек по приёмам", "Доступ в главном меню Приём");
            AddPermission("IsMenuVisitTypeVisibility", @"Модуль 'Приём'", "Просмотр списка типов приёма", "Доступ в главном меню Приём");
            AddPermission("IsMenuVisitWokringTimeVisibility", @"Модуль 'Приём'", "Просмотр своего рабочего времени", "Доступ в главном меню Приём");

            AddPermission("IsMenuCommissionVisibility", @"Модуль 'Комиссия'", "Вкладка 'Комиссия' предназначена для просмотра комиссий", "Доступ в главном меню Комиссия");
            AddPermission("IsMenuCommissionListVisibility", @"Модуль 'Комиссия'", "Просмотр списка повесток", "Доступ в главном меню Комиссия");


            AddPermission("IsMenuUnitReportVisibility", @"Модуль 'Кадровый учет'", "Просмотр отчетов", "Доступ в главном меню кадровый учет");
            AddPermission("IsMenuUnitDismissedEmployeesVisibility", @"Модуль 'Кадровый учет'", "Просмотр уволенных сотрудников", "Доступ в главном меню кадровый учет");
            AddPermission("IsMenuUnitDismissedEmployeesDepVisibility", @"Модуль 'Кадровый учет'", "Просмотр уволенных сотрудников своего департамента", "Доступ в главном меню кадровый учет");
            //
            AddPermission("IsMenuUnitEmployeeListPermission", @"Модуль 'Кадровый учет'", "Список доступных сотрудников в орг.структуре", "Доступ в главном меню кадровый учет", PermissionValueTypes.Tree);

            //
            AddPermission("IsAllNomenclature", @"Модуль 'Доступ к справочникам'", "Просмотр списка номенклатуры", "Доступ к спискам справочникам");

            AddPermission("IsAddBp", @"Возможность добавлять согласование", "Возможность добавлять согласование", "Разрешения на действия в Системе");

            AddPermission("IsEditDoc", @"Возможность редактировать документы", "Возможность редактировать документы", "Разрешения на действия в Системе");

            AddPermission("IsEditCor", @"Возможность редактировать корреспондентов", "Возможность редактировать корреспондентов", "Разрешения на действия в Системе");

            AddPermission("IsEditRefAnalyseIndicator", @"Возможность редактировать справочник показателей(Аналитическая экспертиза)", "Возможность редактировать справочник показателей(Аналитическая экспертиза)", "Разрешения на действия в Системе");
            AddPermission("IsEditRefChangeType", @"Возможность редактировать справочник изменения типов", "Возможность редактировать справочник изменения типов", "Разрешения на действия в Системе");
            AddPermission("IsEditRefNormDocFarm", @"Возможность редактировать справочник нормативных документов", "Возможность редактировать справочник нормативных документов", "Разрешения на действия в Системе");
            AddPermission("IsEditRefPrimaryOtd", @"Возможность редактировать модули первичной экспертизы", "Возможность редактировать модули первичной экспертизы", "Разрешения на действия в Системе");

            AddPermission("IsEditOs", @"Возможность редактировать организационную структуру", "Возможность редактировать организационную структуру", "Доступ в главном меню кадровый учет");

            AddPermission("IsCreateCorDoc", @"Возможность создавать служебные записки на имя Руководства", "Возможность создавать служебные записки на имя Руководства", "Разрешения на действия в Системе");

            AddPermission("IsAllEmploye", @"Доступ к орг структуре всем или только к своему подразделению", "Доступ к орг структуре всем или только к своему подразделению", "Разрешения на действия в Системе");

            AddPermission("IsAllStructureDashboard", @"Модуль 'Дашборд'", "Просмотр данных всего структурного подразделения", "Дашборд");

            //
            AddPermission("IsMenuProjectVisibility", @"Модуль 'Ценообразование'", "Вкладка 'Ценообразование' главная", "Доступ в главном меню Ценообразование");
            AddPermission("IsMenuProjectPriceAllVisibility", @"Модуль 'Ценообразование'", "Вкладка 'Ценообразование' Все", "Доступ в главном меню Ценообразование");
            AddPermission("IsMenuProjectPriceRpcVisibility", @"Модуль 'Ценообразование'", "Вкладка 'Ценообразование' Журнал заявок ЦОЗ", "Доступ в главном меню Ценообразование");
            AddPermission("IsMenuProjectPriceLsVisibility", @"Модуль 'Ценообразование'", "Вкладка 'Ценообразование' Регистрация цен на ЛС", "Доступ в главном меню Ценообразование");
            AddPermission("IsMenuProjectPriceImnVisibility", @"Модуль 'Ценообразование'", "Вкладка 'Ценообразование' Регистрация цен ИМН", "Доступ в главном меню Ценообразование");
            AddPermission("IsMenuProjectRePriceLsVisibility", @"Модуль 'Ценообразование'", "Вкладка 'Ценообразование' Внесение изменений цен ЛС", "Доступ в главном меню Ценообразование");
            AddPermission("IsMenuProjectRePriceImnVisibility", @"Модуль 'Ценообразование'", "Вкладка 'Ценообразование' Внесение изменений цен ИМН", "Доступ в главном меню Ценообразование");
            AddPermission("IsMenuProjectProtocolsVisibility", @"Модуль 'Ценообразование'", "Вкладка 'Ценообразование' Протоколы", "Доступ в главном меню Ценообразование");
            AddPermission("IsPriceProjectProcCenterVisibility", @"Модуль 'Ценообразование'", "Просмотр журнала подразделением 'ЦОЗ'", "Просмотр журнала заявлений модуля 'Ценообразование'");
            AddPermission("IsPriceProjectChiefVisibility", @"Модуль 'Ценообразование'", "Просмотр журнала руководителем", "Просмотр журнала заявлений модуля 'Ценообразование'");
            AddPermission("IsPriceProjectExpertVisibility", @"Модуль 'Ценообразование'", "Просмотр журнала экспертом", "Просмотр журнала заявлений модуля 'Ценообразование'");
            AddPermission("IsMenuProjectRequestOrdersVisibility", @"Модуль 'Ценообразование'", "Вкладка 'Ценообразование' Запросы", "Доступ в главном меню Ценообразование");

            AddPermission("IsMenuProjectRegister", @"Модуль 'Регистрация'", "Вкладка 'Регистрация' главная", "Доступ в главном меню Регистрация");
            AddPermission("IsMenuProjectRegisterAll", @"Модуль 'Регистрация'", "Вкладка 'Регистрация' Все", "Доступ в главном меню Регистрация");
            AddPermission("IsMenuProjectRegisterLsVisibility", @"Модуль 'Регистрация'", "Вкладка 'Регистрация' Регистрация ЛС", "Доступ в главном меню Регистрация");
            AddPermission("IsMenuProjectReRegisterLsVisibility", @"Модуль 'Регистрация'", "Вкладка 'Регистрация' Перегистрация ЛС", "Доступ в главном меню Регистрация");
            AddPermission("IsMenuProjectChRegisterLsVisibility", @"Модуль 'Регистрация'", "Вкладка 'Регистрация' Внесение изменений ЛС", "Доступ в главном меню Регистрация");
            AddPermission("CanDrugDeclarationExecutorsAssignment", @"Модуль 'Регистрация'", "Распределение заявлений", "Работа с заявлениями на экспертизу");

            AddPermission("IsMenuContractMainVisibility", @"Модуль 'Договоры'", "Вкладка 'Договоры' главная", "Доступ в главном меню Договоры");
            AddPermission("IsMenuContractIndexVisibility", @"Модуль 'Договоры'", "Вкладка 'Договоры' Договоры", "Доступ в главном меню Договоры");
            AddPermission("IsMenuContractAllVisibility", @"Модуль 'Договоры'", "Вкладка 'Договоры' Все", "Доступ в главном меню Договоры");
            AddPermission("IsMenuContractContractExVisibility", @"Модуль 'Договоры'", "Вкладка 'Договоры' Дополнительное соглашение", "Доступ в главном меню Договоры");
            AddPermission("IsMenuContractSettingsVisibility", @"Модуль 'Договоры'", "Вкладка 'Договоры' Настройки", "Доступ в главном меню Договоры");
            AddPermission("IsProcHeadContractDashboardVisibility", @"Модуль 'Договоры'", "Доступ к информационной доске руководителя", "Доступ к информационной доске");
            AddPermission("IsLawyerContractDashboardVisibility", @"Модуль 'Договоры'", "Доступ к информационной доске юрисконсульта", "Доступ к информационной доске");
            //

            AddPermission("IsMenuTmcMainVisibility", @"Модуль 'ЛИМС'", "Вкладка 'ТМС' главная", "Доступ в главном меню ЛИМС кладовщик");
            AddPermission("IsMenuMyTmcVisibility", @"Модуль 'ЛИМС'", "Вкладка 'ТМС' главная", "Доступ в главном меню ЛИМС");
            AddPermission("IsMenuDocumentMainVisibility", @"Модуль 'ЛИМС'", "Вкладка 'Документы' главная", "Доступ в главном меню ЛИМС");
            AddPermission("IsMenuEquipmentVisibility", @"Модуль 'ЛИМС'", "Вкладка 'Оборудование' главная", "Доступ в главном меню ЛИМС");

            AddPermission("IsMenuCozVisibility", @"Модуль 'Цоз'", "Вкладка 'Цоз' главная", "Доступ в главном меню Цоз");


            AddPermission("IsMenuStateRegisterVisibility", @"Модуль 'Гос.Реестр'", "Вкладка 'Гос.Реестр' главная", "Доступ в главном меню Гос.Реестр");

            AddPermission("IsMenuExpertiseMainVisibility", @"Модуль 'Экспертиза'", "Вкладка 'Экспертиза' главная", "Доступ в главном меню Экспертиза");
            AddPermission("IsMenuExpertiseStage1Visibility", @"Модуль 'Экспертиза'", "Вкладка 'Первичная экспертиза'", "Доступ в главном меню Экспертиза");
            AddPermission("IsMenuExpertiseStage2Visibility", @"Модуль 'Экспертиза'", "Вкладка 'Фармацевтическая экспертиза'", "Доступ в главном меню Экспертиза");
            AddPermission("IsMenuExpertiseStage3Visibility", @"Модуль 'Экспертиза'", "Вкладка 'Фармакологическая экспертиза'", "Доступ в главном меню Экспертиза");
            AddPermission("IsMenuExpertiseStage4Visibility", @"Модуль 'Экспертиза'", "Вкладка 'Аналитическая экспертиза'", "Доступ в главном меню Экспертиза");
            AddPermission("IsMenuExpertiseStage5Visibility", @"Модуль 'Экспертиза'", "Вкладка 'Перевод заключения'", "Доступ в главном меню Экспертиза");
            AddPermission("IsMenuExpertiseStage6Visibility", @"Модуль 'Экспертиза'", "Вкладка 'Заключение безопасности'", "Доступ в главном меню Экспертиза");
            AddPermission("IsMenuExpertiseStage7Visibility", @"Модуль 'Экспертиза'", "Вкладка 'Экспертный совет'", "Доступ в главном меню Экспертиза");
            AddPermission("IsMenuExpertiseStage8Visibility", @"Модуль 'Экспертиза'", "Вкладка 'Оплата бугалтерия'", "Доступ в главном меню Экспертиза");
            AddPermission("IsMenuExpertiseStage9Visibility", @"Модуль 'Экспертиза'", "Вкладка 'Настройка этапов'", "Доступ в главном меню Экспертиза");
            AddPermission("IsMenuExpertiseStage10Visibility", @"Модуль 'Экспертиза'", "Вкладка 'Протокола'", "Доступ в главном меню Экспертиза");
            AddPermission("IsMenuExpertiseStage11Visibility", @"Модуль 'Экспертиза'", "Вкладка 'Согласование'", "Доступ в главном меню Экспертиза");
            AddPermission("IsMenuExpertiseReportsVisibility", @"Модуль 'Экспертиза'", "Вкладка 'Отчеты'", "Доступ в главном меню Экспертиза");
            AddPermission("IsMenuExpertiseDocAgreementVisibility", @"Модуль 'Экспертиза'", "Вкладка 'Документы на согласовании'", "Доступ в главном меню Экспертиза");


            // TMC

            AddPermission("IsViewTmcList", @"Возможность просматривать список ТМЦ", "Возможность просматривать список ТМЦ", "Разрешения на действия в Системе");
            AddPermission("IsFrpCenterTmc", @"Право на выдачу ТМЦ со склада ИЦ", "Право на выдачу ТМЦ со склада ИЦ", "Разрешения на действия в Системе");

            // ОБК
            AddPermission("IsMenuSafetyAssessmentVisibility", @"Модуль 'ОБК'", "Вкладка 'ОБК Заявления' главная", "Доступ в главном меню ОБК");
            AddPermission("CanSafetyAssessmentExecutorsAssignment", @"Модуль 'ОБК'", "Распределение заявлений", "Работа с заявлениями ОБК");
            AddPermission("CanSARejectAndReviewButton", @"Модуль 'ОБК'", "Функционал подверждения или отклонения заявки ЦОЗ", "Работа с заявлениями ОБК");
            AddPermission("CanSafetyExpertiseDocumentList", @"Модуль 'ОБК'", "Экспертиза документов", "Работа с заявлениями ОБК");

            // Организационная стуктура
            AddPermission("CanChangeBankUnits", @"Модуль 'Организационная структура'", "Просмотр и добавление банковских реквизитов для организации", "Работа с организационной структурой");
            AddPermission("CanChangeSignerUnits", @"Модуль 'Организационная структура'", "Просмотр и добавление подписывающих лиц для организации", "Работа с организационной структурой");
            AddPermission("CanChangeAddressUnits", @"Модуль 'Организационная структура'", "Просмотр и добавление юридического адреса для организации", "Работа с организационной структурой");

            // ОБК Договоры
            AddPermission("IsMenuOBKContractVisibility", @"Модуль 'ОБК' 'Договоры'", "Вкладка 'ОБК Договоры' главная", "Доступ в главном меню ОБК Договоры");

            // ОБК Договоры меню
            AddPermission("CanViewMenuItemNotAssignedOBKContracts", @"Модуль 'ОБК' 'Договоры'", "Просмотр пункта меню \"Нераспределенные\"", "Работа с договорами ОБК");
            AddPermission("CanViewMenuItemInWorkOBKContracts", @"Модуль 'ОБК' 'Договоры'", "Просмотр пункта меню \"В работе\"", "Работа с договорами ОБК");
            AddPermission("CanViewMenuItemOnCorrectionOBKContracts", @"Модуль 'ОБК' 'Договоры'", "Просмотр пункта меню \"На корректировке у заявителя\"", "Работа с договорами ОБК");
            AddPermission("CanViewMenuItemOnAgreementOBKContracts", @"Модуль 'ОБК' 'Договоры'", "Просмотр пункта меню \"Требуют согласования\"", "Работа с договорами ОБК");
            AddPermission("CanViewMenuItemAgreedOBKContracts", @"Модуль 'ОБК' 'Договоры'", "Просмотр пункта меню \"Согласованные\"", "Работа с договорами ОБК");
            AddPermission("CanViewMenuItemNotAgreedOBKContracts", @"Модуль 'ОБК' 'Договоры'", "Просмотр пункта меню \"Несогласованные\"", "Работа с договорами ОБК");
            AddPermission("CanViewMenuItemRefusedOBKContracts", @"Модуль 'ОБК' 'Договоры'", "Просмотр пункта меню \"Отказанные\"", "Работа с договорами ОБК");
            AddPermission("CanViewMenuItemRequiresSigningOBKContracts", @"Модуль 'ОБК' 'Договоры'", "Просмотр пункта меню \"Требуют подписания\"", "Работа с договорами ОБК");
            AddPermission("CanViewMenuItemRequiresRegistrationOBKContracts", @"Модуль 'ОБК' 'Договоры'", "Просмотр пункта меню \"Требуют регистрации\"", "Работа с договорами ОБК");
            AddPermission("CanViewMenuItemActiveOBKContracts", @"Модуль 'ОБК' 'Договоры'", "Просмотр пункта меню \"Активные\"", "Работа с договорами ОБК");
            AddPermission("CanViewMenuItemPastOBKContracts", @"Модуль 'ОБК' 'Договоры'", "Просмотр пункта меню \"Истекшие\"", "Работа с договорами ОБК");

            // ОБК Договоры
            AddPermission("CanAssignOBKContract", @"Модуль 'ОБК' 'Договоры'", "Функционал распределения договоров", "Работа с договорами ОБК");
            AddPermission("CanViewMeetAndNotMeetRqrmntsBtnObkContract", @"Модуль 'ОБК' 'Договоры'", "Отображать кнопки \"Соответствует требованиям\"/\"Не соответствует требованиям\"", "Работа с договорами ОБК");
            AddPermission("CanViewReturnToApplicantAndSendToBossForApproval", @"Модуль 'ОБК' 'Договоры'", "Отображать кнопки \"Вернуть на доработку\"/\"На согласование руководителю\"", "Работа с договорами ОБК");
            AddPermission("CanViewDoApprovementAndRefuseApprovement", @"Модуль 'ОБК' 'Договоры'", "Отображать кнопки \"Согласовать\"/\"Отказать в согласовании\"", "Работа с договорами ОБК");
            AddPermission("CanViewRegisterAndAttachContract", @"Модуль 'ОБК' 'Договоры'", "Отображать кнопки \"Зарегистрировать\"/\"Прикрепить договор\"", "Работа с договорами ОБК");
            AddPermission("CanOBKPayment", @"Модуль 'ОБК' 'Договоры'", "Отображать кнопку \"Счет на оплату\"", "Работа с договорами ОБК");

            RemoveNonActualKeys();
        }

        private static void AddPermission(string key, string keyName, string keyDescription, string groupName, PermissionValueTypes type = PermissionValueTypes.Classic)
        {
            _actualPermissionKeys.Add(key);

            PermissionKey permissionKey = db.PermissionKeys.FirstOrDefault(o => o.Key == key);
            if (permissionKey != null)
            {
                permissionKey.KeyName = keyName;
                permissionKey.GroupName = groupName;
                permissionKey.KeyDescription = keyDescription;
                db.SaveChanges();
                return;
            }
            permissionKey = new PermissionKey()
            {
                Key = key,
                KeyName = keyName,
                KeyDescription = keyDescription,
                GroupName = groupName
            };
            List<PermissionValue> permissionValueList;
            switch (type)
            {
                case PermissionValueTypes.Classic:
                    permissionValueList = new List<PermissionValue>
                    {
                        new PermissionValue()
                        {
                            PermissionKey = key,
                            Value = "false",
                            Name = "Запрещено",
                            IsDefault = true
                        },
                        new PermissionValue()
                        {
                            PermissionKey = key,
                            Value = "true",
                            Name = "Разрешено",
                            IsDefault = false
                        },
                    };
                    break;
                case PermissionValueTypes.Tree:
                    permissionValueList = new List<PermissionValue>
                    {
                        new PermissionValue()
                        {
                            PermissionKey = key,
                            Value = "employee",
                            Name = "Только себя",
                            IsDefault = true
                        },
                        new PermissionValue()
                        {
                            PermissionKey = key,
                            Value = "departament",
                            Name = "Подразделение",
                            IsDefault = false
                        },
                        new PermissionValue()
                        {
                            PermissionKey = key,
                            Value = "all",
                            Name = "Вся организация",
                            IsDefault = false
                        },
                    };
                    break;
                default:
                    throw new Exception("этого не должно произойти никогда");
            }
            db.PermissionValues.AddRange(permissionValueList);
            db.PermissionKeys.Add(permissionKey);
            var dbPermissionRoleKeys = new List<PermissionRoleKey>();
            var roleIds = db.PermissionRoles.Select(x => x.Id).ToList();
            foreach (var roleId in roleIds)
            {
                var dbKey = new PermissionRoleKey();
                dbKey.PermissionKey = key;
                dbKey.PermissionRoleId = roleId;
                dbKey.PermissionValue = permissionValueList.Single(x => x.IsDefault).Value;
                dbPermissionRoleKeys.Add(dbKey);
            }
            db.PermissionRoleKeys.AddRange(dbPermissionRoleKeys);
            db.SaveChanges();

        }

        private static void RemoveNonActualKeys()
        {
            var nonActualKeys = db.PermissionKeys.Where(x => !_actualPermissionKeys.Contains(x.Key));
            db.PermissionKeys.RemoveRange(nonActualKeys);
            var nonActualKeyValues = db.PermissionValues.Where(x => !_actualPermissionKeys.Contains(x.PermissionKey));
            db.PermissionValues.RemoveRange(nonActualKeyValues);
            var nonActualRoleKeyValues = db.PermissionRoleKeys.Where(x => !_actualPermissionKeys.Contains(x.PermissionKey));
            db.PermissionRoleKeys.RemoveRange(nonActualRoleKeyValues);
            db.SaveChanges();
        }

        public static void AddRoleDefaultPermissionKeys(int roleId)
        {
            var dbPermissionRoleKeys = new List<PermissionRoleKey>();
            var dbPermissionKeys = db.PermissionKeys
                .Join(db.PermissionValues.Where(x => x.IsDefault), x => x.Key, x => x.PermissionKey, (k, v) => new { Key = k.Key, DefaultValue = v.Value }).ToList();
            foreach (var dbPermissionKey in dbPermissionKeys)
            {
                var dbKey = new PermissionRoleKey();
                dbKey.PermissionKey = dbPermissionKey.Key;
                dbKey.PermissionRoleId = roleId;
                dbKey.PermissionValue = dbPermissionKey.DefaultValue;
                dbPermissionRoleKeys.Add(dbKey);
            }
            db.PermissionRoleKeys.AddRange(dbPermissionRoleKeys);
            db.SaveChanges();
        }

        public static void RemoveRolePermissionKeys(int roleId)
        {
            var dbPermissionRoleKeys = db.PermissionRoleKeys.Where(x => x.PermissionRoleId == roleId);
            db.PermissionRoleKeys.RemoveRange(dbPermissionRoleKeys);
            db.SaveChanges();
        }

        private static List<PermissionKey> _keys;

        public static List<PermissionKey> GeKeys()
        {
            if (_keys == null)
            {
                _keys = db.PermissionKeys.ToList();
            }
            return _keys;
        }
        private static List<PermissionValue> _keysValues;

        public static List<PermissionValue> GeKeysValue()
        {
            if (_keysValues == null)
            {
                _keysValues = db.PermissionValues.ToList();
            }
            return _keysValues;
        }
        public static void InitEmploye()
        {

        }

        private static void GetEmployeePermissionIfEmpty()
        {
            if (_employeePermissions == null)
            {
                ncelsEntities ncelsEntities = UserHelper.GetCn();
                var permissions = ncelsEntities.EmployeePermissionRoles.Join(ncelsEntities.PermissionRoleKeys, x => x.PermissionRoleId, x => x.PermissionRoleId,
                    (epr, prk) => new EmployeePermission
                    {
                        EmployeeId = epr.EmployeeId,
                        PermissionKey = prk.PermissionKey,
                        PermissionValue = prk.PermissionValue
                    });
                _employeePermissions = permissions.ToList();
            }
        }

        /// <summary>
        /// нет смысла очищать все в некоторых случаях, но пока так
        /// </summary>
        public static void ClearEmployeePermission()
        {
            _employeePermissions = null;
        }

        private static List<EmployeePermission> _employeePermissions { get; set; }

        private static bool IsVisibility(string key)
        {
            Guid employeeId = UserHelper.GetCurrentEmployee().Id;
            var result = IsVisibility(key, employeeId);
            return result;
        }

        public static bool IsVisibility(string key, Guid employeeId)
        {
            GetEmployeePermissionIfEmpty();
            var result = _employeePermissions.Where(o => o.PermissionKey == key && o.EmployeeId == employeeId).Any(x => bool.Parse(x.PermissionValue));
            return result;
        }

        private static string IsVisibilityString(string key)
        {
            GetEmployeePermissionIfEmpty();

            Guid guid = UserHelper.GetCurrentEmployee().Id;
            var permissions = _employeePermissions.Where(o => o.PermissionKey == key && o.EmployeeId == guid).ToList();
            if (permissions.Any(x => x.PermissionValue == "all"))
            {
                return "all";
            }
            if (permissions.Any(x => x.PermissionValue == "department"))
            {
                return "department";
            }
            return "employee";
        }

        /// <summary>
        /// Модуль служебные записки департамента
        /// </summary>
        public static bool IsMenuCozVisibility
        {
            get { return IsVisibility("IsMenuCozVisibility"); }
        }

        /// <summary>
        /// Модуль Документы ЛИМС
        /// </summary>
        public static bool IsMenuDocumentMainVisibility
        {
            get { return IsVisibility("IsMenuDocumentMainVisibility"); }
        }

        /// <summary>
        /// Модуль служебные записки департамента
        /// </summary>
        public static bool IsMenuTmcMainVisibility
        {
            get { return IsVisibility("IsMenuTmcMainVisibility"); }
        }

        /// <summary>
        /// Модуль служебные записки департамента
        /// </summary>
        public static bool IsMenuEquipmentVisibility
        {
            get { return IsVisibility("IsMenuEquipmentVisibility"); }
        }

        /// <summary>
        /// Модуль Гос.Реестр
        /// </summary>
        public static bool IsMenuStateRegisterVisibility
        {
            get { return IsVisibility("IsMenuStateRegisterVisibility"); }
        }


        /// <summary>
        /// Модуль служебные записки департамента
        /// </summary>
        public static bool IsMenuMyTmcVisibility
        {
            get { return IsVisibility("IsMenuMyTmcVisibility"); }
        }

        /// <summary>
        /// Модуль служебные записки департамента
        /// </summary>
        public static bool IsMenuInnerDepartmentPermission
        {
            get { return IsVisibility("IsMenuInnerDepartmentPermission"); }
        }

        /// <summary>
        /// Модуль орг структура список сотрудников 
        /// </summary>
        public static string IsMenuUnitEmployeeListPermission
        {
            get { return IsVisibilityString("IsMenuUnitEmployeeListPermission"); }
        }

        /// <summary>
        /// Модуль трудовых договоров
        /// </summary>
        public static bool IsMenuContractVisibility
        {
            get { return IsVisibility("IsMenuContractVisibility"); }
        }

        public static bool IsMenuContractSettingsVisibility
        {
            get { return IsVisibility("IsMenuContractSettingsVisibility"); }
        }
        public static bool IsProcHeadContractDashboardVisibility
        {
            get { return IsVisibility("IsProcHeadContractDashboardVisibility"); }
        }
        public static bool IsLawyerContractDashboardVisibility
        {
            get { return IsVisibility("IsLawyerContractDashboardVisibility"); }
        }
        /// <summary>
        /// Модуль dashboard, просмотр dashboarda 
        /// </summary>
        public static string IsAllStructureDashboard
        {
            get { return IsVisibilityString("IsAllStructureDashboard"); }
        }

        /// <summary>
        /// Модуль каровый учет, просмотр отчетов 
        /// </summary>
        public static bool IsMenuUnitReportVisibility
        {
            get { return IsVisibility("IsMenuUnitReportVisibility"); }
        }

        /// <summary>
        /// Модуль каровый учет, просмотр уволенных сотрудников 
        /// </summary>
        public static bool IsMenuUnitDismissedEmployeesVisibility
        {
            get { return IsVisibility("IsMenuUnitDismissedEmployeesVisibility"); }
        }

        /// <summary>
        /// Модуль каровый учет, просмотр уволенных сотрудников 
        /// </summary>
        public static bool IsMenuUnitDismissedEmployeesDepVisibility
        {
            get { return IsVisibility("IsMenuUnitDismissedEmployeesDepVisibility"); }
        }
        /// <summary>
        /// Модуль каровый учет, просмотр уволенных сотрудников 
        /// </summary>
        public static bool IsMenuArchiv
        {
            get { return IsVisibility("IsMenuArchiv"); }
        }
        /// <summary>
        /// Модуль канцелярия 
        /// </summary>
        public static bool IsMenuOfficeVisibility
        {
            get { return IsVisibility("IsMenuOfficeVisibility"); }
        }

        /// <summary>
        /// Модуль меню входящие 
        /// </summary>
        public static bool IsMenuInDocVisibility
        {
            get { return IsVisibility("IsMenuInDocVisibility"); }
        }

        /// <summary>
        /// Модуль меню исходящие 
        /// </summary>
        public static bool IsMenuOutDocVisibility
        {
            get { return IsVisibility("IsMenuOutDocVisibility"); }
        }


        /// <summary>
        /// Модуль меню исходящие 
        /// </summary>
        public static bool IsMenuInitiativeOutDocVisibility
        {
            get { return IsVisibility("IsMenuInitiativeOutDocVisibility"); }
        }

        /// <summary>
        /// Модуль меню обращения 
        /// </summary>
        public static bool IsMenuCitizenDocVisibility
        {
            get { return IsVisibility("IsMenuCitizenDocVisibility"); }
        }

        /// <summary>
        /// Модуль меню ОРД 
        /// </summary>
        public static bool IsMenuAdminDocVisibility
        {
            get { return IsVisibility("IsMenuAdminDocVisibility"); }
        }
        /// <summary>
        /// Модуль меню ОРД по основной деятельности
        /// </summary>
        public static bool IsMenuAdminMainDocVisibility
        {
            get { return IsVisibility("IsMenuAdminMainDocVisibility"); }
        }

        /// <summary>
        /// Модуль меню служебные записки
        /// </summary>
        public static bool IsMenuInnerDocVisibility
        {
            get { return IsVisibility("IsMenuInnerDocVisibility"); }
        }

        /// <summary>
        /// Модуль меню Отчеты 
        /// </summary>
        public static bool IsMenuReportDocVisibility
        {
            get { return IsVisibility("IsMenuReportDocVisibility"); }
        }

        /// <summary>
        /// Модуль мой задания 
        /// </summary>
        public static bool IsMenuTaskVisibility
        {
            get { return IsVisibility("IsMenuTaskVisibility"); }
        }

        /// <summary>
        /// Новые задания
        /// </summary>
        public static bool IsMenuTaskNewVisibility
        {
            get { return IsVisibility("IsMenuTaskNewVisibility"); }
        }

        /// <summary>
        /// Модуль список задания 
        /// </summary>
        public static bool IsMenuTaskListVisibility
        {
            get { return IsVisibility("IsMenuTaskListVisibility"); }
        }

        /// <summary>
        /// Модуль исполненные задания 
        /// </summary>
        public static bool IsMenuTaskExcludeVisibility
        {
            get { return IsVisibility("IsMenuTaskExcludeVisibility"); }
        }

        /// <summary>
        /// Модуль отказанные задания 
        /// </summary>
        public static bool IsMenuTaskRejectVisibility
        {
            get { return IsVisibility("IsMenuTaskRejectVisibility"); }
        }

        /// <summary>
        /// Модуль Мои документы
        /// </summary>
        public static bool IsMenuMyDocVisibility
        {
            get { return IsVisibility("IsMenuMyDocVisibility"); }
        }

        /// <summary>
        /// Модуль Мои документы Проекты 
        /// </summary>
        public static bool IsMenuMyDocProjectVisibility
        {
            get { return IsVisibility("IsMenuMyDocProjectVisibility"); }
        }

        /// <summary>
        /// Модуль Мои документы служебки 
        /// </summary>
        public static bool IsMenuMyDocInnerVisibility
        {
            get { return IsVisibility("IsMenuMyDocInnerVisibility"); }
        }

        /// <summary>
        /// Модуль Мои документы служебки 
        /// </summary>
        public static bool IsMenuMyDocReportVisibility
        {
            get { return IsVisibility("IsMenuMyDocReportVisibility"); }
        }

        /// <summary>
        /// Модуль справочники
        /// </summary>
        public static bool IsMenuDicVisibility
        {
            get { return IsVisibility("IsMenuDicVisibility"); }
        }

        /// <summary>
        /// Модуль Список справочников
        /// </summary>
        public static bool IsMenuDicAllVisibility
        {
            get { return IsVisibility("IsMenuDicAllVisibility"); }
        }

        /// <summary>
        /// Модуль справочник номенклутура
        /// </summary>
        public static bool IsMenuDicNomenVisibility
        {
            get { return IsVisibility("IsMenuDicNomenVisibility"); }
        }

        /// <summary>
        /// Модуль организационная структура
        /// </summary>
        public static bool IsMenuUnitVisibility
        {
            get { return IsVisibility("IsMenuUnitVisibility"); }
        }

        /// <summary>
        /// Модуль организационная структура список
        /// </summary>
        public static bool IsMenuUnitListVisibility
        {
            get { return IsVisibility("IsMenuUnitListVisibility"); }
        }

        /// <summary>
        /// Роли прав доступаы
        /// </summary>
        public static bool IsMenuPermissionRoleListVisibility
        {
            get { return IsVisibility("IsMenuPermissionRoleListVisibility"); }
        }

        /// <summary>
        /// Просмотр логов действий пользователей
        /// </summary>
        public static bool IsMenuActionLogsListVisibility
        {
            get { return IsVisibility("IsMenuActionLogsListVisibility"); }
        }

        /// <summary>
        /// Is Вся номенклатура
        /// </summary>
        public static bool IsAllNomenclature
        {
            get { return IsVisibility("IsAllNomenclature"); }
        }

        /// <summary>
        /// Возможность добавлять BP
        /// </summary>
        public static bool IsAddBp
        {
            get { return IsVisibility("IsAddBp"); }
        }

        /// <summary>
        /// IsMenuProtocolDocVisibility
        /// </summary>
        public static bool IsMenuProtocolDocVisibility
        {
            get { return IsVisibility("IsMenuProtocolDocVisibility"); }
        }
        /// <summary>
        /// Модуль меню Филиалы входящие
        /// </summary>
        public static bool IsMenuInDocRegionsVisibility
        {
            get { return IsVisibility("IsMenuInDocRegionsVisibility"); }
        }

        /// <summary>
        /// Возможность редактировать Доки
        /// </summary>
        public static bool IsEditDoc
        {
            get { return IsVisibility("IsEditDoc"); }
        }

        /// <summary>
        /// Возможность редактировать Кор-ты
        /// </summary>
        public static bool IsEditCor
        {
            get { return IsVisibility("IsEditCor"); }
        }

        public static bool IsEditRefAnalyseIndicator { get { return IsVisibility("IsEditRefAnalyseIndicator"); } }
        public static bool IsEditRefChangeType { get { return IsVisibility("IsEditRefChangeType"); } }
        public static bool IsEditRefNormDocFarm { get { return IsVisibility("IsEditRefNormDocFarm"); } }
        public static bool IsEditRefPrimaryOtd { get { return IsVisibility("IsEditRefPrimaryOtd"); } }

        public static bool IsEditOs
        {
            get { return IsVisibility("IsEditOs"); }
        }

        public static bool IsCreateCorDoc
        {
            get { return IsVisibility("IsCreateCorDoc"); }
        }
        /// <summary>
        /// Доступ к орг структуре
        /// </summary>
        public static bool IsAllEmploye
        {
            get { return IsVisibility("IsAllEmploye"); }
        }

        public static bool IsMenuCommissionVisibility { get { return IsVisibility("IsMenuCommissionVisibility"); } }
        public static bool IsMenuCommissionListVisibility { get { return IsVisibility("IsMenuCommissionListVisibility"); } }


        #region TMC

        /// <summary>
        /// Возможность просмотра списка ТМЦ
        /// </summary>
        public static bool IsViewTmcList
        {
            get { return IsVisibility("IsViewTmcList"); }
        }

        /// <summary>
        /// Права МОЛ ИЛ
        /// </summary>
        public static bool IsFrpCenterTmc
        {
            get { return IsVisibility("IsFrpCenterTmc"); }
        }

        #endregion



        public static bool IsMenuVisitVisibility { get { return IsVisibility("IsMenuVisitVisibility"); } }
        public static bool IsMenuVisitTypeVisibility { get { return IsVisibility("IsMenuVisitTypeVisibility"); } }
        public static bool IsMenuVisitWokringTimeVisibility { get { return IsVisibility("IsMenuVisitWokringTimeVisibility"); } }
        public static bool IsMenuProjectVisibility { get { return IsVisibility("IsMenuProjectVisibility"); } }
        public static bool IsMenuProjectPriceLsVisibility { get { return IsVisibility("IsMenuProjectPriceLsVisibility"); } }
        public static bool IsMenuProjectPriceImnVisibility { get { return IsVisibility("IsMenuProjectPriceImnVisibility"); } }
        public static bool IsMenuProjectRePriceLsVisibility { get { return IsVisibility("IsMenuProjectRePriceLsVisibility"); } }
        public static bool IsMenuProjectProtocolsVisibility { get { return IsVisibility("IsMenuProjectProtocolsVisibility"); } }
        public static bool IsMenuProjectRequestOrdersVisibility { get { return IsVisibility("IsMenuProjectRequestOrdersVisibility"); } }
        public static bool IsMenuProjectRePriceImnVisibility { get { return IsVisibility("IsMenuProjectRePriceImnVisibility"); } }
        public static bool IsMenuProjectRegister { get { return IsVisibility("IsMenuProjectRegister"); } }
        public static bool IsMenuProjectRegisterLsVisibility { get { return IsVisibility("IsMenuProjectRegisterLsVisibility"); } }
        public static bool IsMenuProjectReRegisterLsVisibility { get { return IsVisibility("IsMenuProjectReRegisterLsVisibility"); } }
        public static bool IsMenuProjectChRegisterLsVisibility { get { return IsVisibility("IsMenuProjectChRegisterLsVisibility"); } }
        public static bool IsMenuContractMainVisibility { get { return IsVisibility("IsMenuContractMainVisibility"); } }
        public static bool IsMenuContractIndexVisibility { get { return IsVisibility("IsMenuContractIndexVisibility"); } }
        public static bool IsMenuContractContractExVisibility { get { return IsVisibility("IsMenuContractContractExVisibility"); } }
        public static bool IsMenuProjectRegisterAll { get { return IsVisibility("IsMenuProjectRegisterAll"); } }
        public static bool IsMenuProjectPriceAllVisibility { get { return IsVisibility("IsMenuProjectPriceAllVisibility"); } }
        public static bool IsMenuProjectPriceRpcVisibility { get { return IsVisibility("IsMenuProjectPriceRpcVisibility"); } }
        public static bool IsMenuContractAllVisibility { get { return IsVisibility("IsMenuContractAllVisibility"); } }
        public static bool IsMenuExpertiseMainVisibility { get { return IsVisibility("IsMenuExpertiseMainVisibility"); } }
        public static bool IsMenuExpertiseStage1Visibility { get { return IsVisibility("IsMenuExpertiseStage1Visibility"); } }
        public static bool IsMenuExpertiseStage2Visibility { get { return IsVisibility("IsMenuExpertiseStage2Visibility"); } }
        public static bool IsMenuExpertiseStage3Visibility { get { return IsVisibility("IsMenuExpertiseStage3Visibility"); } }
        public static bool IsMenuExpertiseStage4Visibility { get { return IsVisibility("IsMenuExpertiseStage4Visibility"); } }
        public static bool IsMenuExpertiseStage5Visibility { get { return IsVisibility("IsMenuExpertiseStage5Visibility"); } }
        public static bool IsMenuExpertiseStage6Visibility { get { return IsVisibility("IsMenuExpertiseStage6Visibility"); } }
        public static bool IsMenuExpertiseStage7Visibility { get { return IsVisibility("IsMenuExpertiseStage7Visibility"); } }
        public static bool IsMenuExpertiseStage8Visibility { get { return IsVisibility("IsMenuExpertiseStage8Visibility"); } }
        public static bool IsMenuExpertiseStage9Visibility { get { return IsVisibility("IsMenuExpertiseStage9Visibility"); } }
        public static bool IsMenuExpertiseStage10Visibility { get { return IsVisibility("IsMenuExpertiseStage10Visibility"); } }
        public static bool IsMenuExpertiseStage11Visibility { get { return IsVisibility("IsMenuExpertiseStage11Visibility"); } }
        public static bool IsMenuExpertiseReportsVisibility { get { return IsVisibility("IsMenuExpertiseReportsVisibility"); } }
        public static bool IsMenuExpertiseDocAgreementVisibility
        {
            get
            {
                return /*IsVisibility("IsMenuExpertiseDocAgreementVisibility");*/ true;
            }
        }
        public static bool CanDrugDeclarationExecutorsAssignment
        {
            get
            {
                return IsVisibility("CanDrugDeclarationExecutorsAssignment");
            }
        }
        public static bool IsPriceProjectProcCenterVisibility { get { return IsVisibility("IsPriceProjectProcCenterVisibility"); } }
        public static bool IsPriceProjectChiefVisibility { get { return IsVisibility("IsPriceProjectChiefVisibility"); } }
        public static bool IsPriceProjectExpertVisibility { get { return IsVisibility("IsPriceProjectExpertVisibility"); } }

        #region ОБК
        /// <summary>
        /// модуль обк заявления
        /// </summary>
        public static bool IsMenuSafetyAssessmentVisibility { get { return IsVisibility("IsMenuSafetyAssessmentVisibility"); } }
        /// <summary>
        /// показывать кнопку назначить исполнителя
        /// </summary>
        public static bool CanSafetyAssessmentExecutorsAssignment { get { return IsVisibility("CanSafetyAssessmentExecutorsAssignment"); } }
        /// <summary>
        /// ЦОЗ кнопки вернуть и следеющий этап в одельное право
        /// </summary>
        public static bool CanSARejectAndReviewButton { get { return IsVisibility("CanSARejectAndReviewButton"); } }

        /// <summary>
        /// Показывать кнопку Экспертиза документов в меню ОБК
        /// </summary>
        public static bool CanSafetyExpertiseDocumentList { get { return IsVisibility("CanSafetyExpertiseDocumentList"); } }

        #endregion

        #region Организационная структура настройки реквизитов
        /// <summary>
        /// Возможность редактирования банковских реквизитов в организационной структуре
        /// </summary>
        public static bool CanChangeBankUnits { get { return IsVisibility("CanChangeBankUnits"); }}
        /// <summary>
        /// Возможность редактирования подписывающих лиц в организационной структуре
        /// </summary>
        public static bool CanChangeSignerUnits { get { return IsVisibility("CanChangeSignerUnits"); }}
        /// <summary>
        /// Возможность редактирования Юридического адреса в организационной структуре
        /// </summary>
        public static bool CanChangeAddressUnits { get { return IsVisibility("CanChangeAddressUnits"); } }
        #endregion

        #region ОБК Договоры
        /// <summary>
        /// Отображать модуль ОБК Договоры
        /// </summary>
        public static bool IsMenuOBKContractVisibility { get { return IsVisibility("IsMenuOBKContractVisibility"); } }
        /// <summary>
        /// Просмотр пункта меню "Нераспределенные"
        /// </summary>
        public static bool CanViewMenuItemNotAssignedOBKContracts { get { return IsVisibility("CanViewMenuItemNotAssignedOBKContracts"); } }
        /// <summary>
        /// Просмотр пункта меню "В работе"
        /// </summary>
        public static bool CanViewMenuItemInWorkOBKContracts { get { return IsVisibility("CanViewMenuItemInWorkOBKContracts"); } }
        /// <summary>
        /// Просмотр пункта меню "На корректировке у заявителя"
        /// </summary>
        public static bool CanViewMenuItemOnCorrectionOBKContracts { get { return IsVisibility("CanViewMenuItemOnCorrectionOBKContracts"); } }
        /// <summary>
        /// Просмотр пункта меню "Требуют согласования"
        /// </summary>
        public static bool CanViewMenuItemOnAgreementOBKContracts { get { return IsVisibility("CanViewMenuItemOnAgreementOBKContracts"); } }
        /// <summary>
        /// Просмотр пункта меню "Согласованные"
        /// </summary>
        public static bool CanViewMenuItemAgreedOBKContracts { get { return IsVisibility("CanViewMenuItemAgreedOBKContracts"); } }
        /// <summary>
        /// Просмотр пункта меню "Несогласованные"
        /// </summary>
        public static bool CanViewMenuItemNotAgreedOBKContracts { get { return IsVisibility("CanViewMenuItemNotAgreedOBKContracts"); } }
        /// <summary>
        /// Просмотр пункта меню "Отказанные"
        /// </summary>
        public static bool CanViewMenuItemRefusedOBKContracts { get { return IsVisibility("CanViewMenuItemRefusedOBKContracts"); } }
        /// <summary>
        /// Просмотр пункта меню "Требуют подписания"
        /// </summary>
        public static bool CanViewMenuItemRequiresSigningOBKContracts { get { return IsVisibility("CanViewMenuItemRequiresSigningOBKContracts"); } }
        /// <summary>
        /// Просмотр пункта меню "Требуют регистрации"
        /// </summary>
        public static bool CanViewMenuItemRequiresRegistrationOBKContracts { get { return IsVisibility("CanViewMenuItemRequiresRegistrationOBKContracts"); } }
        /// <summary>
        /// Просмотр пункта меню "Активные"
        /// </summary>
        public static bool CanViewMenuItemActiveOBKContracts { get { return IsVisibility("CanViewMenuItemActiveOBKContracts"); } }
        /// <summary>
        /// Просмотр пункта меню "Истекшие"
        /// </summary>
        public static bool CanViewMenuItemPastOBKContracts { get { return IsVisibility("CanViewMenuItemPastOBKContracts"); } }


        /// <summary>
        /// Имеет возможность распределять договоры
        /// </summary>
        public static bool CanAssignOBKContract { get { return IsVisibility("CanAssignOBKContract"); } }

        /// <summary>
        /// Отображать кнопки "Соответствует требованиям"/"Не соответствует требованиям"
        /// </summary>
        public static bool CanViewMeetAndNotMeetRqrmntsBtnObkContract { get { return IsVisibility("CanViewMeetAndNotMeetRqrmntsBtnObkContract"); } }
        /// <summary>
        /// Отображать кнопки "Вернуть на доработку"/"На согласование руководителю"
        /// </summary>
        public static bool CanViewReturnToApplicantAndSendToBossForApproval { get { return IsVisibility("CanViewReturnToApplicantAndSendToBossForApproval"); } }
        /// <summary>
        /// Отображать кнопки "Согласовать"/"Отказать в согласовании"
        /// </summary>
        public static bool CanViewDoApprovementAndRefuseApprovement { get { return IsVisibility("CanViewDoApprovementAndRefuseApprovement"); } }
        /// <summary>
        /// Отображать кнопки "Зарегистрировать"/"Прикрепить договор"
        /// </summary>
        public static bool CanViewRegisterAndAttachContract { get { return IsVisibility("CanViewRegisterAndAttachContract"); } }
        #endregion


        #region ОБК опалата
        /// <summary>
        /// Отображать кнопку "Счет на оплату"
        /// </summary>
        public static bool CanOBKPayment { get { return IsVisibility("CanOBKPayment"); } }
        #endregion
    }
}