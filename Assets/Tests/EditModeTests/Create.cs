using CarSumo.Teams;
using CarSumo.Vehicles.Selector;

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