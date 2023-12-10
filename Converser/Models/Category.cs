using System.Xml.Serialization;

namespace Converser.Models
{
    public class Category
    {
        [XmlAttribute("id")]
        public string ID { get; set; }

        [XmlAttribute("parentId")]
        public string ParentID { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
