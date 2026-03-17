using MyCarApp.ViewModels;

namespace MyCarApp.Views;

public partial class AddMaintainPage : ContentPage
{
	public AddMaintainPage(AddMaintainViewModel addMaintainViewModel)
	{
		InitializeComponent();
		BindingContext = addMaintainViewModel;
	}
}