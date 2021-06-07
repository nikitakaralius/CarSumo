using System;
using UnityEngine;
using System.Linq;

namespace CarSumo.SceneManagement
{
    public class TypedProcessor : MonoBehaviour
    {
        private void Awake()
        {
            var awakesHandlers = FindObjectsOfType<MonoBehaviour>()
                                                        .OfType<ITypedAwakeHandler>();

            foreach (var handler in awakesHandlers)
                handler.OnSceneAwake();
        }
    }
}