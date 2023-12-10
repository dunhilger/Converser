using System.Xml.Serialization;

namespace Converser.Models
{
    public class Param
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("unit")]
        public string Unit { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}

