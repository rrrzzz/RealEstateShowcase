using UnityEngine;

namespace Code.Views.ProjectViews
{
    public interface IProjectViewManipulator
    {
        GameObject CreateProjectOnMap(GameObject prefab, Vector3 worldPoint);
    }
}