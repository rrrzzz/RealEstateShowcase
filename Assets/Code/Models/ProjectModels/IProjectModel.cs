using UnityEngine;

namespace Code.Models.ProjectModels
{
    public interface IProjectModel
    {
        int Id { get; }
        string Title { get; }
        Vector3 WorldCoordinates { get; }
        string Grade { get; }
        float TotalCost { get; }
        int FlatsCount { get; }
    }
}