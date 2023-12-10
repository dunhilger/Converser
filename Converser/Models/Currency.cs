using System.Xml.Serialization;

namespace Converser.Models
{
    public class Currency
    {
        [XmlAttribute("id")]
        public string ID { get; set; } = "RUR";

        [XmlAttribute("rate")]
        public string Rate { get; set; } = "1";
    }
}
