using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
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
    [QueryProperty(nameof(CarKmModel), "CarKmModel")]

    [QueryProperty(nameof(TotalKilometers), "TotalKilometers")]
    public partial class CarMaintainViewModel (ICarMaintainService carMaintainService, ICarKmService carKmService) : BaseViewModel
    {

        [ObservableProperty]
        CarsModel car = new();

        [ObservableProperty]
        CarMaintainModel maintain;

        [ObservableProperty]
        ObservableCollection<CarMaintainModel> maintainLogs = new();

        [ObservableProperty]
        int totalMaintains;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(RemainingKm))]
        [NotifyPropertyChangedFor(nameof(MaintenanceStatusMessage))]
        double kmSinceLastMaintain;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(RemainingKm))]
        [NotifyPropertyChangedFor(nameof(MaintenanceStatusMessage))]
        double totalKilometers;
        

        private const double ServiceInterval = 10000;


        public double RemainingKm => ServiceInterval - KmSinceLastMaintain;

         public string MaintenanceStatusMessage => RemainingKm switch
         {
             > 2000 => "You've got plenty of road left. Floor it, Vin Diesel is watching.",
             > 0 => $"The service time is coming! (Remaining {RemainingKm:N0} Km)",
             _ => "WARNING: Your car needs service immediately!"
         }; 


        public bool IsMaintenanceOverdue => KmSinceLastMaintain > 10000;
        public ObservableCollection<CarMaintainModel> CarMaintains { get; set; } = new ObservableCollection<CarMaintainModel>();


        [RelayCommand]
        public async Task NotifyAboutLastMaintain()
        {
            if (Car == null) return;

            await ExecuteAsync(async () =>
            {
                var allTrips = await carKmService.GetCarKmList();
                TotalKilometers = allTrips
                    .Where(x => x.CarId == Car.CarId)
                    .Sum(x => x.Kilometers);

                var allMaintains = await carMaintainService.GetCarMaintainList();

                var lastMaintenance = allMaintains
                    .Where(x => x.CarId == Car.CarId)
                    .OrderByDescending(x => x.Date)
                    .FirstOrDefault();

                if (lastMaintenance != null)
                {
                    KmSinceLastMaintain = TotalKilometers - lastMaintenance.Kilometers;

                }
                else
                {
                    KmSinceLastMaintain = TotalKilometers;
                }

                OnPropertyChanged(nameof(RemainingKm));
                OnPropertyChanged(nameof(MaintenanceStatusMessage));

            });
        }

        [RelayCommand]
        public async Task GetCarMaintainList()
        {
            if (Car == null) return;

            var allMaintains = await carMaintainService.GetCarMaintainList();

            var filteredMaintains = allMaintains.Where(x => x.CarId == Car.CarId).ToList();

            maintainLogs.Clear();
            foreach (var maintain in filteredMaintains)
            {
                maintainLogs.Add(maintain);
            }

            TotalMaintains = filteredMaintains.Count;
        }

        [RelayCommand]
        public async Task SelectMaintain(CarMaintainModel carMaintainModel)
        {
            if (carMaintainModel == null) return;

            await Shell.Current.GoToAsync(nameof(MaintainDetailsPage), true, new Dictionary<string, object>
                {
                    {"Maintain", carMaintainModel}
                });
        }

        [RelayCommand]
        public async Task GoToAddMaintain()
        {
            await Shell.Current.GoToAsync(nameof(AddMaintainPage), true, new Dictionary<string, object>
                {
                    {"Car", Car}
                });
        }


    }
}
