using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace Glav.WebApiDemo.Web.Controllers
{
	
    public class TestAuthHandlerController : ApiController
    {
		public string Get()
		{
			return "The secrets of the universe are revealed here";
		}

		public string Get(string stuff)
		{
			return "Hi. We dont care who you are, come right in and leave your "+stuff+" at the door.";
		}
    }
}
