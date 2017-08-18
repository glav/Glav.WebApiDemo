using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http.Formatting;
using System.IO;

namespace Glav.WebApiDemo.Web.Framework
{
	public class DodgyMediaFormatter : MediaTypeFormatter
	{
		public const string MediaType = "application/x-dodgy";
		public DodgyMediaFormatter()
		{
			SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue(MediaType));
		}
		public override bool CanReadType(Type type)
		{
			return true;
		}

		public override bool CanWriteType(Type type)
		{
			return false;
		}

        public override System.Threading.Tasks.Task<object> ReadFromStreamAsync(Type type, Stream readStream, System.Net.Http.HttpContent content, IFormatterLogger formatterLogger)
        {
			return System.Threading.Tasks.Task.Factory.StartNew<object>(() =>
			{
				if (type != typeof(DodgyModel))
				{
					throw new FormatException(string.Format("Type: {0} not support for MediaType:{0}", type.ToString(), MediaType));
				}

				var model = new DodgyModel { Atributes = new List<string>()};
				
				string textContent = null;
                using (var rdr = new StreamReader(readStream))
				{
                    textContent = rdr.ReadToEnd();
				}

                var items = textContent.Split(new char[] { ',' });
				foreach (var item in items)
				{
					var keyvalue = item.Split(new char[] { '|' });
					if (keyvalue[0] == "N")
					{
						model.Name = keyvalue[1];
					}
					if (keyvalue[0].StartsWith("A"))
					{
						model.Atributes.Add(keyvalue[1]);
					}
				}

				return model;
			});
		}

	}
}