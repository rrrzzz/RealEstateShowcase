using Code.DataEntities.GeoJsonEntities;
using UnityEngine;

namespace Code.Utility
{
    public interface ICoordinatesConverter
    {
        Vector2 ConvertToWorldPosition2d(GeoCoordinate coordinates, Vector2 originOffset);
        Vector2 ConvertToWorldPosition2d(float[] coordinate, Vector2 originOffset);
        Vector2 GetOriginOffset(GeoCoordinate point);
    }
}