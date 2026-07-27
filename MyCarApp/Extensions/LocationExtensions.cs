using System.Collections.Generic;
using System.Linq;
using Mapsui;
using Mapsui.Layers;
using Mapsui.Nts;
using Mapsui.Nts.Extensions;
using Mapsui.Styles;
using Mapsui.UI.Maui;
using Microsoft.Maui.Devices.Sensors;
using NetTopologySuite.Geometries;
using Brush = Mapsui.Styles.Brush;
using Color = Mapsui.Styles.Color;
using Location = Microsoft.Maui.Devices.Sensors.Location;
using Position = Mapsui.UI.Maui.Position;

namespace MyCarApp.Extensions;

public static class LocationExtensions
{
    // Converts Mapsui coordinates
    public static Location ToMauiLocation(this Mapsui.MPoint worldPosition)
    {
        var (lon, lat) = Mapsui.Projections.SphericalMercator.ToLonLat(worldPosition.X, worldPosition.Y);
        return new Location(lat, lon);
    }

    // Converts Position object
    public static Location ToMauiLocation(this Position position)
    {
        return new Location(position.Latitude, position.Longitude);
    }

    // Converts Location
    public static PointFeature ToPointFeature(this Location location, Color color)
    {
        var mapsuiPoint = Mapsui.Projections.SphericalMercator.FromLonLat(location.Longitude, location.Latitude);
        var feature = new PointFeature(mapsuiPoint);

        feature.Styles.Add(new SymbolStyle
        {
            SymbolType = SymbolType.Ellipse,
            SymbolScale = 0.5,
            Fill = new Brush(color),
            Outline = new Pen(Color.Black, 2)
        });

        return feature;
    }

    // Converts a collection of Locations
    public static GeometryFeature ToLineFeature(this IEnumerable<Location> locations, Color color, double thickness = 3)
    {
        var points = locations.Select(loc =>
            Mapsui.Projections.SphericalMercator.FromLonLat(loc.Longitude, loc.Latitude).ToCoordinate()
        ).ToArray();

        var lineString = new LineString(points);
        var feature = new GeometryFeature(lineString);

        feature.Styles.Add(new VectorStyle
        {
            Line = new Pen
            {
                Color = color,
                Width = thickness,
                PenStyle = PenStyle.Solid,
                PenStrokeCap = PenStrokeCap.Round,
                StrokeJoin = StrokeJoin.Round
            }
        });

        return feature;
    }

    // Applies custom restrictions and lock behaviors to the map viewport
    public static void ApplySmartLock(this Mapsui.Map map)
    {
        double maxResolution = 0;
        map.BackColor = Mapsui.Styles.Color.FromString("#AADAFF");

        bool isUpdating = false;

        map.Navigator.RotationLock = true;

        void Init()
        {
            if (map.Extent == null || map.Navigator.Viewport.Width <= 0)
                return;

            map.Navigator.CenterOn(new Mapsui.MPoint(0, 0));
            var resolutionWidth = map.Extent.Width / map.Navigator.Viewport.Width;
            var resolutionHeight = map.Extent.Height / map.Navigator.Viewport.Height;
            maxResolution = Math.Max(resolutionWidth, resolutionHeight);
            map.Navigator.ZoomTo(maxResolution);
        }

        map.Navigator.ViewportChanged += (s, e) =>
        {
            
            if (!isUpdating) return;

            if (map.Navigator.Viewport.Rotation != 0)
            {
                isUpdating = true;
                map.Navigator.RotateTo(0);
                isUpdating = false;
            }

            if (maxResolution == 0 && map.Extent != null && map.Navigator.Viewport.Width > 0)
            {
                Init();
                return;
            }

            if (maxResolution <= 0) return;

            if (map.Navigator.Viewport.Resolution >= maxResolution - 0.5)
            {
                if (map.Navigator.Viewport.Resolution > maxResolution)
                    map.Navigator.ZoomTo(maxResolution);

                if (map.Navigator.Viewport.CenterX != 0 || map.Navigator.Viewport.CenterY != 0)
                    map.Navigator.CenterOn(new Mapsui.MPoint(0, 0));

                map.Navigator.PanLock = true;
            }
            else
            {
                map.Navigator.PanLock = false;
            }
        };
    }

}