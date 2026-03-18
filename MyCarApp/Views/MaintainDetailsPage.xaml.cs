using MyCarApp.ViewModels;

namespace MyCarApp.Views;

public partial class MaintainDetailsPage : ContentPage
{
	public MaintainDetailsPage(MaintainDetailsViewModel maintainDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = maintainDetailsViewModel;
    }

}