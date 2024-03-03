using System.Xml.Serialization;
using ConverserLibrary.Interfaces;
using ConverserLibrary.Models;
using Microsoft.Extensions.Logging;

namespace ConverserLibrary.Services
{
    /// <summary>
    /// Сервис для создания XML-фидов 2ГИС.
    /// </summary>
    public class TwoGisFeedCreatorService : ITwoGisFeedCreatorService
    {
        private readonly ILogger<TwoGisFeedCreatorService> _logger;

        private readonly ITransliterationService _transliterationService;

        /// <summary>
        /// Инициализирует новый экземпляр класса TwoGisFeedCreatorService.
        /// </summary>
        /// <param name="logger">Интерфейс логгера</param>
        /// <exception cref="ArgumentNullException"></exception>
        public TwoGisFeedCreatorService(ILogger<TwoGisFeedCreatorService> logger,
               ITransliterationService transliterationService)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _transliterationService = transliterationService;
        }

        /// <summary>
        /// Создает XML-фиды для 2ГИС на основе данных о товарах по городам.
        /// </summary>
        /// <param name="path">Путь к файлу Excel</param>
        /// <param name="citySeparatorResult">Результат разделения товаров по городам</param>
        public void CreateXml(string path, CitySeparatorResult citySeparatorResult)
        {
            var directoryPath = Path.Combine(path, "TwoGisFeeds");
            Directory.CreateDirectory(directoryPath);

            var jsonDataReader = new InfoDataService();
            var jsonCities = jsonDataReader.GetCities();

            foreach (var city in jsonCities)
            {
                if (!citySeparatorResult.CityProducts.ContainsKey(city.CityName))
                {
                    _logger.LogError("Город '{cityName}' не найден в Excel.", city.CityName);
                    continue;
                }

                var catalog = new Catalog
                {
                    Date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") + city.TimeZone,
                    Shop = CreateShop(city.CityName, citySeparatorResult)
                };

                var nameSpace = new XmlSerializerNamespaces();
                nameSpace.Add("", "");

                var serializer = new XmlSerializer(typeof(Catalog));
                var filePath = Path.Combine(directoryPath, $"{city.TransliterationCityName}.xml");

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    serializer.Serialize(fileStream, catalog, nameSpace);
                }
            }
        }

        /// <summary>
        /// Создает XML-узел shop
        /// </summary>
        /// <param name="cityName">Имя города</param>
        /// <param name="citySeparatorResult">Результат разделения товаров по городам</param>
        /// <returns></returns>
        private Shop CreateShop(string cityName, CitySeparatorResult citySeparatorResult)
        {
            var shop = new Shop
            {
                Name = "MYBOX",
                Company = "MYBOX",
                Url = "https://mybox.ru/",
                Currencies = new List<Currency>
            {
                new Currency { ID = "RUR", Rate = "1" }
            },
                Categories = citySeparatorResult.Categories,
                Offers = CreateOffers(cityName, citySeparatorResult)
            };
            return shop;
        }

        /// <summary>
        /// Создает XML-узел offers с оферами в пределах города
        /// </summary>
        /// <param name="cityName">Имя города</param>
        /// <param name="citySeparatorResult">Результат разделения товаров по городам</param>
        /// <returns>Список оферов offers</returns>
        private List<Offer> CreateOffers(string cityName, CitySeparatorResult citySeparatorResult)
        {
            if (citySeparatorResult.CityProducts.TryGetValue(cityName, out var cityProducts))
            {
                var offers = new List<Offer>();                

                foreach (var product in cityProducts)
                {
                    var transliteratedName = _transliterationService.Transliterate(product.TechnicalName);

                    var offer = new Offer
                    {
                        ID = product.BitrixCode,
                        Model = product.CommercialName,
                        Url = $"https://mybox.ru/{_transliterationService.Transliterate(cityName)}/products/{transliteratedName}/?utm_source=2gis&utm_medium=app&utm_campaign=2gis_feed",
                        Price = product.Price,
                        CurrencyId = product.Currency,
                        CategoryId = product.CategoryId,
                        Picture = product.PictureLink,
                        Params = new List<Param>()
                    {
                        new Param() { Name = "Вес", Unit ="Грамм", Value = product.Weight },
                        new Param() { Name = "Количество", Unit = "Штук", Value = product.Quantity }
                    },
                        Description = product.Description,
                    };

                    offers.Add(offer);
                }

                return offers;
            }

            return new List<Offer>();
        }
    }
}
