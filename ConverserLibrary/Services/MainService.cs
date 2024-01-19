using ConverserLibrary.Interfaces;
using Microsoft.Extensions.Logging;

namespace ConverserLibrary.Services
{
    /// <summary>
    /// Сервис для обработки данных и создания XML-фидов Яндекс и 2ГИС.
    /// </summary>
    [Obsolete("Невостребован при использовании на десктоп")]
    public class MainService : IMainService
    {
        private readonly ILogger<MainService> _logger;
        private readonly IExcelParserService _parser;
        private readonly ICitySeparatorService _separator;
        private readonly IYandexFeedCreatorService _yandexFeedCreatorService;
        private readonly ITwoGisFeedCreatorService _twoGisFeedCreatorService;

        /// <summary>
        /// Инициализирует экземпляр класса MainService.
        /// </summary>
        /// <param name="logger">Интерфейс логгера</param>
        /// <param name="parser">Интерфейс сервиса парсера</param>
        /// <param name="separator">Интерфейс сервиса сепаратора</param>
        /// <param name="yandexFeedCreatorService">Интерфейс сервиса для создания XML-фидов Яндекс</param>
        /// <param name="twoGisFeedCreatorService">Интерфейс сервиса для создания XML-фидов 2ГИС</param>
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

        /// <summary>
        /// Запускает обработку данных и создание XML-фидов.
        /// </summary>
        /// <param name="path">Путь к файлу Excel</param>
        public void Run(string path)
        {
            _logger.LogInformation("Старт.");

            // парсинг эксель, получение списка всех товаров  
            var products = _parser.GetXLSXFile(path);

            // разбивка списка по городам
            var cityDictionary = _separator.SeparateByCity(products);

            // создание фидов яндекс
            _yandexFeedCreatorService.CreateXml(path, cityDictionary);

            // создание фидов 2ГИС
            _twoGisFeedCreatorService.CreateXml(path, cityDictionary);

            // C:\Users\Farad\Desktop\фид ноябрь.xlsx

            _logger.LogInformation("Генерация XML-файлов завершена.");
            _logger.LogInformation("Нажмите любую клавишу для завершения...");
        }
    }
}
