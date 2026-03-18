using System;
using System.Collections.Generic;
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
    public partial class MaintainDetailsViewModel (ICarMaintainService carMaintainService) : ObservableObject
    {
        [ObservableProperty]
        CarMaintainModel maintain = new();
        
        [RelayCommand]
        public async Task GoToUpdateMaintain()
        {
            await Shell.Current.GoToAsync(nameof(UpdateMaintainPage), true, new Dictionary<string, object>
            {
                { "Maintain", Maintain }
            });

        }

        [RelayCommand]
        public async Task DeleteMaintain()
        {
            if (Maintain == null) return;
            bool answer = await Shell.Current.DisplayAlertAsync("Delete",$"Are you sure that you want to delete this maintenance;","Yes", "No");
            if (answer)
            {
                await carMaintainService.DeleteCarMaintain(Maintain);
                await Shell.Current.DisplayAlertAsync("Success", "The maintenance was deleted successfully", "OK");
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
