using MyCarApp.ViewModels;

namespace MyCarApp.Views;

public partial class UpdateCarPage : ContentPage
{
    public UpdateCarPage(UpdateCarPageViewModel updateCarPageViewModel)
	{
		InitializeComponent();
		BindingContext = updateCarPageViewModel;
	}
}