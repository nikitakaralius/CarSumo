using System.Collections.Generic;
using System.Threading.Tasks;
using DataModel.FileData;
using DataModel.GameData.GameSave;
using DataModel.GameData.Vehicles;
using DataModel.Vehicles;
using UnityEngine;
using Zenject;

namespace Infrastructure.Initialization
{
    public class VehicleStorageInitialization : IAsyncInitializable
    {
        private readonly DiContainer _container;
        private readonly IVehicleStorageConfiguration _configuration;
        private readonly IAsyncFileService _fileService;

        public VehicleStorageInitialization(DiContainer container,
                                            IVehicleStorageConfiguration configuration,
                                            IAsyncFileService fileService)
        {
            _container = container;
            _configuration = configuration;
            _fileService = fileService;
        }

        public async Task InitializeAsync()
        {
            SerializableVehicleStorage serializableVehicleStorage = await LoadSerializableVehicleStorageAsync() ?? EnsureCreated();

            BindVehicleStorageInterfaces(serializableVehicleStorage.Vehicles);
            BindVehicleStorageSave();
        }
        
        private async Task<SerializableVehicleStorage> LoadSerializableVehicleStorageAsync()
        {
            string filePath = _configuration.VehicleStorageFilePath;
            return await _fileService.LoadAsync<SerializableVehicleStorage>(filePath);
        }

        private SerializableVehicleStorage EnsureCreated()
        {
            return new SerializableVehicleStorage()
            {
                Vehicles = new[]
                {
	                VehicleId.Jeep,
	                VehicleId.Jeep,
	                VehicleId.Jeep,
	            
	                VehicleId.Wagon,
	                VehicleId.Wagon,

	                VehicleId.OldCar,
	                
	                VehicleId.Van,
	                
	                VehicleId.Ute,
                }
            };
        }

        private void BindVehicleStorageSave()
        {
	        _container
                .Bind<VehicleStorageSave>()
                .FromNew()
                .AsSingle()
                .NonLazy();

	        _container.Resolve<VehicleStorageSave>();
        }

        private void BindVehicleStorageInterfaces(IEnumerable<VehicleId> vehicles)
        {
            var vehicleStorage = new GameVehicleStorage(vehicles);

            _container
                .BindInterfacesAndSelfTo<GameVehicleStorage>()
                .FromInstance(vehicleStorage)
                .AsSingle()
                .NonLazy();
        }
    }
}