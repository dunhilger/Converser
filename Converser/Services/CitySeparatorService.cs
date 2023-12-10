using Converser.Models;
using Microsoft.Extensions.Logging;

namespace Converser
{
    public class CitySeparatorService : ICitySeparatorService
    {
        private readonly ILogger<CitySeparatorService> _logger;

        public CitySeparatorService(ILogger<CitySeparatorService> logger)
        {
            _logger = logger ?? 
                throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        // Принимает список товаров products и разбивает его по городам, возвращает словарь (объект типа CitySeparatorResult),
        // в котором ключами являются имена городов, а значениями - списки товаров для каждого города и списки категорий
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public CitySeparatorResult SeparateByCity(List<Product> products)
        {
            var cityDictionary = new Dictionary<string, List<Product>>();
            var categoryDictionary = new Dictionary<string, Category>();

            var cityManager = new CityManager();
            List<City> allExistCities = cityManager.GetCities();

            var excludedCategoryIds = new List<string> { "90", "35", "38" }; // набор костылей

            foreach (var product in products)
            {
                if (!string.IsNullOrEmpty(product.CityName)) 
                {
                    string cityName = product.CityName;

                    if (IsValidCity(cityName, allExistCities))
                    {
                        if (!cityDictionary.ContainsKey(cityName))
                        {
                            cityDictionary[cityName] = new List<Product>();
                        }

                        if (!excludedCategoryIds.Contains(product.CategoryId)) // условия костыля
                        {
                            cityDictionary[cityName].Add(product);
                        }

                        AddIfNotExists(categoryDictionary,
                            product.CategoryId, product.CategoryName, product.ParentCategoryId);

                        AddIfNotExists(categoryDictionary,
                            product.ParentCategoryId, product.ParentCategoryName, null);
                    }
                    else
                    {
                        //Console.WriteLine($"Ошибка: Название города '{cityName}' не существует.");
                        _logger.LogError("Ошибка: Название города '{0a}' не существует.", cityName);
                    }
                }
            }

            return new CitySeparatorResult() 
            { 
                CityProducts = cityDictionary,
                Categories = categoryDictionary.Values
                    .Where(category => !excludedCategoryIds.Contains(category.ID)) // костыль в действии
                    .OrderBy(i => i.ParentID ?? i.ID)
                    .ThenBy(i => i.ID)
                    .ToList(), // пользовательский выбор категорий для фидов
            };
        }

        private static void AddIfNotExists(Dictionary<string, Category> dict, 
            string categoryId, 
            string categoryName,
            string parentCategoryId)
        {
            if (categoryId != null && !dict.ContainsKey(categoryId))
            {
                dict[categoryId] = new Category()
                {
                    ID = categoryId,
                    ParentID = parentCategoryId,
                    Value = categoryName,
                };
            }
        }

        private static bool IsValidCity(string cityName, List<City> validCities)
        {
            return validCities.Any(city => city.CityName.Equals(cityName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
