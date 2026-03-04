using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyCarApp.Interfaces;
using MyCarApp.Models;
using MyCarApp.Views;

namespace MyCarApp.ViewModels
{
    public partial class CarsViewModel(ICarService _carService) : ObservableObject
    {

        public ObservableCollection<CarsModel> Cars { get; set; } = new ObservableCollection<CarsModel>();

        [RelayCommand]
        public async Task GetCarList()
        {
            var carList = await _carService.GetCarList();

            Cars.Clear();

            if (carList != null && carList.Count > 0)
            {
                foreach (var car in carList)
                {
                    Cars.Add(car);
                }
            }
        }
       
        [RelayCommand]
        public async Task SelectCar(CarsModel carModel)
        {
            if (carModel == null) return;

            await Shell.Current.GoToAsync(nameof(CarDetailsPage), true, new Dictionary<string, object>
    {
        {"Car", carModel}
    });
        }

        [RelayCommand]
        public async Task GoToAddCar()
        {
            await Shell.Current.GoToAsync(nameof(AddCarPage));
        }
    }
}
