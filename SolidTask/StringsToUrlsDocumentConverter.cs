using System;
using System.Collections.Generic;
using System.Text;
using SolidTask.Interfaces;
using SolidTask.XmlSerializationModel;

namespace SolidTask
{
    public class StringsToUrlsDocumentConverter : IConverter<IEnumerable<string>, UrlsDocument>
    {
        private readonly IParser<string, Url> parser;

        public StringsToUrlsDocumentConverter(IParser<string, Url> parser)
        {
            this.parser = parser ?? throw new ArgumentNullException(nameof(parser));
        }

        public UrlsDocument Convert(IEnumerable<string> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var urls = new UrlsDocument
            {
                UrlAddresses = new List<XmlSerializationModel.Url>(),
            };

            foreach (var urlAddressString in source)
            {
                var url = this.parser.Parse(urlAddressString);
                var urlModel = new XmlSerializationModel.Url
                {
                    HostName = new Host { HostName = url.Host },
                    Parameters = new Parameters { UrlParameters = new List<Parameter>() },
                    Uri = new Segments { SegmentsNames = new List<string>() },
                };

                url.Parameters.ForEach(p => urlModel.Parameters.UrlParameters.Add(new Parameter { Key = p.Key, Value = p.Value }));
                url.Segments.ForEach(s => urlModel.Uri.SegmentsNames.Add(s));

                urls.UrlAddresses.Add(urlModel);
            }

            return urls;
        }
    }
}
