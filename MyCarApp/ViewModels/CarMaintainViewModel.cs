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
    [QueryProperty(nameof(Maintain), "Maintain")]
    public partial class CarMaintainViewModel (ICarMaintainService carMaintainService) : ObservableObject
    {
        [ObservableProperty]
        CarsModel car = new();

        [ObservableProperty]
        CarMaintainModel maintain;

        [ObservableProperty]
        ObservableCollection<CarMaintainModel> maintainLogs = new();

        [ObservableProperty]
        int totalMaintains;

        public ObservableCollection<CarMaintainModel> CarMaintains { get; set; } = new ObservableCollection<CarMaintainModel>();

        [RelayCommand]
        public async Task GetCarMaintainList()
        {
            if (Maintain == null) return;

            // Παίρνουμε ΟΛΑ τα χιλιόμετρα από τη βάση
            var allMaintains = await carMaintainService.GetCarMaintainList();

            // Φιλτράρουμε ώστε να δούμε μόνο αυτά που ανήκουν στο τρέχον αυτοκίνητο
            var filteredMaintains = allMaintains.Where(x => x.CarId == Maintain.CarId).ToList();

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

            await Shell.Current.GoToAsync(nameof(MaintainDetailsViewModel), true, new Dictionary<string, object>
                {
                    {"Maintain", carMaintainModel}
                });
        }

        [RelayCommand]
        public async Task GoToAddMaintain()
        {
            await Shell.Current.GoToAsync(nameof(AddMaintainViewModel));
        }


    }
}
