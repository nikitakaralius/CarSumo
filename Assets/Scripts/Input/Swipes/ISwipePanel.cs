using System;
using UnityEngine.EventSystems;

namespace CarSumo.Input.Swipes
{
    public interface ISwipePanel : IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        event Action<SwipeData> Swiping;
        event Action<SwipeData> Released;
    }
}