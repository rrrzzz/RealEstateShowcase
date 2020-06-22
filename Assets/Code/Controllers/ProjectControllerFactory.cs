using System;
using System.Collections.Generic;
using Code.Models.ProjectModels;
using Code.Utility.DataProviders;
using Code.Views.ProjectViews;

namespace Code.Controllers
{
    public class ProjectControllerFactory : IProjectControllerFactory
    {
        private const string PremiumGrade = "premium";
        private const string EconomyGrade = "economy";
        private const string ComfortGrade = "comfort";
        
        public ProjectsManager ProjectsManager { get; }

        public ProjectControllerFactory(IAssetProvider assetProvider,
            IProjectModelsContainer container, IProjectViewManipulator manipulator)
        {
            var comfortModels = new List<IProjectModel>();
            var economyModels = new List<IProjectModel>();
            var premiumModels = new List<IProjectModel>();

            foreach (var projectModel in container.ProjectModels)
            {
                switch (projectModel.Grade)
                {
                    case PremiumGrade:
                        premiumModels.Add(projectModel);
                        break;
                    case EconomyGrade:
                        economyModels.Add(projectModel);
                        break;
                    case ComfortGrade:
                        comfortModels.Add(projectModel);
                        break;
                    default:
                        throw new ArgumentException($"There is no grade {projectModel.Grade}");
                }
            }
            
            var comfortController = new ComfortProjectsController(container.ProjectsInfo, assetProvider, comfortModels, manipulator);
            var economyController = new PremiumProjectsController(container.ProjectsInfo, assetProvider, premiumModels, manipulator);
            var premiumController = new EconomyProjectsController(container.ProjectsInfo, assetProvider, economyModels, manipulator);

            var projects = new List<ProjectsControllerBase> {comfortController, economyController, premiumController};
            ProjectsManager = new ProjectsManager(projects);
        }
    }
}