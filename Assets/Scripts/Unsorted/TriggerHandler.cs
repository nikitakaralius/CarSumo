using UnityEngine;
using UnityEngine.Events;

namespace CarSumo
{
    public class TriggerHandler : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Collider> _triggerEntered;

        private void OnTriggerEnter(Collider other)
        {
            _triggerEntered.Invoke(other);
        }
    }
}