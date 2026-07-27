using System;
using System.Collections.Generic;
using System.Text;
using MyCarApp.Models;

namespace MyCarApp.Interfaces
{
    public interface ICarMaintainService
    {
        Task<List<CarMaintainModel>> GetCarMaintainList();
        Task SaveCarMaintain(CarMaintainModel carMaintainModel);
        Task DeleteCarMaintain(CarMaintainModel carMaintainModel);
    }
}
