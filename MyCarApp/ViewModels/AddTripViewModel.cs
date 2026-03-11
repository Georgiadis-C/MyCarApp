using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyCarApp.Interfaces;
using MyCarApp.Models;

namespace MyCarApp.ViewModels
{
    public partial class AddTripViewModel(ICarKmService carKmService) : ObservableObject
    {
        [ObservableProperty]
        CarKmModel trip = new();

        [RelayCommand]
        public async Task SaveCarKm()
        {
            await carKmService.SaveCarKm(Trip);
            await AppShell.Current.GoToAsync("..");
        }
    }
}

