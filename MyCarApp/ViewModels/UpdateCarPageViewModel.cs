using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyCarApp.Interfaces;
using MyCarApp.Models;

namespace MyCarApp.ViewModels
{
    [QueryProperty(nameof(Car), "Car")]
    public partial class UpdateCarPageViewModel(ICarService carService) : ObservableObject
    {
        [ObservableProperty]
        CarsModel car = new();

        [RelayCommand]
        public async Task UpdateCar()
        {
            await Shell.Current.DisplayAlertAsync("Debug", $"Saving Car with ID: {Car.CarId}", "OK");

            await carService.SaveCar(Car);
            await AppShell.Current.GoToAsync("..");
        }
    }
}
