using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Glav.WebApiDemo.Web.Framework
{
	public class GlavsModelBinderProvider : System.Web.Http.ModelBinding.ModelBinderProvider
	{
        public override System.Web.Http.ModelBinding.IModelBinder GetBinder(System.Web.Http.HttpConfiguration configuration, Type modelType)
		{
			if (modelType == typeof(PagingInfo))
			{
				return new PagingInfoModelBinder();
			}
			return null;
		}

    }
}