using ConverserLibrary.Interfaces;
using ConverserLibrary.Models.JSON_Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog.Core;

namespace ConverserLibrary.Services
{
    public class CitiesResult 
    {
        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public JsonRoot JsonRoot { get; set; }
    }

    public class JsonApiDataService : IJsonApiDataService
    {
        public JsonApiDataService(ILogger<JsonApiDataService> logger)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(Logger));
        }

        private readonly ILogger<JsonApiDataService> _logger;

        public async Task<CitiesResult> GetCities(string url)
        {
            var result = new CitiesResult();

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();

                        var root = JsonConvert.DeserializeObject<JsonRoot>(content);

                        if (root is not null)
                        {
                            _logger.LogInformation("Данные о городах успешно загружены");
                            result.JsonRoot = root;
                            result.Success = true;
                        }
                        else
                        {
                            var message = "Ответ успешно получен, но данные отсутствуют.";
                            _logger.LogWarning(message);
                            result.ErrorMessage = message;
                        }
                    }
                    else
                    {
                        var message = $"Ошибка при получении данных: {response.StatusCode}";
                        _logger.LogError(message);
                        result.ErrorMessage = message;
                    }
                }
            }
            catch (JsonReaderException ex)
            {
                var message = "Ошибка при десериализации JSON.";
                _logger.LogError(ex, message);
                result.ErrorMessage = message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при получении данных");
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
