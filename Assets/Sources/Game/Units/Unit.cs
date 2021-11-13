using CarSumo.Structs;
using CarSumo.Teams;
using CarSumo.Units.Tracking;
using CarSumo.Vehicles;
using UnityEngine;
using Zenject;

namespace CarSumo.Units
{
    public class Unit : MonoBehaviour, IUnit
    {
	    [SerializeField] private Team _team;

	    private IUnitTrackingOperations _trackingOperations;

	    [Inject]
	    private void Construct(IUnitTrackingOperations trackingOperations)
	    {
		    _trackingOperations = trackingOperations;
	    }
	    
	    public Team Team => _team;
	    
	    public Vehicle Vehicle { get; private set; }

	    public void InitializeVehicleBySelf(Vehicle vehicle)
	    {
		    var worldPlacement = new WorldPlacement(transform.position, -transform.forward);
		    vehicle.Initialize(_team, worldPlacement, () => _trackingOperations.Remove(this));
		    _trackingOperations.Add(this);
		    
		    vehicle.transform.SetParent(transform);

		    Vehicle = vehicle;
	    }
    }
}