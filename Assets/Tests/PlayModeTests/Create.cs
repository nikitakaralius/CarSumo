using CarSumo.Teams;
using CarSumo.Vehicles;
using CarSumo.Vehicles.Selector;
using UnityEngine;

public static class Create
{
    public static VehicleCollection VehicleCollection()
    {
        return new VehicleCollection();
    }

    public static IVehicle.FakeVehicleMono FakeVehicleMono(Team team = Team.First)
    {
        return new GameObject().AddComponent<IVehicle.FakeVehicleMono>().Init(team);
    }
}