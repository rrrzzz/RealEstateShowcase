using Code.Controllers;
using Code.Utility;
using Code.Utility.DataProviders;
using UnityEngine;

namespace Code
{
    public class ApplicationManager : MonoBehaviour
    {
        [SerializeField] private Vector3 mapNormal = Vector3.up;
        [SerializeField] private Vector3 mapForwardDirection = Vector3.forward;

        private IProjectsManager _manager;
        private IUiController _uiController;
        
        private void Start()
        {
            CreateApplicationEntities();
            SetUpViewEvents();
        }
        
        private void CreateApplicationEntities()
        {
            IAssetProvider assetProvider = new AssetProvider(new JsonParser());
            IEntitiesCreator creator = new EntitiesCreator(gameObject, assetProvider);
            ICoordinatesConverter coordsConverter = new CoordinatesConverter();
            IGeoJsonParser geoParser = new GeoJsonParser();
            IMeshGenerator meshGenerator = new MeshGenerator();
            IOutlineDrawer outlineDrawer = new OutlineDrawer();
            
            var mapModel = creator.CreateMapModel(mapNormal, mapForwardDirection, coordsConverter, geoParser);
            creator.CreateMapView(mapModel, meshGenerator, outlineDrawer);

            IMapScanner mapScanner = new MapScanner(mapModel.MapInfo);
            var container =
                creator.CreateProjectModelContainer(mapModel, coordsConverter);
            var controllerFactory = creator.CreateProjectControllerFactory(mapScanner, container);
            
            _manager = controllerFactory.ProjectsManager;
            
            _uiController = creator.CreateUiController();
        }
        
        private void SetUpViewEvents()
        {
            _uiController.AddCostToggleClickListener(_manager.ResizeAllProjectsByCost);
            _uiController.AddFlatsToggleClickListener(_manager.ResizeAllProjectsByFlatCount);
        }
    }
}