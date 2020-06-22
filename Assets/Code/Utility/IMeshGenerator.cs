using Code.Models.MapModels;
using UnityEngine;

namespace Code.Utility
{
    public interface IMeshGenerator
    {
        Mesh CreateZoneMesh(IMapZoneModel zone);
    }
}