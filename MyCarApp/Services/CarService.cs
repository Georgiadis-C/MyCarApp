using System;
using System.Collections.Generic;
using System.Text;
using MyCarApp.Interfaces;
using MyCarApp.Models;
using SQLite;

namespace MyCarApp.Services
{
    public class CarService : ICarService
    {
        private SQLiteAsyncConnection _dbConnection;
        public CarService()
        {
            SetUpDB();
        }

        private async void SetUpDB()
        {
            if (_dbConnection == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Car.db3");
                _dbConnection = new SQLiteAsyncConnection(dbPath);
                await _dbConnection.CreateTableAsync<CarModel>();
            }
        }

        public async Task<List<CarModel>> GetCarList()
        {
            var CarList = await _dbConnection.Table<CarModel>().ToListAsync();
            return CarList;
        }

        public async Task SaveCar (CarModel carModel)
        {
            if (carModel.CarId == 0)
            {
                await _dbConnection.InsertAsync(carModel);
            }
            else
            {
                await _dbConnection.UpdateAsync(carModel);
            }
        }

        public async Task DeleteCar(CarModel carModel)
        {
            await _dbConnection.DeleteAsync(carModel);
        }
    }
}
