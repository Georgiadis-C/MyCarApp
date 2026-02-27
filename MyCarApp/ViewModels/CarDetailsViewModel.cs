using CommunityToolkit.Mvvm.ComponentModel;
using MyCarApp.Models;
using MyCarApp.ViewModels;


namespace MyCarApp.ViewModels
{
    public partial class CarDetailsViewModel : ObservableObject, IQueryAttributable // Πρόσθεσε το interface
    {
        [ObservableProperty]
        CarsModel? car;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // Εδώ παίρνουμε το αντικείμενο "Car" που στέλνει η προηγούμενη σελίδα
            if (query.ContainsKey("Car"))
            {
                Car = query["Car"] as CarsModel;
            }
        }
    }
}
