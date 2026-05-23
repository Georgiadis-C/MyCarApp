
using MyCarApp.Models;
using MyCarApp.ViewModels;

namespace MyCarApp.Views;

public partial class CarMaintainPage : ContentPage
{
    public CarMaintainPage(CarMaintainViewModel carMaintainViewModel)
    {
        InitializeComponent();
        BindingContext = carMaintainViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is CarMaintainViewModel vm)
        {
            await vm.GetCarMaintainListCommand.ExecuteAsync(null);

            await vm.NotifyAboutLastMaintainCommand.ExecuteAsync(null);
        }
    }
}
