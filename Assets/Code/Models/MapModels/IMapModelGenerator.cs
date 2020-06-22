using System.Collections.Generic;
using Code.DataEntities.GeoJsonEntities;
using UnityEngine;

namespace Code.Models.MapModels
{
    public interface IMapModelGenerator
    {
        IMapModel InitializeMapModel(List<GeoFeature> features, Vector3 mapNormal, Vector3 mapForwardDirection);
    }
}