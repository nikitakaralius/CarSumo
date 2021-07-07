using CarSumo.Teams;
using CarSumo.Vehicles;
using CarSumo.Vehicles.Selector;
using UnityEngine;
using UnityEngine.UI;

public static class Create
{
    public static SequentialTeamDefiner SequentialTeamDefiner()
    {
        return new SequentialTeamDefiner();
    }

    public static PreviousSequentialTeamDefiner PreviousSequentialTeamDefiner()
    {
        return new PreviousSequentialTeamDefiner();
    }

    public static VehicleCollection VehicleCollection()
    {
        return new VehicleCollection();
    }
}