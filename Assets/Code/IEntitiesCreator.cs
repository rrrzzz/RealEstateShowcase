using Code.Controllers;
using Code.Models.MapModels;
using Code.Models.ProjectModels;
using Code.Utility;
using Code.Utility.DataProviders;
using Code.Views.MapViews;
using UnityEngine;

namespace Code
{
    public interface IEntitiesCreator
    {
        IMapModel CreateMapModel(Vector3 mapNormal, Vector3 mapForwardDirection,
            ICoordinatesConverter coordinatesConverter, IGeoJsonParser geoJsonParser);
        IMapView CreateMapView(IMapModel mapModel, IMeshGenerator meshGenerator, IOutlineDrawer outlineDrawer);
        IProjectModelsContainer CreateProjectModelContainer(IMapModel mapModel, ICoordinatesConverter coordinatesConverter);
        IProjectControllerFactory CreateProjectControllerFactory(IMapScanner mapScanner, IProjectModelsContainer container);
        IUiController CreateUiController();
    }
}