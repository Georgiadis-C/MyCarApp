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
        CarKmModel trip = new();

        [RelayCommand]
        public async Task SaveCarKm()
        {
            // ΕΔΩ ΕΙΝΑΙ Η ΔΙΟΡΘΩΣΗ:
            // Πρέπει να δώσεις το CarId του αυτοκινήτου στο Trip πριν το σώσεις
            if (Car != null)
            {
                Trip.CarId = Car.CarId;
            }

            await carKmService.SaveCarKm(Trip);
            await Shell.Current.GoToAsync("..");
        }
    }
}

