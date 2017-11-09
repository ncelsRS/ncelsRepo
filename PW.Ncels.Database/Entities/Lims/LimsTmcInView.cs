using PW.Ncels.Database.Interfaces;

namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    public partial class LimsTmcInView : ITmcRequest
    {
        public string StateTypeValue
        {
            get
            {
                switch (StateType)
                {
                    case -1:
                        return "Отклонен";
                    case 0:
                        return "Новый";
                    case 1:
                        return "Отправлен в 1С";
                    case 2:
                        return "Получена из 1С";
                    case 3:
                        return "Закрыт";
                    case 4:
                        return "Аннулирован";
                    case 5:
                        return "Принятие переданы в 1С";
                    case 10:
                        return "Согласование ИЦ";
                    case 11:
                        return "Согласование Бухгалтерия";
                    case 12:
                        return "Согласование Руководство";
                }
                return "не известный статус (" + StateType + ")";
            }
        }
    }
}