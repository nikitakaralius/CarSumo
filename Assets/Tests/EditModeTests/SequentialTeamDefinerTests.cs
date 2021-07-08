using CarSumo.Teams;
using FluentAssertions;
using NUnit.Framework;

public class SequentialTeamDefinerTests
{
    [Test]
    public void WhenDefiningTeam_AndPassTeamFirst_ThenDefinedTeamShouldBeSecond()
    {
        var definer = Create.SequentialTeamDefiner();
        var team = Team.First;

        Team definedTeam = definer.DefineTeam(team);
        
        definedTeam.Should().Be(Team.Second);
    }
    
    [Test]
    public void WhenDefiningTeam_AndTeamsMoreThenTwo_ThenTeamAfterSecondShouldNotBeFirst()
    {
        var definer = Create.SequentialTeamDefiner();
        var team = Team.Second;

        if (Condition.TeamsMoreThenTwo == false)
            return;
        Team teamAfterSecond = definer.DefineTeam(team);

        teamAfterSecond.Should().NotBe(Team.First);
    }
}