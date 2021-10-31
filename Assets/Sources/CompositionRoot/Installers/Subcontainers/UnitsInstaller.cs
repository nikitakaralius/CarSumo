using CarSumo.Units.Tracking;
using Zenject;

namespace Infrastructure.Installers.SubContainers
{
    public class UnitsInstaller : Installer<UnitsInstaller>
    {
	    public override void InstallBindings()
	    {
		    BindUnitTrackerInterfaces();
	    }

	    private void BindUnitTrackerInterfaces()
	    {
		    Container
			    .BindInterfacesTo<UnitTracker>()
			    .AsSingle();
	    }
    }
}