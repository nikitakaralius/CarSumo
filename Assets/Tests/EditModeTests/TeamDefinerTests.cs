using System;
using System.Collections;
using System.Collections.Generic;
using CarSumo.Teams;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TeamDefinerTests
{
    [Test]
    public void SequentialTeamDefiner_DetermineNextTeamCorrect()
    {
        var definer = new SequentialTeamDefiner();

        Assert.AreEqual(definer.DefineTeam(Team.First), Team.Second);
    }

    [Test]
    public void PreviousSequentialTeamDefiner_DetermineNextTeamCorrect()
    {
        var definer = new PreviousSequentialTeamDefiner();

        Assert.AreEqual(definer.DefineTeam(Team.Second), Team.First);
    }

    [Test]
    public void SequentialTeamDefiner_NextTeamAfterSecondIsNotFirst_IfTeamsMoreThanTwo()
    {
        var definer = new SequentialTeamDefiner();

        if (Enum.GetNames(typeof(Team)).Length <= 2)
        {
            Assert.Pass();
            return;
        }

        Assert.AreNotEqual(definer.DefineTeam(Team.Second), Team.First);
    }

    [Test]
    public void PreviousSequentialTeamDefiner_PreviousTeamAfterFirstIsNotSecond_IfTeamsMoreThanTwo()
    {
        var definer = new SequentialTeamDefiner();

        if (Enum.GetNames(typeof(Team)).Length <= 2)
        {
            Assert.Pass();
            return;
        }

        Assert.AreNotEqual(definer.DefineTeam(Team.First), Team.Second);
    }
}
