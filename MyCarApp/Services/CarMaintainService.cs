using System;
using System.Collections.Generic;
using System.Text;
using MyCarApp.Interfaces;
using MyCarApp.Models;
using SQLite;

namespace MyCarApp.Services
{
    public class CarMaintainService : ICarMaintainService
    {
        private SQLiteAsyncConnection _dbConnection;

        public CarMaintainService()
        {
            SetUpDB();
        }
        private async Task SetUpDB()
        {
            if (_dbConnection == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Car.db3");
                _dbConnection = new SQLiteAsyncConnection(dbPath);
                await _dbConnection.CreateTableAsync<CarMaintainModel>();
            }
        }

        public async Task<List<CarMaintainModel>> GetCarMaintainList()
        {
            var CarMaintainList = await _dbConnection.Table<CarMaintainModel>().ToListAsync();
            return CarMaintainList;
        }

        public async Task SaveCarMaintain(CarMaintainModel carMaintainModel)
        {
            if (carMaintainModel.Id == 0)
            {
                await _dbConnection.InsertAsync(carMaintainModel);
            }
            else
            {
                await _dbConnection.UpdateAsync(carMaintainModel);
            }
        }

        public async Task DeleteCarMaintain(CarMaintainModel carMaintainModel)
        {
            await _dbConnection.DeleteAsync(carMaintainModel);
        }
    }
}
