using System.Collections.Generic;
using System.Xml.Serialization;

namespace Converser.Models
{
    public class Shop
    {
        public Shop()
        {
            Currencies = new List<Currency>()
            {
                new Currency()
            };
        }

        [XmlElement("name")]
        public string Name { get; set; } = "MYBOX";

        [XmlElement("company")]
        public string Company { get; set; } = "MYBOX";

        [XmlElement("url")]
        public string Url { get; set; } = "https://mybox.ru";

        [XmlArray("currencies")]
        [XmlArrayItem("currency")]
        public List<Currency> Currencies { get; set; }

        [XmlArray("categories")]
        [XmlArrayItem("category")]
        public List<Category> Categories { get; set; }

        [XmlArray("offers")]
        [XmlArrayItem("offer")]
        public List<Offer> Offers { get; set; }
    }
}
