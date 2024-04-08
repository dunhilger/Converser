using System.Xml.Serialization;

namespace ConverserLibrary.Models
{
    /// <summary>
    /// XML-узел магазина shop.
    /// </summary>
    public class Shop
    {
        /// <summary>
        /// Инициализирует экземпляр класса Shop.
        /// </summary>
        public Shop()
        {
            Currencies = new List<Currency>()
            {
                new Currency()
            };
        }

        /// <summary>
        /// Получает имя узла магазина.
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; } /*= "MYBOX";*/

        /// <summary>
        /// Получает название компании.
        /// </summary>
        [XmlElement("company")]
        public string Company { get; set; } /*= "MYBOX";*/

        // <summary>
        /// Получает URL магазина.
        /// </summary>
        [XmlElement("url")]
        public string Url { get; set; } /*= "https://mybox.ru";*/

        /// <summary>
        /// Получает список валют магазина.
        /// </summary>
        [XmlArray("currencies")]
        [XmlArrayItem("currency")]
        public List<Currency> Currencies { get; set; }

        /// <summary>
        /// Получает список категорий магазина.
        /// </summary>
        [XmlArray("categories")]
        [XmlArrayItem("category")]
        public List<Category> Categories { get; set; }

        /// <summary>
        /// Получает список оферов магазина.
        /// </summary>
        [XmlArray("offers")]
        [XmlArrayItem("offer")]
        public List<Offer> Offers { get; set; }
    }
}
