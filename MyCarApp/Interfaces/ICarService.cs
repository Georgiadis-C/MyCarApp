using System;
using System.Collections.Generic;
using System.Text;
using MyCarApp.Models;

namespace MyCarApp.Interfaces
{
    public interface ICarService
    {
        Task<List<CarsModel>> GetCarList();

        Task SaveCar(CarsModel carModel);
        Task DeleteCar(CarsModel carModel);
    }
}
