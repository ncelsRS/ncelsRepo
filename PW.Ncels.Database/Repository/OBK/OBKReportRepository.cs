using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Ncels.Helpers;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Models.Expertise;
using PW.Ncels.Database.Models.OBK;
using PW.Ncels.Database.Repository.Expertise;
using System.Data.Objects.SqlClient;
using System.Web.Script.Serialization;
using PW.Ncels.Database.Notifications;
using static PW.Ncels.Database.Constants.CodeConstManager;

namespace PW.Ncels.Database.Repository.OBK
{
    public class OBKReportRepository : ARepository
    {
        public IQueryable<DeclarationReportView> DeclarationReportList()
        {
            return AppContext.DeclarationReportViews;
        }

        public IQueryable<OBK_ZBKReportView> ZBKReportList()
        {
            return AppContext.OBK_ZBKReportView;
        }

        public IQueryable<OBK_ZBKCopyReportView> ZBKCopyReportList()
        {
            return AppContext.OBK_ZBKCopyReportView;
        }
    }
}
