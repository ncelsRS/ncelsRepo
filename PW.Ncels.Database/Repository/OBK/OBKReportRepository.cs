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

        public IQueryable<DirectionPaymentsReportView> DirectionPaymentsReportList()
        {
            return AppContext.DirectionPaymentsReportViews;
        }

        public IQueryable<ZBKDetailReportView> ZBKDetailReportView()
        {
            return AppContext.ZBKDetailReportViews;
        }

        public IQueryable<OBK_DefectiveProductsReportView> OBK_DefectiveProductsReportView()
        {
            return AppContext.OBK_DefectiveProductsReportView;
        }

        public IQueryable<OBK_GMPReportView> OBK_GMPReportView()
        {
            return AppContext.OBK_GMPReportView;
        }

        public IQueryable<OBK_ZBKApplicationReportView> ZBKApplicationReportView()
        {
            return AppContext.OBK_ZBKApplicationReportView;
        }

        public IQueryable<SummaryReportTF_Result> OBK_SummaryReportTF(Nullable<System.DateTime> dateFrom, Nullable<System.DateTime> dateTo)
        {
            return AppContext.SummaryReportTF(dateFrom, dateTo);
        }

        public IQueryable<OBK_LaboratoryFunction_Result> OBK_LaboratoryFunction(Guid? unitLaboratoryId)
        {
            return AppContext.OBK_LaboratoryFunction(unitLaboratoryId).Where(o => o.Tests != null && !"".Equals(o.Tests) && o.ProtocolResult != 2);
        }

        public IQueryable<LaboratoryListFunction_Result> LaboratoryListFunction_Result()
        {
            return AppContext.LaboratoryListFunction(UserHelper.GetCurrentEmployee().OrganizationId).Where(o => !"Руководство".Equals(o.Name));
        }

        public IQueryable<OBK_SpecialistsReport> OBK_SpecialistsReport()
        {
            return AppContext.OBK_SpecialistsReport.Where(o => o.ProtocolResult != 2);
        }

        public object LaboratoryWorkers()
        {
            return AppContext.OBK_laboratoryWorkers.Select(o => new { Id = o.Id, Name = o.Executor}).ToList();
        }

        public IEnumerable<object> OBK_Ref_Type()
        {
            return AppContext.OBK_Ref_Type.Where(o => o.ViewOption == 1).Select(o => new { Id = o.Id, Name = o.NameRu});
        }
    }
}
