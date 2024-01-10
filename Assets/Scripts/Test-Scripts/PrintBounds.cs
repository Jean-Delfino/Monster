using System;
using UnityEngine;

namespace Test_Scripts
{
    public class PrintBounds : MonoBehaviour
    {
        private void OnEnable()
        {
            Renderer childRenderer = GetComponent<Renderer>();
            var bounds = childRenderer.bounds;
            print("BOUNDS(MAX X, MAX Y, MIN X, MIN Y) :" + bounds.max.x + " " + bounds.max.y + " " + bounds.min.x + " " + bounds.min.y);
            print("CENTER = " + bounds.center);
        }
    }
}