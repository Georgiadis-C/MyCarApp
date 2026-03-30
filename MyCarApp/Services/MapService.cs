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
        private readonly WritableLayer _lineLayer; // 1. Το στρώμα για τη γραμμή

        public MapService()
        {
            _map = new Mapsui.Map();

            // Δημιουργούμε το βασικό Layer
            var osmLayer = Mapsui.Tiling.OpenStreetMap.CreateTileLayer();

            // ΔΙΟΡΘΩΣΗ: Προσθήκη του layer στη λίστα InfoLayers του χάρτη
            _map.Layers.Add(osmLayer);

            _map.ApplySmartLock();

            _lineLayer = new WritableLayer { Name = "Lines" };
            _map.Layers.Add(_lineLayer);

            _pinLayer = new WritableLayer { Name = "Pins" };
            _map.Layers.Add(_pinLayer);

            // Ενεργοποίηση των Info events για τον χάρτη συνολικά
            _map.Info += (s, e) => {
                // Αυτό το event είναι απαραίτητο για να "ακούει" ο χάρτης
            };

            foreach (var widget in _map.Widgets) widget.Enabled = false;
        }

        public Mapsui.Map GetMap() => _map;

        public void AddPin(Mapsui.MPoint location, string label)
        {
            // Δημιουργία του σημείου (Feature)
            var feature = new Mapsui.Layers.PointFeature(location);

            // 1. Στυλ για την κόκκινη πινέζα
            var pinStyle = new Mapsui.Styles.SymbolStyle
            {
                SymbolScale = 0.6,
                Fill = new Mapsui.Styles.Brush(Mapsui.Styles.Color.Red),
                Outline = new Mapsui.Styles.Pen(Mapsui.Styles.Color.White, 2)
            };

            // 2. Στυλ για το κείμενο (Label) πάνω από την πινέζα
            var textStyle = new Mapsui.Styles.LabelStyle
            {
                Text = label,
                Offset = new Mapsui.Styles.Offset(0, 25),
                VerticalAlignment = Mapsui.Styles.LabelStyle.VerticalAlignmentEnum.Bottom,
                ForeColor = Mapsui.Styles.Color.Black,
                Font = new Mapsui.Styles.Font { Size = 14, Bold = true },
                // Το Halo είναι το περίγραμμα γύρω από τα γράμματα για να φαίνονται καθαρά
                Halo = new Mapsui.Styles.Pen(Mapsui.Styles.Color.White, 2)
            };

            // Προσθέτουμε τα στυλ στο feature
            feature.Styles.Add(pinStyle);
            feature.Styles.Add(textStyle);

            // Προσθήκη στο Layer
            _pinLayer.Add(feature);

            // ΕΝΗΜΕΡΩΣΗ ΧΑΡΤΗ:
            // Λέμε στο Layer ότι τα δεδομένα του άλλαξαν
            _pinLayer.DataHasChanged();

            // Ζητάμε από τον χάρτη να ξαναζωγραφίσει τα γραφικά
            _map.RefreshGraphics();
        }

        public void AddLine(IEnumerable<Location> locations)
        {
            // Χρησιμοποιεί το extension ToLineFeature
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