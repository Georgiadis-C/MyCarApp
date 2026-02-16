using Microsoft.Extensions.Logging;

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
            builder.Services.AddSingleton<ViewModels.CarKmViewModel>();
            builder.Services.AddSingleton<ViewModels.CarMaintainViewModel>();
            builder.Services.AddSingleton<ViewModels.CarViewModel>();

            // Views
            builder.Services.AddSingleton<Views.CarKmPage>();
            builder.Services.AddSingleton<Views.CarMaintainPage>();
            builder.Services.AddSingleton<Views.CarPage>();

            //Services


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
