using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Glav.WebApiDemo.Web.Framework;
using System.Text;

namespace Glav.WebApiDemo.Web.Controllers
{
    public class CustomBindingController : ApiController
    {
		/// <summary>
		/// Dodgy Demo of binding some dodgy data toa dodgy model
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public string Post(DodgyModel model)
		{
			// Do something dodgy with dodgy model

			var msg = new StringBuilder();
			msg.AppendFormat("Received DodgyObject-> Name:{0}, Number Of Attributes:{1}",model.Name,model.Atributes.Count);
			model.Atributes.ForEach(a => msg.AppendFormat(", Attrib:{0}",a));
			return msg.ToString();
		}

		/// <summary>
		/// Get the site stats.
		/// </summary>
		/// <returns>Numbers of awesomeness</returns>
		public string GetStatistics()
		{
			return ConstructStatisticsText();
		}

		private string ConstructStatisticsText()
		{
			var cache = System.Web.HttpContext.Current.Cache;
			var stats = cache["stats"] as SiteStats;
			if (stats == null)
			{
				stats = new SiteStats();
			}

			var output = new StringBuilder();
			foreach (var urlHit in stats.UrlCount)
			{
				output.AppendFormat("<br />Url:[{0}] hit {1} times.", urlHit.Key, urlHit.Value);
			}
			foreach (var methodHit in stats.HttpMethodCount)
			{
				output.AppendFormat("<br />Http Method:[{0}] used {1} times.", methodHit.Key, methodHit.Value);
			}
			foreach (var codeHit in stats.StatusCodeCount)
			{
				output.AppendFormat("<br />Http Status Code:[{0}] returned {1} times.", codeHit.Key, codeHit.Value);
			}
			return output.ToString();
		}
    }
}
