using UnityEngine;
using Sirenix.OdinInspector;

namespace CarSumo.GUI.Core
{
    public class GUIElement : SerializedMonoBehaviour
    {
        [SerializeField] private IGUIProcess[] _processes = new IGUIProcess[0];

        private void Awake()
        {
            foreach (IGUIProcess process in _processes)
            {
                process.Init();
            }
        }

        public void Process()
        {
            foreach (IGUIProcess process in _processes)
            {
                process.ApplyProcess();
            }
        }
    }
}