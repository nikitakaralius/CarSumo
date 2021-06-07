using System;
using UnityEngine;
using CarSumo.Input;
using Sirenix.OdinInspector;

namespace CarSumo.Units
{
    public class UnitSelector : SerializedMonoBehaviour
    {
        [SerializeField] private ISwipePanel _panel;

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }
    }
}