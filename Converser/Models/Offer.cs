using System.Xml.Serialization;

namespace Converser.Models
{
    /// <summary>
    /// XML-узел оффера.
    /// </summary>
    public class Offer
    {
        /// <summary>
        /// Получает ID.
        /// </summary>
        [XmlAttribute("id")]
        public string ID { get; set; }

        /// <summary>
        /// Получает тип.
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get; set; } = "vendor.model";

        /// <summary>
        /// Получает наличие.
        /// </summary>
        [XmlAttribute("available")]
        public bool Available { get; set; } = true;

        /// <summary>
        /// Получает имя вендора.
        /// </summary>
        [XmlElement("vendor")]
        public string Vendor { get; set; } = "MYBOX";

        /// <summary>
        /// Получает название.
        /// </summary>
        [XmlElement("model")]
        public string Model { get; set; }

        /// <summary>
        /// Получает URL.
        /// </summary>
        [XmlElement("url")]
        public string Url { get; set; }

        /// <summary>
        /// Получает цену.
        /// </summary>
        [XmlElement("price")]
        public string Price { get; set; }

        /// <summary>
        /// Получает валюту.
        /// </summary>
        [XmlElement("currencyId")]
        public string CurrencyId { get; set; } = "RUR";

        /// <summary>
        /// Получает ID категории.
        /// </summary>
        [XmlElement("categoryId")]
        public string CategoryId { get; set; }

        /// <summary>
        /// Получает URL изображения.
        /// </summary>
        [XmlElement("picture")]
        public string Picture { get; set; }

        /// <summary>
        /// Получает признак возможности самовывоза.
        /// </summary>
        [XmlElement("pickup")]
        public bool Pickup { get; set; } = true;

        /// <summary>
        /// Получает список параметров.
        /// </summary>
        [XmlElement("param")]
        public List<Param> Params { get; set; } = new List<Param>();

        /// <summary>
        /// Получает описание.
        /// </summary>
        [XmlElement("description")]
        public string Description { get; set; }

        /// <summary>
        /// Получает признак гарантии производителя.
        /// </summary>
        [XmlElement("manufacturer_warranty")]
        public bool ManufacturerWarranty { get; set; } = true;

        /// <summary>
        /// Получает страну происхождения.
        /// </summary>
        [XmlElement("country_of_origin")]
        public string CountryOfOrigin { get; set; } = "Россия";
    }
}
