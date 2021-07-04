using UnityEngine;
using Sirenix.OdinInspector;

namespace CarSumo.GUI.Core
{
    public class TweenElement : SerializedMonoBehaviour
    {
        [SerializeField] private ITweenProcess[] _processes = new ITweenProcess[0];

        private void Awake()
        {
            foreach (ITweenProcess process in _processes)
            {
                process.Init();
            }
        }

        public void Process()
        {
            foreach (ITweenProcess process in _processes)
            {
                process.ApplyProcess();
            }
        }
    }
}