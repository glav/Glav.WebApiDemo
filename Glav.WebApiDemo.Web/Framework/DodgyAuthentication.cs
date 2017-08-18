using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net;
using System.Web.Http;

namespace Glav.WebApiDemo.Web.Framework
{
	public class DodgyAuthentication : DelegatingHandler
	{
		protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
		{
			// Only try and "authenticate" if going to "TestAuth" path/controller
			if (request.RequestUri.ToString().ToLowerInvariant().Contains("/testauthhandler"))
			{
				// If the request contains our super hard to crack token,
				// then they may pass...
				if (request.RequestUri.ToString().ToLowerInvariant().Contains("isglav=true"))
				{
					return base.SendAsync(request, cancellationToken);
				}

                // Return unauthorised status code
				return System.Threading.Tasks.Task.Factory.StartNew(() =>
				{
					return request.CreateResponse<string>(HttpStatusCode.Unauthorized,"You shall not pass");
				});
			}


			return base.SendAsync(request, cancellationToken);
		}
	}
}