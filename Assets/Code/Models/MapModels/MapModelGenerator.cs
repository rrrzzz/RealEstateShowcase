using System.Collections.Generic;
using System.Linq;
using Code.DataEntities.GeoJsonEntities;
using Code.Utility;
using UnityEngine;

namespace Code.Models.MapModels
{
    public class MapModelGenerator : IMapModelGenerator
    {
        private Vector2 _maxPoint = new Vector2(float.MinValue, float.MinValue);
        private Vector2 _minPoint = new Vector2(float.MaxValue, float.MaxValue);
        private Vector2 _originOffset;
        private readonly ICoordinatesConverter _coordinatesConverter;

        public MapModelGenerator(ICoordinatesConverter coordinatesConverter)
        {
            _coordinatesConverter = coordinatesConverter;
        }

        public IMapModel InitializeMapModel(List<GeoFeature> features, Vector3 mapNormal, Vector3 mapForwardDirection)
        { 
            var mapZones = GetMapZones(features);
            
            var mapBounds = new MapBounds(_maxPoint, _minPoint);
            var mapInfo = new MapInfo(mapBounds, _originOffset, mapNormal, mapForwardDirection);
            var mapModel = new MapModel(mapInfo, mapZones);
            return mapModel;
        }

        private List<IMapZoneModel> GetMapZones(List<GeoFeature> features)
        {
            SetFirstPointAsOrigin(features);
            var zones = new List<IMapZoneModel>();
            var id = 0;
            
            foreach (var feature in features)
            {
                var zoneName = feature.Name;

                foreach (var polygon in feature.GeoPolygons)
                {
                    var zone = CreateMapZone(id++, zoneName, polygon);
                    zones.Add(zone);
                }
            }
            return zones;
        }

        private IMapZoneModel CreateMapZone(int id, string zoneName, GeoPolygon polygon)
        {
            var holes = new List<List<Vector2>>();

            var bounds = GetWorldPoints2d(polygon.OuterBounds);

            foreach (var hole in polygon.Holes)
            {
                holes.Add(GetWorldPoints2d(hole));
            }
            
            return new MapZoneModel(zoneName, id, bounds, holes);
        }

        private void SetFirstPointAsOrigin(List<GeoFeature> features)
        {
            var firstPoint = features.First()
                                     .GeoPolygons
                                     .First()
                                     .OuterBounds.First();

            _originOffset = _coordinatesConverter.GetOriginOffset(firstPoint);
        }

        private List<Vector2> GetWorldPoints2d(List<GeoCoordinate> coordinates)
        {
            var worldPoints = new List<Vector2>();
            foreach (var coordinate in coordinates)
            {
                var worldPoint = _coordinatesConverter.ConvertToWorldPosition2d(coordinate, _originOffset);
                UpdateMinMaxMapPoints(worldPoint);
                worldPoints.Add(worldPoint);
            }

            return worldPoints;
        }
        
        private void UpdateMinMaxMapPoints(Vector2 point)
        {
            if (point.x > _maxPoint.x) _maxPoint.x = point.x;
            if (point.y > _maxPoint.y) _maxPoint.y = point.y;
         
            if (point.x < _minPoint.x) _minPoint.x = point.x;
            if (point.y < _minPoint.y) _minPoint.y = point.y;
        }
    }
}