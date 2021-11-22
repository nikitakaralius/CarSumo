using System;
using System.Collections.Generic;
using System.Linq;
using CarSumo.DataModel.Accounts;
using TweenAnimations;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu.Accounts
{
    public class AccountListItemDragHandler : ItemDragHandler<AccountListItemDragHandler>
    {
	    private SizeTweenAnimation _animation;
        private IClientAccountStorageOperations _storageOperations;

        private Account _account;
        private Button _button;
        private IDisposable _animationSubscription;

        public void Initialize(float requiredHoldTime,
								IClientAccountStorageOperations storageOperations,
	        					Account account,
	        					Button button,
	        					SizeTweenAnimation tweenAnimation,
	        					IReadOnlyDragHandlerData dragHandlerData)
        {
	        _account = account;
	        _button = button;
	        _animation = tweenAnimation;
	        _storageOperations = storageOperations;
	        
	        Initialize(dragHandlerData);
	        SetRequiredHoldTime(requiredHoldTime);
	        
	        _animationSubscription = CanDrag.Subscribe(canDrag =>
	        {
		        if (canDrag)
		        {
			        _animation.IncreaseSize(transform);
		        }
		        else
		        {
			        _animation.DecreaseSize(transform);
		        }
	        });
        }

        private void OnDestroy()
        {
            _animationSubscription.Dispose();
        }

        protected override void OnAfterBeginDragging()
        {
            _button.enabled = false;
        }

        protected override void OnDragUpdate(PointerEventData eventData)
        {
            Vector3 originalPosition = transform.position;
            Vector2 dragPosition = eventData.position;
            transform.position = new Vector3(originalPosition.x, dragPosition.y);
        }

        protected override void OnAfterEndDragging()
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