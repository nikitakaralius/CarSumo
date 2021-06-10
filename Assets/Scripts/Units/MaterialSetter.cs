using System;
using UnityEngine;

namespace CarSumo.Units
{
    public class MaterialSetter : MonoBehaviour
    {
        [SerializeField] private Material _material;

        private void Start()
        {
            GetComponentInChildren<MeshRenderer>().material = _material;
        }
    }
}