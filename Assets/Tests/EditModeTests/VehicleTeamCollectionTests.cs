using CarSumo.Vehicles;
using CarSumo.Teams;
using CarSumo.Vehicles.Selector;
using NUnit.Framework;
using System;

public class VehicleTeamCollectionTests
{
    [Test]
    public void VehicleTeamCollection_DoesNotContainsNullElementsByDefaultOnInitialize()
    {
        var collection = new VehicleCollection();

        foreach (IVehicle vehicle in collection)
        {
            Assert.IsNotNull(vehicle);
        }
    }

    [Test]
    public void VehicleTeamCollection_CanNotAddNull()
    {
        var collection = new VehicleCollection();

        Assert.Throws<NullReferenceException>(() => collection.Add(null));
        Assert.Throws<NullReferenceException>(() => collection.Add(null, Team.First));
        Assert.Throws<NullReferenceException>(() => collection[Team.First] = null);
    }

    [Test]
    public void VehicleTeamCollection_HasSlotsForAllTeams()
    {
        var collection = new VehicleCollection();

        var teamsCount = Enum.GetNames(typeof(Team)).Length;

        Assert.AreEqual(teamsCount, collection.Count);
    }

    [Test]
    public void VehicleTeamCollection_ÑanNotAddVehicleWithOtherTeam()
    {
        var collection = new VehicleCollection();

        Assert.Throws<InvalidOperationException>(() => collection[Team.First] = new IVehicle.FakeVehicle(Team.Second));
        Assert.Throws<InvalidOperationException>(() => collection.Add(new IVehicle.FakeVehicle(Team.First), Team.Second));
    }
}
