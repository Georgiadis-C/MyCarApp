using System;
using System.Collections.Generic;
using System.Text;
using MyCarApp.Models;

namespace MyCarApp.Interfaces
{
    public interface ICarKmService
    {
        Task<List<CarKmModel>> GetCarKmList();
        Task SaveCarKm(CarKmModel carKmModel);
        Task DeleteCarKm(CarKmModel carKmModel);
    }
}
