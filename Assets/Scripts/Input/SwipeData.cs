using UnityEngine;

namespace CarSumo.Input
{
    public struct SwipeData
    {
        public Vector2 StartPosition { get; set; }
        public Vector2 EndPosition { get; set; }

        public Vector2 Delta { get; set; }

        public float Distance => Vector2.Distance(StartPosition, EndPosition);

        public Vector2 Direction => EndPosition - StartPosition;
    }
}