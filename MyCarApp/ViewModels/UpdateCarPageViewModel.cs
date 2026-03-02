using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyCarApp.Interfaces;
using MyCarApp.Models;

namespace MyCarApp.ViewModels
{
    public partial class UpdateCarPageViewModel(ICarService carService) : ObservableObject, Microsoft.Maui.Controls.IQueryAttributable
    {
        [ObservableProperty]
        CarsModel car = new();

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("Car"))
            {
                Car = query["Car"] as CarsModel;
            }
        }

        [RelayCommand]
        public async Task UpdateCar()
        {
            await carService.SaveCar(Car);
            await AppShell.Current.GoToAsync("..");
        }
    }
}
