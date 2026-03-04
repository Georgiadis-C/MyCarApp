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

        private async Task SetUpDB()
        {
            if (_dbConnection == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Car.db3");
                _dbConnection = new SQLiteAsyncConnection(dbPath);
                await _dbConnection.CreateTableAsync<CarsModel>();
            }
        }

        public async Task<List<CarsModel>> GetCarList()
        {
            var CarList = await _dbConnection.Table<CarsModel>().ToListAsync();
            return CarList;
        }

        public async Task SaveCar (CarsModel carsModel)
        {
            if (carsModel.CarId == 0)
            {
                await _dbConnection.InsertAsync(carsModel);
            }
            else
            {
                await _dbConnection.UpdateAsync(carsModel);
            }
        }

        public async Task DeleteCar(CarsModel carsModel)
        {
            await _dbConnection.DeleteAsync(carsModel);
        }
    }
}
