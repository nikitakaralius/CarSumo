using System;
using Cinemachine;
using UnityEngine.EventSystems;

namespace CarSumo.Input
{
    public interface ISwipeScreen : 
        IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        event Action<Swipe> Begun; 
        event Action<Swipe> Swiping;
        event Action<Swipe> Released;
    }
}