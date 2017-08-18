using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;

namespace Glav.WebApiDemo.Web.Controllers
{
    public class GenericController : ApiController
    {
		public HttpResponseMessage PostGeneric(HttpRequestMessage request)
		{
			var body = request.Content.ReadAsAsync<object>().Result;
			var jsonBody = body as Newtonsoft.Json.Linq.JContainer;
			var jsonItem = jsonBody.Values<Newtonsoft.Json.Linq.JProperty>().First();
			var returnMsg = string.Format("{0}={1}", jsonItem.Name,jsonItem.Value.ToString());
			
			var response = request.CreateResponse<string>(HttpStatusCode.OK, string.Format("You sent: [Body] {0}  [json]{1}", body,returnMsg));
			
			// Modify/add some header values
			response.Headers.Add("WebServer", "Web1");
			response.Headers.Add("Glav", "Bald");
			response.Headers.Age = TimeSpan.FromSeconds(60);
			return response;
		}
    }

}
