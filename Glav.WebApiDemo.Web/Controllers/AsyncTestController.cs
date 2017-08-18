using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;

namespace Glav.WebApiDemo.Web.Controllers
{
    public class AsyncTestController : ApiController
    {
        //
        // GET: /AsyncTest/

        public Task<string> Get()
        {
			var startTaskId = System.Threading.Tasks.Task.CurrentId;
			System.Threading.Thread.CurrentThread.Name = "Glavs Thread";

            return Task.Factory.StartNew<string>(() =>
                {
                    System.Threading.Thread.Sleep(3000);
                    var internalTaskId = System.Threading.Tasks.Task.CurrentId;
                    return string.Format("Just slept for 3 seconds and I feel refreshed. Starting Task Id:[{0}], Currently running Task Id:[{1}], Currently running ThreadName:[{2}]",
                                            startTaskId, internalTaskId, System.Threading.Thread.CurrentThread.Name);
                });
        }

    }
}
