using MyCarApp.ViewModels;

namespace MyCarApp.Views;


public partial class CarKmPage : ContentPage
{
	public CarKmPage(CarKmViewModel carKmViewModel)
	{
		InitializeComponent();
		BindingContext = carKmViewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is CarKmViewModel vm)
        {
            // Μην βάζεις await. Το Execute "τρέχει" την εντολή 
            // και αφήνει το UI να αναπνεύσει.
            vm.GetCarKmListCommand.Execute(null);
        }
    }
}