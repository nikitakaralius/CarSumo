using CarSumo.Teams;
using FluentAssertions;
using NUnit.Framework;

public class SequentialTeamDefinerTests
{
    [Test]
    public void WhenDefiningNextTeam_AndPassTeamFirst_ThenDefinedTeamShouldBeSecond()
    {
        var definer = Create.SequentialTeamDefiner();
        var team = Team.First;

        Team definedTeam = definer.DefineNext(team);
        
        definedTeam.Should().Be(Team.Second);
    }
    
    [Test]
    public void WhenDefiningNextTeam_AndTeamsMoreThenTwo_ThenTeamAfterSecondShouldNotBeFirst()
    {
        var definer = Create.SequentialTeamDefiner();
        var team = Team.Second;

        if (Condition.TeamsMoreThenTwo == false)
            return;
        Team teamAfterSecond = definer.DefineNext(team);

        teamAfterSecond.Should().NotBe(Team.First);
    }
    
    [Test]
    public void WhenDefiningPreviousTeam_AndPassTeamSecond_ThenDefinedTeamShouldBeFirst()
    {
        var definer = Create.SequentialTeamDefiner();
        var team = Team.Second;

        Team definedTeam = definer.DefinePrevious(team);

        definedTeam.Should().Be(Team.First);
    }

    [Test]
    public void WhenDefiningPreviousTeam_AndTeamsMoreThenTwo_ThenTeamAfterFirstShouldNotBeSecond()
    {
        var definer = Create.SequentialTeamDefiner();
        var team = Team.First;

        if (Condition.TeamsMoreThenTwo == false)
            return;
        Team teamAfterFirst = definer.DefinePrevious(team);

        teamAfterFirst.Should().NotBe(Team.Second);
    }
}