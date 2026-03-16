using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyCarApp.Interfaces;
using MyCarApp.Models;
using MyCarApp.Views;

namespace MyCarApp.ViewModels
{
    [QueryProperty(nameof(Trip), "Trip")]
    public partial class TripDetailsViewModel(ICarKmService carKmService) : ObservableObject
    {
        [ObservableProperty]
        CarKmModel? trip;

        [RelayCommand]
        public async Task GoToUpdateTrip()
        {
            await Shell.Current.GoToAsync(nameof(UpdateTripPage), true, new Dictionary<string, object>
            {
                { "Trip", Trip }
            });

        }

        [RelayCommand]
        public async Task DeleteTrip()
        {
            if (Trip == null) return;
            bool answer = await Shell.Current.DisplayAlertAsync("Delete",
                $"Are you sure that you want to delete the trip from {Trip.StartingPoint} to {Trip.Destination} on {Trip.Date:d};",
                "Yes", "No");
            if (answer)
            {
                await carKmService.DeleteCarKm(Trip);
                await Shell.Current.DisplayAlertAsync("Success", $"The trip from {Trip.StartingPoint} to {Trip.Destination} on {Trip.Date:d} deleted!", "OK");
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
