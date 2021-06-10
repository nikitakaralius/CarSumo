using CarSumo.Storage;
using CarSumo.Units;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CarSumo.Processors
{
    public class WinnerDeterminant : SerializedMonoBehaviour
    {
        [SerializeField] private IReactiveUnitStorage _storage;

        private void OnEnable()
        {
            _storage.Emptied += OnStorageEmptied;
        }

        private void OnDisable()
        {
            _storage.Emptied -= OnStorageEmptied;
        }

        private void OnStorageEmptied(Unit unit)
        {
            //Debug.Log($"{unit.Team} Lose");
        }
    }
}