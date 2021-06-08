using System;
using Cinemachine;
using UnityEngine.EventSystems;

namespace CarSumo.Input
{
    public interface ISwipePanel : 
        IBeginDragHandler, IDragHandler, IEndDragHandler, AxisState.IInputAxisProvider
    {
        event Action<SwipeData> Begun; 
        event Action<SwipeData> Swiping;
        event Action<SwipeData> Released;
    }
}