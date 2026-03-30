using System;
using System.Collections.Generic;
using System.Text;

namespace MyCarApp.Interfaces
{
    public interface IMapService
    {
        Mapsui.Map GetMap();
        void AddPin(Mapsui.MPoint location, string label);
        void AddLine(IEnumerable<Location> locations); // Πρόσθεσε αυτό
        void ClearMap(); // Αντικατέστησε το ClearPins με ένα γενικό ClearMap
    }

}