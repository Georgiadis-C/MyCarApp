using MyCarApp.ViewModels;

namespace MyCarApp.Views;

public partial class CarPage : ContentPage
{
	public CarPage(CarViewModel carViewModel)
	{
		InitializeComponent();
		BindingContext = carViewModel;
    }
}