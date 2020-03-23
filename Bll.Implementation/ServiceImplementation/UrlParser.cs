using System;
using System.Linq;
using System.Web;
using Bll.Contract.Interfaces;
using Bll.Contract.Entities;

namespace Bll.Implementation.ServiceImplementation
{
    public class UrlParser : IParser<Url>
    {
        public Url Parse(string source)
        {
            var uri = new Uri(source);
            var parameters = HttpUtility.ParseQueryString(uri.Query);

            return new Url
            {
                Host = uri.Host,
                Segments = uri.Segments.Select(s => s.Trim('/')).Where(s => !string.IsNullOrEmpty(s)).ToList(),
                Parameters = parameters.AllKeys.Select(key => new UrlParameter { Key = key, Value = parameters[key] }).ToList(),
            };
        }
    }
}
