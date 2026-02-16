using MyCarApp.Models;

namespace MyCarApp.Views;

public partial class CarMaintainPage : ContentPage
{
	public CarMaintainPage(CarMaintainModel carMaintainModel)
	{
		InitializeComponent();
		BindingContext = carMaintainModel;
    }
}