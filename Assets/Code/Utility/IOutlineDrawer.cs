using System.Collections.Generic;
using UnityEngine;

namespace Code.Utility
{
    public interface IOutlineDrawer
    {
        Material OutlineMaterial { get; set; }
        void DrawOutline(GameObject objectToOutline, List<Vector2> points);
    }
}