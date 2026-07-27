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

            RegisterViewModels(builder.Services);
            RegisterViews(builder.Services);
            RegisterServices(builder.Services);



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
        private static void RegisterViewModels(IServiceCollection services)
        {
            services.AddTransient<CarKmViewModel>();
            services.AddTransient<CarMaintainViewModel>();
            services.AddTransient<CarsViewModel>();
            services.AddTransient<AddCarViewModel>();
            services.AddTransient<CarDetailsViewModel>();
            services.AddTransient<UpdateCarPageViewModel>();
            services.AddTransient<AddTripViewModel>();
            services.AddTransient<TripDetailsViewModel>();
            services.AddTransient<UpdateTripViewModel>();
            services.AddTransient<AddMaintainViewModel>();
            services.AddTransient<MaintainDetailsViewModel>();
            services.AddTransient<UpdateMaintainViewModel>();
        }

        private static void RegisterViews(IServiceCollection services)
        {
            services.AddTransient<CarKmPage>();
            services.AddTransient<CarMaintainPage>();
            services.AddTransient<CarsPage>();
            services.AddTransient<AddCarPage>();
            services.AddTransient<CarDetailsPage>();
            services.AddTransient<UpdateCarPage>();
            services.AddTransient<AddTripPage>();
            services.AddTransient<TripDetailsPage>();
            services.AddTransient<UpdateTripPage>();
            services.AddTransient<AddMaintainPage>();
            services.AddTransient<MaintainDetailsPage>();
            services.AddTransient<UpdateMaintainPage>();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<ICarService, CarService>();
            services.AddSingleton<ICarKmService, CarKmService>();
            services.AddSingleton<ICarMaintainService, CarMaintainService>();
            services.AddTransient<IMapService, MapService>();
            services.AddSingleton<IRoutingService, RoutingService>();
        }
    } 
}

