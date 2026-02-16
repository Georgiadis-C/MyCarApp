using System;
using System.Collections.Generic;
using System.Text;

namespace MyCarApp.Models
{
    public class CarMaintainModel
    {
        public bool ChangedOil { get; set; }
        public bool ChangedOilFilter { get; set; }
        public bool ChangedAirFilter { get; set; }
        public bool ChangedIgnitionCoil { get; set; }
        public bool ChangedFuelFilter { get; set; }
    }
}