using UnityEngine;

namespace Code.Views.MapViews
{
    public interface IMapZoneView
    {
        int Id { get; }
        GameObject ZoneObject { get; }
    }
}