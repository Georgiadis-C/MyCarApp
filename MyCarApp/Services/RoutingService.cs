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
            // OSRM API URL
            string url = $"https://routing.openstreetmap.de/routed-car/route/v1/driving/{start.Longitude.ToString(CultureInfo.InvariantCulture)},{start.Latitude.ToString(CultureInfo.InvariantCulture)};{end.Longitude.ToString(CultureInfo.InvariantCulture)},{end.Latitude.ToString(CultureInfo.InvariantCulture)}?overview=full&geometries=polyline";

            var response = await _httpClient.GetFromJsonAsync<OsrmResponse>(url);
            if (response?.Routes == null || response.Routes.Count == 0) return (new List<Location>(), 0);

            // Χρησιμοποιούμε τη δική μας μέθοδο αποκωδικοποίησης παρακάτω
            var path = DecodePolyline(response.Routes[0].Geometry);
            double distanceKm = response.Routes[0].Distance / 1000.0;

            return (path, distanceKm);
        }
        catch { return (new List<Location>(), 0); }
    }

    // Αλγόριθμος της Google για Polyline (δουλεύει παντού χωρίς NuGet)
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