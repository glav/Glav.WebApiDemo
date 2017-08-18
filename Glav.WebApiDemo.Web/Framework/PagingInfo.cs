using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Glav.WebApiDemo.Web.Framework
{
	[FromUri]
	public class PagingInfo
	{
		public int Page { get; set; }
		public int PageSize { get; set; }
		public List<string> SortFields { get; set; }
	}
}