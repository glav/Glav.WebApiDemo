using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace Glav.WebApiDemo.Web.Controllers
{
	
    public class TestAuthController : ApiController
    {
		[Authorize]
		public string Get()
		{
			return "Bugger off";
		}

		[AllowAnonymous]
		public string Get(string auth)
		{
			return "Hi. We dont care who you are, come right in.";
		}
    }
}
