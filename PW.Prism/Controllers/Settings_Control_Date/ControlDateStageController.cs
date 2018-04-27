using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Repository.DictionaryRep;
using PW.Ncels.Database.Repository.Equipment;
using PW.Ncels.Database.Repository.Lims;
using PW.Ncels.Database.Repository.Security;
using PW.Ncels.Database.Repository.OBK;

namespace PW.Prism.Controllers.Settings_Control_Date
{
    public class ControlDateStageController : Controller
    {
        // GET: ControlDateStage
        public ActionResult Index()
        {
            Guid guid = Guid.NewGuid();

            return PartialView(guid);
        }
    }
}