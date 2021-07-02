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
        Assert.AreEqual(definer.DefineTeam(Team.Second), Team.First);
    }
}
