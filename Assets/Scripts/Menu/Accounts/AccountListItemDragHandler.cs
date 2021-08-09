using System;
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

        public void Initialize(Account account, Button button, Transform originalParent)
        {
            _account = account;
            _button = button;
            Initialize(originalParent);
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

        protected override void OnLateBeginDrag(PointerEventData eventData)
        {
            _button.enabled = false;
        }

        public override void OnDragUpdate(PointerEventData eventData)
        {
            Vector3 originalPosition = transform.position;
            Vector2 dragPosition = eventData.position;
            transform.position = new Vector3(originalPosition.x, dragPosition.y);
        }

        protected override void OnLateEndDrag(PointerEventData eventData)
        {
            _storageOperations.ChangeOrder(Siblings
                .Select(item => item._account)
                .ToArray());

            _button.enabled = true;
        }
    }
}