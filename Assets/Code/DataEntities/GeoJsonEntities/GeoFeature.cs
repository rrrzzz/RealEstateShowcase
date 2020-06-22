using System.Collections.Generic;

namespace Code.DataEntities.GeoJsonEntities
{
    public readonly struct GeoFeature
    {
        public string Name { get; }
        public List<GeoPolygon> GeoPolygons { get;}
        
        public GeoFeature(string name, List<GeoPolygon> geoPolygons)
        {
            Name = name;
            GeoPolygons = geoPolygons;
        }
    }
}