using System.Collections.Generic;

namespace Code.Models.MapModels
{
    public interface IMapModel
    {
        MapInfo MapInfo { get; }
        List<IMapZoneModel> MapZones { get; }
    }
}