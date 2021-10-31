using DataModel.GameData.Vehicles;
using Zenject;

namespace Infrastructure.Installers.Factories
{
    public class VehicleLayoutBuilderFactory : IFactory<IVehicleLayoutBuilder>
    {
        private const int _slotsAmount = 3;
        
        public IVehicleLayoutBuilder Create()
        {
            return new BoundedVehicleLayoutBuilder(_slotsAmount);
        }
    }
}