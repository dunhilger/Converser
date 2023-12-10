using System.Xml.Serialization;

namespace Converser.Models
{
    /// <summary>
    /// XML-узел категории.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Получает идентификатор.
        /// </summary>
        [XmlAttribute("id")]
        public string ID { get; set; }

        /// <summary>
        /// Получает идентификатор родительской категории.
        /// </summary>
        [XmlAttribute("parentId")]
        public string ParentID { get; set; }

        /// <summary>
        /// Получает значение.
        /// </summary>
        [XmlText]
        public string Value { get; set; }
    }
}
