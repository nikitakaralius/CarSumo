using CarSumo.Structs;
using CarSumo.Teams;
using CarSumo.Units.Tracking;
using CarSumo.Vehicles;
using UnityEngine;
using Zenject;

namespace CarSumo.Units
{
    public class Unit : MonoBehaviour, IUnit, IVehicleDestroyer
    {
	    [SerializeField] private Team _team;

	    private IUnitTrackerOperations _trackerOperations;

	    [Inject]
	    private void Construct(IUnitTrackerOperations trackerOperations)
	    {
		    _trackerOperations = trackerOperations;
	    }
	    
	    public Team Team => _team;

	    public void InitializeVehicleBySelf(Vehicle vehicle)
	    {
		    var worldPlacement = new WorldPlacement(transform.position, -transform.forward);
		    vehicle.Initialize(_team, worldPlacement, this);
		    _trackerOperations.Add(this);
	    }
	    
	    public void Destroy(Vehicle vehicle)
	    {
		    _trackerOperations.Remove(this);
		    Destroy(vehicle.gameObject);
	    }
    }
}