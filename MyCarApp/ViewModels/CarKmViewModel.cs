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

        public ObservableCollection<CarKmModel> Trips { get; set; } = new ObservableCollection<CarKmModel>();


        [RelayCommand]
        public async Task GetCarKmList()
        {
            var carKmList = await carKmService.GetCarKmList();

            Trips.Clear();

            if (carKmList != null && carKmList.Count > 0)
            {
                foreach (var trip in carKmList)
                {
                    Trips.Add(trip);
                }
            }
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
            await Shell.Current.GoToAsync(nameof(AddTripPage));
        }
    }
}
