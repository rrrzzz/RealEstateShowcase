using System.Collections.Generic;

namespace Code.DataEntities.GeoJsonEntities
{
    public readonly struct GeoPolygon
    {
        public List<GeoCoordinate> OuterBounds { get;}
        public List<List<GeoCoordinate>> Holes { get;}

        public GeoPolygon(List<GeoCoordinate> coordinates, List<List<GeoCoordinate>> holes)
        {
            OuterBounds = coordinates;
            Holes = holes;
        }
    }
}