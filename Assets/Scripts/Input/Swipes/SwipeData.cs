using UnityEngine;

namespace CarSumo.Input.Swipes
{
    public struct SwipeData
    {
        public Vector2 Delta { get; set; }

        public Vector2 StartPosition { get; set; }
        public Vector2 EndPosition { get; set; }

        public float DragTime { get; set; }
    }
}