using System.Windows.Input;
using Mapsui.UI.Maui;
using MyCarApp.Extensions;
using Microsoft.Maui.Devices.Sensors;

namespace MyCarApp.Behaviors;

public class MapInfoBehavior : Behavior<MapControl>
{
    public static readonly BindableProperty MapInfoCommandProperty =
        BindableProperty.Create(nameof(MapInfoCommand), typeof(ICommand), typeof(MapInfoBehavior));

    public ICommand MapInfoCommand
    {
        get => (ICommand)GetValue(MapInfoCommandProperty);
        set => SetValue(MapInfoCommandProperty, value);
    }

    protected override void OnAttachedTo(MapControl bindable)
    {
        base.OnAttachedTo(bindable);
        // Χρησιμοποιούμε το Info event που είναι το πιο σταθερό
        bindable.Info += OnInfo;
    }

    protected override void OnDetachingFrom(MapControl bindable)
    {
        bindable.Info -= OnInfo;
        base.OnDetachingFrom(bindable);
    }

    private void OnInfo(object sender, Mapsui.MapInfoEventArgs e)
    {
        // Αν το WorldPosition είναι null, το κλικ δεν καταγράφηκε σωστά
        if (e.WorldPosition == null) return;

        // Μετατροπή από SphericalMercator σε Lon/Lat
        var (lon, lat) = Mapsui.Projections.SphericalMercator.ToLonLat(e.WorldPosition.X, e.WorldPosition.Y);

        // Δημιουργία του Location για το MAUI
        var location = new Microsoft.Maui.Devices.Sensors.Location(lat, lon);

        // ΕΛΕΓΧΟΣ: Εδώ στέλνουμε το κλικ στο ViewModel
        if (MapInfoCommand != null && MapInfoCommand.CanExecute(location))
        {
            MapInfoCommand.Execute(location);
        }
    }
}