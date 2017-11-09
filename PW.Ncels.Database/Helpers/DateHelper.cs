using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Helpers
{
    public class DateHelper
    {
        public const string JS_START_DATE = "1/1/1970";
        public const string DATE_TIME_FORMAT = "dd.MM.yyyy HH:mm";
        public const string JS_DATE_TIME_FORMAT = "dd.mm.yy";

        public const string DATE_FORMAT = "dd/MM/yyyy";
        public const string JS_DATE_FORMAT = "dd/mm/yy";

        public static long GetJavascriptTimestamp(DateTime input)
        {
            var span = new TimeSpan(DateTime.Parse(JS_START_DATE).Ticks);
            var time = input.Subtract(span);
            return time.Ticks / 10000;
        }

        public static DateTime GetPreviousMonthLastDay()
        {
            var now = DateTime.Now.AddMonths(-1);
            var need = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));
            return need;
        }

        public static string GetDate(DateTime? date)
        {
            return date != null ? date.Value.ToString(DATE_FORMAT, CultureInfo.InvariantCulture) : null;
        }

        public static DateTime? GetDate(string date)
        {
            if (String.IsNullOrEmpty(date))
            {
                return null;
            }
            return GetDateFormat(date);
        }


        public static string GetDateTime(DateTime? date)
        {
            return date != null ? date.Value.ToString(DATE_TIME_FORMAT, CultureInfo.InvariantCulture) : null;
        }

        public static DateTime? GetDateTime(string date)
        {
            if (String.IsNullOrEmpty(date))
            {
                return null;
            }
            return GetDateTimeFormat(date);
        }


        private static DateTime? GetDateTimeFormat(string val)
        {
            DateTime date;
            if (DateTime.TryParseExact(val, DATE_TIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out date))
            //                DateTime.TryParse(val, out date))
            {
                return date;
            }
            return null;
        }

        private static DateTime? GetDateFormat(string val)
        {
            DateTime date;
            if (DateTime.TryParseExact(val, DATE_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            //                DateTime.TryParse(val, out date))
            {
                return date;
            }
            return null;
        }
    }
}
