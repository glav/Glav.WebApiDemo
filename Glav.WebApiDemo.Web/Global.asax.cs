using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Services;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Glav._WebApiDemo.Web.Data;
using Ninject.Web.Mvc;
using Ninject;
using Glav.WebApiDemo.Web;
using Glav.WebApiDemo.Web.Framework;
using Glav.WebApiDemo.Web.Controllers;
using System.Web.Http.Description;
using System.Web.Http.SelfHost;
using System.Web.Http.ModelBinding;

namespace Glav._WebApiDemo.Web
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class WebApiApplication : System.Web.HttpApplication
	{
		private static HttpSelfHostServer _server = null;

		protected void Application_Start()
		{
			RegisterDependencies();

			AreaRegistration.RegisterAllAreas();

			SetupApiRoutes();
			SetupMessageHandlers();
			SetupCustomFormatters();
			SetupDocumentationProvider();
			SetupSelfHostedServer();

			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

		}


		private void SetupDocumentationProvider()
		{
			// Simple documentation provider
			GlobalConfiguration.Configuration.Services.Replace(typeof(IDocumentationProvider), new CustomDocoProvider());

			// Xml comment doco provider
			//GlobalConfiguration.Configuration.Services.Replace(typeof(IDocumentationProvider), new XmlCommentDocumentationProvider(HttpContext.Current.Server.MapPath("~/Doco/Glav.WebApiDemo.Web.XML")));
		}

		private void SetupMessageHandlers()
		{
			// Register our delegating handler
			GlobalConfiguration.Configuration.MessageHandlers.Add(new ApiUsageLogger());
			GlobalConfiguration.Configuration.MessageHandlers.Add(new DodgyAuthentication());
		}

		private void SetupCustomFormatters()
		{
			// Register our dodgy formatter
			GlobalConfiguration.Configuration.Formatters.Add(new DodgyMediaFormatter());
		}

		private void SetupApiRoutes()
		{
			RouteTable.Routes.MapHttpRoute("Simple-specific", "Beer/{kindOfBeer}", new { controller = "Simple", action = "GetMeSomeBeer" });
			RouteTable.Routes.MapHttpRoute("default-http", "api/{controller}/{slideNumber}");
		}

		private void RegisterDependencies()
		{
			var kernel = new StandardKernel();
			kernel.Bind<IRepository>().To<Repository>();

			// Hookup our ModelBinderProvider.
			kernel.Bind<ModelBinderProvider>().To<GlavsModelBinderProvider>();


			//Note!: The IDependencyResolver in System.Web.Mvc is different
			// to the IDependencyResolver in System.Web.Http

			// This does MVC (ie. System.Web.Mvc.IDependencyResolver)
			DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

			// This does the Web Api (ie. System.Web.Http.Dependencies.IDependencyResolver)
			var apiResolver = new ApiDependencyResolver(kernel);
			GlobalConfiguration.Configuration.DependencyResolver = apiResolver;
		}

		private void SetupSelfHostedServer()
		{
			try
			{
				var host = System.Web.HttpContext.Current.Request.Url.Host;

				var config = new HttpSelfHostConfiguration("http://" + host + ":8000");
				config.Routes.MapHttpRoute("test", "selfhost/{controller}");

				System.Threading.Tasks.Task.Factory.StartNew(() => {
						using (_server = new HttpSelfHostServer(config))
						{
                            // returns a System.Threading.Task
                            var openTask = _server.OpenAsync();
                            // Ask the server task to wait until it completes
                            openTask.Wait();

							while (true) { }
						}
					});
			}
			catch (Exception ex)
			{
				// this aint gonna work in Azure so just swallow for now
				System.Diagnostics.Trace.WriteLine(string.Format("Exception creating SelfHost: {0}",ex.Message));
			}
		}
	}
}