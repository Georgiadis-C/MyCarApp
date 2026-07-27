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

    // Attaches the info event handler to the map control
    protected override void OnAttachedTo(MapControl bindable)
    {
        base.OnAttachedTo(bindable);
        bindable.Info += OnInfo;
    }

    // Detaches the info event handler from the map control
    protected override void OnDetachingFrom(MapControl bindable)
    {
        bindable.Info -= OnInfo;
        base.OnDetachingFrom(bindable);
    }

    // Handles map click info events and triggers the command with location data
    private void OnInfo(object sender, Mapsui.MapInfoEventArgs e)
    {
        if (e.WorldPosition == null) return;

        var (lon, lat) = Mapsui.Projections.SphericalMercator.ToLonLat(e.WorldPosition.X, e.WorldPosition.Y);
        var location = new Microsoft.Maui.Devices.Sensors.Location(lat, lon);

        if (MapInfoCommand != null && MapInfoCommand.CanExecute(location))
        {
            MapInfoCommand.Execute(location);
        }
    }
}