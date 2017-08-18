using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Text;

namespace Glav.WebApiDemo.Web.Controllers
{
    public class ExplorerController : ApiController
    {
		public string GetApiHelp()
		{
			var info = new StringBuilder();

			var explorer = GlobalConfiguration.Configuration.Services.GetApiExplorer();
			var descriptions = explorer.ApiDescriptions;
			foreach (var apiDesc in descriptions)
			{
				info.AppendFormat("<span class='path'><em>Path:</em> <strong>{0}</strong></span><br />", apiDesc.RelativePath);
				info.AppendFormat("<span class='method'><em>Method:</em> {0}</span><br />", apiDesc.HttpMethod);
				info.AppendFormat("<span class='doco'><em>Desc:</em> {0}</span><br />", apiDesc.Documentation);
				info.Append("<span class='parms'><em>Parameters:</em></span><br />&nbsp;&nbsp;");

				bool needSeparator = false;
				foreach (var parm in apiDesc.ParameterDescriptions)
				{
					if (needSeparator)
					{
						info.Append(", ");
					}
					info.AppendFormat("<span class='parm-item'>(<em>{0}</em> {1})</span>", parm.ParameterDescriptor.ParameterType, parm.Name);
					needSeparator = true;
				}
				info.Append("<br /><br />");
			}

			return info.ToString();
		}
    }
}
