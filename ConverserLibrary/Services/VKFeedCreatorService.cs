using ConverserLibrary.Interfaces;
using System.Xml.Serialization;
using ConverserLibrary.Models;
using Microsoft.Extensions.Logging;

namespace ConverserLibrary.Services
{
    public class VKFeedCreatorService : IVKFeedCreatorService
    {
        private readonly ILogger<VKFeedCreatorService> _logger;

        /// <summary>
        /// Инициализирует новый экземпляр класса YandexFeedCreatorService.
        /// </summary>
        /// <param name="logger">Интерфейс логгера</param>
        /// <exception cref="ArgumentNullException"></exception>
        public VKFeedCreatorService(ILogger<VKFeedCreatorService> logger/*, IInformationService informationService*/)
        {
            _logger = logger ??
                    throw new ArgumentNullException(nameof(logger));
        }

        public event EventHandler<XmlCreatedEventArgs> XmlCreated;

        /// <summary>
        /// Создает XML-фиды для VK на основе данных о товарах по городам.
        /// </summary>
        /// <param name="path">Путь к файлу Excel</param>
        /// <param name="citySeparatorResult">Результат разделения товаров по городам</param>
        public void CreateXml(string path, CitySeparatorResult citySeparatorResult)
        {
            var directoryPath = Path.Combine(path, "VKFeeds");
            Directory.CreateDirectory(directoryPath);

            var cityManager = new CityManager();
            var jsonCities = cityManager.GetCities();

            foreach (var city in jsonCities)
            {
                if (!citySeparatorResult.CityProducts.ContainsKey(city.CityName))
                {
                    _logger.LogError("Город '{cityName}' не найден в Excel.", city.CityName);
                    continue;
                }

                var catalog = new Catalog
                {
                    Date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
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

                XmlCreated?.Invoke(this, new XmlCreatedEventArgs(filePath));
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
                Url = "https://mybox.ru",
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
                    var offer = new Offer
                    {
                        ID = product.BitrixCode,
                        Model = product.Model,
                        Price = product.Price,
                        CurrencyId = product.Currency,
                        CategoryId = product.CategoryId,                    
                        Description = $@"{product.Description}<br/><br/>" +
                        $"{product.Quantity} шт / {product.Weight} г<br/><br/>" +
                        $"Цена может отличаться в зависимости от твоего города.<br/>" +
                        $"Точную цену можно уточнить на сайте.<br/>",
                        Picture = product.Picture,
                    };

                    offers.Add(offer);
                }

                return offers;
            }

            return new List<Offer>();
        }
    }
}
