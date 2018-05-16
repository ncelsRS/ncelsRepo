using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Schema;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Models;

namespace PW.Ncels.Database.Helpers
{
   public class NotificationServices
    {
        private static NotificationServices _instance;


        public static NotificationServices Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NotificationServices();
                }
                return _instance;
            }
        }


      
        public async Task<object> GetCurrentList(ncelsEntities db, ModelRequest request)
        {
            try {
                var employeeId = UserHelper.GetCurrentEmployee().Id;

                //Database query
                var v =  db.Notifications.Where(o => o.EmployeesId == employeeId.ToString()).OrderBy(m=>m.CreatedDate).AsQueryable();
                //search
                if (!string.IsNullOrEmpty(request.SearchValue))
                {
                    v =
                        v.Where(
                            a =>
                                a.Note.Contains(request.SearchValue) 
                                );
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
                        Data =  expertiseViews
                    };
            }

            catch (Exception e)
            {
                return new { IsError = true, Message = e.Message };
            }

        }

        public async Task<object> GetNewNotification(ncelsEntities context)
        {
            var employeeId = UserHelper.GetCurrentEmployee().Id;

            var data =
                await context.Notifications.Where(m => !m.IsRead && m.EmployeesId == employeeId.ToString())
                     .OrderByDescending(m => m.CreatedDate)
                     .Take(3).ToListAsync();
            var count = await context.Notifications.CountAsync(m => !m.IsRead && m.EmployeesId == employeeId.ToString());
            return new { Data = data, Count = count };
        }


        public ModelMessage SetViewed(ncelsEntities context, List<int> list)
        {
            try
            {
                foreach (var item in list)
                {
                    context.Notifications.Find(item).IsRead = true;
                }
                context.SaveChanges();
                return new ModelMessage() { IsError = false };
            }
            catch (Exception ex)
            {
                return new ModelMessage() { ErrorText = ex.Message, IsError = true };
            }
        }

        public async Task<List<Notification>> GetListAllNotification(ncelsEntities context)
        {
            var employeeId = UserHelper.GetCurrentEmployee().Id;

            return await
                context.Notifications.Where(m => m.EmployeesId == employeeId.ToString())
                    .OrderByDescending(m => !m.IsRead)
                    .ThenByDescending(m => m.CreatedDate)
                    .ToListAsync();
        }
    }
}
