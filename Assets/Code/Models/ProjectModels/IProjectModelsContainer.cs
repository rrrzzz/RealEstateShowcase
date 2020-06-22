using System.Collections.Generic;

namespace Code.Models.ProjectModels
{
    public interface IProjectModelsContainer
    {
        ProjectsInfo ProjectsInfo { get; }
        List<IProjectModel> ProjectModels { get; }
    }
}