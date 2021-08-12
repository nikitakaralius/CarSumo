using CarSumo.Teams;
using CarSumo.Vehicles;
using CarSumo.Vehicles.Selector;
using UnityEngine;

public static class Create
{
    private const string AssetPath = "Explosion Cue Test";

    public static VehicleCollection VehicleCollection()
    {
        return new VehicleCollection();
    }

    public static IVehicle.FakeVehicleMono FakeVehicleMono(Team team = Team.Blue)
    {
        return new GameObject().AddComponent<IVehicle.FakeVehicleMono>().Init(team);
    }
}