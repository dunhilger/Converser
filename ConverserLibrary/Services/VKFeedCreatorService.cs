using ConverserLibrary.Interfaces;
using System.Xml.Serialization;
using ConverserLibrary.Models;
using Microsoft.Extensions.Logging;
using System.Text;
using ConverserLibrary.Dto;

namespace ConverserLibrary.Services
{
    public class VKFeedCreatorService : IVKFeedCreatorService
    {
        private readonly ILogger<VKFeedCreatorService> _logger;

        private readonly IInfoDataService _infoDataService;

        /// <summary>
        /// Инициализирует новый экземпляр класса YandexFeedCreatorService.
        /// </summary>
        /// <param name="logger">Интерфейс логгера</param>
        /// <exception cref="ArgumentNullException"></exception>
        public VKFeedCreatorService(ILogger<VKFeedCreatorService> logger, 
                                    IInfoDataService infoDataService)
        {
            _logger = logger ??
                    throw new ArgumentNullException(nameof(logger));
            _infoDataService = infoDataService;
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

            var jsonCities = _infoDataService.GetCities();

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

                var overrides = new XmlAttributeOverrides();

                var pickup = new XmlAttributes { XmlIgnore = true };
                overrides.Add(typeof(Offer), "Pickup", pickup);

                var manufacturerWarranty = new XmlAttributes { XmlIgnore = true };
                overrides.Add(typeof(Offer), "ManufacturerWarranty", manufacturerWarranty);

                var countryOfOrigin = new XmlAttributes { XmlIgnore = true };
                overrides.Add(typeof(Offer), "CountryOfOrigin", countryOfOrigin);

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
                var utmLabels = _infoDataService.GetUtmLabels();

                foreach (var product in cityProducts)
                {
                    string description = GetDescription(utmLabels, product);

                    var offer = new Offer
                    {
                        ID = product.BitrixCode,
                        Price = product.Price,
                        CurrencyId = product.Currency,
                        CategoryId = product.CategoryId,
                        Model = product.CommercialName,
                        Description = description,
                        Picture = product.PictureLink,
                    };

                    offers.Add(offer);
                }

                return offers;
            }

            return new List<Offer>();
        }

        private string GetDescription(List<UtmLabel> utmLabels, Product product)
        {
            var descriptionBuilder = new StringBuilder();

            descriptionBuilder.AppendLine(product.Description);
            //descriptionBuilder.AppendLine("");
            //descriptionBuilder.AppendLine("<br/>");
            descriptionBuilder.AppendLine($"{product.Quantity} шт / {product.Weight} г");
            //descriptionBuilder.AppendLine("");
            //descriptionBuilder.AppendLine("<br/>");
            descriptionBuilder.AppendLine("Цена может отличаться в зависимости от твоего города.");
            //descriptionBuilder.AppendLine("");
            //descriptionBuilder.AppendLine("<br/>");
            descriptionBuilder.AppendLine("Точную цену можно уточнить на сайте.");

            var matchingUtmLabel = utmLabels.FirstOrDefault(label => label.CategoryId == product.CategoryId);

            if (matchingUtmLabel is not null)
            {
                //descriptionBuilder.AppendLine("");
                //descriptionBuilder.AppendLine("<br/>");
                descriptionBuilder.AppendLine(matchingUtmLabel.CategoryUTMLabel);
            }
            else
            {
                _logger.LogError("Ошибка: Для категории '{CategoryName}' UTM метка не найдена.", matchingUtmLabel.CategoryName);
            }

            return descriptionBuilder.ToString();
        }
    }
}
