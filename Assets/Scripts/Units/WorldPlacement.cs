using UnityEngine;

namespace CarSumo.Units
{
    public struct WorldPlacement
    {
        public Vector3 Position { get; set; }
        public Vector3 ForwardVector { get; set; }

        public WorldPlacement(Vector3 position, Vector3 forwardVector)
        {
            Position = position;
            ForwardVector = forwardVector;
        }
    }
}