using MyCarApp.ViewModels;

namespace MyCarApp.Views;

public partial class UpdateMaintainPage : ContentPage
{
	public UpdateMaintainPage(UpdateMaintainViewModel updateMaintainViewModel)
	{
		InitializeComponent();
		BindingContext = updateMaintainViewModel;
	}
}