using UnityEngine;

namespace Code.Models.MapModels
{
    public readonly struct MapBounds
    {
        public Vector2 Center { get; }
        public Vector2 Max { get; }
        public Vector2 Min { get; }

        public MapBounds(Vector2 max, Vector2 min)
        {
            Max = max;
            Min = min;
            Center = (min + max) / 2;
        }
    }
}