using System;
using System.Collections.Generic;
using Menu.Vehicles.Layout;
using UnityEngine;

namespace Menu.Vehicles
{
    internal class VehicleEnvironmentLayoutPreview : VehicleLayoutView<MenuVehicle>
    {
        private int _selectedVehicleIndex;

        protected override Transform LayoutRoot => transform;

        public void ChangeOnNext()
        {
            int index = (int) Mathf.Repeat(_selectedVehicleIndex + 1, Items.Count);
            SelectVehicle(index);
        }

        public void ChangeOnPrevious()
        {
            int index = (int) Mathf.Repeat(_selectedVehicleIndex - 1, Items.Count);
            SelectVehicle(index);
        }

        protected override void ProcessCreatedLayout(IEnumerable<MenuVehicle> layout)
        {
            foreach (MenuVehicle vehicle in layout)
            {
                vehicle.gameObject.SetActive(false);
            }

            _selectedVehicleIndex = 0;
            Items[_selectedVehicleIndex].gameObject.SetActive(true);
        }

        private void SelectVehicle(int index)
        {
            if (index < 0 || index >= Items.Count)
            {
                throw new InvalidOperationException("Trying to select vehicle with wrong index");
            }

            Items[_selectedVehicleIndex].gameObject.SetActive(false);
            _selectedVehicleIndex = index;
            Items[_selectedVehicleIndex].gameObject.SetActive(true);
        }
    }
}