using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using Bll.Contract.Interfaces;

namespace Bll.Implementation.ServiceImplementation
{
    public class UrlToXDocumentConverter : IConverter<IEnumerable<string>, XDocument>
    {
        private readonly ILogger logger;
        private readonly IParser<Contract.Entities.Url> parser;
        private readonly IValidator<string> validator;

        public UrlToXDocumentConverter(ILogger logger, IParser<Contract.Entities.Url> parser, IValidator<string> validator)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.parser = parser ?? throw new ArgumentNullException(nameof(parser));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public XDocument Convert(IEnumerable<string> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var elements = new List<XElement>();
            foreach (var urlAddressString in source)
            {
                if (this.validator.IsValid(urlAddressString))
                {
                    var url = this.parser.Parse(urlAddressString);
                    var host = new XElement("host", new XAttribute("name", url.Host));

                    XElement uri = null;
                    if (url.Segments.Count > 0)
                    {
                        uri = new XElement("uri");
                        url.Segments.ForEach(s => uri.Add(new XElement("segment", s)));
                    }

                    XElement parameters = null;
                    if (url.Parameters.Count > 0)
                    {
                        parameters = new XElement("parameters");
                        url.Parameters.ForEach(
                            p => parameters.Add(new XElement("parameter", new XAttribute("key", p.Key ?? string.Empty), new XAttribute("value", p.Value ?? string.Empty)))
                            );
                    }

                    elements.Add(new XElement("urlAddress", host, uri, parameters));
                }
                else
                {
                    this.logger.Log(LogLevel.Information, $"Invalid URL: {urlAddressString}");
                }
            }

            var document = new XDocument();
            document.Add(new XElement("urlAddresses", elements));

            return document;
        }
    }
}
