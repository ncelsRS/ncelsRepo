using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Constants
{
    public class OperatorConsts
    {
        public const string AgreeCode = "AGREE";
        public const string AgreedCode = "AGREED";

        public const string RejectCode = "REJECT";
        public const string RejectedCode = "REJECTED";

        public const string ReturnCode = "RETURN";
        public const string ReturnedCode = "RETURNED";

        public const string SendCode = "SEND";
        public const string SendedCode = "SENDED";


        public static string GetDisplayText(string code)
        {
            if (code == AgreeCode)
                return "На согласовании";

            if (code == AgreedCode)
                return "Согласовано";

            return code;
        }
    }
}
