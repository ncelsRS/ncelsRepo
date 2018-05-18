export enum EntityTypes{
  contract = 'contract',
  application = 'application',
  payment = 'payment'
}

export enum FilePermissions{
  all,
  add,
  newVersion,
  delete
}

export const FileTypeCodes = {
  letterManager: {
    key: 'letterManager',
    value: '1.Доверенность на менеджера'
  },
  letManufactur: {
    key: 'letManufactur',
    value: '2.Доверенность от производителя'
  },
  application: {
    key: 'application',
    value: 'Заявление'
  },
  certificate: {
    key: 'certificate',
    value: 'Документ, удостоверяющий регистрацию в стране производителе или держателе регистрационного удостоверения (регистрационное удостоверение, Сертификат свободной продажи (FreeSale), Сертификат на экспорт и т.д.) с аутентичным переводом на русский язык, заверенный нотариально'
  },
  declaration: {
    key: 'declaration',
    value: 'Документ, подтверждающий класс безопасности в зависимости от степени потенциального риска применения (Декларация соответствия; письмо-обоснование от производителя и т.д) с аутентичным переводом на русский язык, заверенный нотариально'
  }
};
