using System.Xml;
using System.Xml.Serialization;

namespace Converser.Models
{
    [XmlRoot("yml_catalog")]
    public class Catalog
    {
        [XmlAttribute("date")]
        public string Date { get; set; }

        [XmlElement("shop")]
        public Shop Shop { get; set; }
    }
}
