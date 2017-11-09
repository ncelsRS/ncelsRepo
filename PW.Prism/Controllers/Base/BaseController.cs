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

namespace PW.Prism.Controllers.Base
{
    public class BaseController : Controller
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
    }
}