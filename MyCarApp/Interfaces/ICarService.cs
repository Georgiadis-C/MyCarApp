using System;
using System.Collections.Generic;
using System.Text;
using MyCarApp.Models;

namespace MyCarApp.Interfaces
{
    public interface ICarService
    {
        Task<List<CarModel>> GetCarList();

        Task SaveCar(CarModel carModel);
        Task DeleteCar(CarModel carModel);
    }
}
