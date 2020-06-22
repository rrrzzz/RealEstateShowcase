using UnityEngine;

namespace Code.Models.MapModels
{
    public readonly struct MapInfo
    {
        public MapBounds MapBounds { get; }
        public Vector2 OriginOffset { get; }
        public Vector3 Normal { get; }
        public Vector3 Forward { get; }

        public MapInfo(MapBounds mapBounds, Vector2 originOffset, Vector3 normal, Vector3 forward)
        {
            MapBounds = mapBounds;
            OriginOffset = originOffset;
            Normal = normal;
            Forward = forward;
        }
    }
}