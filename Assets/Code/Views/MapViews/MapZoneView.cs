using UnityEngine;

namespace Code.Views.MapViews
{
    public readonly struct MapZoneView : IMapZoneView
    {
        public int Id { get; }
        public GameObject ZoneObject { get; }
        
        public MapZoneView(int id, GameObject zoneObject)
        {
            Id = id;
            ZoneObject = zoneObject;
        }
    }
}