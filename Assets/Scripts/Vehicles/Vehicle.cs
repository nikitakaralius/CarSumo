using System;
using CarSumo.Extensions;
using CarSumo.Structs;
using CarSumo.Teams;
using CarSumo.Vehicles.Engine;
using CarSumo.Vehicles.Rotation;
using CarSumo.Vehicles.Stats;
using UnityEngine;

namespace CarSumo.Vehicles
{
	[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
    [RequireComponent(typeof(VehicleEngine), typeof(VehicleCollision))]
    public class Vehicle : MonoBehaviour, IVehicle
    {
	    [SerializeField] private VehiclePreferencesSo _vehiclePreferences;

	    private Rigidbody _rigidbody;
	    private Action _destroyHandler;
	    private IVehicleStatsProvider _statsProvider;
	    
	    public void Initialize(Team team, WorldPlacement worldPlacement, Action onVehicleDestroying)
	    {
		    _statsProvider = new TeamVehicleStats(team, _vehiclePreferences);
		    
		    transform.SetWorldPlacement(worldPlacement);
		    
		    _destroyHandler = onVehicleDestroying;

		    MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
		    meshRenderer.material = _vehiclePreferences.GetTeamVehicleMaterial(team);
	    }
	    
	    public IVehicleEngine Engine { get; private set; }
	    
	    public IRotation Rotation { get; private set; }

	    public VehicleStats Stats => _statsProvider.GetStats();

	    private void Awake()
	    {
		    _rigidbody = GetComponent<Rigidbody>();
	    }
	    
	    public void Destroy()
	    {
		    _destroyHandler?.Invoke();
		    Destroy(gameObject);	    
	    }
    }
}
