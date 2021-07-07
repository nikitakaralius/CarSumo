using System;
using CarSumo.Teams;
using CarSumo.Vehicles;
using CarSumo.Vehicles.Selector;
using FluentAssertions;
using NUnit.Framework;

public class VehicleCollectionTests
{
    [Test]
    public void WhenUsingDestroyedVehicle_AndVehicleWasAddedToCollection_ThenVehicleShouldBeFake()
    {
        // Arrange.
        VehicleCollection collection = Create.VehicleCollection();
        var team = Team.First;
        IVehicle vehicle = Create.FakeVehicleMono(team);
        collection.AddVehicle(vehicle);

        // Act.
        vehicle.Destroy();
        IVehicle vehicleFromCollection = collection.GetVehicle(team);
        Action vehicleUsing = () => vehicleFromCollection.Upgrade();

        // Assert.
        vehicleUsing.Should().NotThrow();
    }
}