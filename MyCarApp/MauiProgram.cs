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
            builder.Services.AddTransient<CarKmViewModel>();
            builder.Services.AddTransient<CarMaintainViewModel>();
            builder.Services.AddTransient<CarsViewModel>();
            builder.Services.AddTransient<AddCarViewModel>();
            builder.Services.AddTransient<CarDetailsViewModel>();
            builder.Services.AddTransient<UpdateCarPageViewModel>();
            builder.Services.AddTransient<AddTripViewModel>();
            builder.Services.AddTransient<TripDetailsViewModel>();
            builder.Services.AddTransient<UpdateTripViewModel>();
            builder.Services.AddTransient<AddMaintainViewModel>();
            builder.Services.AddTransient<MaintainDetailsViewModel>();
            builder.Services.AddTransient<UpdateMaintainViewModel>();

            // Views
            builder.Services.AddTransient<CarKmPage>();
            builder.Services.AddTransient<CarMaintainPage>();
            builder.Services.AddTransient<CarsPage>();
            builder.Services.AddTransient<AddCarPage>();
            builder.Services.AddTransient<CarDetailsPage>();
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
            builder.Services.AddTransient<IMapService, MapService>();
            builder.Services.AddSingleton<IRoutingService, RoutingService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
