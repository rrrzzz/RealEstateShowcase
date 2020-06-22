using System.Collections.Generic;
using Code.Models.ProjectModels;
using Code.Utility.DataProviders;
using Code.Views.ProjectViews;
using UnityEngine;

namespace Code.Controllers
{
    public abstract class ProjectsControllerBase
    {
        protected readonly IAssetProvider AssetProvider;
        private readonly IProjectViewManipulator _viewManipulator;
        private readonly ProjectsInfo _projectsInfo;
        private readonly List<IProjectView> _projectViews = new List<IProjectView>();
        private readonly List<IProjectModel> _projectModels;
        protected GameObject ProjectPrefab;

        protected ProjectsControllerBase(ProjectsInfo projectsInfo, IAssetProvider assetProvider,
            List<IProjectModel> models, IProjectViewManipulator manipulator)
        {
            _viewManipulator = manipulator;
            AssetProvider = assetProvider;
            _projectModels = models;
            _projectsInfo = projectsInfo;
            
            InitializeProjectViews();
        }
        
        public void ResizeProjectsByCost()
        {
            foreach (var view in _projectViews)
            {
                var ratio = view.TotalCost / _projectsInfo.MaxCost;
                view.ResizeProjectHeight(ratio);
            }  
        }

        public void ResizeProjectsByFlatCount()
        {
            foreach (var view in _projectViews)
            {
                var ratio = (float)view.FlatCount / _projectsInfo.MaxFlatCount;
                view.ResizeProjectHeight(ratio);
            }  
        }
        
        protected abstract void SetProjectPrefab();
        
        protected void InitializeProjectViews()
        {
            SetProjectPrefab();
            
            foreach (var model in _projectModels)
            {
                var gameObject = _viewManipulator.CreateProjectOnMap(ProjectPrefab, model.WorldCoordinates);
                gameObject.name = model.Title;
                var projectView = gameObject.AddComponent<ProjectView>();
                projectView.Id = model.Id;
                projectView.TotalCost = model.TotalCost;
                projectView.FlatCount = model.FlatsCount;
                _projectViews.Add(projectView);
            }
        }
    }
}