using System.Collections.Generic;
using UnityEngine;

namespace Code.Models.MapModels
{
    public interface IMapZoneModel
    {
        string Name { get; }
        int Id { get; }
        List<Vector2> OuterBounds { get; }
        List<List<Vector2>> Holes { get; }
    }
}