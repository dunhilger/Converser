namespace Converser
{
    /// <summary>
    /// Класс города для сериализации в Json
    /// </summary>
    public class City
    {
        public string CityName { get; set; }

        public string TimeZone { get; set; }

        public string TransliterationCityName { get; set; }

        public City(string cityName, string timeZone, string transliterationCityName)
        {
            CityName = cityName;
            TimeZone = timeZone;
            TransliterationCityName = transliterationCityName;
        }
    }
}
