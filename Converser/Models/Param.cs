using System.Xml.Serialization;

namespace Converser.Models
{
    /// <summary>
    /// Параметр товара для XML-файла.
    /// </summary>
    public class Param
    {
        /// <summary>
        /// Получает имя параметра.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Получает единицу измерения параметра.
        /// </summary>
        [XmlAttribute("unit")]
        public string Unit { get; set; }

        /// <summary>
        /// Получает значение параметра.
        /// </summary>
        [XmlText]
        public string Value { get; set; }
    }
}

