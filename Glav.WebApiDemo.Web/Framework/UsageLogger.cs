using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.IO;

namespace Glav.WebApiDemo.Web.Framework
{
	public class ApiUsageLogger : DelegatingHandler
	{
		protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
		{
			// Extract the request logging information
			ExtractLoggingInfoFromRequest(request);

			// Execute the request
			var response = base.SendAsync(request, cancellationToken);

			response.ContinueWith((responseMsg) =>
			{
				// Extract the response logging info then persist the information
				ExtractResponseLoggingInfo(responseMsg.Result);

			});
			return response;
		}

		private void ExtractLoggingInfoFromRequest(HttpRequestMessage request)
		{
			IncrementUsageCount(StatsType.HttpMethod, request.Method.Method);
			IncrementUsageCount(StatsType.Url, request.RequestUri.AbsoluteUri);

			// Be carefull reading the incoming content as it can really only
			// be read once. Once read, it can be unavailable to your actions
			// Need to use the LoadIntoBufferAsync call to copy it

            //request.Content.LoadIntoBufferAsync().ContinueWith(t =>
            //    {
            //var memStream = new MemoryStream();
            //request.Content.CopyToAsync(memStream).ContinueWith(task =>
            //    {
            //        memStream.Position = 0;
            //        using (var rdr = new StreamReader(memStream))
            //        {
            //            var content = rdr.ReadToEnd();
            //            // Do something with the contents...
            //        }
            //    });
            //    });
		}

		private void ExtractResponseLoggingInfo(HttpResponseMessage response)
		{
			IncrementUsageCount(StatsType.StatusCode, string.Format("StatusCode:{0}", response.StatusCode));
		}

		private void IncrementUsageCount(StatsType type, string apiItem)
		{
			//Note: In the async collection of response info,
			//      the System.Web.HttpContext.Current is NULL
			//      so must use System.Web.HttpRuntime.Cache
			if (System.Web.HttpRuntime.Cache != null)
			{
				var cache = System.Web.HttpRuntime.Cache;
				var item = cache["stats"] as SiteStats;
				if (item == null)
				{
					item = new SiteStats();
				}
				item.IncrementCount(type, apiItem);
				cache.Add("stats", item, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
			}
		}


	}

	#region Helper types
	public enum StatsType
	{
		Url,
		HttpMethod,
		StatusCode,
		LastBodyPost
	}
	public class SiteStats
	{
		public Dictionary<string, int> UrlCount = new Dictionary<string, int>();
		public Dictionary<string, int> HttpMethodCount = new Dictionary<string, int>();
		public Dictionary<string, int> StatusCodeCount = new Dictionary<string, int>();
		public string LastBodyContent = null;

		public void IncrementCount(StatsType listType, string key)
		{
			if (listType == StatsType.LastBodyPost)
			{
				LastBodyContent = key;
			}
			var list = UrlCount;
			switch (listType)
			{
				case StatsType.HttpMethod:
					list = HttpMethodCount;
					break;
				case StatsType.StatusCode:
					list = StatusCodeCount;
					break;
			}
			if (list.ContainsKey(key))
			{
				var count = list[key] + 1;
				list[key] = count;
			}
			else
			{
				list[key] = 1;
			}
		}
	}
	#endregion

}