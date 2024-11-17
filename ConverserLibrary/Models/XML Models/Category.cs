using System.Xml.Serialization;

namespace ConverserLibrary.Models
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

        /// <summary>
        /// Переопределяет ToString для вывода данных о категории в заданном формате
        /// </summary>
        /// <returns>Строку с названием и ID категории</returns>
        public override string ToString()
        {
            return $"{Value} [{ID}]";
        }
    }
}
