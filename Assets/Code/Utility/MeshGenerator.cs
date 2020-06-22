using Code.Libraries.TriangleNetUnity;
using Code.Models.MapModels;
using TriangleNet;
using TriangleNet.Geometry;
using UnityEngine;

namespace Code.Utility
{
    public class MeshGenerator : IMeshGenerator
    {
        public Mesh CreateZoneMesh(IMapZoneModel zone)
        {
            var poly = new Polygon();
            poly.Add(zone.OuterBounds);

            foreach (var hole in zone.Holes) poly.Add(hole, true);

            var triangleNetMesh = (TriangleNetMesh) poly.Triangulate();
            var mesh = triangleNetMesh.GenerateUnityMesh();
            return mesh;
        }
    }
}