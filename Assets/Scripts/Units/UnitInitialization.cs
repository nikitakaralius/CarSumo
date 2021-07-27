using System.Collections.Generic;
using UnityEngine;

namespace CarSumo.Units
{
    public class UnitInitialization : MonoBehaviour
    {
        private async void Awake()
        {
            IEnumerable<Unit> units = FindObjectsOfType<Unit>();

            foreach (Unit unit in units)
            {
                await unit.Initialize();
            }
        }
    }
}