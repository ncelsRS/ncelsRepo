using Ncels.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PW.Ncels.Database.Helpers
{

    public static class MonthHelper
    {

        public static readonly Dictionary<String, String> MonthsRu = new Dictionary<String, String>()
        {
        {"01", "январь"},
        {"02", "февраль"},
        {"03", "март"},
        {"04", "апрель"},
        {"05", "май"},
        {"06", "июнь"},
        {"07", "июль"},
        {"08", "август"},
        {"09", "сентябрь"},
        {"10", "октябрь"},
        {"11", "ноябрь"},
        {"12", "декабрь"},
        };

        public static readonly Dictionary<String, String> MonthsKz = new Dictionary<String, String>()
        {
        {"01", "қаңтар"},
        {"02", "ақпан"},
        {"03", "наурыз"},
        {"04", "сәуiр"},
        {"05", "мамыр"},
        {"06", "маусым"},
        {"07", "шiлде"},
        {"08", "тамыз"},
        {"09", "қыркүйек"},
        {"10", "қазан"},
        {"11", "қараша"},
        {"12", "желтоқсан"},
        };

        public static string getMonthRu(DateTime? date)
        {
            if (date == null)
            {
                return "";
            }

            var month = ((DateTime)date).ToString("MM");

            return MonthsRu[month];
        }

        public static string getMonthKz(DateTime? date)
        {
            if (date == null)
            {
                return "";
            }

            var month = ((DateTime)date).ToString("MM");

            return MonthsKz[month];
        }

    }

}