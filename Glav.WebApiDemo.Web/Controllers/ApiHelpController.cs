using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;

namespace Glav.WebApiDemo.Web.Controllers
{
    public class ApiHelpController : Controller
    {
        //
        // GET: /Help/

        public ActionResult Index()
        {
			var explorer = GlobalConfiguration.Configuration.Services.GetApiExplorer();
            return View(explorer);
        }

    }
}
