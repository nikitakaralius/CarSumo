using CarSumo.Teams;
using FluentAssertions;
using NUnit.Framework;

public class PreviousSequentialTeamDefinerTests
{
    [Test]
    public void WhenDefiningTeam_AndPassTeamSecond_ThenDefinedTeamShouldBeFirst()
    {
        var definer = Create.PreviousSequentialTeamDefiner();
        var team = Team.Second;

        Team definedTeam = definer.DefineTeam(team);

        definedTeam.Should().Be(Team.First);
    }

    [Test]
    public void WhenDefiningTeam_AndTeamsMoreThenTwo_ThenTeamAfterFirstShouldNotBeSecond()
    {
        var definer = Create.PreviousSequentialTeamDefiner();
        var team = Team.First;

        if (Condition.TeamsMoreThenTwo == false)
            return;
        Team teamAfterFirst = definer.DefineTeam(team);

        teamAfterFirst.Should().NotBe(Team.Second);
    }
}