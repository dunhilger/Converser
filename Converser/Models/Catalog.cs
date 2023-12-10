using System.Xml.Serialization;

namespace Converser.Models
{
    /// <summary>
    /// XML-узел каталога.
    /// </summary>
    [XmlRoot("yml_catalog")]
    public class Catalog
    {
        /// <summary>
        /// Получает дату создания.
        /// </summary>
        [XmlAttribute("date")]
        public string Date { get; set; }

        /// <summary>
        /// Получает магазин.
        /// </summary>
        [XmlElement("shop")]
        public Shop Shop { get; set; }
    }
}
