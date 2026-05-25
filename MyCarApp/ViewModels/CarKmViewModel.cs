using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyCarApp.Interfaces;
using MyCarApp.Models;
using MyCarApp.Services;
using MyCarApp.Views;

namespace MyCarApp.ViewModels
{
    [QueryProperty(nameof(Car), "Car")]
    public partial class CarKmViewModel : BaseViewModel
    {
        private readonly ICarKmService _carKmService;
        private readonly IMapService _mapService;
        private readonly IRoutingService _routingService;

        [ObservableProperty]
        CarsModel car = new();

        [ObservableProperty]
        CarKmModel trip = new();

        [ObservableProperty]
        ObservableCollection<CarKmModel> kmLogs = new();

        [ObservableProperty]
        double totalKilometers;

        [ObservableProperty]
        private Mapsui.Map _map;

        [ObservableProperty]
        private string _distanceResult = "0 km";

        [ObservableProperty]
        private bool _isMapLocked = false;

        private ObservableCollection<Location> Points { get; } = new();

        public CarKmViewModel(ICarKmService carKmService, IMapService mapService, IRoutingService routingService)
        {
            _carKmService = carKmService;
            _mapService = mapService;
            _routingService = routingService;

            Map = _mapService.GetMap();
        }

        [RelayCommand]
        public async Task GetCarKmList()
        {
            if (Car == null) return;
            await ExecuteAsync(async () =>
            {
                var allTrips = await _carKmService.GetCarKmList();

                var filteredTrips = allTrips
                    .Where(x => x.CarId == Car.CarId)
                    .OrderByDescending(x => x.Date)
                    .ToList();

                KmLogs.Clear();
                foreach (var tripItem in filteredTrips)
                {
                    KmLogs.Add(tripItem);
                }

                TotalKilometers = filteredTrips.Sum(x => x.Kilometers);
            });
        }


        [RelayCommand]
        private async Task AddPoint(Location location)
        {
            if (IsMapLocked || location == null || Points.Count >= 2) return;

            string label = await Application.Current.MainPage.DisplayPromptAsync("Point", "Give a Name:", "OK", "Cancel");
            if (label == null) return; 

            if (string.IsNullOrWhiteSpace(label))
            {
                label = Points.Count == 0 ? "Starting Point" : "Destination";
            }

            var (lon, lat) = Mapsui.Projections.SphericalMercator.FromLonLat(location.Longitude, location.Latitude);
            _mapService.AddPin(new Mapsui.MPoint(lon, lat), label);
            Points.Add(location);

            if (Points.Count == 1) Trip.StartingPoint = label;
            else if (Points.Count == 2) Trip.Destination = label;

            if (Points.Count == 2)
            {
                var (path, distance) = await _routingService.GetRouteAsync(Points[0], Points[1]);

                if (path != null && path.Count > 0)
                {
                    _mapService.AddLine(path);
                    DistanceResult = $"{distance:F2} km";

                    double consumed = (distance * (Car.FuelConsumption ?? 0.0)) / 100;

                    Trip.Kilometers = distance;
                    Trip.Path = path;
                    Trip.FuelConsumed = consumed;
                    Trip.CarId = Car.CarId;
                    Trip.Date = DateTime.Now;

                    await _carKmService.SaveCarKm(Trip);

                    KmLogs.Insert(0, Trip);

                    var savedTrip = Trip;
                    Trip = new CarKmModel();

                    TotalKilometers = KmLogs.Sum(x => x.Kilometers);

                    IsMapLocked = true;

                    await Application.Current.MainPage.DisplayAlertAsync("Success", "Trip recorded automatically!", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlertAsync("Error", "Unable to find a route.", "OK");
                    await ClearMap(); 
                }
            }
        }


        [RelayCommand]
        private async Task ClearMap()
        {
            Points.Clear();
            DistanceResult = "0 km";
            IsMapLocked = false;
            Trip = new CarKmModel();
            _mapService.ClearMap();
            Map.RefreshGraphics();
        }


        [RelayCommand]
        private async Task ShowRouteFromHistory(CarKmModel selectedTrip)
        {
            if (selectedTrip == null) return;

            _mapService.ClearMap();

            if (selectedTrip.Path != null && selectedTrip.Path.Count > 0)
            {
                _mapService.AddLine(selectedTrip.Path);

                var start = selectedTrip.Path.First();
                var end = selectedTrip.Path.Last();
                var startPos = Mapsui.Projections.SphericalMercator.FromLonLat(start.Longitude, start.Latitude);
                var endPos = Mapsui.Projections.SphericalMercator.FromLonLat(end.Longitude, end.Latitude);

                _mapService.AddPin(new Mapsui.MPoint(startPos.x, startPos.y), selectedTrip.StartingPoint);
                _mapService.AddPin(new Mapsui.MPoint(endPos.x, endPos.y), selectedTrip.Destination);

                DistanceResult = $"{selectedTrip.Kilometers:F2} km";
                Map.RefreshGraphics();
                IsMapLocked = true;
            }
        }

        [RelayCommand]
        public async Task DeleteTrip(CarKmModel carKmModel)
        {
            if (carKmModel == null) return;

            bool answer = await Application.Current.MainPage.DisplayAlertAsync("Delete", "Are you sure you want to delete this route?", "Yes", "No");

            if (!answer) return;

            try
            {
                await _carKmService.DeleteCarKm(carKmModel);

                KmLogs.Remove(carKmModel);

                TotalKilometers = KmLogs.Sum(x => x.Kilometers);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlertAsync("Error", $"Delete failed: {ex.Message}", "OK");
            }
        }
    }
}