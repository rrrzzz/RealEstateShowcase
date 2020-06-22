using System.Collections.Generic;

namespace Code.Models.MapModels
{
    public readonly struct MapModel : IMapModel
    {
        public MapInfo MapInfo { get; }
        public List<IMapZoneModel> MapZones { get; }
        public MapModel(MapInfo mapInfo, List<IMapZoneModel> mapZones)
        {
            MapInfo = mapInfo;
            MapZones = mapZones;
        }
    }
}