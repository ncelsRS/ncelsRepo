using System;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Interfaces;

namespace PW.Ncels.Database.DataModel
{
    public partial class LimsTmcOutView: ITmcRequest
    {
        //public Guid? MeasureTypeConvertDicId { get; set; }

        public string OutTypeDicValue
        {
            get { return LocalizationHelper.GetString(OutTypeName, OutTypeNameKz); }
        }

        public string StateTypeValue => StateTypeValueStatic(StateType);

        public static string StateTypeValueStatic(int stateType)
        {
            
                switch (stateType)
                {
                    case TmcOut.TmcOutStatuses.Ordered:
                        return "Заказан";

                    case TmcOut.TmcOutStatuses.New:
                        return "Новая";

                    case TmcOut.TmcOutStatuses.Issued:
                        return "Выдан";

                    case TmcOut.TmcOutStatuses.Sended:
                        return "Отправлен";

                    case TmcOut.TmcOutStatuses.Rejected:
                        return "Отклонен";

                    case TmcOut.TmcOutStatuses.Repealed:
                        return "Аннулирован";
                }
                return "Неизвестный статус";
            
        }


        public bool IsEdit { get; set; }

        public decimal CountActual { get; set; }

    }
}