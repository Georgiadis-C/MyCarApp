using MyCarApp.Views;
namespace MyCarApp

{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AddCarPage), typeof(AddCarPage));
            Routing.RegisterRoute(nameof(CarDetailsPage), typeof(CarDetailsPage));

            Routing.RegisterRoute(nameof(UpdateCarPage), typeof(UpdateCarPage));

            Routing.RegisterRoute(nameof(CarKmPage), typeof(CarKmPage));
            Routing.RegisterRoute(nameof(AddTripPage), typeof(AddTripPage));

            Routing.RegisterRoute(nameof(TripDetailsPage), typeof(TripDetailsPage));

            Routing.RegisterRoute(nameof(UpdateTripPage), typeof(UpdateTripPage));

            Routing.RegisterRoute(nameof(AddMaintainPage), typeof(AddMaintainPage));
            Routing.RegisterRoute(nameof(MaintainDetailsPage), typeof(MaintainDetailsPage));
            Routing.RegisterRoute(nameof(UpdateMaintainPage), typeof(UpdateMaintainPage));
        }
    }
}
