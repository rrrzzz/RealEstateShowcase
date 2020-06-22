using System.Collections.Generic;
using Code.DataEntities.GeoJsonEntities;

namespace Code.Utility.DataProviders
{
    public interface IGeoJsonParser
    {
        List<GeoFeature> GetGeoJsonFeatures(string geoJsonText);
    }
}