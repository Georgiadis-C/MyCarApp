using MyCarApp.ViewModels;

namespace MyCarApp.Views;

public partial class AddTripPage : ContentPage
{
	public AddTripPage(AddTripViewModel addTripViewModel)
	{
		InitializeComponent();
		BindingContext = addTripViewModel;
    }
}