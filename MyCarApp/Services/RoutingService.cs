using System.Net.Http.Json;
using System.Globalization;
using MyCarApp.Interfaces;
namespace MyCarApp.Services;



public class RoutingService : IRoutingService
{
    private readonly HttpClient _httpClient = new();

    public async Task<(List<Location> Path, double Distance)> GetRouteAsync(Location start, Location end)
    {
        try
        {
            var totalSw = System.Diagnostics.Stopwatch.StartNew();
            var stepSw = System.Diagnostics.Stopwatch.StartNew();

            // OSRM API URL
            string url = $"https://routing.openstreetmap.de/routed-car/route/v1/driving/{start.Longitude.ToString(CultureInfo.InvariantCulture)},{start.Latitude.ToString(CultureInfo.InvariantCulture)};{end.Longitude.ToString(CultureInfo.InvariantCulture)},{end.Latitude.ToString(CultureInfo.InvariantCulture)}?overview=simplified&geometries=polyline";

            var response = await _httpClient.GetFromJsonAsync<OsrmResponse>(url);

            System.Console.WriteLine("*********************************************************");
            System.Console.WriteLine("!!! [OSRM] Ξεκινάει το Network Request...");

            if (response?.Routes == null || response.Routes.Count == 0) return (new List<Location>(), 0);

            stepSw.Restart();

            var path = await Task.Run(() => DecodePolyline(response.Routes[0].Geometry));

            System.Diagnostics.Debug.WriteLine($"=== [OSRM] Χρόνος Αποκωδικοποίησης (Decode Time): {stepSw.ElapsedMilliseconds} ms ===");

            double distanceKm = response.Routes[0].Distance / 1000.0;

            System.Console.WriteLine($"!!! [OSRM] ΣΥΝΟΛΙΚΟΣ ΧΡΟΝΟΣ: {totalSw.ElapsedMilliseconds} ms");
            System.Console.WriteLine("*********************************************************");

            return (path, distanceKm);
        }
        catch { return (new List<Location>(), 0); }
    }

    private List<Location> DecodePolyline(string encodedPoints)
    {
        if (string.IsNullOrEmpty(encodedPoints)) return new List<Location>();
        var poly = new List<Location>();
        int index = 0, lat = 0, lng = 0;
        while (index < encodedPoints.Length)
        {
            int b, shift = 0, result = 0;
            do { b = encodedPoints[index++] - 63; result |= (b & 0x1f) << shift; shift += 5; } while (b >= 0x20);
            int dlat = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1)); lat += dlat;
            shift = 0; result = 0;
            do { b = encodedPoints[index++] - 63; result |= (b & 0x1f) << shift; shift += 5; } while (b >= 0x20);
            int dlng = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1)); lng += dlng;
            poly.Add(new Location(lat * 1e-5, lng * 1e-5));
        }
        return poly;
    }
}

public class OsrmResponse { public List<OsrmRoute> Routes { get; set; } }
public class OsrmRoute { public string Geometry { get; set; } public double Distance { get; set; } }