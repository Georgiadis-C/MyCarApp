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


        private async Task SetUpDB()
        {
            if (_dbConnection != null)
                return;

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Car.db3");
            _dbConnection = new SQLiteAsyncConnection(dbPath);

            // Με το await εδώ, είμαστε σίγουροι ότι οι πίνακες θα δημιουργηθούν σωστά
            await _dbConnection.CreateTableAsync<CarsModel>();
            await _dbConnection.CreateTableAsync<CarKmModel>();
        }

        public async Task<List<CarsModel>> GetCarList()
        {
            await SetUpDB();
            var CarList = await _dbConnection.Table<CarsModel>().ToListAsync();
            return CarList;
        }

        public async Task SaveCar(CarsModel carsModel)
        {
            await SetUpDB();
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
            await SetUpDB();
            await _dbConnection.DeleteAsync(carsModel);
        }
    }
}
