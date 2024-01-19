using System.Text.Json;

namespace ConverserLibrary
{
    /// <summary>
    /// Менеджер городов
    /// </summary>
    public class CityManager
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
    }
}
