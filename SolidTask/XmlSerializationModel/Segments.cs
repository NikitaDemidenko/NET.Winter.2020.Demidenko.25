using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SolidTask.XmlSerializationModel
{
    public class Segments
    {
        [XmlElement("segment")]
        public List<string> SegmentsNames { get; set; }
    }
}
