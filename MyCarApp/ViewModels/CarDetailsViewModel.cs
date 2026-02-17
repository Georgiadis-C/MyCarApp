
using CommunityToolkit.Mvvm.ComponentModel;
using MyCarApp.Models;

namespace MyCarApp.ViewModels
{
    [QueryProperty(nameof(CarsModel), nameof(CarsModel))]
    public partial class CarDetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        CarsModel _carsModel;
    }
}
