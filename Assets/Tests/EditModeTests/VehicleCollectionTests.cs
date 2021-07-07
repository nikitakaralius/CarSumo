using System;
using System.Collections;
using CarSumo.Teams;
using CarSumo.Vehicles;
using CarSumo.Vehicles.Selector;
using NUnit.Framework;
using FluentAssertions;

public class VehicleCollectionTests
{
    [Test]
    public void WhenGettingVehicles_AndCollectionJustCreated_ThenElementsShouldNotBeNull()
    {
        var collection = Create.VehicleCollection();

        IEnumerable vehicles = collection;
        
        foreach (IVehicle vehicle in vehicles)
            vehicle.Should().NotBeNull();
    }

    [Test]
    public void WhenAddingVehicle_AndVehicleIsNull_ThenCollectionShouldThrowNullReferenceException()
    {
        // Arrange.
        VehicleCollection collection = Create.VehicleCollection();
        IVehicle vehicle = null;

        // Act.
        Action addingByMethod = () => collection.AddVehicle(vehicle);
        Action addingByMethodWithTeam = () => collection.AddVehicle(vehicle, Team.First);
        Action addingByIndexer = () => collection[Team.First] = vehicle;

        // Assert.
        addingByMethod.Should().Throw<NullReferenceException>();
        addingByMethodWithTeam.Should().Throw<NullReferenceException>();
        addingByIndexer.Should().Throw<NullReferenceException>();
    }

    [Test]
    public void WhenGettingTeamsCount_AndCreteVehicleCollection_ThenCollectionCountShouldBeSameAsTeamsCount()
    {
        // Arrange.
        VehicleCollection collection = Create.VehicleCollection();

        // Act.
        int teamsCount = Enum.GetNames(typeof(Team)).Length;

        // Assert.
        collection.Count.Should().Be(teamsCount);
    }
    
    [Test]
    public void WhenAddingVehicle_AndVehicleTeamIsDifferent_ThenCollectionThrowInvalidOperationException()
    {
        // Arrange.
        VehicleCollection collection = Create.VehicleCollection();

        // Act.
        Action addingByMethod = () => collection.AddVehicle(new IVehicle.FakeVehicle(Team.First), Team.Second);
        Action addingByIndexer = () => collection[Team.First] = new IVehicle.FakeVehicle(Team.Second);

        // Assert.
        addingByMethod.Should().Throw<InvalidOperationException>();
        addingByIndexer.Should().Throw<InvalidOperationException>();
    }
}
