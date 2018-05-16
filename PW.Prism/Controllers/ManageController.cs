using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace PW.Prism.Controllers {
    public class ManageController : Controller {

        private ncelsEntities db = UserHelper.GetCn();
        
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult Execute(string key, string text, string type) {
            if (key != "{E9365724-93CA-4689-9DD7-5D76D13CDA10}") {
                return Json(new {success = false, text = "Не верный ключ!"});
            }

            bool isSuccess = true;
            string result;
            
            switch (type) {
                case "Update":
                    try {
                        //result = $"<tr><td>(строк обработано: {db.Database.ExecuteSqlCommand(text)})</td></tr>";
                        result = string.Format("<tr><td>(строк обработано: {0})</td></tr>", db.Database.ExecuteSqlCommand(text));
                    }
                    catch (Exception exception) {
                        isSuccess = false;
                        result = exception.Message;
                    }
                    break;
                case "Select":
                    try {
                        //text = $"select({text} FOR XML RAW('tr'), ELEMENTS ) as Name";
						text = string.Format("select({0} FOR XML RAW('tr'), ELEMENTS ) as Name", text);
                        IList<Item> documents = db.Database.SqlQuery<Item>(text).Take(1).ToList();
                        result = documents[0].Name;
                    }
                    catch (Exception exception)
                    {
                        isSuccess = false;
                        result = exception.Message;
                    }
                    break;
                default:
                    isSuccess = false;
                    result = "Не верный тип запроса";
                    break;
            }
            if (isSuccess) {
                return Json(new { success = true, text = result });
            }
            return Json(new {success = false, text = result });
        }
    }
}