using ConverserLibrary.Dto;
using ConverserLibrary.Interfaces;
using ConverserLibrary.Models;
using Microsoft.Extensions.Logging;

namespace ConverserLibrary
{
    /// <summary>
    /// Сервис для разделения списка товаров по городам и категориям.
    /// </summary>
    public class CitySeparatorService : ICitySeparatorService
    {
        private readonly ILogger<CitySeparatorService> _logger;
        private readonly IInfoDataService _infoDataService;

        /// <summary>
        /// Инициализирует новый экземпляр класса CitySeparatorService.
        /// </summary>
        /// <param name="logger">Интерфейс логгера</param>
        /// <exception cref="ArgumentNullException"></exception>
        public CitySeparatorService(ILogger<CitySeparatorService> logger,
            IInfoDataService infoDataService)
        {
            _logger = logger ?? 
                throw new ArgumentNullException(nameof(logger));
            _infoDataService = infoDataService;
        }

        /// <summary>
        // Принимает список товаров products и разбивает его по городам, возвращает словарь (объект типа CitySeparatorResult),
        // в котором ключами являются имена городов, а значениями - списки товаров для каждого города и списки категорий
        /// </summary>
        /// <param name="products">Список товаров</param>
        /// <returns>Объект типа CitySeparatorResult, содержащий словарь с товарами и словарь с ID категорий товаров</returns>
        public CitySeparatorResult SeparateByCity(List<Product> products)
        {
            var cityDictionary = new Dictionary<string, List<Product>>();
            var categoryDictionary = new Dictionary<string, Category>();

            List<City> allExistCities = _infoDataService.GetCities();

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

                        cityDictionary[cityName].Add(product);

                        AddIfNotExists(categoryDictionary,
                            product.CategoryId, product.CategoryName, product.ParentCategoryId);

                        AddIfNotExists(categoryDictionary,
                            product.ParentCategoryId, product.ParentCategoryName, null);
                    }
                    else
                    {
                        _logger.LogError("Ошибка: Название города '{cityName}' не существует.", cityName);
                    }
                }
            }

            return new CitySeparatorResult() 
            { 
                CityProducts = cityDictionary,
                Categories = categoryDictionary.Values
                    .OrderBy(i => i.ParentID ?? i.ID)
                    .ThenBy(i => i.ID)
                    .ToList(), 
            };
        }

        /// <summary>
        /// Добавляет категорию в словарь, если ее там еще нет.
        /// </summary>
        /// <param name="dict">Словарь категорий</param>
        /// <param name="categoryId">ID категории</param>
        /// <param name="categoryName">Имя категории</param>
        /// <param name="parentCategoryId">ID родительской категории</param>
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

        /// <summary>
        /// Проверяет, является ли указанное название города допустимым, сравнивая его со списком имен из Json
        /// </summary>
        /// <param name="cityName">Имя города</param>
        /// <param name="validCities">писок допустимых городов из Json</param>
        /// <returns>
        /// <c>true</c>, если имя города допустимо
        /// <c>false</c>, если имя города недопустимо
        /// </returns>
        private static bool IsValidCity(string cityName, List<City> validCities)
        {
            return validCities.Any(city => city.CityName.Equals(cityName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
