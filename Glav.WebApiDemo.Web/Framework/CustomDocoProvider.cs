using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace Glav.WebApiDemo.Web.Framework
{
	public class CustomDocoProvider : IDocumentationProvider
	{
		public string GetDocumentation(System.Web.Http.Controllers.HttpParameterDescriptor parameterDescriptor)
		{
			return string.Empty;
		}

		public string GetDocumentation(System.Web.Http.Controllers.HttpActionDescriptor actionDescriptor)
		{
			
			return "{" + actionDescriptor.ActionName + " Documentation Placeholder}";
		}
	}
}