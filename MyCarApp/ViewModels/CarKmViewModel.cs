using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using MyCarApp.Models;

namespace MyCarApp.ViewModels
{
    [QueryProperty(nameof(Car), "Car")]
    public partial class CarKmViewModel : ObservableObject
    {
        [ObservableProperty]
        CarsModel car;
    }
}
