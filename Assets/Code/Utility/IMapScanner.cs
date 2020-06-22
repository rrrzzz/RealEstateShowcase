using UnityEngine;

namespace Code.Utility
{
    public interface IMapScanner
    {
        bool CheckPositionOnMap(Vector3 mapPosition);
        bool CheckPositionObstructed(Vector3 mapPosition, Vector3 objectSize);
    }
}