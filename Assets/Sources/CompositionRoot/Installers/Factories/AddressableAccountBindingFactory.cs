using CarSumo.DataModel.GameData.Accounts;
using DataModel.GameData.Vehicles;
using Zenject;

namespace Infrastructure.Installers.Factories
{
    public class AddressableAccountBindingFactory : IFactory<AddressableAccountBinding>
    {
        private const string DefaultIconPath = "Players/Icons/DefaultUserIcon.png";
        private readonly IVehicleLayoutBuilder _layoutBuilder;

        public AddressableAccountBindingFactory(IVehicleLayoutBuilder layoutBuilder)
        {
            _layoutBuilder = layoutBuilder;
        }

        public AddressableAccountBinding Create()
        {
            return new AddressableAccountBinding(DefaultIconPath, _layoutBuilder);
        }
    }
}