namespace ConverserLibrary
{
    /// <summary>
    /// Город для сериализации в Json.
    /// </summary>
    public class City
    {
        /// <summary>
        /// Принимает название города.
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Принимает смещение по временной зоне UTC.
        /// </summary>
        public string TimeZone { get; set; }

        /// <summary>
        /// Принимает транслитерационное название.
        /// </summary>
        public string TransliterationCityName { get; set; }

        /// <summary>
        /// Создает экземпляр City
        /// </summary>
        /// <param name="cityName">Название города</param>
        /// <param name="timeZone">Смещение по UTC</param>
        /// <param name="transliterationCityName">Транслитерационное название</param>
        public City(string cityName, string timeZone, string transliterationCityName)
        {
            CityName = cityName;
            TimeZone = timeZone;
            TransliterationCityName = transliterationCityName;
        }
    }
}
