using System.Xml.Serialization;
using Converser;
using Converser.Models;
using Microsoft.Extensions.Logging;

public class YandexFeedCreatorService : IYandexFeedCreatorService
{
    private readonly ILogger<YandexFeedCreatorService> _logger;

    public YandexFeedCreatorService(ILogger<YandexFeedCreatorService> logger)
    {
        _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
    }

    public void CreateXml(string path, CitySeparatorResult citySeparatorResult)
    {
        var directoryPath = Path.Combine(Path.GetDirectoryName(path), "YandexFeeds");
        Directory.CreateDirectory(directoryPath);

        var cityManager = new CityManager();
        var jsonCities = cityManager.GetCities();

        foreach (var city in jsonCities)
        {
            if (!citySeparatorResult.CityProducts.ContainsKey(city.CityName))
            {
                //Console.WriteLine($"Город {city.CityName} не найден в Excel.");
                _logger.LogError("Город '{0a}' не найден в Excel.", city.CityName);
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
                    Url = $"https://mybox.ru/products/{product.Url}",
                    Price = product.Price,
                    CurrencyId = product.Currency,
                    CategoryId = product.CategoryId,
                    Picture = product.Picture,
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