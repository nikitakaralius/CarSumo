using CarSumo.Teams;

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
}