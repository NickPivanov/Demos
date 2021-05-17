using DemoConsoleReader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DemoConsoleReader.XmlFileReaders
{
    public static class XmlFileReaderExtensions
    {
        public static XElement CreateXElementForEngine(this XmlFileReader fileReader, object engine)
        {
            var e = engine as Engine;
            var result = new XElement("Engine",
                new XElement("Name", e.Name),
                new XElement("SerialNumber", e.SerialNumber),
                new XElement("Operator", new XAttribute("Name", e.Operator.Name))
                );
            var measurements = new XElement("Measurements");
            foreach (var value in e.Measurements)
            {
                measurements.Add(new XElement("int", value));
            }
            result.Add(measurements);
            return result;
        }
    }
}
