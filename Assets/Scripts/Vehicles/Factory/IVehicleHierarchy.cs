using CarSumo.Teams;

namespace CarSumo.Vehicles.Factory
{
    public interface IVehicleHierarchy
    {
        bool CanCreate(int generation);
        VehicleFactory GetVehicleFactoryByGeneration(Team team, int generation);
    }
}