using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Menu
{
    public abstract class ItemDragHandler<T> : SerializedMonoBehaviour,
                                               IBeginDragHandler,
                                               IDragHandler,
                                               IEndDragHandler,
                                               IPointerDownHandler,
                                               IPointerUpHandler where T : Component
    {
        [SerializeField] private float _requiredHoldTime;
        
        private Transform _layoutRoot;
        private ReactiveProperty<bool> _canDrag = new ReactiveProperty<bool>(false);
        private bool _pointerDown;

        protected IEnumerable<T> Siblings => GetAllSiblingAccounts(_layoutRoot);

        protected IReadOnlyReactiveProperty<bool> CanDrag => _canDrag;

        protected void Initialize(Transform originalParent)
        {
            _layoutRoot = originalParent;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _pointerDown = true;    
            MainThreadDispatcher.StartUpdateMicroCoroutine(IncreaseHoldTime(_requiredHoldTime));
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            _pointerDown = false;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_canDrag.Value == false)
                return;

            OnLateBeginDrag(eventData);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if (_canDrag.Value == false)
                return;

            OnDragUpdate(eventData);
        }

        public abstract void OnDragUpdate(PointerEventData eventData);

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_canDrag.Value == false)
                return;
            
            IEnumerable<T> siblings = GetAllSiblingAccounts(_layoutRoot);
            T closest = FindClosest(siblings, transform.GetSiblingIndex());

            int siblingIndex = closest.transform.GetSiblingIndex();
            transform.SetSiblingIndex(siblingIndex);

            OnLateEndDrag(eventData);
            
            _pointerDown = false;
            _canDrag.Value = false;
        }

        protected virtual void OnLateBeginDrag(PointerEventData eventData) { }

        protected virtual void OnLateEndDrag(PointerEventData eventData) { }

        private IEnumerator IncreaseHoldTime(float requiredHoldTime)
        {
            float holdTime = 0.0f;
            
            while (_pointerDown)
            {
                if (holdTime >= requiredHoldTime)
                {
                    _canDrag.Value = true;
                    yield break;
                }
                
                holdTime += Time.deltaTime;
                yield return null;
            }
        }

        private T FindClosest(IEnumerable<T> items, int originalSiblingIndex)
        {
            T closestItem = items.FirstOrDefault(item => item.transform.GetSiblingIndex() != originalSiblingIndex);

            if (closestItem is null)
                return items.First();
            
            foreach (T item in items)
            {
                if (item.transform.GetSiblingIndex() == originalSiblingIndex)
                    continue;
                
                if (Vector3.Distance(transform.position, item.transform.position) <
                    Vector3.Distance(transform.position, closestItem.transform.position))
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