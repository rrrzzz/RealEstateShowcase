using System;
using System.IO;
using Code.DataEntities;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Utility.DataProviders
{
    public class AssetProvider : IAssetProvider
    {
        private const string DataFolder = @"Data";
        private const string PrefabsFolder = @"Prefabs";
        private const string MapData = @"Administrative.geojson";
        private const string ProjectsData = @"ApartmentProjects";
        private const string PremiumProjectPrefab = "PremiumModel";
        private const string ComfortProjectPrefab = "ComfortModel";
        private const string EconomyProjectPrefab = "EconomyModel";
        private const string CanvasPrefab = "Canvas";
        private const string CostTogglePrefab = "CostToggle";
        private const string FlatCountTogglePrefab = "FlatsToggle";
        private const string MapZonePrefab = "MapZone";
        private const string MaterialsFolder = "Materials";
        private const string OutlineMaterial = "OutlineMaterial";
        
        private readonly Type[] _requiredMapComponents = { typeof(MeshCollider), typeof(MeshFilter) };
        private readonly Type[] _requiredProjectComponents = { typeof(BoxCollider) };
        private readonly Type[] _requiredToggleComponents = { typeof(Button) };

        private readonly JsonParser _jsonParser;

        public AssetProvider(JsonParser jsonParser) {
            _jsonParser = jsonParser;
        }

        public string GetGeoJsonData() => 
            _jsonParser.GetJsonText(Path.Combine(DataFolder, MapData));

        public ProjectsData GetProjectsData() =>
            _jsonParser.GetJsonDataObject<ProjectsData>(Path.Combine(DataFolder, ProjectsData));

        public GameObject GetPremiumPrefab() => GetPrefabByName(PremiumProjectPrefab, _requiredProjectComponents);
        public GameObject GetComfortPrefab() => GetPrefabByName(ComfortProjectPrefab, _requiredProjectComponents);
        public GameObject GetEconomyPrefab() => GetPrefabByName(EconomyProjectPrefab, _requiredProjectComponents);
        public GameObject GetMapZonePrefab() => GetPrefabByName(MapZonePrefab, _requiredMapComponents);
        public GameObject GetCanvasPrefab() => GetPrefabByName(CanvasPrefab);
        public GameObject GetCostTogglePrefab() => GetPrefabByName(CostTogglePrefab, _requiredToggleComponents);
        public GameObject GetFlatCountTogglePrefab() => GetPrefabByName(FlatCountTogglePrefab, _requiredToggleComponents);
        
        public Material GetOutlineMaterial()
        {
            var path = Path.Combine(MaterialsFolder, OutlineMaterial);
            var material = Resources.Load<Material>(path);
            CheckAssetNull(material, path);
            return material;
        }
        
        private GameObject GetPrefabByName(string prefabName, params Type[] types)
        {
            var path = Path.Combine(PrefabsFolder, prefabName);
            var prefab = Resources.Load<GameObject>(path);
            CheckAssetNull(prefab, path);
            CheckPrefabHasComponents(prefab, types);
            return prefab;
        }

        private void CheckPrefabHasComponents(GameObject prefab, params Type[] types)
        {
            foreach (var type in types)
            {
                if (prefab.GetComponent(type) == null)
                    throw new NullReferenceException($"Prefab {prefab.name}" +
                                                     $" must have {type.FullName} component");
            }
        }
        
        private void CheckAssetNull(object asset, string path)
        {
            if (asset == null) 
                throw new NullReferenceException($"There is no asset at {path}");
        }
    }
}