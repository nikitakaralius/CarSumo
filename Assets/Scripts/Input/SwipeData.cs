using UnityEngine;

namespace CarSumo.Input
{
    public struct SwipeData
    {
        public Vector2 StartPosition { get; }
        public Vector2 EndPosition { get; }
        public Vector2 Delta { get; }

        public float Distance => Vector2.Distance(StartPosition, EndPosition);
        public Vector2 Direction => EndPosition - StartPosition;

        public SwipeData(Vector2 startPosition, Vector2 endPosition, Vector2 delta)
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
            Delta = delta;
        }
    }
}