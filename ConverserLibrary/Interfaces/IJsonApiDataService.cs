using ConverserLibrary.Models.JSON_Models;

namespace ConverserLibrary.Interfaces
{
    /// <summary>
    /// Интерфейс для сервиса чтения Json.
    /// </summary>
    public interface IJsonApiDataService
    {
        /// <summary>
        /// Получает список городов из Json по указанному url.
        /// </summary>
        /// <param name="path">Данные в формате Json</param>
        /// <returns>Список товаров</returns>
        public Task<JsonRoot> GetCities();
    }
}
