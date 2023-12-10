namespace Converser
{
    /// <summary>
    /// Интерфейс для сервиса разделения списка товаров по городам.
    /// </summary>
    public interface ICitySeparatorService
    {
        /// <summary>
        /// Разделяет список товаров по городам и возвращает результат в виде объекта CitySeparatorResult.
        /// </summary>
        /// <param name="products">Список товаров для разделения</param>
        /// <returns>Объект CitySeparatorResult, содержащий разделенные списки товаров и категорий по городам</returns>
        CitySeparatorResult SeparateByCity(List<Product> products);
    }
}
