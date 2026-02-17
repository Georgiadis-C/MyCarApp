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
    public partial class AddCarViewModel(ICarService carService) : ObservableObject
    {
        [ObservableProperty]
        CarsModel car = new();

        [RelayCommand]
        public async Task SaveCar()
        {
            await carService.SaveCar(Car);
            await AppShell.Current.GoToAsync("..");
        }
    }
}
