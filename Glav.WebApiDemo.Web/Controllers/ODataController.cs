using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Glav.WebApiDemo.Web.Models;
using Glav._WebApiDemo.Web.Data;

namespace Glav.WebApiDemo.Web.Controllers
{
    public class ODataController : ApiController
    {
		private IRepository _repo;

		public ODataController(IRepository repo)
		{
			_repo = repo;
		}

		//[Queryable]// <-- was available in RC
		public IQueryable<Slide> Get()
		{
			return _repo.GetAllSlides().AsQueryable();
		}
    }
}
