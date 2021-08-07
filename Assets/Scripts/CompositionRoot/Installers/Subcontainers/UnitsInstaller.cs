using CarSumo.Units;
using Zenject;

namespace Infrastructure.Installers.SubContainers
{
    public class UnitsInstaller : Installer<UnitsInstaller>
    {
        private const int TeamsCount = 2;

        public override void InstallBindings()
        {
            BindUnitTracker();
        }

        private void BindUnitTracker()
        {
            Container
                .Bind<IUnitTracker>()
                .FromInstance(new UnitTracker(TeamsCount))
                .AsSingle();
        }
    }
}