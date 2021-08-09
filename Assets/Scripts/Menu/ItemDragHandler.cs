using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Menu
{
    public abstract class ItemDragHandler<T> : MonoBehaviour,
        IBeginDragHandler, IDragHandler, IEndDragHandler where T : Component
    {
        private Transform _originalParent;
        private Transform _draggingParent;

        protected IEnumerable<T> Siblings => GetAllSiblingAccounts(_originalParent);

        protected void Initialize(Transform originalParent, Transform draggingParent)
        {
            _originalParent = originalParent;
            _draggingParent = draggingParent;
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            transform.SetParent(_draggingParent);
            OnLateBeginDrag(eventData);
        }

        public abstract void OnDrag(PointerEventData eventData);

        public void OnEndDrag(PointerEventData eventData)
        {
            IEnumerable<T> siblings = GetAllSiblingAccounts(_originalParent);
            T closest = FindClosest(siblings);

            int siblingIndex = closest is null ? 0 : closest.transform.GetSiblingIndex() + 1;

            transform.SetParent(_originalParent);
            transform.SetSiblingIndex(siblingIndex);
            
            OnLateEndDrag(eventData);
        }

        protected virtual void OnLateBeginDrag(PointerEventData eventData) { }

        protected virtual void OnLateEndDrag(PointerEventData eventData) { }
        
        [CanBeNull]
        private T FindClosest(IEnumerable<T> items)
        {
            T closestItem = null;
            foreach (T item in items)
            {
                if (transform.position.y - item.transform.position.y < 0)
                {
                    closestItem = item;
                }
            }

            return closestItem;
        }
        
        private IEnumerable<T> GetAllSiblingAccounts(Transform layoutParent)
        {
            for (int i = 0; i < layoutParent.childCount; i++)
            {
                Transform child = layoutParent.GetChild(i);

                if (child.TryGetComponent<T>(out var sibling) == false)
                    continue;

                yield return sibling;
            }
        }
    }
}