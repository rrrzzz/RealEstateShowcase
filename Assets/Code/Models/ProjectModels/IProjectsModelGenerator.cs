using Code.DataEntities;
using Code.Models.MapModels;

namespace Code.Models.ProjectModels
{
    public interface IProjectsModelGenerator
    {
        IProjectModelsContainer ParseProjectsData(ApartmentsProjectData[] data, MapInfo mapInfo);
    }
}