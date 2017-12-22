using PW.Ncels.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace PW.Ncels.WebServiceEdo
{
    /// <summary>
    /// Summary description for RegisterLetter
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RegisterLetter : System.Web.Services.WebService
    {

        [WebMethod]
        public bool RegisterLetterFromEdo(CommingRegInfoFromEdo edo)
        {
            return true;
        }


        [WebMethod]
        public bool RecieveAnswerLetterFromEdo(CommingLetterFromEdo edoLetter)
        {
            return true;
        }
    }
}
