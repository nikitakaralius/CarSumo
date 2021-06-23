using UnityEngine;
using AdvancedAudioSystem;

namespace CarSumo.Vehicles
{
    public class VehicleCollision : MonoBehaviour
    {
        [SerializeField] private MonoAudioCuePlayer _collisionCuePlayer;

        private void OnCollisionEnter(Collision collision)
        {
            _collisionCuePlayer.Play();
        }
    }
}
