using System;
using Code.DataEntities.GeoJsonEntities;
using UnityEngine;

namespace Code.Utility
{
    public class CoordinatesConverter : ICoordinatesConverter
    {
        private const int EarthRadius = 6378137;
        private const double EarthOriginShift = 2 * Math.PI * EarthRadius / 2;
        private const int ScalingDownFactor = 1000;

        public Vector2 ConvertToWorldPosition2d(GeoCoordinate coordinate, Vector2 originOffset)
        {
            var pointInMeters = LatLonToMeters(coordinate.Latitude, coordinate.Longitude);
            var scaledPoint = ScaleCoordinatesDown(pointInMeters);
            return OffsetPoint(scaledPoint, originOffset);
        }
        
        public Vector2 ConvertToWorldPosition2d(float[] coordinate, Vector2 originOffset)
        {
            var geoCoordinate = ConvertToGeoCoordinate(coordinate);
            return ConvertToWorldPosition2d(geoCoordinate, originOffset);
        }
        
        public Vector2 GetOriginOffset(GeoCoordinate point)
        {
            var origin = LatLonToMeters(point.Latitude, point.Longitude);
            return ScaleCoordinatesDown(origin);
        }

        private GeoCoordinate ConvertToGeoCoordinate(float[] coordinate) => new GeoCoordinate(coordinate[1], coordinate[0]); 
        
        private Vector2 LatLonToMeters(double lat, double lon)
        {
            var point = new Vector2
            {
                x = (float) (lon * EarthOriginShift / 180),
                y = (float) (Math.Log(Math.Tan((90 + lat) * Math.PI / 360)) / (Math.PI / 180))
            };
            
            point.y = (float)(point.y * EarthOriginShift / 180);
            return point;
        }

        private Vector2 ScaleCoordinatesDown(Vector2 point) => 
            new Vector2(point.x / ScalingDownFactor, point.y / ScalingDownFactor);
        
        private Vector2 OffsetPoint(Vector2 point, Vector2 originOffset) => 
            new Vector2(point.x - originOffset.x, point.y - originOffset.y);
    }
}