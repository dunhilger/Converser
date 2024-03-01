using ConverserLibrary.Dto;
namespace ConverserLibrary.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInfoDataService
    {
        /// <summary>
        /// Возвращает список городов из Json
        /// </summary>
        /// <returns>Список городов</returns>
        List<City> GetCities();

        /// <summary>
        /// Возвращает список utm меток
        /// </summary>
        /// <returns>Список utm меток</returns>
        List<UtmLabel> GetUtmLabels();
    }
}
