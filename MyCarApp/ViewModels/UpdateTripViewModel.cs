using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyCarApp.Interfaces;
using MyCarApp.Models;
using MyCarApp.Services;

namespace MyCarApp.ViewModels
{
    [QueryProperty(nameof(Trip), "Trip")]
    public partial class UpdateTripViewModel(ICarKmService carKmService) : ObservableObject
    {
        [ObservableProperty]
        CarKmModel trip = new();

        [RelayCommand]
        public async Task UpdateTrip()
        {
            await Shell.Current.DisplayAlertAsync("Debug", $"Saving Trip with ID: {Trip.Id}", "OK");

            await carKmService.SaveCarKm(Trip);
            await AppShell.Current.GoToAsync("..");
        }

    }
}
