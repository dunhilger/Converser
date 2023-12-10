using System.Xml.Serialization;

namespace Converser.Models
{
    /// <summary>
    /// XML-узел валюты.
    /// </summary>
    public class Currency
    {
        /// <summary>
        /// Получает идентификатор.
        /// </summary>
        [XmlAttribute("id")]
        public string ID { get; set; } = "RUR";

        /// <summary>
        /// Получает курс к рублю.
        /// </summary>
        [XmlAttribute("rate")]
        public string Rate { get; set; } = "1";
    }
}
