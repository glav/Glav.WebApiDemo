using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Glav.WebApiDemo.Web.Framework;

namespace Glav.WebApiDemo.Web.Controllers
{
    public class ModelBinderController : ApiController
    {
		public string Get(PagingInfo pagingInfo)
		{
			var sortFields = pagingInfo.SortFields == null ? "none" : string.Join(" AND ",pagingInfo.SortFields.ToArray()); 
			return string.Format("Returning Next {0} records from page:{1}, Sort Fields:{2}", pagingInfo.PageSize, pagingInfo.Page, sortFields);
		}

		public string Get(PagingInfo pagingInfo, string stuff)
		{
			var sortFields = pagingInfo.SortFields == null ? "none" : string.Join(" AND ", pagingInfo.SortFields.ToArray());
			return string.Format("Using input of {0}, I am going to return the next {1} records from page:{2} sort by: {3}", stuff, pagingInfo.PageSize, pagingInfo.Page, sortFields);
		}
	}
}
