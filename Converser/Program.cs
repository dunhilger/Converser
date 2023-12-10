using Converser.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Converser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs/converser.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var serviceProvider = new ServiceCollection()
                .AddLogging(lb => lb.AddSerilog(dispose: true))
                .AddSingleton<IMainService, MainService>()
                .AddSingleton<IExcelParserService, ExcelParserService>()
                .AddSingleton<ICitySeparatorService, CitySeparatorService>()
                .AddSingleton<IYandexFeedCreatorService, YandexFeedCreatorService>()
                .AddSingleton<ITwoGisFeedCreatorService, TwoGisFeedCreatorService>()
                .BuildServiceProvider();

            var mainService = serviceProvider.GetService<IMainService>();

            Console.WriteLine("Введите путь к директории с файлом Excel:");
            var path = Console.ReadLine();

            mainService.Run(path);

            Console.ReadKey();
        }
    }
}
