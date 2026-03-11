using System;
using System.Collections.Generic;
using System.Text;
using MyCarApp.Interfaces;
using MyCarApp.Models;
using SQLite;

namespace MyCarApp.Services
{
    public class CarKmService : ICarKmService
    {

        private SQLiteAsyncConnection _dbConnection;

        public CarKmService()
        {
            SetUpDB();
        }
        private async Task SetUpDB()
        {
            if (_dbConnection == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Car.db3");
                _dbConnection = new SQLiteAsyncConnection(dbPath);
                await _dbConnection.CreateTableAsync<CarKmModel>();
            }
        }

        public async Task<List<CarKmModel>> GetCarKmList()
        {
            var CarKmList = await _dbConnection.Table<CarKmModel>().ToListAsync();
            return CarKmList;
        }

        public async Task SaveCarKm(CarKmModel carKmModel)
        {
            if (carKmModel.CarId == 0)
            {
                await _dbConnection.InsertAsync(carKmModel);
            }
            else
            {
                await _dbConnection.UpdateAsync(carKmModel);
            }
        }

        public async Task DeleteCarKm(CarKmModel carKmModel)
        {
            await _dbConnection.DeleteAsync(carKmModel);
        }

    }
}
