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
    public partial class AddMaintainViewModel (ICarMaintainService carMaintainService) : ObservableObject
    {
        [ObservableProperty]
        CarMaintainModel service;

        [RelayCommand]
        public async Task SaveCar()
        {
            await carMaintainService.SaveCarMaintain(service);
            await AppShell.Current.GoToAsync("..");
        }
    }
}
