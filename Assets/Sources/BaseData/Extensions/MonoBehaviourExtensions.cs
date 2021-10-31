using UnityEngine;

namespace CarSumo.Extensions
{
    public static class MonoBehaviourExtensions
    {
        public static bool IsDestroyed(this MonoBehaviour component)
        {
            // this doesn't work, probably because Unity is destroying it but doesn't set the object to null
            // (condition is true in debug mode but is skips anyway)

            // return component is null;
            
            return component.ToString() == "null";
        }
    }
}