using System;
using Menu.Vehicles.Cards;
using Menu.Vehicles.Storage;
using System.Collections.Generic;
using UnityEngine;

namespace Menu.Vehicles.Layout
{
    public class CardVehicleLayoutView : VehicleLayoutView<VehicleCard>
    {
        [SerializeField] private Transform _layoutRoot;
        [SerializeField] private VehicleStorageView _storage;
        [SerializeField] private Vector3 _cardSize = Vector3.one * 0.75f;

        public event Action Canceled;

        protected override Transform CollectionRoot => _layoutRoot;
        
        protected override void ProcessCreatedLayout(IEnumerable<VehicleCard> layout)
        {
            foreach (VehicleCard card in layout)
            {
                card.transform.localScale = _cardSize;
                card.Initialize(OnLayoutVehicleCardClicked);
            }
        }

        private void OnLayoutVehicleCardClicked(bool clicked)
        {
            if (clicked)
            {
                _storage.Enable();
            }
            else
            {
                _storage.Disable();
            }
        }
    }
}