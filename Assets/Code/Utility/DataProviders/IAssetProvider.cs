using Code.DataEntities;
using UnityEngine;

namespace Code.Utility.DataProviders
{
    public interface IAssetProvider
    {
        string GetGeoJsonData();
        ProjectsData GetProjectsData();
        GameObject GetPremiumPrefab();
        GameObject GetComfortPrefab();
        GameObject GetEconomyPrefab();
        GameObject GetMapZonePrefab();
        GameObject GetCanvasPrefab();
        GameObject GetCostTogglePrefab();
        GameObject GetFlatCountTogglePrefab();
        Material GetOutlineMaterial();
    }
}