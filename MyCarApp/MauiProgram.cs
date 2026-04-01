using Microsoft.Extensions.Logging;
using MyCarApp.Interfaces;
using MyCarApp.Services;
using MyCarApp.ViewModels;
using MyCarApp.Views;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace MyCarApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // ViewModels
            builder.Services.AddSingleton<CarKmViewModel>();
            builder.Services.AddSingleton<CarMaintainViewModel>();
            builder.Services.AddSingleton<CarsViewModel>();
            builder.Services.AddSingleton<AddCarViewModel>();
            builder.Services.AddSingleton<CarDetailsViewModel>();
            builder.Services.AddTransient<UpdateCarPageViewModel>();
            builder.Services.AddTransient<AddTripViewModel>();
            builder.Services.AddTransient<TripDetailsViewModel>();
            builder.Services.AddTransient<UpdateTripViewModel>();
            builder.Services.AddSingleton<AddMaintainViewModel>();
            builder.Services.AddSingleton<MaintainDetailsViewModel>();
            builder.Services.AddSingleton<UpdateMaintainViewModel>();

            // Views
            builder.Services.AddSingleton<CarKmPage>();
            builder.Services.AddSingleton<CarMaintainPage>();
            builder.Services.AddSingleton<CarsPage>();
            builder.Services.AddSingleton<AddCarPage>();
            builder.Services.AddSingleton<CarDetailsPage>();
            builder.Services.AddTransient<UpdateCarPage>();
            builder.Services.AddTransient<AddTripPage>();
            builder.Services.AddTransient<TripDetailsPage>();
            builder.Services.AddTransient<UpdateTripPage>();
            builder.Services.AddTransient<AddMaintainPage>();
            builder.Services.AddTransient<MaintainDetailsPage>();
            builder.Services.AddTransient<UpdateMaintainPage>();


            //Services
            builder.Services.AddSingleton<ICarService,CarService>();
            builder.Services.AddSingleton<ICarKmService, CarKmService>();
            builder.Services.AddSingleton<ICarMaintainService, CarMaintainService>();
            builder.Services.AddSingleton<ICarKmService, CarKmService>();
            builder.Services.AddSingleton<IMapService, MapService>();
            builder.Services.AddSingleton<IRoutingService, RoutingService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
