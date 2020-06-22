using System;
using System.Collections.Generic;
using System.Linq;
using Code.DataEntities.GeoJsonEntities;
using GeoJSON.Net;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;

namespace Code.Utility.DataProviders
{
    public class GeoJsonParser : IGeoJsonParser
    {
        private const string FeatureNameProperty = @"ABBREV";
        
        public List<GeoFeature> GetGeoJsonFeatures(string geoJsonText)
        {
            var collection =  JsonConvert.DeserializeObject<FeatureCollection>(geoJsonText);
            
            if (collection == null)
                throw new NullReferenceException($"There is no feature collection in file {geoJsonText}");
            
            return ParseFeatureCollection(collection);
        }
        
        private List<GeoFeature> ParseFeatureCollection(FeatureCollection collection)
        {
            var geoFeatures = new List<GeoFeature>();
            
            foreach (var feature in collection.Features)
            {
                var featureName = feature.Properties[FeatureNameProperty] as string;
                if (featureName == null) 
                    throw new NullReferenceException($"There is no property {FeatureNameProperty} in feature {feature}");

                var geoFeature = ParseFeature(featureName, feature);
                geoFeatures.Add(geoFeature);
            }
            
            return geoFeatures;
        }

        private GeoFeature ParseFeature(string featureName, Feature feature)
        {
            var geoPolygons = new List<GeoPolygon>();
            GeoPolygon geoPolygon;
            
            var geometry = feature.Geometry;

            if (geometry.Type == GeoJSONObjectType.MultiPolygon)
            {
                var multiPolygon = geometry as MultiPolygon;
                var polygons = multiPolygon.Coordinates;
                foreach (var polygon in polygons)
                {
                    geoPolygon = ParsePolygon(polygon);
                    geoPolygons.Add(geoPolygon);
                }
            }
            else if (geometry.Type == GeoJSONObjectType.Polygon)
            {
                var polygon = geometry as Polygon;
                geoPolygon = ParsePolygon(polygon);
                geoPolygons.Add(geoPolygon);
            }
            return new GeoFeature(featureName, geoPolygons);
        }

        private GeoPolygon ParsePolygon(Polygon polygon)
        {
            var bounds = new List<GeoCoordinate>();
            var holes = new List<List<GeoCoordinate>>();
        
            var isOuterBounds = true; 
            
            foreach (var lineString in polygon.Coordinates)
            {
                var geoCoordinates = lineString.Coordinates
                    .Select(position => new GeoCoordinate(position.Latitude, position.Longitude)).ToList();
                
                if (isOuterBounds)
                {
                    isOuterBounds = false;
                    bounds = geoCoordinates;
                    continue;
                }
                holes.Add(geoCoordinates);
            }
            
            return new GeoPolygon(bounds, holes);
        }
    }
}