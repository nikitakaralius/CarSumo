using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu
{
    public abstract class ItemDragHandler<T> : SerializedMonoBehaviour,
            IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
            where T : Component
    {
        [SerializeField] private float _requiredHoldTime;

        private LayoutGroup _layoutGroup;

        private ReactiveProperty<bool> _canDrag = new ReactiveProperty<bool>(false);
        private bool _pointerDown;

        private IReadOnlyDictionary<T, int> _itemIndexes;

        protected Transform ContentParent { get; private set; }

        protected Transform DraggingParent { get; private set; }

        protected IReadOnlyReactiveProperty<bool> CanDrag => _canDrag;

        protected void Initialize(Transform contentParent, Transform draggingParent, LayoutGroup layoutGroup)
        {
            ContentParent = contentParent;
            DraggingParent = draggingParent;
            _layoutGroup = layoutGroup;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _pointerDown = true;
            MainThreadDispatcher
               .StartUpdateMicroCoroutine(
                StartTimingHoldTime(_requiredHoldTime, IsPlayerHoldingItem, BeginDrag));
        }

        private void BeginDrag()
        {
	        OnBeforeBeginDrag();
            _canDrag.Value = true;

            _itemIndexes = PrepareLayoutItemIndexes(ContentParent);
            transform.SetParent(DraggingParent);

            _layoutGroup.enabled = false;
            OnAfterBeginDrag();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_canDrag.Value == false)
                return;

            OnDragUpdate(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _pointerDown = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_canDrag.Value == false)
                return;

            int closestIndex = FindClosestIndex(_itemIndexes);

            transform.SetParent(ContentParent);
            _layoutGroup.enabled = true;
            transform.SetSiblingIndex(closestIndex);

            OnAfterEndDrag();

            _pointerDown = false;
            _canDrag.Value = false;
        }

        protected abstract void OnDragUpdate(PointerEventData eventData);

        protected virtual void OnBeforeBeginDrag() { }
        
        protected virtual void OnAfterBeginDrag() { }

        protected virtual void OnAfterEndDrag() { }

        protected void SetRequiredHoldTime(float requiredHoldTime)
        {
	        _requiredHoldTime = requiredHoldTime;
        }

        private IEnumerator StartTimingHoldTime(float requiredHoldTime, Func<bool> stopTiming, Action onReachingRequiredTime)
        {
            float holdTime = 0;

            while (stopTiming.Invoke())
            {
                if (holdTime >= requiredHoldTime)
                {
                    onReachingRequiredTime?.Invoke();
                    yield break;
                }

                holdTime += Time.deltaTime;
                yield return null;
            }
        }

        private int FindClosestIndex(IReadOnlyDictionary<T, int> itemIndexes)
        {
            if (itemIndexes.Count == 1)
            {
                return 0;
            }

            KeyValuePair<T, int> closestItem = itemIndexes.First(item => item.Key != this);

            foreach (KeyValuePair<T, int> item in itemIndexes)
            {
                if (item.Key == this)
                {
                    continue;
                }

                if (Vector3.Distance(transform.position, item.Key.transform.position) <
                    Vector3.Distance(transform.position, closestItem.Key.transform.position))
                {
                    closestItem = item;
                }
            }

            return closestItem.Value;
        }

        private IReadOnlyDictionary<T, int> PrepareLayoutItemIndexes(Transform contentTransform)
        {
            var itemIndexes = new Dictionary<T, int>(contentTransform.childCount);

            for (int i = 0; i < contentTransform.childCount; i++)
            {
                Transform child = contentTransform.GetChild(i);

                if (child.TryGetComponent(out T item) == false)
                {
                    continue;
                }

                itemIndexes.Add(item, item.transform.GetSiblingIndex());
            }

            return itemIndexes;
        }

        private bool IsPlayerHoldingItem()
        {
            return _pointerDown;
        }
    }
}