using System;
using CarSumo.Input;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CarSumo.Debugging
{
    public class GUISwipeDebugger : SerializedMonoBehaviour
    {
        [SerializeField] private ISwipePanel _panel;
        [SerializeField] private GUIStyle _style;

        private float _distance;

        private void OnEnable()
        {
            _panel.Swiping += data => _distance = data.Distance;
        }

        private void OnGUI()
        {
            GUILayout.TextField($"{_distance}", _style);
        }
    }
}