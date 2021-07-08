using System;
using CarSumo.Teams;

public static class Condition
{
    public static bool TeamsMoreThenTwo => Enum.GetNames(typeof(Team)).Length > 2;
}