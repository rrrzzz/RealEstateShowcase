using System;
using Code.Controllers;
using Code.Models.MapModels;
using Code.Models.ProjectModels;
using Code.Utility;
using Code.Utility.DataProviders;
using Code.Views.MapViews;
using Code.Views.ProjectViews;
using Code.Views.UIViews;
using UnityEngine;

namespace Code
{
    public class EntitiesCreator : IEntitiesCreator
    {
        private readonly IAssetProvider _assetProvider;
        private readonly MonoBehaviourProvider _monoProvider;

        public EntitiesCreator(GameObject appManagerObject, IAssetProvider assetProvider)
        {
            _monoProvider = GetMonoBehaviourProvider(appManagerObject);
            _assetProvider = assetProvider;
        }
        
        public IMapModel CreateMapModel(Vector3 mapNormal, Vector3 mapForwardDirection,
            ICoordinatesConverter coordinatesConverter, IGeoJsonParser geoJsonParser)
        {
            var geoJson = _assetProvider.GetGeoJsonData();
            var geoFeatures = geoJsonParser.GetGeoJsonFeatures(geoJson);
            
            IMapModelGenerator modelGenerator = new MapModelGenerator(coordinatesConverter);
            var mapModel = modelGenerator.InitializeMapModel(geoFeatures, mapNormal, mapForwardDirection);
            
            return mapModel;
        }

        public IMapView CreateMapView(IMapModel mapModel, IMeshGenerator meshGenerator, IOutlineDrawer outlineDrawer)
        {
            var outlineMaterial = _assetProvider.GetOutlineMaterial();
            var mapZonePrefab = _assetProvider.GetMapZonePrefab();

            outlineDrawer.OutlineMaterial = outlineMaterial;
            
            IMapViewGenerator mapViewGenerator = new MapViewGenerator(meshGenerator, outlineDrawer, _monoProvider);
            var mapView = mapViewGenerator.CreateMapView(mapZonePrefab, mapModel);
            
            return mapView;
        }

        public IProjectModelsContainer CreateProjectModelContainer(IMapModel mapModel,
            ICoordinatesConverter coordinatesConverter)
        {
            var projectsJson = _assetProvider.GetProjectsData();
            IProjectsModelGenerator modelGenerator = new ProjectsModelGenerator(coordinatesConverter);
            
            var projectsModelContainer = modelGenerator.ParseProjectsData(projectsJson.Projects, mapModel.MapInfo);
            
            return projectsModelContainer;
        }

        public IProjectControllerFactory CreateProjectControllerFactory(IMapScanner mapScanner,
            IProjectModelsContainer container)
        {
            IProjectViewManipulator manipulator = new ProjectViewManipulator(_monoProvider, mapScanner); 
            var factory = new ProjectControllerFactory(_assetProvider, container, manipulator);

            return factory;
        }

        public IUiController CreateUiController()
        {
            IUiGenerator generator = new UiGenerator(_assetProvider, _monoProvider);
            return new UiController(generator);
        }

        private MonoBehaviourProvider GetMonoBehaviourProvider(GameObject appManagerObject)
        {
            var monoProvider = appManagerObject.GetComponent<MonoBehaviourProvider>();
            if (monoProvider == null)
                throw new NullReferenceException($"MonoBehaviourProvider is not attached to {appManagerObject.name}");

            return monoProvider;
        }
    }
}