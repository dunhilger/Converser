using ConverserLibrary.Dto;

namespace ConverserLibrary.Interfaces
{
    /// <summary>
    /// Интерфейс для сервиса парсера Json.
    /// </summary>
    public interface IJsonParserService
    {
        /// <summary>
        /// Получает список товаров из Json по указанному url.
        /// </summary>
        /// <param name="path">Данные в формате Json</param>
        /// <returns>Список товаров</returns>
        List<Product> GetJson(string Json);
    }
}
