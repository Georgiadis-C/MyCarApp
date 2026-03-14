using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyCarApp.Interfaces;
using MyCarApp.Models;
using MyCarApp.Services;
using MyCarApp.Views;

namespace MyCarApp.ViewModels
{
    [QueryProperty(nameof(Car), "Car")]
    public partial class CarKmViewModel (ICarKmService carKmService) : ObservableObject
    {
        [ObservableProperty]
        CarsModel car = new();

        [ObservableProperty]
        CarKmModel trip;

        [ObservableProperty]
        ObservableCollection<CarKmModel> kmLogs = new();

        [ObservableProperty]
        double totalKilometers;

        public ObservableCollection<CarKmModel> Trips { get; set; } = new ObservableCollection<CarKmModel>();


        [RelayCommand]
        public async Task GetCarKmList()
        {
            if (Car == null) return;

            // Παίρνουμε ΟΛΑ τα χιλιόμετρα από τη βάση
            var allTrips = await carKmService.GetCarKmList();

            // Φιλτράρουμε ώστε να δούμε μόνο αυτά που ανήκουν στο τρέχον αυτοκίνητο
            var filteredTrips = allTrips.Where(x => x.CarId == Car.CarId).ToList();

            KmLogs.Clear();
            foreach (var trip in filteredTrips)
            {
                KmLogs.Add(trip);
            }

            TotalKilometers = filteredTrips.Sum(x => x.Kilometers);
        }

        [RelayCommand]
        public async Task SelectTrip(CarKmModel carKmModel)
        {
            if (carKmModel == null) return;

            await Shell.Current.GoToAsync(nameof(CarDetailsPage), true, new Dictionary<string, object>
                {
                    {"Trip", carKmModel}
                });
        }

        [RelayCommand]
        public async Task GoToAddTrip()
        {
            await Shell.Current.GoToAsync(nameof(AddTripPage), new Dictionary<string, object>
                {
                    { "Car", Car } 
                });
        }
    }
}
