using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

namespace Glav.WebApiDemo.Web.Framework
{
	public class PagingInfoModelBinder : System.Web.Http.ModelBinding.IModelBinder
	{
		public bool BindModel(System.Web.Http.Controllers.HttpActionContext actionContext, System.Web.Http.ModelBinding.ModelBindingContext bindingContext)
		{
			var boundModel = new PagingInfo();

			// Way of getting values via ValueProvider
			int queryvalue;
			var pageValue = bindingContext.ValueProvider.GetValue("page");
			if (pageValue == null || !int.TryParse(pageValue.AttemptedValue, out queryvalue))
			{
				boundModel.Page = 1;
			}
			else
			{
				boundModel.Page = queryvalue;
			}
			var pagesizeValue = bindingContext.ValueProvider.GetValue("pagesize");
			if (pagesizeValue == null || !int.TryParse(pagesizeValue.AttemptedValue, out queryvalue))
			{
				boundModel.PageSize = 25;
			}
			else
			{
				boundModel.PageSize = queryvalue;
			}

			var sortfieldValue = bindingContext.ValueProvider.GetValue("sortfields");
			var sortFields = sortfieldValue != null ? sortfieldValue.AttemptedValue : string.Empty;

			var fieldList = new List<string>();
			if (!string.IsNullOrWhiteSpace(sortFields))
			{
				fieldList.AddRange(sortFields.Split(new char[] { '|' }));
			}
			boundModel.SortFields = fieldList;

			#region Alternate way of extracting values from the Url
			//UriTemplateMatch pagingTemplate = new UriTemplateMatch();
			//pagingTemplate.Template = new UriTemplate("page={0}&pagesize={1}&sortfields={2}");
			//pagingTemplate.RequestUri = new Uri(actionContext.Request.RequestUri.AbsoluteUri.ToLowerInvariant());

			//if (pagingTemplate.QueryParameters.Count > 0)
			//{
			//    boundModel.Page = SafeGetQueryValueAsInt(pagingTemplate.QueryParameters, "page");
			//    boundModel.PageSize = SafeGetQueryValueAsInt(pagingTemplate.QueryParameters, "pagesize");
			//    var sortFields = SafeGetQueryValueAsString(pagingTemplate.QueryParameters, "sortfields");

			//    var fieldList = new List<string>();
			//    if (!string.IsNullOrWhiteSpace(sortFields))
			//    {
			//        fieldList.AddRange(sortFields.Split(new char[] {'|'}));
			//    }
			//    boundModel.SortFields = fieldList;
			//}
			#endregion

			if (boundModel.Page <= 0)
			{
				boundModel.Page = 1;
			}
			if (boundModel.PageSize <= 0)
			{
				boundModel.PageSize = 25;
			}

			bindingContext.Model = boundModel;
			return true;
		}

		#region Helper methods
		private int SafeGetQueryValueAsInt(NameValueCollection queryParameters, string queryStringKey)
		{
			int val = 0;
			if (queryParameters.AllKeys.Contains(queryStringKey))
			{
				if (!int.TryParse(queryParameters[queryStringKey], out val))
					val = 0;
			}
			return val;
		}
		private string SafeGetQueryValueAsString(NameValueCollection queryParameters, string queryStringKey)
		{
			if (queryParameters.AllKeys.Contains(queryStringKey))
			{
				return queryParameters[queryStringKey];
			}
			return string.Empty;
		}
		#endregion



	}
}