using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MyCarApp.Models
{
    public class CarKmModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public double Kilometers { get; set; }
    }
}
