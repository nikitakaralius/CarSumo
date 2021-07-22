using System.Threading.Tasks;
using UnityEngine;

namespace CarSumo.Infrastructure.Services.Instantiate
{
    public interface IInstantiateService
    {
        Task<T> InstantiateAsync<T>(object key) where T : Component;
        Task<T> InstantiateAsync<T>(object key, Transform parent) where T : Component;
        Task<T> InstantiateAsync<T>(object key, Vector3 position, Quaternion rotation) where T : Component;
        Task<T> InstantiateAsync<T>(object key, Vector3 position, Quaternion rotation, Transform parent) where T : Component;
    }
}