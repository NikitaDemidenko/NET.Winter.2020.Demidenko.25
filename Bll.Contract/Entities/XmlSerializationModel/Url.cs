using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Bll.Contract.Entities.XmlSerializationModel
{
    public class Url
    {
        [XmlElement("host")]
        public Host HostName { get; set; }

        [XmlElement("uri")]
        public Segments Uri { get; set; }

        [XmlElement("parameters")]
        public Parameters Parameters { get; set; }
    }
}
