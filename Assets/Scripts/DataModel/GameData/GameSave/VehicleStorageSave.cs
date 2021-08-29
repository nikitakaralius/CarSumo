using System;
using DataModel.FileData;
using DataModel.GameData.Vehicles;
using DataModel.Vehicles;
using UniRx;
using UnityEngine;

namespace DataModel.GameData.GameSave
{
    public class VehicleStorageSave : IDisposable
    {
        private readonly IVehicleStorage _storage;
        private readonly IVehicleStorageConfiguration _configuration;
        private readonly IAsyncFileService _fileService;

        public VehicleStorageSave(IVehicleStorage storage, IAsyncFileService fileService, IVehicleStorageConfiguration configuration)
        {
            _storage = storage;
            _fileService = fileService;
            _configuration = configuration;

            storage.BoughtVehicles.ObserveCountChanged().Subscribe(_ => Save());
        }

        public void Dispose()
        {
            Save();
        }

        private void Save()
        {
            var vehicles = ToSerializableVehicles(_storage);
            var filePath = _configuration.VehicleStorageFilePath;
            _fileService.SaveAsync(vehicles, filePath);
        }

        private SerializableVehicleStorage ToSerializableVehicles(IVehicleStorage vehicleStorage)
        {
            return new SerializableVehicleStorage
            {
                Vehicles = vehicleStorage.BoughtVehicles
            };
        }
    }
}