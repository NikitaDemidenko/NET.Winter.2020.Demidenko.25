﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Bll.Contract.Entities.XmlSerializationModel
{
    public class Parameters
    {
        [XmlElement("parameter")]
        public List<Parameter> UrlParameters { get; set; }
    }
}
