using System;
using DataModel.FileData;
using DataModel.GameData.Vehicles;
using DataModel.Vehicles;
using UniRx;

namespace DataModel.GameData.GameSave
{
    public class VehicleStorageSave : IDisposable
    {
        private readonly IVehicleStorage _storage;
        private readonly IVehicleStorageConfiguration _configuration;
        private readonly IAsyncFileService _fileService;

        public VehicleStorageSave(IVehicleStorage storage,
            IAsyncFileService fileService,
            IVehicleStorageConfiguration configuration)
        {
            _storage = storage;
            _fileService = fileService;
            _configuration = configuration;

            _storage.BoughtVehicles.ObserveCountChanged().Subscribe(_ => Save());
            _storage.BoughtVehicles.ObserveReplace().Subscribe(_ => Save());
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

        private SerializableVehicles ToSerializableVehicles(IVehicleStorage vehicleStorage)
        {
            return new SerializableVehicles
            {
                Vehicles = vehicleStorage.BoughtVehicles
            };
        }
    }
}