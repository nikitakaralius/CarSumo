using UnityEngine;

namespace CarSumo.Units
{
    public class VehicleCollision : MonoBehaviour
    {
        [SerializeField] private VehicleAudio _audio;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<Vehicle>(out var vehicle) == false)
                return;

            _audio.PlayCollisionSound();
        }
    }
}