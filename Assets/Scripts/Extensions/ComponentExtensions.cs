using System;
using UnityEngine;

namespace CarSumo.Extensions
{
    public static class ComponentExtensions
    {
        public static void HandleComponent<T>(this Component gameObject, Action<T> handler)
        {
            var component = gameObject.GetComponent<T>();

            if (component is null)
                return;

            handler?.Invoke(component);
        }

        public static bool HasComponent<T>(this Component gameObject)
        {
            return gameObject.GetComponent<T>() != null;
        }

        public static Vector3 GetRelativeDirection(this Component component, Vector3 originalDirection)
        {
            return component.transform.TransformDirection(originalDirection);
        }
    }
}