using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Dal.Contract.Interafces;

namespace Dal.Implementation
{
    public class XmlFileSaver : ISaver<XDocument>
    {
        private readonly string filePath;

        public XmlFileSaver(string filePath)
        {
            this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        public void Save(XDocument source)
        {
            using var writer = new FileStream(this.filePath, FileMode.OpenOrCreate);
            source.Save(writer);
        }
    }
}
