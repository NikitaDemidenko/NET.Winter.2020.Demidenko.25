﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Bll.Contract.Entities
{
    public class Url
    {
        public string Host { get; set; }

        public List<string> Segments { get; set; }

        public List<UrlParameter> Parameters { get; set; }
    }
}
