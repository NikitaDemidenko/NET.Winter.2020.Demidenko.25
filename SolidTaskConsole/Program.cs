using System;
using SolidTask;
using SolidTask.Interfaces;
using SolidTask.XmlSerializationModel;

namespace SolidTaskConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var loader = new LoadFromFile("urls.txt");
            var urlsStrings = loader.Load();

            var parser = new UrlParser();

            var converter = new StringsToUrlsDocumentConverter(parser);
            var urlsDocument = converter.Convert(urlsStrings);

            var saver = new XmlSerializerSaver("result.xml");
            saver.Save(urlsDocument);
        }
    }
}
