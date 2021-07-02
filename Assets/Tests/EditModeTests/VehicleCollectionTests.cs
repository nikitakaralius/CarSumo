using CarSumo.Vehicles;
using CarSumo.Vehicles.Selector;
using NUnit.Framework;

public class VehicleCollectionTests
{
    [Test]
    public void VehicleCollection_ContainsFakeVehiclesOnInitialize()
    {
        var collection = new VehicleCollection();
    }
}
