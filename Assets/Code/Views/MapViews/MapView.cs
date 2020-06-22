using System.Collections.Generic;
using UnityEngine;

namespace Code.Views.MapViews
{
    public readonly struct MapView : IMapView
    {
        public Transform MapRoot { get; }
        public List<IMapZoneView> MapZoneViews { get; }

        public MapView(List<IMapZoneView> zoneViews, Transform mapRoot)
        {
            MapZoneViews = zoneViews;
            MapRoot = mapRoot;
        }
    }
}