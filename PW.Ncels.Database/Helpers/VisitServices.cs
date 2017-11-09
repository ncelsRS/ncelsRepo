using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Models;

namespace PW.Ncels.Database.Helpers
{
   public class VisitServices
    {
        private static VisitServices _instance;


        public static VisitServices Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new VisitServices();
                }
                return _instance;
            }
        }


        public async Task<object> GetOutgoing(ncelsEntities db, ModelRequest request, int type)
        {
            try
            {
                var employeeId = UserHelper.GetCurrentEmployee().Id;

                //Database query
                var v =  db.VisitsViews.Where(o => o.VisitorId == employeeId).AsQueryable();
                //search
                if (!string.IsNullOrEmpty(request.SearchValue))
                {
                    //poka net poiska
                    //v =
                    //    v.Where(
                    //        a =>
                    //            a.Number.Contains(request.SearchValue) || a.Summary.Contains(request.SearchValue)
                    //            );
                }

                //sort
                if (!(string.IsNullOrEmpty(request.SortColumn) && string.IsNullOrEmpty(request.SortColumnDir)))
                {
                    //for make sort simpler we will add Syste.Linq.Dynamic reference
                    v = v.OrderBy(request.SortColumn + " " + request.SortColumnDir);
                }
                int recordsTotal = await v.CountAsync();
                var dbVisits = v.Skip(request.Skip).Take(request.PageSize);
                return
                    new
                    {
                        draw = request.Draw,
                        recordsFiltered = recordsTotal,
                        recordsTotal = recordsTotal,
                        Data = await dbVisits.Select(m=>
                        new
                        {
                            m.VisitId,
                            m.VisitComment,
                            m.VisitStatusId,
                            m.VisitStatusName,
                            m.VisitTypeId,
                            m.VisitTypeName,
                            m.VisitTypeGroup,
                            m.VisitDate,
                            m.VisitTimeBegin,
                            m.VisitDuration,
                            m.VisitRatingValue,
                            m.VisitRatingComment,
                            m.EmployeeId,
                            m.EmployeeLastName,
                            m.EmployeeFirstName,
                            m.EmployeeMiddleName,
                        }).ToListAsync()
                    };
            }

            catch (Exception e)
            {
                return new { IsError = true, Message = e.Message };
            }

        }
        //public async Task<object> GetCurrentList(ncelsEntities db, ModelRequest request,Guid id)
        //{
        //    try
        //    {
        //        var employeeId = UserHelper.GetCurrentEmployee().Id.ToString();

        //        //Database query
        //        var v =  db.Documents.Where(o => o.IsDeleted == false && o.AnswersId==id.ToString() && (o.DocumentType==0  || o.DocumentType == 1)).AsQueryable();
        //        //search
        //        if (!string.IsNullOrEmpty(request.SearchValue))
        //        {
        //            v =
        //                v.Where(
        //                    a =>
        //                        a.Number.Contains(request.SearchValue) || a.Summary.Contains(request.SearchValue)
        //                        );
        //        }

        //        //sort
        //        if (!(string.IsNullOrEmpty(request.SortColumn) && string.IsNullOrEmpty(request.SortColumnDir)))
        //        {
        //            //for make sort simpler we will add Syste.Linq.Dynamic reference
        //            v = v.OrderBy(request.SortColumn + " " + request.SortColumnDir);
        //        }


        //        int recordsTotal = await v.CountAsync();
        //        var expertiseViews = v.Skip(request.Skip).Take(request.PageSize);
        //        return
        //            new
        //            {
        //                draw = request.Draw,
        //                recordsFiltered = recordsTotal,
        //                recordsTotal = recordsTotal,
        //                Data =  expertiseViews.Select(m=>new {m.Id,m.Summary,m.Number,m.DocumentDate,m.AttachPath}).ToList().Select(m => new {
        //        m.Id,
        //        m.Summary,
        //        m.Number,
        //        m.DocumentDate,
        //        Items = UploadHelper.GetFilesInfo(m.AttachPath, false)
        //    })
        //            };
        //    }

        //    catch (Exception e)
        //    {
        //        return new { IsError = true, Message = e.Message };
        //    }

        //}
    }
}
