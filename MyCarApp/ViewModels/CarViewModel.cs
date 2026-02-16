using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyCarApp.Interfaces;
using MyCarApp.Models;

namespace MyCarApp.ViewModels
{
    public partial class CarViewModel(ICarService _carService) : ObservableObject
    {
        public ObservableCollection<CarModel> Cars { get; set; } = new ObservableCollection<CarModel>();



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

        public async Task SaveCar(CarModel carModel)
        {
            await _carService.SaveCar(carModel);
            await GetCarList();
        }





    }
}
