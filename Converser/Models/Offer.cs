using System.Collections.Generic;
using System.Xml.Serialization;

namespace Converser.Models
{
    public class Offer
    {
        [XmlAttribute("id")]
        public string ID { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; } = "vendor.model";

        [XmlAttribute("available")]
        public bool Available { get; set; } = true;

        [XmlElement("vendor")]
        public string Vendor { get; set; } = "MYBOX";

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("currencyId")]
        public string CurrencyId { get; set; } = "RUR";

        [XmlElement("categoryId")]
        public string CategoryId { get; set; }

        [XmlElement("picture")]
        public string Picture { get; set; }

        [XmlElement("pickup")]
        public bool Pickup { get; set; } = true;

        [XmlElement("param")]
        public List<Param> Params { get; set; } = new List<Param>();

        [XmlElement("description")]
        public string Description { get; set; } 

        [XmlElement("manufacturer_warranty")]
        public bool ManufacturerWarranty { get; set; } = true;

        [XmlElement("country_of_origin")]
        public string CountryOfOrigin { get; set; } = "Россия";
    }
}