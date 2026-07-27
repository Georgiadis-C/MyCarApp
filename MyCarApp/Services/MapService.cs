using Mapsui;
using Mapsui.Layers;
using Mapsui.Styles;
using MyCarApp.Extensions;
using MyCarApp.Interfaces;
using Color = Mapsui.Styles.Color;


namespace MyCarApp.Services
{
    public class MapService : IMapService
    {
        private readonly Mapsui.Map _map;
        private readonly WritableLayer _pinLayer;
        private readonly WritableLayer _lineLayer;

        public MapService()
        {
            _map = new Mapsui.Map();
            var osmLayer = Mapsui.Tiling.OpenStreetMap.CreateTileLayer();
            _map.Layers.Add(osmLayer);
            _map.ApplySmartLock();
            _lineLayer = new WritableLayer { Name = "Lines" };
            _map.Layers.Add(_lineLayer);
            _pinLayer = new WritableLayer { Name = "Pins" };
            _map.Layers.Add(_pinLayer);
            _map.Info += (s, e) => {};
            foreach (var widget in _map.Widgets) widget.Enabled = false;
        }

        public Mapsui.Map GetMap() => _map;

        public void AddPin(Mapsui.MPoint location, string label)
        {
            var feature = new Mapsui.Layers.PointFeature(location);

            var pinStyle = new Mapsui.Styles.SymbolStyle
            {
                SymbolScale = 0.6,
                Fill = new Mapsui.Styles.Brush(Mapsui.Styles.Color.Red),
                Outline = new Mapsui.Styles.Pen(Mapsui.Styles.Color.White, 2)
            };

            var textStyle = new Mapsui.Styles.LabelStyle
            {
                Text = label,
                Offset = new Mapsui.Styles.Offset(0, 25),
                VerticalAlignment = Mapsui.Styles.LabelStyle.VerticalAlignmentEnum.Bottom,
                ForeColor = Mapsui.Styles.Color.Black,
                Font = new Mapsui.Styles.Font { Size = 14, Bold = true },
                Halo = new Mapsui.Styles.Pen(Mapsui.Styles.Color.White, 2)
            };

            feature.Styles.Add(pinStyle);
            feature.Styles.Add(textStyle);

            _pinLayer.Add(feature);
            _pinLayer.DataHasChanged();
            _map.RefreshGraphics();
        }

        public void AddLine(IEnumerable<Location> locations)
        {
            var lineFeature = locations.ToLineFeature(Color.Black, 3);
            _lineLayer.Add(lineFeature);
            _lineLayer.DataHasChanged();
            _map.RefreshGraphics();
        }

        public void ClearMap()
        {
            _pinLayer.Clear();
            _lineLayer.Clear();
            _pinLayer.DataHasChanged();
            _lineLayer.DataHasChanged();
            _map.RefreshGraphics();
        }
    }
}