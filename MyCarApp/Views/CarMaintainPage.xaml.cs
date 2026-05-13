
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

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is CarMaintainViewModel vm)
        {
            vm.GetCarMaintainListCommand.Execute(null);

            vm.GetCarMaintainListCommand.ExecuteAsync(null);

            vm.NotifyAboutLastMaintainCommand.ExecuteAsync(null);
        }
    }
}
