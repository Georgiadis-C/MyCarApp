using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.ComponentModel.DataAnnotations;
using SQLite;

namespace MyCarApp.Models
{
    public class CarsModel
    {
        [PrimaryKey, AutoIncrement]
        public int CarId { get; set; }

        [Required(ErrorMessage = "Horsepower is required")]
        [Range(30, 1500, ErrorMessage = "Horsepower must be between 30 and 1500")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Horsepower must be a number")]
        public int? Horsepower { get; set; }

        [Required(ErrorMessage = "Brand is required")]
        [StringLength(20, ErrorMessage = "Brand must be 20 characters or less")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Brand can only contain letters and spaces")]
        public string Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model is required")]
        [StringLength(20, ErrorMessage = "Model must be 20 characters or less")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Model can only contain letters, numbers, and spaces")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Year is required")]
        [Range(1900, 2026, ErrorMessage = "Year must be between 1900 and 2026")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Year must be a number")]
        public int? Year { get; set; }

        [Required(ErrorMessage = "Cubic Centimeters is required")]
        [Range(500, 7000, ErrorMessage = "Cubic Centimeters must be between 500 and 7000")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Cubic Centimeters must be a number")]
        public int? CubicCentimeters { get; set; }

        [Required(ErrorMessage = "Fuel Consumption is required")]
        [Range(0.1, 100.0, ErrorMessage = "Fuel Consumption must be between 0.1 and 100.0")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Fuel Consumption must be a valid number with up to 2 decimal places")]
        public double? FuelConsumption { get; set; }

    }
}
