using CarSumo.Teams;
using CarSumo.Vehicles.Selector;

public static class Create
{
    public static SequentialTeamDefiner SequentialTeamDefiner()
    {
        return new SequentialTeamDefiner();
    }

    public static VehicleCollection VehicleCollection()
    {
        return new VehicleCollection();
    }
}