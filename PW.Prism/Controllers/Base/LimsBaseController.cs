using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Interfaces;
using PW.Ncels.Database.Repository.Lims;

namespace PW.Prism.Controllers.Base
{
    public class LimsBaseController : Controller
    {
        // Filter By Current User
        protected IQueryable<T> FilterByCurrentUser<T>(IQueryable<T> query, ncelsEntities dbContext) where T: class, ITmcRequest
        {
            var user = UserHelper.GetCurrentEmployee();
            var headDep = dbContext.Units.FirstOrDefault(d => d.Code == OrganizationConsts.HeadCode);
            if (user != null && headDep != null && headDep.BossId != user.Id.ToString())
                query = query.Where(t => t.CreatedEmployeeId == user.Id);

            return query;
        }

        protected IQueryable<T> FilterOwnerByCurrentUser<T>(IQueryable<T> query, ncelsEntities dbContext) where T : class, ITmcRequest
        {
            var user = UserHelper.GetCurrentEmployee();
            var headDep = dbContext.Units.FirstOrDefault(d => d.Code == OrganizationConsts.HeadCode);
            if (user != null && headDep != null && headDep.BossId != user.Id.ToString())
                query = query.Where(t => t.OwnerEmployeeId == user.Id);

            return query;
        }

        protected string ApplyAction(Guid tmcOutId, int status)
        {
            string msg = string.Empty;
            try
            {
                OrderTmcRepository repository = new OrderTmcRepository();
                var tmcOut = repository.GetById(tmcOutId);
                tmcOut.StateType = status;
                repository.Update(tmcOut);
                repository.Save();
            }
            catch (Exception e)
            {
                msg = e.ToString();
            }
            return msg;
        }
        protected string ApplyAction(Guid tmcOutId, int status, string comment)
        {
            string msg = string.Empty;
            try
            {
                OrderTmcRepository repository = new OrderTmcRepository();
                var tmcOut = repository.GetById(tmcOutId);
                tmcOut.StateType = status;
                tmcOut.Comment = comment;
                repository.Update(tmcOut);
                repository.Save();
            }
            catch (Exception e)
            {
                msg = e.ToString();
            }
            return msg;
        }
    }
}