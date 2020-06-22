using System.Collections.Generic;
using UnityEngine;

namespace Code.Models.MapModels
{
    public readonly struct MapZoneModel : IMapZoneModel
    {
        public string Name { get; }
        public int Id { get; }
        public List<Vector2> OuterBounds { get; }
        public List<List<Vector2>> Holes { get; }

        public MapZoneModel(string name, int id, List<Vector2> outerBounds, List<List<Vector2>> holes)
        {
            Name = name;
            Id = id;
            OuterBounds = outerBounds;
            Holes = holes;
        }
    }
}