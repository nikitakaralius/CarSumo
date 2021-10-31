using System.Collections.Generic;
using CarSumo.Structs;
using UnityEngine;

namespace CarSumo.Extensions
{
    public static class TransformExtensions
    {
	    public static void SetWorldPlacement(this Transform transform, WorldPlacement worldPlacement)
	    {
		    transform.position = worldPlacement.Position;
		    transform.forward = worldPlacement.ForwardVector;
	    }
	    
        public static IEnumerable<GameObject> GetAllChildren(this Transform transform)
        {
            int childCount = transform.childCount;
            GameObject[] children = new GameObject[childCount];

            for (int i = 0; i < childCount; i++)
            {
                children[i] = transform.GetChild(0).gameObject;
            }

            return children;
        }
    }
}