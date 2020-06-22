using Code.Models.MapModels;
using UnityEngine;

namespace Code.Views.MapViews
{
    public interface IMapViewGenerator
    {
        IMapView CreateMapView(GameObject mapZonePrefab, IMapModel mapModel);
    }
}