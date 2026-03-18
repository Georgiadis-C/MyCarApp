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
    [QueryProperty(nameof(Car), "Car")]

    public partial class AddMaintainViewModel(ICarMaintainService carMaintainService) : ObservableObject
    {
        [ObservableProperty]
        CarsModel car;

        [ObservableProperty]
        CarMaintainModel service = new()
        {
            Date = DateTime.Now
        };

        [RelayCommand]
        public async Task SaveCarMaintain()
        {

            if (Car != null)
            {
                service.CarId = Car.CarId;
            }

            await carMaintainService.SaveCarMaintain(service);

            service = new CarMaintainModel
            {
                Date = DateTime.Now
            };

            await Shell.Current.GoToAsync("..");
        }
    }
}
