using DG.Tweening;
using TweenAnimations;
using UnityEngine;

namespace Menu.Vehicles.Layout
{
    public class LayoutVehicleCardAnimation : MonoBehaviour
    {
        [SerializeField] private Vector3TweenData _tweenData;

        public void ApplyIncreaseSizeAnimation(Transform transform)
        {
            transform
                .DOScale(_tweenData.To, _tweenData.Duration)
                .SetEase(_tweenData.Ease);
        }

        public void ApplyDecreaseSizeAnimation(Transform transform)
        {
            transform
                .DOScale(_tweenData.From, _tweenData.Duration)
                .SetEase(_tweenData.Ease);
        }
    }
}