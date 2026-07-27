using System;
using System.Collections.Generic;
using System.Text;

namespace MyCarApp.Interfaces
{
    public interface IRoutingService
    {
        Task<(List<Location> Path, double Distance)> GetRouteAsync(Location start, Location end);
    }
}
