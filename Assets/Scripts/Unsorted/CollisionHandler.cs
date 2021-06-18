using UnityEngine;
using UnityEngine.Events;

namespace CarSumo
{
    public class CollisionHandler : MonoBehaviour
    {
        [SerializeField] private UnityEvent _collisionEntered;

        private void OnCollisionEnter(Collision other)
        {
            _collisionEntered.Invoke();
        }
    }
}