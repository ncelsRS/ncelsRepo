using System;
using System.Linq;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Enums;

namespace PW.Ncels.Database.Helpers
{
    public static class ExpStageNameHelper
    {
        public static string GetName(int stageId)
        {
            return GetName(stageId.ToString());
        }

        public static string GetName(string stageId)
        {
            switch (stageId)
            {
                case EXP_DIC_Stage.ProcCenter:
                    return "ЦОЗ";
                case EXP_DIC_Stage.PrimaryExp:
                    return "Первичная экспертиза";
                case EXP_DIC_Stage.PharmaceuticalExp:
                    return "Фармацевтическая экспертиза";
                case EXP_DIC_Stage.PharmacologicalExp:
                    return "Фармакологическая экспертиза";
                case EXP_DIC_Stage.AnalyticalExp:
                    return "Аналитическая экспертиза";
                case EXP_DIC_Stage.Translate:
                    return "Перевод";
                case EXP_DIC_Stage.SafetyConclusion:
                    return "ЗОБ";
                default:
                    return stageId;
            }
        }
    }
}