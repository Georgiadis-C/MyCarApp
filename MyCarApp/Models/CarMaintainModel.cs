using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MyCarApp.Models
{
    public class CarMaintainModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public bool ChangedOil { get; set; }
        public bool ChangedOilFilter { get; set; }
        public bool ChangedAirFilter { get; set; }
        public bool ChangedIgnitionCoil { get; set; }
        public bool ChangedFuelFilter { get; set; }

        public int CarId { get; set; }
    }
}