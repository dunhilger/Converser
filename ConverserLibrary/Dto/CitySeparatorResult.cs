using ConverserLibrary.Models;

namespace ConverserLibrary
{
    // <summary>
    /// Результат разделения товаров по городам.
    /// </summary>
    public class CitySeparatorResult
    {
        /// <summary>
        /// Получает словарь, где ключи - имена городов, значения - списки товаров для каждого города.
        /// </summary>
        public Dictionary<string, List<Product>> CityProducts { get; set; }

        /// <summary>
        /// Получает список категорий.
        /// </summary>
        public List<Category> Categories { get; set; }
    }
}
