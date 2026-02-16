using MyCarApp.ViewModels;

namespace MyCarApp.Views;


public partial class CarKmPage : ContentPage
{
	public CarKmPage(CarKmViewModel carKmViewModel)
	{
		InitializeComponent();
		BindingContext = carKmViewModel;
	}
}