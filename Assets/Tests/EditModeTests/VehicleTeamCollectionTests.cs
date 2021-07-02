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
        var collection = new VehicleTeamCollection();

        foreach (IVehicle vehicle in collection)
        {
            Assert.IsNotNull(vehicle);
        }
    }

    public void VehicleTeamCollection_ReturnsSomeInstanceWhenItsNull()
    {
        var collection = new VehicleTeamCollection();
        collection[Team.First] = null;

        Assert.IsNotNull(collection[Team.First]);
    }

    public void VehicleTeamCollection_HasSlotsForAllTeams()
    {
        var collection = new VehicleTeamCollection();

        var teamsCount = Enum.GetNames(typeof(Team)).Length;

        Assert.AreEqual(teamsCount, collection.Count);
    }

    public void VehicleTeamCollection_AddingWorksCorrectly()
    {
        var collection = new VehicleTeamCollection();

        collection[Team.First] = ;
    }
}
