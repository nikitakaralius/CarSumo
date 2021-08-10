using Menu.Vehicles.Cards;
using Menu.Vehicles.Storage;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Vehicles.Layout
{
    public class CardVehicleLayoutView : VehicleLayoutView<VehicleCard>, IVehicleCardSelectHandler, ILayoutSlotProvider
    {
        [Header("View Components")]
        [SerializeField] private Transform _layoutRoot;
        [SerializeField] private LayoutGroup _contentLayoutGroup;
        
        [Header("Layout Card Driven Components")]
        [SerializeField] private VehicleStorageView _storage;
        [SerializeField] private LayoutVehicleCardAnimation _animation;
        [SerializeField] private Vector3 _cardSize = Vector3.one * 0.75f;

        private IEnumerable<VehicleCard> _layout;
        
        protected override Transform CollectionRoot => _layoutRoot;
        private Transform CardSelectedRoot => transform;
        
        public int Slot { get; private set; }

        private void OnDisable()
        {
            _contentLayoutGroup.enabled = true;
            _storage.Disable();
            Slot = -1;
        }

        protected override void ProcessCreatedLayout(IEnumerable<VehicleCard> layout)
        {
            _layout = layout;

            foreach (VehicleCard card in layout)
            {
                card.transform.localScale = _cardSize;
                card.SetSelectHandler(this);
            }
        }

        public void OnCardSelected(VehicleCard card)
        {
            _contentLayoutGroup.enabled = false;

            Slot = card.DynamicSiblingIndex;
            card.transform.SetParent(CardSelectedRoot);
            
            _storage.Enable();
            _animation.ApplyIncreaseSizeAnimation(card.transform);
            
            NotifyOtherCards(card);
        }

        public void OnCardDeselected(VehicleCard card)
        {
            _storage.Disable();
            OnCardDeselectedInternal(card);
        }

        private void NotifyOtherCards(VehicleCard selectedCard)
        {
            IEnumerable<VehicleCard> otherCards = _layout.Where(card => card != selectedCard);

            foreach (VehicleCard card in otherCards)
            {
                card.NotifyBeingDeselected(true);
                OnCardDeselectedInternal(card);
            }
        }

        private void OnCardDeselectedInternal(VehicleCard card)
        {
            card.transform.SetParent(CollectionRoot);
            card.SetLatestSiblingIndex();
            _animation.ApplyDecreaseSizeAnimation(card.transform);
        }
    }
}