using CarSumo.Teams;
using FluentAssertions;
using NUnit.Framework;

public class SequentialTeamDefinerTests
{
    [Test]
    public void WhenDefiningNextTeam_AndPassTeamFirst_ThenDefinedTeamShouldBeSecond()
    {
        var definer = Create.SequentialTeamDefiner();
        var team = Team.Blue;

        Team definedTeam = definer.DefineNext(team);
        
        definedTeam.Should().Be(Team.Red);
    }
    
    [Test]
    public void WhenDefiningNextTeam_AndTeamsMoreThenTwo_ThenTeamAfterSecondShouldNotBeFirst()
    {
        var definer = Create.SequentialTeamDefiner();
        var team = Team.Red;

        if (Condition.TeamsMoreThenTwo == false)
            return;
        Team teamAfterSecond = definer.DefineNext(team);

        teamAfterSecond.Should().NotBe(Team.Blue);
    }
    
    [Test]
    public void WhenDefiningPreviousTeam_AndPassTeamSecond_ThenDefinedTeamShouldBeFirst()
    {
        var definer = Create.SequentialTeamDefiner();
        var team = Team.Red;

        Team definedTeam = definer.DefinePrevious(team);

        definedTeam.Should().Be(Team.Blue);
    }

    [Test]
    public void WhenDefiningPreviousTeam_AndTeamsMoreThenTwo_ThenTeamAfterFirstShouldNotBeSecond()
    {
        var definer = Create.SequentialTeamDefiner();
        var team = Team.Blue;

        if (Condition.TeamsMoreThenTwo == false)
            return;
        Team teamAfterFirst = definer.DefinePrevious(team);

        teamAfterFirst.Should().NotBe(Team.Red);
    }
}