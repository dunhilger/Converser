using Microsoft.Extensions.Logging;

namespace Converser.Services
{
    public class MainService : IMainService
    {
        private readonly ILogger<MainService> _logger;
        private readonly IExcelParserService _parser;
        private readonly ICitySeparatorService _separator;
        private readonly IYandexFeedCreatorService _yandexFeedCreatorService;
        private readonly ITwoGisFeedCreatorService _twoGisFeedCreatorService;

        public MainService(ILogger<MainService> logger,
            IExcelParserService parser,
            ICitySeparatorService separator,
            IYandexFeedCreatorService yandexFeedCreatorService,
            ITwoGisFeedCreatorService twoGisFeedCreatorService)
        {
            _logger = logger;
            _parser = parser;
            _separator = separator;
            _yandexFeedCreatorService = yandexFeedCreatorService;
            _twoGisFeedCreatorService = twoGisFeedCreatorService;
        }

        public void Run(string path)
        {
            _logger.LogInformation("Старт.");

            // парсинг эксель, получение списка всех товаров  
            var products = _parser.GetXLSXFile(path);

            // разбивка списка по городам
            var cityDictionary = _separator.SeparateByCity(products);

            //// разбивка списка по городам
            //var citySeparator = new CitySeparatorService(null);
            //var cityDictionary = citySeparator.SeparateByCity(products);

            // создание фидов яндекс
            _yandexFeedCreatorService.CreateXml(path, cityDictionary);

            // создание фидов яндекс
            //var xmlYandex = new YandexFeedCreatorService(cityDictionary);
            //xmlYandex.CreateXml(parser.XlsxFilePath);

            // создание фидов 2ГИС
            _twoGisFeedCreatorService.CreateXml(path, cityDictionary);

            // создание фидов 2ГИС
            /* var xmlTwoGis = new TwoGisFeedCreatorService(cityDictionary);
            xmlTwoGis.CreateXml(parser.XlsxFilePath);*/

            // C:\Users\Farad\Desktop\фид ноябрь.xlsx

            _logger.LogInformation("Генерация XML-файлов завершена.");
            _logger.LogInformation("Нажмите любую клавишу для завершения...");
        }
    }
}
