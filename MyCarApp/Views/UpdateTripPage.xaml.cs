using MyCarApp.ViewModels;

namespace MyCarApp.Views;

public partial class UpdateTripPage : ContentPage
{
	public UpdateTripPage(UpdateTripViewModel updateTripViewModel)
	{
		InitializeComponent();
		BindingContext = updateTripViewModel;
    }
}