using System.Xml.Serialization;
using ConverserLibrary;
using ConverserLibrary.Interfaces;
using ConverserLibrary.Models;
using Microsoft.Extensions.Logging;

/// <summary>
/// Сервис для создания XML-фидов Яндекс.
/// </summary>
public class YandexFeedCreatorService : IYandexFeedCreatorService
{
    private readonly ILogger<YandexFeedCreatorService> _logger;

    private readonly IInfoDataService _infoDataService;
    private readonly ITransliterationService _transliterationService;

    /// <summary>
    /// Инициализирует новый экземпляр класса YandexFeedCreatorService.
    /// </summary>
    /// <param name="logger">Интерфейс логгера</param>
    /// <exception cref="ArgumentNullException"></exception>
    public YandexFeedCreatorService(
        ILogger<YandexFeedCreatorService> logger,
        IInfoDataService infoDataService, 
        ITransliterationService transliterationService)
    {
        _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        _infoDataService = infoDataService;
        _transliterationService = transliterationService;
    }

    public event EventHandler<XmlCreatedEventArgs> XmlCreated;

    /// <summary>
    /// Создает XML-фиды для Яндекс на основе данных о товарах по городам.
    /// </summary>
    /// <param name="path">Путь к файлу Excel</param>
    /// <param name="citySeparatorResult">Результат разделения товаров по городам</param>
    public void CreateXml(string path, CitySeparatorResult citySeparatorResult)
    {
        var directoryPath = Path.Combine(path, "YandexFeeds");
        Directory.CreateDirectory(directoryPath);

        var jsonCities = _infoDataService.GetCities();

        foreach (var city in jsonCities)
        {
            if (!citySeparatorResult.CityProducts.ContainsKey(city.CityName))  // ???
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
            var filePath = Path.Combine(directoryPath, $"{_transliterationService.Transliterate(city.CityName)}.xml"); ///

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
                var transliteratedName = _transliterationService
                    .Transliterate(product.TechnicalName);

                var offer = new Offer
                {
                    ID = product.BitrixCode,
                    Model = product.CommercialName,
                    Url = $"https://mybox.ru/{_transliterationService.Transliterate(cityName)}/products/{transliteratedName}", 
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