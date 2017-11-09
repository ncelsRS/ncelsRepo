using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Models;
using System.Linq;
using System.Linq.Dynamic;
namespace PW.Ncels.Database.Helpers
{
    public class ProjectServices
    {
        private static ProjectServices _instance;


        public static ProjectServices Instance {
            get {
                if (_instance == null) {
                    _instance = new ProjectServices();
                }
                return _instance;
            }
        }

		public async Task<object> GetProject(ncelsEntities db, ModelRequest request,bool isRegisterProject,int? type) {
			try {
                //Database query
                var employeeId = UserHelper.GetCurrentEmployee().Id;
                var org = UserHelper.GetCurrentEmployee();
				var v = type!=null ? db.ProjectsViews.Where(m=>m.IsRegisterProject.Value == isRegisterProject && m.Type == type && m.OwnerId == employeeId).AsQueryable()
                    : db.ProjectsViews.Where(m => m.IsRegisterProject.Value == isRegisterProject && m.OwnerId == employeeId).AsQueryable();
				//search
				if (!string.IsNullOrEmpty(request.SearchValue)) {
					v =
						v.Where(
							a =>
								a.Number.Contains(request.SearchValue) || a.NameRu.Contains(request.SearchValue) ||
								a.TypeValue.Contains(request.SearchValue) || a.StausValue.Contains(request.SearchValue) ||
								a.NameRu.Contains(request.SearchValue));
				}

				//sort
			    if (!(string.IsNullOrEmpty(request.SortColumn) && string.IsNullOrEmpty(request.SortColumnDir))) {
					//for make sort simpler we will add Syste.Linq.Dynamic reference
					// сортировка по номеру заявления
			        if (request.SortColumn == "Number")
			        {
			            if (request.SortColumnDir == "asc")
			            {
                            v =
                            v.ToList()
                                .OrderBy(
                                    x =>
                                        (!String.IsNullOrEmpty(x.Number))
                                            ? Convert.ToDouble(x.Number.Replace('.', ','))
                                            : default(double)).AsQueryable();
                        }
			            else
			            {
                            v =
                            v.ToList()
                                .OrderByDescending(
                                    x =>
                                        (!String.IsNullOrEmpty(x.Number))
                                            ? Convert.ToDouble(x.Number.Replace('.', ','))
                                            : default(double)).AsQueryable();
                        }
			        }
			        else
			        {
                        v = v.OrderBy(request.SortColumn + " " + request.SortColumnDir);
                    }
                }


                int recordsTotal = v.Count();
				var expertiseViews = v.Skip(request.Skip).Take(request.PageSize);
				return
					new {
						draw = request.Draw,
						recordsFiltered = recordsTotal,
						recordsTotal = recordsTotal,
						Data = expertiseViews
					};
			}

			catch (Exception e) {
				return new { IsError = true, Message = e.Message };
			}

		}

        public async Task<object> GetPriceRework(ncelsEntities db, ModelRequest request,int type)
        {
            try
            {
                //Database query
                var employeeId = UserHelper.GetCurrentEmployee().Id;
                var org = UserHelper.GetCurrentEmployee();
                var v =  db.ProjectsViews.Where(m => m.Type==type && m.OwnerId == employeeId && (m.Status==3 || m.Status == 5)).AsQueryable();
                //search
                if (!string.IsNullOrEmpty(request.SearchValue))
                {
                    v =
                        v.Where(
                            a =>
                                a.Number.Contains(request.SearchValue) || a.NameRu.Contains(request.SearchValue) ||
                                a.TypeValue.Contains(request.SearchValue) || a.StausValue.Contains(request.SearchValue) ||
                                a.NameRu.Contains(request.SearchValue));
                }

                //sort
                if (!(string.IsNullOrEmpty(request.SortColumn) && string.IsNullOrEmpty(request.SortColumnDir)))
                {
                    //for make sort simpler we will add Syste.Linq.Dynamic reference
                    v = v.OrderBy(request.SortColumn + " " + request.SortColumnDir);
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

        public async Task<object> GetSrReestrView(ncelsEntities db, ModelRequest request,int type) {
            //Database query
            var v = db.SrReestrViews.Where(m=>m.type == type).AsQueryable();
            //search
            if (!string.IsNullOrEmpty(request.SearchValue)) {
                v =  v.Where(a =>a.reg_number.Contains(request.SearchValue) 
                ||a.C_int_name.Contains(request.SearchValue)
                ||a.name.Contains(request.SearchValue)
                //|| a.concentration.Contains(request.SearchValue)
                //|| a.C_dosage_form_name.Contains(request.SearchValue)
                //|| a.C_atc_code.Contains(request.SearchValue)
                || a.C_producer_name.Contains(request.SearchValue)
                );
            }

            int recordsTotal = await v.CountAsync();
            var expertiseViews = v.OrderBy(o=>o.id).Skip(request.Skip).Take(request.PageSize);
            var list = await expertiseViews.AsNoTracking().ToListAsync();

            return new { draw = request.Draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, Data = list };
        }

        public async Task<object> GetOrphanDrugsViewView(ncelsEntities db, ModelRequest request)
        {

            //Database query
            var v = db.OrphanDrugsIcdDeseasesViews.AsQueryable();
            //var v = db.OrphanDrugsIcdDeseasesViews.AsQueryable();
            //search
            if (!string.IsNullOrEmpty(request.SearchValue))
            {
                v = v.Where(a => a.CodeICD.Contains(request.SearchValue)
               || a.Name.Contains(request.SearchValue)
               || a.DiseaseOfICD.Contains(request.SearchValue)
               || a.SysnonimAndRareDesease.Contains(request.SearchValue)
                );
            }

            //sort
            //if (!(string.IsNullOrEmpty(request.SortColumn) && string.IsNullOrEmpty(request.SortColumnDir))) {
            //    //for make sort simpler we will add Syste.Linq.Dynamic reference
            //    v = v.OrderBy(request.SortColumn + " " + request.SortColumnDir);
            //}


            int recordsTotal = await v.CountAsync();
            var expertiseViews = v.OrderBy(o => o.Name).Skip(request.Skip).Take(request.PageSize);

            return new { draw = request.Draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, Data = await expertiseViews.ToListAsync() };
        }

        public Organization GetOrganizationPrice(ncelsEntities db,Guid orgId,  int type) {
            var employeeId = UserHelper.GetCurrentEmployee().Id;

            var priceProject =  db.PriceProjects.FirstOrDefault(m => m.OwnerId == employeeId);
            if (priceProject==null)
                 return new Organization() { Id = orgId, Type = type, DocDate = DateTime.Now, DocExpiryDate = DateTime.Now};
            switch (type) {
                case 9:
                case 0:
                case 3:
                case 6:
                    var orgHolder = db.Organizations.FirstOrDefault(o => o.Id == priceProject.HolderOrganizationId);
                    if (orgHolder==null)
                        return new Organization() { Id = orgId, Type = type, DocDate = DateTime.Now, DocExpiryDate = DateTime.Now };
                    orgHolder.Id = orgId;
                    return orgHolder;
                case 1:
                case 4:
                case 7:
                case 10:
                    var orgProxy = db.Organizations.FirstOrDefault(o => o.Id == priceProject.ProxyOrganizationId);
                    if (orgProxy == null)
                        return new Organization() { Id = orgId, Type = type, DocDate = DateTime.Now, DocExpiryDate = DateTime.Now };
                    orgProxy.Id = orgId;
                    return orgProxy;
                default:
                       return new Organization() { Id = orgId, Type = type, DocDate = DateTime.Now, DocExpiryDate = DateTime.Now };
            }
        }
    }
}