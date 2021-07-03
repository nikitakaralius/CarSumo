using System.Collections;
using CarSumo.Teams;
using CarSumo.Vehicles;
using CarSumo.Vehicles.Selector;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class VehicleCollectionTests
{
    [UnityTest]
    public IEnumerator VehicleCollection_ReturnsFakeWhenOriginalInstanceWasDestroyed()
    {
        var collection = new VehicleCollection();
        var vehicle = new GameObject().AddComponent<IVehicle.FakeVehicleMono>().Init(Team.First);

        collection.AddVehicle(vehicle);
        vehicle.Destroy();

        yield return new WaitForSeconds(1.0f);

        Assert.NotNull(collection.GetVehicle(Team.First));
        Assert.NotNull(collection.GetVehicle(Team.First));
    }
}
