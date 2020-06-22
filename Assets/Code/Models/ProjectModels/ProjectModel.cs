using UnityEngine;

namespace Code.Models.ProjectModels
{
    public readonly struct ProjectModel : IProjectModel
    {
        public int Id { get; }
        public string Title { get; }
        public Vector3 WorldCoordinates { get; }
        public string Grade { get; }
        public float TotalCost { get; }
        public int FlatsCount { get; }

        public ProjectModel(int id, string title, Vector3 worldCoordinates,
            string grade, float totalCost, int flatsCount)
        {
            Id = id;
            Title = title;
            WorldCoordinates = worldCoordinates;
            Grade = grade;
            TotalCost = totalCost;
            FlatsCount = flatsCount;
        }
    }
}