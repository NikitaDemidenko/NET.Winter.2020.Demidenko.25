using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Dal.Contract.Interafces;
using Bll.Contract.Entities.XmlSerializationModel;

namespace Dal.Implementation
{
    public class XmlSerializerSaver : ISaver<UrlsDocument>
    {
        private readonly string filePath;

        public XmlSerializerSaver(string filePath)
        {
            if (filePath is null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            this.filePath = filePath;
        }

        public void Save(UrlsDocument source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using var writer = new StreamWriter(this.filePath);
            var serializer = new XmlSerializer(typeof(UrlsDocument));
            serializer.Serialize(writer, source);
        }
    }
}
