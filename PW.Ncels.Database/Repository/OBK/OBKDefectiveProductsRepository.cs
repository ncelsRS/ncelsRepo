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
    public class OBKDefectiveProductsRepository : ARepository
    {

        public IQueryable<OBK_DefectiveProductsView> ListNotSended()
        {
            return AppContext.OBK_DefectiveProductsView.Where(o => o.OBK_StageSendEDOId == null);
        }

        public IQueryable<OBK_DefectiveProductsView> ListSended()
        {
            return AppContext.OBK_DefectiveProductsView.Where(o => o.OBK_StageSendEDOId != null);
        }

        public bool SendEDO(List<Guid> ides)
        {
            foreach (var temp in ides)
            {
                OBK_StageSendEDO sendEdo = new OBK_StageSendEDO();
                sendEdo.Id = Guid.NewGuid();
                AppContext.OBK_StageSendEDO.Add(sendEdo);

                var expDocument = AppContext.OBK_StageExpDocument.FirstOrDefault(o => o.Id == temp);
                expDocument.OBK_StageSendEDOId = sendEdo.Id;
            }
            AppContext.SaveChanges();
            return true;
        }

        public bool SaveLetterDetails(string number, DateTime date, List<Guid> ides)
        {
            var expDocs = AppContext.OBK_StageExpDocument.Where(o => ides.Contains(o.Id)).Select(o => o.OBK_StageSendEDOId);
            var sendeds = AppContext.OBK_StageSendEDO.Where(o => expDocs.Contains(o.Id)).ToList();

            foreach(var temp in sendeds)
            {
                temp.Number = number;
                temp.SendDate = date;
            }
            AppContext.SaveChanges();
            return true;
        }

        public IQueryable<OBK_DefectiveProductsView> LetterList(List<Guid> list)
        {
            return AppContext.OBK_DefectiveProductsView.Where(o => list.Contains(o.Id));
        }

    }
}
