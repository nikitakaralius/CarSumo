namespace Menu.Vehicles.Cards
{
    public interface IVehicleCardSelectHandler
    {
        void OnCardSelected(VehicleCard card);
        void OnCardDeselected(VehicleCard card);
    }
}