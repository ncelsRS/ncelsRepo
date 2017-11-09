using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using Antlr.Runtime;
using Aspose.Pdf;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Database.Repository.Expertise;
using Document = PW.Ncels.Database.DataModel.Document;

namespace PW.Prism.Controllers
{
    [Authorize]
    public class OBKReferenceController : Controller
    {
        private ncelsEntities db = UserHelper.GetCn();

        public ActionResult ContactAsync()
        {
            return PartialView();
        }

        // GET: /Reference/
        public ActionResult Index()
        {
            Guid guid = Guid.NewGuid();
            ViewBag.DictionaryList = DictionaryHelper.GetOBKList();
            return PartialView(guid);
        }
    }
}
