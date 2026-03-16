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

    public partial class AddTripViewModel(ICarKmService carKmService) : ObservableObject
    {
        [ObservableProperty]
        CarsModel car;

        [ObservableProperty]
        CarKmModel trip = new()
        {
            Date = DateTime.Now,
            StartingPoint = string.Empty,
            Destination = string.Empty
        };

        [RelayCommand]
        public async Task SaveCarKm()
        {

            if (string.IsNullOrWhiteSpace(Trip.StartingPoint) || string.IsNullOrWhiteSpace(Trip.Destination) || Trip.Kilometers <= 0)
            {

                await Shell.Current.DisplayAlertAsync("Error", "You must complete all the fields to continue!", "OK");
                return;
            }

            if (Car != null)
            {
                Trip.CarId = Car.CarId;
            }

            await carKmService.SaveCarKm(Trip);

            Trip = new CarKmModel
            {
                Date = DateTime.Now,
                StartingPoint = string.Empty,
                Destination = string.Empty
            };

            await Shell.Current.GoToAsync("..");
        }
    }
}

