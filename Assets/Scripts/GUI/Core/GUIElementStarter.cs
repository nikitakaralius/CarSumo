using UnityEngine;

namespace CarSumo.GUI.Core
{
    public class GUIElementStarter : MonoBehaviour
    {
        [SerializeField] private GUIElement _element;

        private void Start()
        {
            _element.Process();
        }
    }
}