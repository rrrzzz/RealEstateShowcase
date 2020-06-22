using System.Collections.Generic;

namespace Code.Models.ProjectModels
{
    public readonly struct ProjectModelsContainer : IProjectModelsContainer
    {
        public ProjectsInfo ProjectsInfo { get; }
        public List<IProjectModel> ProjectModels { get; }

        public ProjectModelsContainer(ProjectsInfo projectsInfo, List<IProjectModel> projectModels)
        {
            ProjectsInfo = projectsInfo;
            ProjectModels = projectModels;
        }
    }
}