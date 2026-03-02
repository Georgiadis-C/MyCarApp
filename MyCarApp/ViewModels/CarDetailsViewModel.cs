using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyCarApp.Models;
using MyCarApp.ViewModels;
using MyCarApp.Views;


namespace MyCarApp.ViewModels
{
    public partial class CarDetailsViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        CarsModel? car;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("Car"))
            {
                Car = query["Car"] as CarsModel;
            }
        }

        [RelayCommand]
        public async Task GoToUpdateCar()
        {
            await Shell.Current.GoToAsync(nameof(UpdateCarPage));
        }
    }
}
