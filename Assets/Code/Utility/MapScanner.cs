using Code.Models.MapModels;
using UnityEngine;

namespace Code.Utility
{
    public class MapScanner : IMapScanner
    {
        private const int RayHeight = 100;
        private const int MaxHits = 10;
        private const float RayDistance = 1000;
        private const string MapZoneTag = "MapZone";
        
        private readonly RaycastHit[] _hits = new RaycastHit[MaxHits];
        private readonly Collider[] _colliderHits = new Collider[MaxHits];
        private readonly MapInfo _mapInfo;

        public MapScanner(MapInfo info)
        {
            _mapInfo = info;
        }
        
        public bool CheckPositionOnMap(Vector3 mapPosition)
        {
            var origin = mapPosition + _mapInfo.Normal * RayHeight;
            var ray = new Ray(origin, -_mapInfo.Normal);
            var hitCount = Physics.RaycastNonAlloc(ray, _hits, RayDistance);

            for (int i = 0; i < hitCount; i++)
            {
                if (_hits[i].transform.CompareTag(MapZoneTag))
                    return true;
            }
            return false;
        }

        public bool CheckPositionObstructed(Vector3 mapPosition, Vector3 objectSize)
        {
            var hitCount = Physics.OverlapBoxNonAlloc(mapPosition, objectSize / 2, _colliderHits);
            
            for (int i = 0; i < hitCount; i++)
            {
                if (!_colliderHits[i].CompareTag(MapZoneTag))
                    return true;
            }
            return false;
        }
    }
}