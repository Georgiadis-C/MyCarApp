using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SQLite;

namespace MyCarApp.Models
{
    public class CarKmModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public double Kilometers { get; set; }

        public DateTime Date { get; set; }

        public string StartingPoint { get; set; } = string.Empty;

        public string Destination { get; set; } = string.Empty;

        public double FuelConsumed { get; set; }

        public int CarId { get; set; }

        // Το [Ignore] λέει στην SQLite: "Μην το κοιτάς αυτό, δεν μπορείς να το σώσεις"
        [Ignore]
        public List<Location> Path { get; set; }

        // Κάνει αυτόματα τη μετατροπή από Λίστα σε String και το αντίστροφο
        public string PathData
        {
            get => Path != null ? JsonConvert.SerializeObject(Path) : null;
            set => Path = !string.IsNullOrEmpty(value)
                          ? JsonConvert.DeserializeObject<List<Location>>(value)
                          : new List<Location>();
        }
    }
}
