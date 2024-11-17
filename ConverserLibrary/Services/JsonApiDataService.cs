using ConverserLibrary.Interfaces;
using ConverserLibrary.Models.JSON_Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;

namespace ConverserLibrary.Services
{
    public class JsonApiDataService : IJsonApiDataService
    {
        public JsonApiDataService(ILogger<JsonApiDataService> logger)
        {
            _logger = logger ??
            throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Поле для ведения логов в контексте сервиса парсинга Excel.
        /// </summary>
        private readonly ILogger<JsonApiDataService> _logger;

        public async Task<JsonRoot> GetCities()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync("https://mybile.mybox.ru/api/v1/cities");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();

                        var root = JsonConvert.DeserializeObject<JsonRoot>(content);

                        if (root is not null)
                        {
                            _logger.LogInformation("Данные о городах успешно загружены.");
                            return root;
                        }
                        else
                        {
                            _logger.LogWarning("Ответ успешно получен, но данные отсутствуют.");
                            return null;
                        }
                    }
                    else
                    {
                        _logger.LogError($"Ошибка при получении данных: {response.StatusCode}");
                        return null;
                    }
                }
                catch (JsonReaderException ex)
                {
                    _logger.LogError(ex, "Ошибка при десериализации JSON.");
                    throw;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Произошла ошибка при получении данных.");
                    throw;
                }
            }

                
        }

    }
}
