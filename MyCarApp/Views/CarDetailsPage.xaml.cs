using MyCarApp.ViewModels;

namespace MyCarApp.Views;

public partial class CarDetailsPage : ContentPage
{
	public CarDetailsPage(CarDetailsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}