using MyCarApp.ViewModels;

namespace MyCarApp.Views;

public partial class CarsPage : ContentPage
{
    public CarsPage(CarsViewModel carsViewModel)
    {
        InitializeComponent();
        BindingContext = carsViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var vm = (CarsViewModel)BindingContext;
        await vm.GetCarListCommand.ExecuteAsync(null);
    }
}