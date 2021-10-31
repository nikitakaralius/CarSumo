using DG.Tweening;
using UnityEngine;

namespace TweenAnimations
{
    [System.Serializable]
    public class TweenData<T>
    {
        public T From;
        public T To;

        public float Duration;
        public Ease Ease;
    }
    
    [System.Serializable]
    public class FloatTweenData : TweenData<float> { }
    
    [System.Serializable]
    public class Vector3TweenData : TweenData<Vector3> { }
    
    [System.Serializable]
    public class Vector2TweenData : TweenData<Vector2> { }
}