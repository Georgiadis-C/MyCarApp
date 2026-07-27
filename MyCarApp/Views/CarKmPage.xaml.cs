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
        if (e.WorldPosition == null) return;
        var lonLat = SphericalMercator.ToLonLat(e.WorldPosition.X, e.WorldPosition.Y);
        var location = new Location(lonLat.lat, lonLat.lon);
        var viewModel = BindingContext as CarKmViewModel;

        if (viewModel != null && viewModel.AddPointCommand.CanExecute(location))
        {
            viewModel.AddPointCommand.Execute(location);
        }
    }
    private async void OnHistorySelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedRoute = e.CurrentSelection.FirstOrDefault() as CarKmModel;

        if (selectedRoute != null)
        {
            var viewModel = BindingContext as CarKmViewModel;
            if (viewModel != null)
            {
                await viewModel.ShowRouteFromHistoryCommand.ExecuteAsync(selectedRoute);
            }
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