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
    [QueryProperty(nameof(Maintain), "Maintain")]
    public partial class UpdateMaintainViewModel (ICarMaintainService carMaintainService) : ObservableObject
    {
        [ObservableProperty]
        CarMaintainModel maintain;

        [RelayCommand]
        public async Task UpdateTrip()
        {
            await Shell.Current.DisplayAlertAsync("Debug", $"Saving maintenance with ID: {Maintain.Id}", "OK");

            await carMaintainService.SaveCarMaintain(Maintain);
            await AppShell.Current.GoToAsync("..");
        }

    }
}
