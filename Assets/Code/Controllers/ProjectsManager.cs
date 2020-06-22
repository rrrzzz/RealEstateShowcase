using System.Collections.Generic;

namespace Code.Controllers
{
    public class ProjectsManager : IProjectsManager
    {
        private readonly List<ProjectsControllerBase> _controllers;

        public ProjectsManager(List<ProjectsControllerBase> controllers)
        {
            _controllers = controllers;
        }

        public void ResizeAllProjectsByCost()
        {
            _controllers.ForEach(controller => controller.ResizeProjectsByCost());
        }
        
        public void ResizeAllProjectsByFlatCount()
        {
            _controllers.ForEach(controller => controller.ResizeProjectsByFlatCount()); 
        }
    }
}