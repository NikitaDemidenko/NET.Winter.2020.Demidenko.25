using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Bll.Contract.Entities.XmlSerializationModel
{
    public class Host
    {
        [XmlAttribute("name")]
        public string HostName { get; set; }
    }
}
