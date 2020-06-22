using System.Collections.Generic;
using Code.Models.MapModels;
using Code.Utility;
using UnityEngine;

namespace Code.Views.MapViews
{
    public class MapViewGenerator : IMapViewGenerator
    {
        private const string MapRootName = "MapRoot";
        
        private readonly IMeshGenerator _meshGenerator;
        private readonly IOutlineDrawer _outlineDrawer;
        private readonly MonoBehaviourProvider _monoBehaviourProvider;

        public MapViewGenerator(IMeshGenerator meshGenerator, IOutlineDrawer outlineDrawer,
            MonoBehaviourProvider monoBehaviourProvider)
        {
            _meshGenerator = meshGenerator;
            _outlineDrawer = outlineDrawer;
            _monoBehaviourProvider = monoBehaviourProvider;
        }

        public IMapView CreateMapView(GameObject mapZonePrefab, IMapModel mapModel)
        {
            var holeGameObject = new GameObject();
            var zoneViews = new List<IMapZoneView>();
            var mapRoot = CreateAndRotateMapRoot(mapModel.MapInfo);
            
            foreach (var zoneModel in mapModel.MapZones)
            {
                var zoneRepresentation = _monoBehaviourProvider.CreateInstance(mapZonePrefab, mapRoot);
                zoneRepresentation.name = zoneModel.Name;

                DrawZoneOutlines(zoneRepresentation, zoneModel, holeGameObject);
                
                var mesh = _meshGenerator.CreateZoneMesh(zoneModel);
                var meshFilter = zoneRepresentation.GetComponent<MeshFilter>();
                meshFilter.mesh = mesh;
                var meshCollider = zoneRepresentation.GetComponent<MeshCollider>();
                meshCollider.sharedMesh = mesh;
                zoneViews.Add(new MapZoneView(zoneModel.Id, zoneRepresentation));
            }

            return new MapView(zoneViews, mapRoot);
        }

        private Transform CreateAndRotateMapRoot(MapInfo info)
        {
            var transform = new GameObject(MapRootName).transform;

            if(!IsMapIn2dSpace(info))
            {
                var rotationAxis = Vector3.Cross(info.Normal, info.Forward);
                transform.rotation = Quaternion.Euler(rotationAxis * 90);
            } 

            return transform;
        }
        
        private bool IsMapIn2dSpace(MapInfo info) => 
            info.Normal == Vector3.forward || info.Normal == Vector3.back;

        private void DrawZoneOutlines(GameObject zoneRepresentation, IMapZoneModel zoneModel, GameObject holeGameObject)
        {
            _outlineDrawer.DrawOutline(zoneRepresentation, zoneModel.OuterBounds);

            foreach (var hole in zoneModel.Holes)
            {
                var holeInstance = _monoBehaviourProvider.CreateInstance(holeGameObject, zoneRepresentation.transform);
                holeInstance.name = zoneModel.Name + " hole";
                _outlineDrawer.DrawOutline(holeInstance, hole);
            }
        }
    }
}