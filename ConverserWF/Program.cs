using ConverserLibrary;
using ConverserLibrary.Interfaces;
using ConverserLibrary.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ConverserWF
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs/converser.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var serviceProvider = new ServiceCollection()
                .AddLogging(lb => lb.AddSerilog(dispose: true))
                .AddSingleton<IExcelParserService, ExcelParserService>()
                .AddSingleton<ICitySeparatorService, CitySeparatorService>()
                .AddSingleton<IYandexFeedCreatorService, YandexFeedCreatorService>()
                .AddSingleton<ITwoGisFeedCreatorService, TwoGisFeedCreatorService>()
                .AddSingleton<IVKFeedCreatorService, VKFeedCreatorService>()
                .AddSingleton<IInfoDataService, InfoDataService>()
                .AddSingleton<ITransliterationService, TransliterationService>()
                .AddSingleton<MainForm>()
                .BuildServiceProvider();

            var mainForm = serviceProvider.GetRequiredService<MainForm>();
            Application.Run(mainForm);
        }
    }
}