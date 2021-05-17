using DemoConsoleReader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DemoConsoleReader.XmlFileReaders
{
    public class XmlFileReader : IFileReader
    {
        private XmlReaderSettings settings;
        public XmlFileReader()
        {
            settings = new XmlReaderSettings
            {
                IgnoreComments = true,
                IgnoreWhitespace = true
            };
        }

        public void CreateFile(string path, object[] data)
        {
            XElement root = new XElement("Engines");
            foreach (var item in data)
            {
                XElement element = this.CreateXElementForEngine(item);
                root.Add(element);
            }
            using (Stream stream = File.Create(path))
                root.Save(stream);
        }

        public void AddDataToFile(string path, object data)
        {
            XElement xmlDoc = XElement.Load(path);
            var root = xmlDoc.Elements().First().Parent;
            var engine = this.CreateXElementForEngine(data);
            root.Add(engine);
            using (var sw = File.Open(path, FileMode.Create))
                 xmlDoc.Save(sw);
        }

        public IEnumerable<object> ReadFile<T>(string path)
        {
            List<object> result = new();
            using (XmlReader xmlReader = XmlReader.Create(path, settings))
            {
                xmlReader.ReadStartElement("Engines");
                while (xmlReader.Name == "Engine")
                {
                    result.Add((XElement)XNode.ReadFrom(xmlReader));
                }
            }

            return result;
        }
    }
}
