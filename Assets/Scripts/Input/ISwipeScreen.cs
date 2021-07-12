using System;
using Cinemachine;
using UnityEngine.EventSystems;

namespace CarSumo.Input
{
    public interface ISwipeScreen : 
        IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        event Action<SwipeData> Begun; 
        event Action<SwipeData> Swiping;
        event Action<SwipeData> Released;
    }
}