using UnityEngine;
using AdvancedAudioSystem;

namespace CarSumo.NewVehicles
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
