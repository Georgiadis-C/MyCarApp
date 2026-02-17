using MyCarApp.ViewModels;

namespace MyCarApp.Views;

public partial class AddCarPage : ContentPage
{
    public AddCarPage(AddCarViewModel addCarViewModel)
	{
		InitializeComponent();
		BindingContext = addCarViewModel;
    }

}