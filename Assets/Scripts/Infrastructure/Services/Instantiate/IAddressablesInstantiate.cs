﻿using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CarSumo.Infrastructure.Services.Instantiate
{
    public interface IAddressablesInstantiate
    {
        Task<T> InstantiateAsync<T>(AssetReferenceGameObject reference, Transform parent = null) where T : Component;
    }
}