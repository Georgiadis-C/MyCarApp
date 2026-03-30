using System;
using System.Collections.Generic;
using System.Text;

namespace MyCarApp.Interfaces
{
    public interface IRoutingService
    {
        /// <summary>
        /// Λαμβάνει τη διαδρομή μεταξύ δύο γεωγραφικών σημείων.
        /// </summary>
        /// <param name="start">Σημείο αφετηρίας</param>
        /// <param name="end">Σημείο προορισμού</param>
        /// <returns>Ένα Tuple που περιέχει τη λίστα των τοποθεσιών και την απόσταση σε km</returns>
        Task<(List<Location> Path, double Distance)> GetRouteAsync(Location start, Location end);
    }
}
