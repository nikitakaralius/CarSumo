using System;
using DataModel.Vehicles;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Vehicles.Cards
{
    [RequireComponent(typeof(Button))]
    public class VehicleCard : MonoBehaviour
    {
        [SerializeField] private VehicleId _vehicleId;
        
        private readonly ReactiveProperty<bool> _selected = new ReactiveProperty<bool>(false);
        private bool _notifyingDisabled;

        private IDisposable _selectedSubscription;
        private IVehicleCardSelectHandler _selectHandler;

        public int DynamicSiblingIndex { get; private set; }
        public VehicleId VehicleId => _vehicleId;

        private void Awake()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(() => _selected.Value = !_selected.Value);

            _notifyingDisabled = true;
            
            _selectedSubscription = _selected.Subscribe(selected =>
            {
                if (_notifyingDisabled)
                {
                    return;
                }
                
                if (selected)
                {
                    OnCardSelectedInternal();
                    _selectHandler?.OnCardSelected(this);
                }
                else
                {
                    OnCardDeselectedInternal();
                    _selectHandler?.OnCardDeselected(this);
                }
            });
            
            _notifyingDisabled = false;
        }

        private void OnDestroy()
        {
            _selectedSubscription?.Dispose();
        }

        public void SetSelectHandler(IVehicleCardSelectHandler selectHandler)
        {
            _selectHandler = selectHandler;
        }

        public void NotifyBeingDeselected(bool disableHandlerNotify)
        {
            _notifyingDisabled = disableHandlerNotify;
            _selected.Value = false;
            _notifyingDisabled = false;
            
            OnCardDeselectedInternal();
        }

        public void SetLatestSiblingIndex()
        {
            transform.SetSiblingIndex(DynamicSiblingIndex);
        }

        private void OnCardSelectedInternal()
        {
            DynamicSiblingIndex = transform.GetSiblingIndex();
        }

        private void OnCardDeselectedInternal()
        {
        }
    }
}