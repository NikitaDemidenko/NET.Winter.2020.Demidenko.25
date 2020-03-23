using System;
using System.Collections.Generic;
using System.Text;
using Bll.Contract.Interfaces;
using Bll.Contract.Entities;
using Bll.Contract.Entities.XmlSerializationModel;
using Microsoft.Extensions.Logging;


namespace Bll.Implementation.ServiceImplementation
{
    public class StringsToUrlsDocumentConverter : IConverter<IEnumerable<string>, UrlsDocument>
    {
        private readonly ILogger logger;
        private readonly IParser<Contract.Entities.Url> parser;
        private readonly IValidator<string> validator;

        public StringsToUrlsDocumentConverter(ILogger logger, IParser<Contract.Entities.Url> parser, IValidator<string> validator)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.parser = parser ?? throw new ArgumentNullException(nameof(parser));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public UrlsDocument Convert(IEnumerable<string> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var urls = new UrlsDocument
            {
                UrlAddresses = new List<Contract.Entities.XmlSerializationModel.Url>(),
            };

            foreach (var urlAddressString in source)
            {
                if (this.validator.IsValid(urlAddressString))
                {
                    var url = this.parser.Parse(urlAddressString);
                    var urlModel = new Contract.Entities.XmlSerializationModel.Url
                    {
                        HostName = new Host { HostName = url.Host },
                        Parameters = new Parameters { UrlParameters = new List<Parameter>() },
                        Uri = new Segments { SegmentsNames = new List<string>() },
                    };

                    url.Parameters.ForEach(p => urlModel.Parameters.UrlParameters.Add(new Parameter { Key = p.Key, Value = p.Value }));
                    url.Segments.ForEach(s => urlModel.Uri.SegmentsNames.Add(s));

                    urls.UrlAddresses.Add(urlModel);
                }
                else
                {
                    this.logger.Log(LogLevel.Information, $"Invalid URL: {urlAddressString}");
                }
            }

            return urls;
        }
    }
}
