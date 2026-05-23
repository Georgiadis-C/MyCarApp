using Mapsui.Projections;
using MyCarApp.Models;
using MyCarApp.ViewModels;

namespace MyCarApp.Views;


public partial class CarKmPage : ContentPage
{
    public CarKmPage(CarKmViewModel carKmViewModel)
    {
        InitializeComponent();
        BindingContext = carKmViewModel;
    }

    private void MyMap_Info(object sender, Mapsui.MapInfoEventArgs e)
    {
        // Αν ο χάρτης δεν έχει φορτωθεί ή δεν έχουμε θέση, σταματάμε
        if (e.WorldPosition == null) return;

        // Μετατροπή των συντεταγμένων του χάρτη σε μοίρες GPS (Lon, Lat)
        var lonLat = SphericalMercator.ToLonLat(e.WorldPosition.X, e.WorldPosition.Y);

        // Δημιουργία αντικειμένου Location του MAUI
        var location = new Location(lonLat.lat, lonLat.lon);

        // Κλήση της εντολής στο ViewModel
        var viewModel = BindingContext as CarKmViewModel;
        if (viewModel != null && viewModel.AddPointCommand.CanExecute(location))
        {
            viewModel.AddPointCommand.Execute(location);
        }
    }
    private async void OnHistorySelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Παίρνουμε τη διαδρομή που επιλέχθηκε
        var selectedRoute = e.CurrentSelection.FirstOrDefault() as CarKmModel;

        if (selectedRoute != null)
        {
            var viewModel = BindingContext as CarKmViewModel;
            if (viewModel != null)
            {
                // Καλούμε απευθείας την Command του ViewModel
                await viewModel.ShowRouteFromHistoryCommand.ExecuteAsync(selectedRoute);
            }

            // ΚΑΘΑΡΙΣΜΟΣ ΕΠΙΛΟΓΗΣ: 
            // Ξε-επιλέγουμε το item ώστε να μπορείς να το ξαναπατήσεις αν χρειαστεί
            ((CollectionView)sender).SelectedItem = null;
        }
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is CarKmViewModel vm)
        {
            await vm.ClearMapCommand.ExecuteAsync(null);
            await vm.GetCarKmListCommand.ExecuteAsync(null);
        }
    }
}