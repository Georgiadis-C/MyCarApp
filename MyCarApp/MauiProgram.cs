using Microsoft.Extensions.Logging;
using MyCarApp.Interfaces;
using MyCarApp.Services;
using MyCarApp.ViewModels;
using MyCarApp.Views;

namespace MyCarApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // ViewModels
            builder.Services.AddSingleton<CarKmViewModel>();
            builder.Services.AddSingleton<CarMaintainViewModel>();
            builder.Services.AddSingleton<CarViewModel>();

            // Views
            builder.Services.AddSingleton<CarKmPage>();
            builder.Services.AddSingleton<CarMaintainPage>();
            builder.Services.AddSingleton<CarPage>();

            //Services
            builder.Services.AddSingleton<ICarService,CarService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
