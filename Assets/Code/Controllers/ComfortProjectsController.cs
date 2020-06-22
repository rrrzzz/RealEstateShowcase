using System.Collections.Generic;
using Code.Models.ProjectModels;
using Code.Utility.DataProviders;
using Code.Views.ProjectViews;

namespace Code.Controllers
{
    public class ComfortProjectsController : ProjectsControllerBase
    {
        public ComfortProjectsController(ProjectsInfo projectsInfo, IAssetProvider assetProvider, List<IProjectModel> models, 
            IProjectViewManipulator manipulator) : base(projectsInfo, assetProvider, models, manipulator) {}
            
        protected override void SetProjectPrefab()
        {
            ProjectPrefab = AssetProvider.GetComfortPrefab();
        }
    }
}