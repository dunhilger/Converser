using ConverserLibrary.Interfaces;
using ConverserLibrary.Models.JSON_Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog.Core;

namespace ConverserLibrary.Services
{
    public class JsonApiDeserializationService : IJsonApiDeserializationService
    {
        public JsonApiDeserializationService(ILogger<JsonApiDeserializationService> logger)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(Logger));
        }

        private readonly ILogger<JsonApiDeserializationService> _logger;

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
                        string content = await response.Content.ReadAsStringAsync();

                        var root = JsonConvert.DeserializeObject<JsonRoot>(content);

                        if (root.JsonCityDataList is not null)
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
