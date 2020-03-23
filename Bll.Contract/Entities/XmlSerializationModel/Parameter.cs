using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Bll.Contract.Entities.XmlSerializationModel
{
    public class Parameter
    {
        [XmlAttribute("value")]
        public string Value { get; set; }

        [XmlAttribute("key")]
        public string Key { get; set; }
    }
}
