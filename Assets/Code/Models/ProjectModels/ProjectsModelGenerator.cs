using System.Collections.Generic;
using System.Linq;
using Code.DataEntities;
using Code.Models.MapModels;
using Code.Utility;

namespace Code.Models.ProjectModels
{
    public class ProjectsModelGenerator : IProjectsModelGenerator
    {
        private readonly ICoordinatesConverter _coordinatesConverter;

        private bool _isFirst;
        
        public ProjectsModelGenerator(ICoordinatesConverter coordinatesConverter)
        {
            _coordinatesConverter = coordinatesConverter;
        }

        public IProjectModelsContainer ParseProjectsData(ApartmentsProjectData[] data, MapInfo mapInfo)
        {
            var projectModels = new List<IProjectModel>();
            
            var maxFlatCount = data.Max(x => x.flatsCount);
            var maxCost = data.Max(x => x.totalCost);
            var projectsInfo = new ProjectsInfo(maxCost, maxFlatCount);

            foreach (var project in data)
            {
                var point2d = 
                    _coordinatesConverter.ConvertToWorldPosition2d(project.coordinates, mapInfo.OriginOffset);

                var point3d = mapInfo.Forward * point2d.y;
                point3d.x = point2d.x;

                var projectModel = new ProjectModel(project.id, project.title, point3d,
                    project.grade, project.totalCost, project.flatsCount);
                
                projectModels.Add(projectModel);
            }

            return new ProjectModelsContainer(projectsInfo, projectModels);
        }
    }
}