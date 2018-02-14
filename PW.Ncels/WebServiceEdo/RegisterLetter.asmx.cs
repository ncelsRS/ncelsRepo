using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Repository.OBK;
using PW.Ncels.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            LetterWithEdoRepository rep = new LetterWithEdoRepository(false);
            return rep.saveRegNomerFromEdo(long.Parse(edo.IdLetterObk), edo.ID_intLetter, Convert.ToDateTime(edo.DateIntLetter));
        }

        [WebMethod]
        public bool RegisterProjectLetterFromEdo(CommingRegProInfoFromEdo edo)
        {

            return true;
        }


        [WebMethod]
        public bool RecieveAnswerLetterFromEdo(CommingLetterFromEdo edoLetter)
        {
            long result = 0;
            try
            {
                LetterWithEdoRepository rep = new LetterWithEdoRepository(false);
                OBK_LetterFromEdo edo = new OBK_LetterFromEdo();
                edo.IdEdo = edoLetter.id_edo;
                edo.LetterRegNomer = edoLetter.id_letter_edo;
                edo.LetterText = edoLetter.letter_text;
                edo.LetterRegDate = Convert.ToDateTime(edoLetter.date_letter_edo);
                edo.UserEdo = edoLetter.user_edo;
                edo.LetterPortalEdoID = long.Parse(edoLetter.letter_obk);

                //string fileNameWithPath = Server.MapPath("/Content/json/rite.txt");
                //if (!File.Exists(fileNameWithPath))
                //{
                //    string createText = "Hello and Welcome" + edoLetter.id_edo+ edoLetter.id_letter_edo+ edoLetter.letter_text+ edoLetter.date_letter_edo+ edoLetter.user_edo+ edoLetter.letter_obk;
                //    File.WriteAllText(fileNameWithPath, createText);
                //}

                result = rep.saveIncomingLetter(edo);
                if (result != 0 && edoLetter.attachments.Count != 0)
                {
                    List<OBK_LetterAttachments> list = new List<OBK_LetterAttachments>();
                    foreach (CommingAttachFileFromEdo d in edoLetter.attachments)
                    {
                        OBK_LetterAttachments a = new OBK_LetterAttachments();
                        a.SignXmlBigData = d.sign;
                        a.LetterId = result;
                        a.ContentFile = System.Text.Encoding.UTF8.GetBytes(d.content);
                        list.Add(a);
                    }
                    rep.saveAttachFilesFromEdo(list);
                }
            }
            catch (Exception e)
            {
                return false;
            }
            if (result == 0)
                return false;
            else
                return true;
        }
    }
}
