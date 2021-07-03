using System;
using CarSumo.Teams;
using CarSumo.Vehicles;
using CarSumo.Vehicles.Selector;
using NUnit.Framework;

public class VehicleCollectionTests
{
    [Test]
    public void VehicleCollection_DoesNotContainsNullElementsByDefaultOnInitialize()
    {
        var collection = new VehicleCollection();

        foreach (IVehicle vehicle in collection)
        {
            Assert.IsNotNull(vehicle);
        }
    }

    [Test]
    public void VehicleCollection_CanNotAddNull()
    {
        var collection = new VehicleCollection();

        Assert.Throws<NullReferenceException>(() => collection.AddVehicle(null));
        Assert.Throws<NullReferenceException>(() => collection.AddVehicle(null, Team.First));
        Assert.Throws<NullReferenceException>(() => collection[Team.First] = null);
    }

    [Test]
    public void VehicleCollection_HasSlotsForAllTeams()
    {
        var collection = new VehicleCollection();

        var teamsCount = Enum.GetNames(typeof(Team)).Length;

        Assert.AreEqual(teamsCount, collection.Count);
    }

    [Test]
    public void VehicleCollection_ĐanNotAddVehicleWithOtherTeam()
    {
        var collection = new VehicleCollection();

        Assert.Throws<InvalidOperationException>(() => collection[Team.First] = new IVehicle.FakeVehicle(Team.Second));
        Assert.Throws<InvalidOperationException>(() => collection.AddVehicle(new IVehicle.FakeVehicle(Team.First), Team.Second));
    }
}
