using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyCarApp.Interfaces;
using MyCarApp.Models;
using MyCarApp.ViewModels;
using MyCarApp.Views;


namespace MyCarApp.ViewModels
{

    [QueryProperty(nameof(Car), "Car")]

    public partial class CarDetailsViewModel(ICarService carService) : ObservableObject
    {
        [ObservableProperty]
        CarsModel? car;


        [RelayCommand]
        public async Task GoToUpdateCar()
        {
            await Shell.Current.GoToAsync(nameof(UpdateCarPage), true, new Dictionary<string, object>
                 {
                     {"Car", Car}
                 });
        }

        [RelayCommand]
        public async Task GoToCarKm()
        {
            await Shell.Current.GoToAsync(nameof(CarKmPage), true, new Dictionary<string, object>
                {
                     {"Car", Car}
                 });
        }

        [RelayCommand]
        public async Task DeleteCar()
        {
            if (Car == null) return;

            bool answer = await Shell.Current.DisplayAlertAsync("Delete",
                $"Are you sure that you want to delete the {Car.Brand} {Car.Model};",
                "Yes", "No");

            if (answer)
            {
                await carService.DeleteCar(Car);

                await Shell.Current.DisplayAlertAsync("Success", $"The {Car.Brand} {Car.Model} deleted!", "OK");

                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
