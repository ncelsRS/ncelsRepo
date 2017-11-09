using System;
using System.Linq;
using System.Web;

namespace PW.Ncels.Database.Models
{
	public class ModelRequest
	{
		public ModelRequest()
		{

            var draw = int.Parse(HttpContext.Current.Request.Form.GetValues("draw").FirstOrDefault());
            //paging parameter
            var start = HttpContext.Current.Request.Form.GetValues("start").FirstOrDefault();
			var length = HttpContext.Current.Request.Form.GetValues("length").FirstOrDefault();
			//sorting parameter
			var sortColumn = HttpContext.Current.Request.Form.GetValues("columns[" + HttpContext.Current.Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
			var sortColumnDir = HttpContext.Current.Request.Form.GetValues("order[0][dir]").FirstOrDefault();
			//filter parameter
			var searchValue = HttpContext.Current.Request.Form.GetValues("search[value]").FirstOrDefault();
			
			int pageSize = length != null ? Convert.ToInt32(length) : 0;
			int skip = start != null ? Convert.ToInt32(start) : 0;
			Draw = draw;
			Start = start;
			Length = length;
			SortColumn = sortColumn;
			SortColumnDir = sortColumnDir;
			SearchValue = searchValue;
			PageSize = pageSize;
			Skip = skip;
		}

		public int Draw { get; set; }
		public string Start { get; set; }
		public string Length { get; set; }
		public string SortColumn { get; set; }
		public string SortColumnDir { get; set; }
		public string SearchValue { get; set; }
		public int PageSize { get; set; }
		public int Skip { get; set; }
	}
}