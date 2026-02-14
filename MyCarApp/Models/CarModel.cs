using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using SQLite;

namespace MyCarApp.Models
{
    public class CarModel
    {
        [PrimaryKey, AutoIncrement]
        public int CarId { get; set; }
        public int Horsepower { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int CubicCentimeters { get; set; }

    }
}
