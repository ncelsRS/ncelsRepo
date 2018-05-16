using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using PW.Ncels.Database.Models;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace PW.Ncels.Helpers
{
    public static class DataTablesHelper
    {
        public static ModelResult ToDataSourceResult(this IQueryable query, ModelRequest request)
        {
            var recordsTotal = query.Count();
            if (!(string.IsNullOrEmpty(request.SortColumn) && string.IsNullOrEmpty(request.SortColumnDir)))
            {
                query = query.OrderBy(request.SortColumn + " " + request.SortColumnDir);
            }
            else
            {
                query = query.OrderBy("Id desc");
            }
            query = query.Skip(request.Skip).Take(request.PageSize);
            return new ModelResult()
            {
                draw = request.Draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                Data = Execute(query)
            };
        }

        public static ModelResult ToDataSourceResult<TModel, TResult>(this IQueryable<TModel> query, ModelRequest request, Expression<Func<TModel, TResult>> selector)
        {
            var recordsTotal = query.Count();
            if (!(string.IsNullOrEmpty(request.SortColumn) && string.IsNullOrEmpty(request.SortColumnDir)))
            {
                query = query.OrderBy(request.SortColumn + " " + request.SortColumnDir);
            }
            else
            {
                query = query.OrderBy("Id desc");
            }
            var queryProjection = query.Skip(request.Skip).Take(request.PageSize).Select(selector);
            return new ModelResult()
            {
                draw = request.Draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                Data = Execute(queryProjection)
            };
        }
        private static IEnumerable Execute(this IQueryable source)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            Type elementType = source.ElementType;
            IList instance = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));
            try
            {
                foreach (object obj in (IEnumerable)source)
                    instance.Add(obj);
            }
            catch (Exception e)
            {
                throw;
            }
            return (IEnumerable)instance;
        }
    }
}