using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Ninject;
using Ninject.Syntax;
using Ninject.Activation.Blocks;
using Ninject.Activation;
using Ninject.Parameters;

namespace Glav.WebApiDemo.Web
{
	public class ApiDependencyResolver : ApiScopeContainer, IDependencyResolver
	{
		private IKernel _kernel;
		public ApiDependencyResolver(IKernel kernel)
			: base(kernel)
		{
			_kernel = kernel;
		}
		public IDependencyScope BeginScope()
		{
			return new ApiScopeContainer(_kernel.BeginBlock());
		}
	}

	public class ApiScopeContainer : IDependencyScope
	{
		// Note: Could also use IResolutionRoot here
		private IResolutionRoot _root;

		public ApiScopeContainer(IResolutionRoot root)
		{
			_root = root;
		}
		public object GetService(Type serviceType)
		{
			IRequest request = _root.CreateRequest(serviceType, null, new Parameter[0], true, true);
			return _root.Resolve(request).SingleOrDefault();
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			IRequest request = _root.CreateRequest(serviceType, null, new Parameter[0], true, true);
			return _root.Resolve(request).ToList();
		}
		public void Dispose()
		{
			var disposableRoot = _root as IDisposable;
			if (disposableRoot != null)
			{
				disposableRoot.Dispose();
				_root = null;
			}
		}
	}

}