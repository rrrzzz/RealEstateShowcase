using Code.Utility;
using Code.Utility.DataProviders;
using UnityEngine;

namespace Code.Views.UIViews
{
    public class UiGenerator : IUiGenerator
    {
        private readonly IAssetProvider _assetProvider;
        private readonly MonoBehaviourProvider _monoBehaviourProvider;
        private readonly Transform _canvas;

        public UiGenerator(IAssetProvider assetProvider, MonoBehaviourProvider monoBehaviourProvider)
        {
            _assetProvider = assetProvider;
            _monoBehaviourProvider = monoBehaviourProvider;
            var canvasPrefab = _assetProvider.GetCanvasPrefab();
            _canvas = _monoBehaviourProvider.CreateInstance(canvasPrefab).transform;
        }

        public IGradeToggleView CreateCostToggle()
        {
            var costTogglePrefab = _assetProvider.GetCostTogglePrefab();
            return CreateToggle(costTogglePrefab);
        }

        public IGradeToggleView CreateFlatCountToggle()
        {
            var flatsTogglePrefab = _assetProvider.GetFlatCountTogglePrefab();
            return CreateToggle(flatsTogglePrefab);
        }

        private IGradeToggleView CreateToggle(GameObject prefab)
        {
            IGradeToggleView view = _monoBehaviourProvider.CreateInstance(prefab, _canvas).AddComponent<GradeToggleView>();
            return view;
        }
    }
}