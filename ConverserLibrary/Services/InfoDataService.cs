using ConverserLibrary.Dto;
using ConverserLibrary.Interfaces;
using System.Text.Json;

namespace ConverserLibrary.Services
{
    /// <summary>
    /// Менеджер городов
    /// </summary>
    public class InfoDataService : IInfoDataService
    {
        /// <summary>
        /// Возвращает список городов из Json
        /// </summary>
        /// <returns>Список городов</returns>
        public List<City> GetCities()
        {
            string json = File.ReadAllText("CitiesTimeZoneUTC.json");

            return JsonSerializer.Deserialize<List<City>>(json);
        }

        /// <summary>
        /// Возвращает список utm меток
        /// </summary>
        /// <returns>Список utm меток</returns>
        public List<UtmLabel> GetUtmLabels()
        {
            string json = File.ReadAllText("UTMLabels.json");

            return JsonSerializer.Deserialize<List<UtmLabel>>(json);
        }
    }
}
