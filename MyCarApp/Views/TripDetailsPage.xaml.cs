using MyCarApp.ViewModels;

namespace MyCarApp.Views;

public partial class TripDetailsPage : ContentPage
{
	public TripDetailsPage(TripDetailsViewModel tripDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = tripDetailsViewModel;
    }
}