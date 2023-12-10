using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Converser
{
    /// <summary>
    /// Класс менеджера городов
    /// </summary>
    public class CityManager
    {
        /// <summary>
        /// Возвращает список городов
        /// </summary>
        /// <returns>Список городов</returns>
        public List<City> GetCities()
        {
            string json = File.ReadAllText("CitiesTimeZoneUTC.json");

            return JsonSerializer.Deserialize<List<City>>(json);
        }
    }
}
