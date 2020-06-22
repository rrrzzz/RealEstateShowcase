using System;
using Code.Utility;
using UnityEngine;

namespace Code.Views.ProjectViews
{
    public class ProjectViewManipulator : IProjectViewManipulator
    {
        private readonly MonoBehaviourProvider _monoBehaviourProvider;
        private readonly IMapScanner _mapScanner;

        public ProjectViewManipulator(MonoBehaviourProvider monoBehaviourProvider, IMapScanner mapScanner)
        {
            _monoBehaviourProvider = monoBehaviourProvider;
            _mapScanner = mapScanner;
        }

        public GameObject CreateProjectOnMap(GameObject prefab, Vector3 worldPoint)
        {
            var isPositionOnMap = _mapScanner.CheckPositionOnMap(worldPoint);
            if (!isPositionOnMap)
                throw new ArgumentException($"No map at point {worldPoint}. Cannot spawn {prefab.name} there.");

            var gameObject = _monoBehaviourProvider.CreateInstance(prefab, worldPoint);
            var projectSize = gameObject.GetComponent<Collider>().bounds.size;
            gameObject.SetActive(false);
            
            var isPositionObstructed = _mapScanner.CheckPositionObstructed(worldPoint, projectSize);
            if (isPositionObstructed)
                throw new ArgumentException($"{prefab.name} collides with another project at {worldPoint}.");
            
            gameObject.SetActive(true);
            return gameObject;
        }
    }
}