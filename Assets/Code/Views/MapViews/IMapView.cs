using System.Collections.Generic;
using UnityEngine;

namespace Code.Views.MapViews
{
    public interface IMapView
    {
        Transform MapRoot { get; }
        List<IMapZoneView> MapZoneViews { get; }
    }
}