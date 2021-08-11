using System;
using System.Collections.Generic;
using System.Linq;
using CarSumo.DataModel.Accounts;
using TweenAnimations;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Menu.Accounts
{
    public class AccountListItemDragHandler : ItemDragHandler<AccountListItemDragHandler>
    {
        [SerializeField] private SizeTweenAnimation _animation;

        private IClientAccountStorageOperations _storageOperations;

        private Account _account;
        private Button _button;
        private IDisposable _animationSubscription;

        [Inject]
        private void Construct(IClientAccountStorageOperations storageOperations)
        {
            _storageOperations = storageOperations;
        }

        public void Initialize(Account account,
                               Button button,
                               Transform contentParent,
                               Transform draggingParent,
                               LayoutGroup layoutGroup)
        {
            _account = account;
            _button = button;
            Initialize(contentParent, draggingParent, layoutGroup);
        }

        private void OnEnable()
        {
            _animationSubscription = CanDrag.Subscribe(canDrag =>
            {
                if (canDrag)
                {
                    _animation.IncreaseSize();
                }
                else
                {
                    _animation.DecreaseSize();
                }
            });
        }

        private void OnDisable()
        {
            _animationSubscription.Dispose();
        }

        protected override void OnAfterBeginDrag()
        {
            _button.enabled = false;
        }

        protected override void OnDragUpdate(PointerEventData eventData)
        {
            Vector3 originalPosition = transform.position;
            Vector2 dragPosition = eventData.position;
            transform.position = new Vector3(originalPosition.x, dragPosition.y);
        }

        protected override void OnAfterEndDrag()
        {
            _storageOperations.ChangeOrder(
                GetAccountLayout(ContentParent)
                .ToArray());

            _button.enabled = true;
        }

        private IEnumerable<Account> GetAccountLayout(Transform contentParent)
        {
            for (int i = 0; i < contentParent.childCount; i++)
            {
                var child = contentParent.GetChild(i);

                if (child.TryGetComponent<AccountListItemDragHandler>(out var item) == false)
                    continue;

                yield return item._account;
            }
        }
    }
}