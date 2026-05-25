using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ExceptionServices;
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

        [ObservableProperty]
        string horsepowerError;

        [ObservableProperty]
        string brandError;
        [ObservableProperty]
        string modelError;

        [ObservableProperty]
        string yearError;

        [ObservableProperty]
        string ccError;

        [ObservableProperty]
        string fuelConsError;

        [RelayCommand]
        public async Task SaveCar()
        {
            HorsepowerError = BrandError = ModelError = YearError = CcError = FuelConsError = string.Empty;

            var context = new ValidationContext(Car);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(Car, context, results, true);
            if (!isValid)
            {
                foreach (var error in results)
                {
                    var propertyName = error.MemberNames.FirstOrDefault();
                    switch (propertyName)
                    {
                        case nameof(CarsModel.Horsepower):
                            HorsepowerError = error.ErrorMessage;
                            break;
                        case nameof(CarsModel.Brand):
                            BrandError = error.ErrorMessage;
                            break;
                        case nameof(CarsModel.Model):
                            ModelError = error.ErrorMessage;
                            break;
                        case nameof(CarsModel.Year):
                            YearError = error.ErrorMessage;
                            break;
                        case nameof(CarsModel.CubicCentimeters):
                            CcError = error.ErrorMessage;
                            break;
                        case nameof(CarsModel.FuelConsumption):
                            FuelConsError = error.ErrorMessage;
                            break;
                    }
                }
                return;
            }

            await carService.SaveCar(Car);
            await AppShell.Current.GoToAsync("..");
        }
    }
}
