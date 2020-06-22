using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Utility
{
    public class OutlineDrawer : IOutlineDrawer
    {
        private const float MinDistanceBetweenPoints = 20;
        private const float SubdivisionAmount = 5;

        private readonly float _outlineWidth;

        public Material OutlineMaterial { get; set; }

        public OutlineDrawer(Material outlineMaterial, float outlineWidth = 0.8f)
        {
            OutlineMaterial = outlineMaterial;
            _outlineWidth = outlineWidth;
        }
        
        public OutlineDrawer(float outlineWidth = 0.8f)
        {
            _outlineWidth = outlineWidth;
        }
    
        public void DrawOutline(GameObject objectToOutline, List<Vector2> points)
        {
            if (OutlineMaterial == null)
                throw new NullReferenceException("Outline material property must be set");    
            
            var lineRenderer = objectToOutline.AddComponent<LineRenderer>();

            points = AddIntermediatePoints(points);
            var points3d = points.Select(point => (Vector3)point).ToArray();

            lineRenderer.sharedMaterial = OutlineMaterial;
            lineRenderer.positionCount = points3d.Length;
            lineRenderer.SetPositions(points3d);
            lineRenderer.startWidth = _outlineWidth;
            lineRenderer.endWidth = _outlineWidth;
            lineRenderer.useWorldSpace = false;
        }

        List<Vector2> AddIntermediatePoints(List<Vector2> points)
        {
            var res = new List<Vector2>();
            res.Add(points[0]);

            for (int i = 1; i < points.Count; i++)
            {
                var prev = points[i - 1];
                var current = points[i];
                var dif = current - prev;
                var dir = dif.normalized;
                var dist = dif.magnitude;

                if (dist > MinDistanceBetweenPoints)
                {
                    dist /= SubdivisionAmount;
                    for (int j = 1; j <= SubdivisionAmount; j++)
                    {
                   
                        var newVec = prev + dir * dist;
                        res.Add(newVec);
                        prev = newVec;
                    }
                }

                res.Add(current);
            }

            return res;
        }
    }
}