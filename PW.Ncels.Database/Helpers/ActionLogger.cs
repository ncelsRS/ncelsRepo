using System;
using System.Linq;
using System.Web;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Enums;

namespace PW.Ncels.Database.Helpers
{
    public static class ActionLogger
    {
        public static void Write(LogPlace place, Guid? employeeId, string text, string additionalText = null)
        {
            NcelsEntities context = UserHelper.GetCn();
            Write(context, place, employeeId, text, additionalText);
            context.SaveChanges();
        }

        public static void WriteInt(string text, string additionalText = null)
        {
            NcelsEntities context = UserHelper.GetCn();
            WriteInt(context, text, additionalText);
            context.SaveChanges();
        }

        public static void WriteInt(NcelsEntities context, Guid? employeeId, string text, string additionalText = null, bool withoutIp = false)
        {
            Write(context, LogPlace.Int, employeeId, text, additionalText, withoutIp);
        }

        public static void WriteInt(NcelsEntities context, string text, string additionalText = null)
        {
            var e = UserHelper.GetCurrentEmployee();
            var employeeId = e == null ? (Guid?)null : e.Id;
            Write(context, LogPlace.Int, employeeId, text, additionalText);
        }

        public static void WriteExt(string text, string additionalText = null)
        {
            NcelsEntities context = UserHelper.GetCn();
            WriteExt(context, text, additionalText);
            context.SaveChanges();
        }

        public static void WriteExt(NcelsEntities context, string text, string additionalText = null)
        {
            var e = UserHelper.GetCurrentEmployee();
            var employeeId = e == null ? (Guid?)null : e.Id;
            Write(context, LogPlace.Ext, employeeId, text, additionalText);
        }

        public static void WriteExt(NcelsEntities context, Guid? employeeId, string text, string additionalText = null, bool withoutIp = false)
        {
            Write(context, LogPlace.Ext, employeeId, text, additionalText, withoutIp);
        }

        public static void Write(NcelsEntities context, LogPlace place, Guid? employeeId, string text,  string additionalText = null, bool withoutIp = false)
        {
            string ipAddress = "";
            if (withoutIp == false)
            {
                try
                {
                    ipAddress = HttpContext.Current.Request.UserHostAddress;
                }
                catch (Exception ex)
                {
                    ipAddress = "ошибка получения: " + ex.Message;
                    if (ipAddress.Length > 1000)
                    {
                        ipAddress = ipAddress.Substring(0, 1000);
                    }
                }
            }
            var lg = new ActionLog();
            lg.Date = DateTime.Now;
            lg.EmployeeId = employeeId;
            lg.PlaceId = (int)place;
            lg.Text = text;
            lg.Type = 1; 
            lg.AdditionalText = additionalText;
            lg.IpAddress = ipAddress;
            context.ActionLogs.Add(lg);
        }
    }
}