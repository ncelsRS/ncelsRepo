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
    public class ContractServices
    {
        private static ContractServices _instance;


        public static ContractServices Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ContractServices();
                }
                return _instance;
            }
        }


        public async Task<object> GetContracts(ncelsEntities db, ModelRequest request,int ? code)
        {
            try
            {
                var employeeId = UserHelper.GetCurrentEmployee().Id;
                //Database query
                var v = code != null ? db.ContractsViews.Where(m=>m.Status==code && m.OwnerId==employeeId).AsQueryable() : db.ContractsViews.Where(m=>m.OwnerId == employeeId).AsQueryable();
                //search
                if (!string.IsNullOrEmpty(request.SearchValue))
                {
                    v =
                        v.Where(
                            a =>
                                a.Number.Contains(request.SearchValue) || a.ManufactureOrgName.Contains(request.SearchValue)
                                );
                }

                //sort
                if (!(string.IsNullOrEmpty(request.SortColumn) && string.IsNullOrEmpty(request.SortColumnDir))) {
                    //for make sort simpler we will add Syste.Linq.Dynamic reference
                    v = v.OrderBy(request.SortColumn + " " + request.SortColumnDir);
                }
                else {
                    v = v.OrderByDescending(m => m.CreatedDate);
                }


                int recordsTotal = await v.CountAsync();
                var expertiseViews = v.Skip(request.Skip).Take(request.PageSize);
                return
                    new
                    {
                        draw = request.Draw,
                        recordsFiltered = recordsTotal,
                        recordsTotal = recordsTotal,
                        Data = await expertiseViews.ToListAsync()
                    };
            }

            catch (Exception e)
            {
                return new { IsError = true, Message = e.Message };
            }

        }

        public async Task<object> ReadSigning(ncelsEntities db, ModelRequest request)
        {
            try
            {
                var employeeId = UserHelper.GetCurrentEmployee().Id.ToString();

                //Database query
                var v = db.Tasks
                    .Join(db.Documents,t=>t.DocumentId,d=>d.Id,(t,d)=>new {t,d})
                    .Join(db.Contracts,cd=>cd.d.Id,c=>c.Id,(cd,c)=>new {cd,c})
                    .Where(m => m.cd.t.Type == 6 && m.cd.t.State==0 && m.cd.t.ExecutorId == employeeId).OrderBy(m=>m.cd.t.CreatedDate).AsQueryable();


                //search
                if (!string.IsNullOrEmpty(request.SearchValue))
                {
                    v =
                        v.Where(
                            a =>
                                a.cd.t.Number.Contains(request.SearchValue) || a.cd.t.Text.Contains(request.SearchValue)
                                );
                }

                //sort
                //if (!(string.IsNullOrEmpty(request.SortColumn) && string.IsNullOrEmpty(request.SortColumnDir)))
                //{
                //    //for make sort simpler we will add Syste.Linq.Dynamic reference
                //    v = v.OrderBy(request.SortColumn + " " + request.SortColumnDir);
                //}
            


                int recordsTotal = await v.CountAsync();
                var expertiseViews = v.Skip(request.Skip).Take(request.PageSize);
                return
                    new
                    {
                        draw = request.Draw,
                        recordsFiltered = recordsTotal,
                        recordsTotal = recordsTotal,
                        Data = await expertiseViews.Select(m=>new { m.cd.t.Id, m.cd.t.State, m.cd.t.Text, m.cd.t.DocumentId, m.cd.t.CreatedDate, m.cd.t.DocumentValue, m.cd.t.Number, m.c.Type}).ToListAsync()
                    };
            }

            catch (Exception e)
            {
                return new { IsError = true, Message = e.Message };
            }

        }

        public async Task<object> ReadSigned(ncelsEntities db, ModelRequest request)
        {
            try
            {
                var employeeId = UserHelper.GetCurrentEmployee().Id.ToString();

                //Database query
                var v = db.Documents
                    .Join(db.Contracts, d => d.Id, c => c.Id, (d, c) => new { d, c })
                   .Where(m =>  m.d.RegistratorId == employeeId && m.c.Status==2).OrderBy(m => m.c.ContractDate).AsQueryable();
                //search
                if (!string.IsNullOrEmpty(request.SearchValue))
                {
                    v =
                        v.Where(
                            a =>
                                a.c.Number.Contains(request.SearchValue)
                                );
                }

              //  sort
                //if (!(string.IsNullOrEmpty(request.SortColumn) && string.IsNullOrEmpty(request.SortColumnDir)))
                //{
                //    //for make sort simpler we will add Syste.Linq.Dynamic reference
                //    v = v.OrderBy(request.SortColumn + " " + request.SortColumnDir);
                //}


                int recordsTotal = await v.CountAsync();
                var expertiseViews = v.Skip(request.Skip).Take(request.PageSize);
                return
                    new
                    {
                        draw = request.Draw,
                        recordsFiltered = recordsTotal,
                        recordsTotal = recordsTotal,
                        Data = await expertiseViews.Select(m=>new { m.c.Id, DocumentId=m.d.Id, m.c.ContractDate,m.c.Number,m.c.Status, m.c.Type}).ToListAsync()
                    };
            }

            catch (Exception e)
            {
                return new { IsError = true, Message = e.Message };
            }

        }
    }
}
