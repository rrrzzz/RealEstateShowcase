using System.Collections;
using UnityEngine;

namespace Code.Views.ProjectViews
{
    public class ProjectView : MonoBehaviour, IProjectView
    {
        private const float ResizeMultiplier = 5;
        private const float ResizeUpTime = .3f;
        private const float ResizeDownTime = 0.01f;
        
        public int Id { get; set; }
        public float TotalCost { get; set; }
        public int FlatCount{ get; set; }
        
        private float _initialYScale;
        private float _modificationTolerance;

        public void ResizeProjectHeight(float ratio)
        {
            var target = ratio * _initialYScale * ResizeMultiplier;
            StopCoroutine(ModifySizeSmooth(target));
            StartCoroutine(ModifySizeSmooth(target));
        }

        private IEnumerator ModifySizeSmooth(float target)
        {
            var velocityDown = 0f;
            var velocityUp = 0f;
            var scale = transform.localScale;

            while (scale.y > _modificationTolerance)
            {
                scale.y = Mathf.SmoothDamp(scale.y, 0, ref velocityDown, ResizeDownTime);
                transform.localScale = scale;
                yield return null;
            }

            while (Mathf.Abs(scale.y - target) > _modificationTolerance)
            {
                scale.y = Mathf.SmoothDamp(scale.y, target, ref velocityUp, ResizeUpTime);
                transform.localScale = scale;
                yield return null;
            }
        }

        private void OnEnable()
        {
            _initialYScale = transform.localScale.y;
            _modificationTolerance = _initialYScale / 10;
        }
    }
}