using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SolidTask.XmlSerializationModel
{
    [XmlRoot("urlAddresses")]
    public class UrlsDocument
    {
        [XmlElement("urlAddress")]
        public List<Url> UrlAddresses { get; set; }
    }
}
